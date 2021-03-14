using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

[AddComponentMenu("Starfighter Game Template/Player Controller")]
public class PlayerSpaceship : MonoBehaviour {

	[Header("Health Settings")]
	[Tooltip("Current health value")]
	public float health;
	[Tooltip("Maximum health")]
	public float maxHealth;
	[Tooltip("Current shield value")]
	public float shield;
	[Tooltip("Maximum shield")]
	public float maxShield;
	[Tooltip("Time after which shield will start to recover")]
	public float shieldRecoveryTime;
	public float shieldRecoveryRate;
	float shieldRecoveryTimer;

	[Space(16)]
	[Header("Movement Settings")]
	[Tooltip("Empty transform used for sway animation, you can place any model under it")]
	public Transform ship;
	[Tooltip("Normal speed")]
	public float regSpeed;
	[Tooltip("Acceleration speed")]
	public float maxSpeed;
	[Tooltip("Deceleration speed")]
	public float minSpeed;
	[Tooltip("Overall input sensitivity")]
	public float inputSensitivity;
	[HideInInspector]
	public float curSpeed; // Current speed
	float tarSpeed; // Target speed
	// Helping variables for rotation
	Vector3 mPos;
	float myTR_f = 0;
	float myR_f = 0;
	//
	public float ShipRotationX;
	public float ShipRotationZ;
	[Tooltip("Ship's sensitivity to the input")]
	public float shipSensitivity;
	// Helping variables for ship rotation effect
	float shipTR_x = 0;
	float shipTR_z = 0;
	float shipR_x = 0;
	float shipR_z = 0;
	//

	Transform battleCenter;
	float battleSize;
	bool leavingBattlefield;

	[Space(16)]
	[Header("Shooting Settings")]
	public Weapon[] Weapons;
	[HideInInspector]
	public string myTeam;
	public Transform[] formationTarget;
	Transform homingTarget;
	int currentWeapon = 0;
	float fireTimer;
	float homingTimer;
	float heating; // current heating
	bool heatDec; // true if heating should decrease;
	Quaternion desiredRot;
	[Tooltip("Explosion prefabs to instatiate when player dies, one will be selected randomly")]
	public GameObject [] explosion;

	[Space(16)]
	[Header("UI Settings")]
	public Canvas canvas;
	[Tooltip("Health bar")]
	public Image healthUI;
	[Tooltip("Shield bar")]
	public Image shieldUI;
	public Text weaponName;
	public Text leavingNote;
	float leaving_a;
	public Transform Crosshair;
	public Image HomingIndicator;
	float homingInd_a;
	float homingInd_s;
	public GameObject SelectTargetIndicator;
	float selectInd_s;
	public GameObject LockedTargetIndicator;
	public GameObject LockedTargetArrow;
	float targetInd_s;
	Transform Target;
	Transform LockedTarget;
	float targetTimer;
	public float mouseSmoothing;
	public float crosshairBoundary;
	public float radarDistance;
	// Helping variables for crosshair movement
	float crossX;
	float crossY;
	float crossTX;
	float crossTY;
	//
	[Tooltip("Weapon heating indicator")]
	public Image heatingUI;
	[Tooltip("Speed indicator")]
	public Image speedUI;
	[Tooltip("Screen to show when player dies")]
	public GameObject deathScreen;
	public GameObject mainUI;
	public GameObject pauseScreen;
	bool paused;
	public Text ScoreText;
	[Space(16)]
	[Header("Camera Settings")]
	[Tooltip("Main camera")]
	public Camera cam;
	[Tooltip("Camera position on Y axis")]
	public float camY_Pos;
	[Tooltip("Camera position on Z axis")]
	public float camZ_Pos;
	[Tooltip("Amount of the sway effect on X axis")]
	public float camX_Mov;
	[Tooltip("Amount of the sway effect on Y axis")]
	public float camY_Mov;
	[Tooltip("Speed of the sway effect")]
	public float camSpeed; 
	[Tooltip("Sensitivity of the camera")]
	public float camSensitivity;
	[Tooltip("Particle system of the stars around player's ship")]
	public ParticleSystem dust;
	float targetFOV; // Field of view for the camera
	// Helping variables for camera movement
	float cX = 0;
	float cY = 0;
	float TcX = 0;
	float TcY = 0;
	//

	void Awake () {
		health = maxHealth;
		shield = maxShield;
		PlayerPrefs.SetInt ("Score", 0);
		Cursor.visible = false;
		paused = false;
		heatDec = true;

		battleCenter = GameObject.Find ("BattleCenter").transform;
		battleSize = battleCenter.GetComponent<BattleCenter> ().battleSize;
	}

	public void ModifyHealth(float dmg) {
		if (shield == 0) {
			health -= dmg;
		} else {
			shield -= dmg;
		}

		if (shield < 0) {
			health += shield;
			shield = 0;
		}

		shieldRecoveryTimer = shieldRecoveryTime;

		if (health <= 0) {
			int r = Random.Range (0, explosion.Length);
			Instantiate (explosion [r], transform.position, transform.rotation);
			Die ();
		}
	}

	void Die() {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		dust.Pause ();
		deathScreen.SetActive (true);
		mainUI.SetActive (false);
		ship.gameObject.SetActive (false);
		GetComponent<PlayerSpaceship> ().enabled = false;

	}

	void Fire () {
		if (heating < Weapons[currentWeapon].maxHeating) {
			if (fireTimer == 0) {

				Vector3 target = Vector3.forward;
				Ray ray = cam.ScreenPointToRay (CrossPlatformInputManager.mousePosition);
				RaycastHit hit;
				int layerMask = LayerMask.GetMask("Radar", "RadarPoints", "Space", "Player");
				layerMask = ~layerMask;

				if (Physics.Raycast (ray, out hit, Mathf.Infinity, layerMask)) {
					target = hit.point;
				} else {
					target = cam.ScreenToWorldPoint (new Vector3 (CrossPlatformInputManager.mousePosition.x, CrossPlatformInputManager.mousePosition.y, cam.farClipPlane));
				}

				if (!Weapons[currentWeapon].fireAllAtOnce) {
					Transform helpingTransform = Weapons[currentWeapon].ShootPos [Weapons[currentWeapon].curG].transform;
					helpingTransform.LookAt (target);
					ClampRotation (helpingTransform.localRotation, -35, 35, 0);

					Weapons [currentWeapon].ShootPos [Weapons [currentWeapon].curG].transform.localRotation = desiredRot;

					if (Weapons[currentWeapon].Muzzle [Weapons[currentWeapon].curG] != null)
						Weapons[currentWeapon].Muzzle [Weapons[currentWeapon].curG].GetComponent<ParticleSystem> ().Play ();

					GameObject b = Instantiate (Weapons[currentWeapon].bullet, Weapons[currentWeapon].ShootPos [Weapons[currentWeapon].curG].transform.position, Weapons[currentWeapon].ShootPos [Weapons[currentWeapon].curG].transform.rotation) as GameObject;
					b.GetComponent<BulletScript> ().player = true;
					if (Weapons [currentWeapon].homing) {
						if (homingTarget != null)
							b.GetComponent<BulletScript> ().target = homingTarget;
					}

					if (Weapons [currentWeapon].infinite)
						heating += Weapons [currentWeapon].heatingInc;
					else
						Weapons [currentWeapon].quantity -= 1;

					if (Weapons[currentWeapon].curG < Weapons[currentWeapon].ShootPos.Length - 1)
						Weapons[currentWeapon].curG += 1;
					else
						Weapons[currentWeapon].curG = 0;

					fireTimer = Weapons[currentWeapon].fireRate / Weapons[currentWeapon].ShootPos.Length;
				
				} else {
					for (int i = 0; i < Weapons[currentWeapon].ShootPos.Length; i++) {
						Transform helpingTransform = Weapons[currentWeapon].ShootPos [i].transform;
						helpingTransform.LookAt (target);
						ClampRotation (helpingTransform.localRotation, -35, 35, 0);

						Weapons [currentWeapon].ShootPos [i].transform.localRotation = desiredRot;

						if (Weapons[currentWeapon].Muzzle [i] != null)
							Weapons[currentWeapon].Muzzle [i].GetComponent<ParticleSystem> ().Play ();

						if (Weapons [currentWeapon].infinite)
							heating += Weapons[currentWeapon].heatingInc;
						else
							Weapons [currentWeapon].quantity -= 1;

						GameObject b = Instantiate (Weapons[currentWeapon].bullet, Weapons[currentWeapon].ShootPos [i].transform.position, Weapons[currentWeapon].ShootPos [i].transform.rotation) as GameObject;
						b.GetComponent<BulletScript> ().player = true;
						if (Weapons [currentWeapon].homing) {
							if (homingTarget != null)
								b.GetComponent<BulletScript> ().target = homingTarget;
						}
					}
					fireTimer = Weapons[currentWeapon].fireRate;
				}
			}
		}
	}

	IEnumerator Overheat() {
		heatDec = false;
		yield return new WaitForSeconds (Weapons[currentWeapon].overheatSec);
		heating -= Weapons[currentWeapon].heatingInc/4;
		heatDec = true;
	}

	void ClampRotation(Quaternion rot, float minAngle, float maxAngle, float clampAroundAngle = 0)
	{
		clampAroundAngle += 180;

		float x = rot.eulerAngles.x - clampAroundAngle;
		float y = rot.eulerAngles.y - clampAroundAngle;

		x = WrapAngle(x);
		y = WrapAngle(y);

		x -= 180;
		y -= 180;

		x = Mathf.Clamp(x, minAngle, maxAngle);
		y = Mathf.Clamp(y, minAngle, maxAngle);

		x += 180;
		y += 180;

		desiredRot = Quaternion.Euler(x + clampAroundAngle, y + clampAroundAngle, rot.eulerAngles.z);
	}
		
	float WrapAngle(float angle)
	{

		while (angle < 0)
			angle += 360;


		return Mathf.Repeat(angle, 360);
	}

	public void Pause() {
		if (!paused) {
			pauseScreen.SetActive (true);
			Cursor.visible = true;
			Time.timeScale = 0;
			paused = true;
		} else {
			pauseScreen.SetActive (false);
			Cursor.visible = false;
			Time.timeScale = 1;
			paused = false;
		}
	}

	void Update () {

		//  UI behavior/
		healthUI.fillAmount = Mathf.Lerp (healthUI.fillAmount, health / maxHealth, Time.deltaTime * 5); // Health bar
		shieldUI.fillAmount = Mathf.Lerp (shieldUI.fillAmount, shield / maxShield, Time.deltaTime * 5); // Shield bar
		heatingUI.fillAmount = Mathf.Lerp (heatingUI.fillAmount, heating / Weapons [currentWeapon].maxHeating, Time.deltaTime * 5); // Heating indicator
		speedUI.fillAmount = Mathf.Lerp(speedUI.fillAmount, tarSpeed / maxSpeed, Time.deltaTime * 5); //Speed indicator
		ScoreText.text = "Score: " + PlayerPrefs.GetInt ("Score").ToString ();
		if (leavingBattlefield)
			leaving_a = 1;
		else
			leaving_a = 0;

		Color cLeaving = leavingNote.color;
		cLeaving.a = Mathf.Lerp (cLeaving.a, leaving_a, Time.deltaTime * 10);
		leavingNote.color = cLeaving;

		if (CrossPlatformInputManager.GetButtonDown ("Cancel")) {
			Pause ();
		}
		//  /UI behavior

		if (!leavingBattlefield) {
			if (Vector3.Distance (battleCenter.position, transform.position) > battleSize) {
				leavingBattlefield = true;
			}
		} else {
			if (Vector3.Distance (battleCenter.position, transform.position) < battleSize) {
				leavingBattlefield = false;
			}
		}

		if (Time.timeScale > 0) {

			//  Shield Recovery/
			if (shieldRecoveryTimer > 0) {
				shieldRecoveryTimer -= 0.1f;
			} else if (shieldRecoveryTimer < 0) {
				shieldRecoveryTimer = 0;
			}

			if (shieldRecoveryTimer == 0) {
				if (shield < maxShield) {
					shield += shieldRecoveryRate;
					shield = Mathf.Clamp (shield, -maxShield, maxShield);
				}
			}
			//  /Shield Recovery

			//  Acceleration behavior/
			if (CrossPlatformInputManager.GetAxis ("Vertical") > 0) {
				tarSpeed = maxSpeed;
				targetFOV = 70;
			} else if (CrossPlatformInputManager.GetAxis ("Vertical") < 0) {
				tarSpeed = minSpeed;
				targetFOV = 55;
			} else if (CrossPlatformInputManager.GetAxis ("Vertical") == 0) {
				tarSpeed = regSpeed;
				targetFOV = 60;
			}
			curSpeed = Mathf.Lerp (curSpeed, tarSpeed, Time.deltaTime * 4);
			cam.fieldOfView = Mathf.Lerp (cam.fieldOfView, targetFOV, Time.deltaTime * 4);
			//  /Acceleration behavior

			transform.Translate (0, 0, curSpeed); // Forward movement

			//  Rotation behavior/
			mPos = CrossPlatformInputManager.mousePosition;
			myTR_f = CrossPlatformInputManager.GetAxis ("Horizontal") * (-inputSensitivity);
			myR_f = Mathf.Lerp (myR_f, myTR_f, Time.deltaTime * 3);
			mPos.x = (Screen.height / 2) - CrossPlatformInputManager.mousePosition.y;
			mPos.y = -(Screen.width / 2) + CrossPlatformInputManager.mousePosition.x;
			transform.Rotate (Vector3.forward, myR_f, Space.Self);
			transform.Rotate (mPos * Time.deltaTime * inputSensitivity / 10, Space.Self);
			//  /Rotation behavior

			//  Ship rotation/
			shipTR_x = mPos.x * shipSensitivity;
			shipTR_z = (-mPos.y * shipSensitivity) + (myTR_f / shipSensitivity);
			shipTR_x = Mathf.Clamp (shipTR_x, -ShipRotationX, ShipRotationX);
			shipTR_z = Mathf.Clamp (shipTR_z, -ShipRotationZ, ShipRotationZ);
			shipR_x = Mathf.Lerp (shipR_x, shipTR_x, Time.deltaTime * 2);
			shipR_z = Mathf.Lerp (shipR_z, shipTR_z, Time.deltaTime * 4);
			ship.localRotation = Quaternion.Euler (shipR_x, 0, shipR_z);
			//  /Ship rotation

			//  Camera movement/
			TcX = mPos.y * camSensitivity;
			TcY = (-mPos.x) * camSensitivity;
			TcX = Mathf.Clamp (TcX, -camX_Mov, camX_Mov);
			TcY = Mathf.Clamp (TcY, camY_Pos - camY_Mov, camY_Pos + camY_Mov);
			cX = Mathf.Lerp (cX, TcX, Time.deltaTime * camSpeed);
			cY = Mathf.Lerp (cY, TcY, Time.deltaTime * camSpeed);
			cam.transform.localPosition = new Vector3 (cX, cY, camZ_Pos);
			//  /Camera movement

			//  Crosshair movement/
			crossTX = CrossPlatformInputManager.mousePosition.x;
			crossTY = CrossPlatformInputManager.mousePosition.y;
			crossTX = Mathf.Clamp (crossTX, (Screen.width / 2) - crosshairBoundary, (Screen.width / 2) + crosshairBoundary);
			crossTY = Mathf.Clamp (crossTY, (Screen.height / 2) - crosshairBoundary, (Screen.height / 2) + crosshairBoundary);
			crossX = Mathf.Lerp (crossX, crossTX, Time.deltaTime * mouseSmoothing);
			crossY = Mathf.Lerp (crossY, crossTY, Time.deltaTime * mouseSmoothing);
			Crosshair.position = new Vector3 (crossX, crossY, 0);
			// /Crosshair movement

			//  Shooting behavior/
			if (CrossPlatformInputManager.GetButton ("Fire1")) {
				if (Weapons [currentWeapon].infinite)
					Fire ();
				else if (Weapons [currentWeapon].quantity > 0)
					Fire ();
			}

			///Targetting
			Ray rayTargetting = cam.ScreenPointToRay (CrossPlatformInputManager.mousePosition);
			RaycastHit hitTargetting;
			int layerMaskTargetting = LayerMask.GetMask ("Radar", "RadarPoints", "Space", "Player");
			layerMaskTargetting = ~layerMaskTargetting;
			if (Physics.Raycast (rayTargetting, out hitTargetting, Mathf.Infinity, layerMaskTargetting)) {
				if (hitTargetting.transform.gameObject.GetComponent<DamageControl> () != null) {
					if (hitTargetting.transform.gameObject.GetComponent<DamageControl> ().team != myTeam) {
						if (hitTargetting.transform.gameObject.GetComponent<DamageControl> ().Controller.transform != LockedTarget) {
							Target = hitTargetting.transform.gameObject.GetComponent<DamageControl> ().Controller.transform;
							targetTimer = 5;
						}
					}
				}
			}

			if (Target == null) {
				selectInd_s = 1;
				SelectTargetIndicator.SetActive (false);
			} else {
				selectInd_s = .75f;
				SelectTargetIndicator.SetActive (true);

				RectTransform CanvasRect = canvas.GetComponent<RectTransform> ();

				Vector2 ViewportPosition = Camera.main.WorldToViewportPoint (Target.position);
				Vector2 WorldObject_ScreenPosition = new Vector2 (
					((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
					((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

				SelectTargetIndicator.GetComponent<RectTransform> ().anchoredPosition = WorldObject_ScreenPosition;
			}
				
			if (LockedTarget == null) {
				targetInd_s = 1;
				LockedTargetArrow.SetActive (false);
				LockedTargetIndicator.SetActive (false);
			} else if (LockedTarget.gameObject.activeSelf) {
				targetInd_s = .75f;
				LockedTargetIndicator.SetActive (true);

				RectTransform CanvasRect = canvas.GetComponent<RectTransform> ();

				Vector2 ViewportPosition = Camera.main.WorldToViewportPoint (LockedTarget.position);
				Vector2 WorldObject_ScreenPosition = new Vector2 (
					                                     ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
					                                     ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

				LockedTargetIndicator.GetComponent<RectTransform> ().anchoredPosition = WorldObject_ScreenPosition;

				bool onScreen = ViewportPosition.x > 0 && ViewportPosition.x < 1 && ViewportPosition.y > 0 && ViewportPosition.y < 1;
				bool behindPlayer = false;
				Vector3 camRelative = Camera.main.transform.InverseTransformPoint(LockedTarget.position);

				if (camRelative.z > 0)
					behindPlayer = false;
				else
					behindPlayer = true;

				if (onScreen && !behindPlayer) {
					LockedTargetIndicator.SetActive (true);
					LockedTargetArrow.SetActive (false);
				} else {
					LockedTargetIndicator.SetActive (false);
					LockedTargetArrow.SetActive (true);

					LockedTargetArrow.transform.LookAt (LockedTarget.position);
				}
			} else {
				LockedTarget = null;
			}

			float sTargetting = SelectTargetIndicator.GetComponent<RectTransform>().localScale.x;
			sTargetting = Mathf.Lerp (sTargetting, selectInd_s, Time.deltaTime * 10);
			SelectTargetIndicator.GetComponent<RectTransform>().localScale = new Vector3 (sTargetting, sTargetting, sTargetting);

			float sLockedTargetting = LockedTargetIndicator.GetComponent<RectTransform>().localScale.x;
			sLockedTargetting = Mathf.Lerp (sLockedTargetting, targetInd_s, Time.deltaTime * 10);
			LockedTargetIndicator.GetComponent<RectTransform>().localScale = new Vector3 (sLockedTargetting, sLockedTargetting, sLockedTargetting);

			if (CrossPlatformInputManager.GetButtonDown("LockTarget")) {
				if (Target != null) {
					LockedTarget = Target;
					Target = null;
				} else {
					LockedTarget = null;
				}
			}
			////Targetting

			if (Weapons [currentWeapon].infinite)
				weaponName.text = Weapons [currentWeapon].name;
			else
				weaponName.text = Weapons [currentWeapon].name + " " + Weapons [currentWeapon].quantity.ToString () + "/" + Weapons [currentWeapon].maxQuantity;

			if (Weapons [currentWeapon].homing) {
				Ray ray = cam.ScreenPointToRay (CrossPlatformInputManager.mousePosition);
				RaycastHit hit;
				int layerMask = LayerMask.GetMask ("Radar", "RadarPoints", "Space", "Player");
				layerMask = ~layerMask;
				if (Physics.Raycast (ray, out hit, Mathf.Infinity, layerMask)) {
					if (hit.transform.gameObject.GetComponent<DamageControl> () != null) {
						if (hit.transform.gameObject.GetComponent<DamageControl> ().team != myTeam) {
							homingTarget = hit.transform.gameObject.GetComponent<DamageControl> ().Controller.transform;
							homingTimer = Weapons [currentWeapon].homingTime;
						}
					}
				}

				if (homingTarget == null) {
					homingInd_a = 0;
					homingInd_s = 0.75f;
				} else {
					homingInd_a = 1;
					homingInd_s = 0.5f;

					RectTransform CanvasRect = canvas.GetComponent<RectTransform> ();

					Vector2 ViewportPosition = Camera.main.WorldToViewportPoint (homingTarget.position);
					Vector2 WorldObject_ScreenPosition = new Vector2 (
						                                    ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
						                                    ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

					HomingIndicator.GetComponent<RectTransform> ().anchoredPosition = WorldObject_ScreenPosition;
				}
			} else {
				homingInd_a = 0;
				homingInd_s = 0.75f;
			}

			Color c = HomingIndicator.color;
			c.a = Mathf.Lerp (c.a, homingInd_a, Time.deltaTime * 10);
			HomingIndicator.color = c;
			float s = HomingIndicator.rectTransform.localScale.x;
			s = Mathf.Lerp (s, homingInd_s, Time.deltaTime * 10);
			HomingIndicator.rectTransform.localScale = new Vector3 (s, s, s);

			if (fireTimer > 0)
				fireTimer -= 0.1f;
			else if (fireTimer < 0)
				fireTimer = 0;
		
			if (heatDec) {
				if (heating > 0)
					heating -= 0.5f;
				else if (heating < 0)
					heating = 0;
			}

			if (homingTimer > 0)
				homingTimer -= 0.1f;
			else if (homingTimer < 0) {
				homingTarget = null;
				homingTimer = 0;
			}

			if (targetTimer > 0)
				targetTimer -= 0.1f;
			else if (targetTimer < 0) {
				Target = null;
				targetTimer = 0;
			}
			
			if (heating >= Weapons [currentWeapon].maxHeating) {
				if (Weapons [currentWeapon].infinite == true)
					StartCoroutine (Overheat ());
			}

			if (CrossPlatformInputManager.GetAxis ("Mouse ScrollWheel") > 0) {
				if (currentWeapon < Weapons.Length - 1)
					currentWeapon += 1;
				else
					currentWeapon = 0;
			} else if (CrossPlatformInputManager.GetAxis ("Mouse ScrollWheel") < 0) {
				if (currentWeapon > 0)
					currentWeapon -= 1;
				else
					currentWeapon = Weapons.Length - 1;
			}
			
			//  /Shooting behavior
		}
	}
}

[System.Serializable]
public class Weapon {
	public string name;
	[Tooltip("Position where bullet prefab will be instatiated")]
	public Transform [] ShootPos;
	[Tooltip("Muzzle effects")]
	public ParticleSystem[] Muzzle;
	[Tooltip("Bullet prefab")]
	public GameObject bullet;
	[Tooltip ("True if bullet is going to chase a target")]
	public bool homing;
	[Tooltip("Time that target will be locked")]
	public float homingTime;
	public float fireRate;
	[Tooltip("If false weapon will not overheat, but quantity will decrease")]
	public bool infinite;
	public int quantity;
	public int maxQuantity;
	[Tooltip("Amount of heating when you fire")]
	public float heatingInc;
	[Tooltip("Maximum heating")]
	public float maxHeating;
	[Tooltip("Time of the overheat in seconds")]
	public float overheatSec;
	[Tooltip("True if you want to fire from all weapon positions at once, false to fire one by one")]
	public bool fireAllAtOnce;
	[HideInInspector]
	public int curG; // The gun that must shoot, if you shoot one by one
}