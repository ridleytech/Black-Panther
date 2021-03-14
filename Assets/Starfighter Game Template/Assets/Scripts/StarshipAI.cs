using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[AddComponentMenu("Starfighter Game Template/Starfighter AI")]
public class StarshipAI : MonoBehaviour {

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
	public float speed;
	public float rotationSmooth;

	[Space(16)]
	[Header("Shooting Settings")]
	public Weapon[] Weapons;
	public float aimSpeed;
	int currentWeapon = 0;
	float fireTimer;
	float heating; // current heating
	bool heatDec; // true if heating should decrease;
	Quaternion desiredRot;
	[Tooltip("Explosion prefab to instatiate when AI dies, one will be selected randomly")]
	public GameObject [] explosion;

	[Space(16)]
	[Header("AI Settings")]
	public string enemyTag;
	public GameObject radarPoint;
	public GameObject target;
	public Transform shipTransform;
	[HideInInspector]
	public bool inFormation = false;
	[HideInInspector]
	public PlayerSpaceship player;
    public Transform battleCenter;

    [HideInInspector]
	public Transform formationTarget;
	Vector3 formShootTarget;
	[Tooltip("Amount of scores when player kills")]
	public int score;
	GameObject [] targets; // Array of all possible targets
	float changeTargetTimer; // Timer that will force AI to change it's target
	float raycastOffset = 35f; // Needed for pathfinding
	float detectionDist = 5f;
	float sight = 45; 
	public float targettingDelay;
	[HideInInspector]
	public float battleSize;
	bool leavingBattlefield;

	void Start () {
		targets = GameObject.FindGameObjectsWithTag (enemyTag);
		CheckTargets ();
		GameObject r = Instantiate (radarPoint, transform.position, transform.rotation) as GameObject;
		r.GetComponent<RadarPointScript> ().target = gameObject.transform;
		heatDec = true;
	}

	public void ModifyHealth(float dmg, bool p) {
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
			if (p)
				PlayerPrefs.SetInt ("Score", PlayerPrefs.GetInt ("Score") + score);
			Die ();
		}
	}

	void Die() {
		int r = Random.Range (0, explosion.Length);
		Instantiate (explosion[r], transform.position, transform.rotation);
		health = maxHealth;
		heatDec = true;
		heating = 0;
		inFormation = false;
		formationTarget = null;
		gameObject.SetActive (false);
	}

	public void CheckTargets() {
		if (targettingDelay == 0) {
			if (!leavingBattlefield) {
				//This function picks a target from the enemies
				changeTargetTimer = Random.Range (75, 100);
				int nextTarget = 0;
				for (int i = 0; i < targets.Length; i++) {
					float p = Vector3.Distance (transform.position, targets [nextTarget].transform.position);
					float n = Vector3.Distance (transform.position, targets [i].transform.position);
					if (n < p)
						nextTarget = i;
				}
			}
		}
	}

	void Aim() {
		for (int i = 0; i < targets.Length; i++) {
			Vector3 targetDir = targets[i].transform.position - transform.position;
			float angle = Vector3.Angle (targetDir, transform.forward);
			if (angle <= sight) {
				if (Vector3.Distance (transform.position, targets [i].transform.position) < 35) {
					target = targets [i];
					formShootTarget = target.transform.position;
					Fire ();
				}
			}
		}
	}

	void Fire () {
		if (heating < Weapons[currentWeapon].maxHeating) {
			if (fireTimer == 0) {
				if (!Weapons[currentWeapon].fireAllAtOnce) {
					Transform helpingTransform = Weapons[currentWeapon].ShootPos [Weapons[currentWeapon].curG].transform;
					desiredRot = Weapons [currentWeapon].ShootPos [Weapons [currentWeapon].curG].transform.localRotation;
					if (!inFormation)
						helpingTransform.LookAt (target.transform.position);
					else
						helpingTransform.LookAt (formShootTarget);
					desiredRot = Quaternion.Slerp (desiredRot, helpingTransform.localRotation, Time.deltaTime * aimSpeed);
					Weapons [currentWeapon].ShootPos [Weapons [currentWeapon].curG].transform.localRotation = desiredRot;
					Weapons[currentWeapon].Muzzle [Weapons[currentWeapon].curG].GetComponent<ParticleSystem> ().Play ();
					GameObject b = Instantiate (Weapons[currentWeapon].bullet, Weapons[currentWeapon].ShootPos [Weapons[currentWeapon].curG].transform.position, Weapons[currentWeapon].ShootPos [Weapons[currentWeapon].curG].transform.rotation) as GameObject;
					b.GetComponent<BulletScript> ().player = inFormation;
					heating += Weapons[currentWeapon].heatingInc;
					if (Weapons[currentWeapon].curG < Weapons[currentWeapon].ShootPos.Length - 1)
						Weapons[currentWeapon].curG += 1;
					else
						Weapons[currentWeapon].curG = 0;
					fireTimer = Weapons[currentWeapon].fireRate / Weapons[currentWeapon].ShootPos.Length;
				} else {
					for (int i = 0; i < Weapons[currentWeapon].ShootPos.Length; i++) {
						desiredRot = Weapons [currentWeapon].ShootPos [i].transform.localRotation;
						Transform helpingTransform = Weapons[currentWeapon].ShootPos [i].transform;
						if (!inFormation)
							helpingTransform.LookAt (target.transform.position);
						else 
							helpingTransform.LookAt (formShootTarget);
						desiredRot = Quaternion.Slerp (desiredRot, helpingTransform.localRotation, Time.deltaTime * aimSpeed);
						Weapons [currentWeapon].ShootPos [i].transform.localRotation = desiredRot;
						Weapons[currentWeapon].Muzzle [i].GetComponent<ParticleSystem> ().Play ();
						heating += Weapons[currentWeapon].heatingInc;
						GameObject b = Instantiate (Weapons[currentWeapon].bullet, Weapons[currentWeapon].ShootPos [i].transform.position, Weapons[currentWeapon].ShootPos [i].transform.rotation) as GameObject;
						b.GetComponent<BulletScript> ().player = inFormation;
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

	void Move () {
		transform.Translate (0, 0, speed);
	}

	void Pathfinding () {
		RaycastHit hit;
		Vector3 turnMod = Vector3.zero;

		Vector3 left = transform.position - transform.right * raycastOffset;
		Vector3 right = transform.position + transform.right * raycastOffset;
		Vector3 up = transform.position + transform.up * raycastOffset;
		Vector3 down = transform.position - transform.up * raycastOffset;

		if (Physics.Raycast (transform.position, left, out hit, detectionDist)) {
			if (hit.collider.gameObject.GetComponent<DamageControl>() == null)
				turnMod += right;
			else if (hit.collider.gameObject.GetComponent<DamageControl>().Controller != gameObject)
				turnMod += right;
		}
		if (Physics.Raycast (transform.position, right, out hit, detectionDist)) {
			if (hit.collider.gameObject.GetComponent<DamageControl>() == null)
				turnMod += left;
			else if (hit.collider.gameObject.GetComponent<DamageControl>().Controller != gameObject)
				turnMod += left;
		}
		if (Physics.Raycast (transform.position, up, out hit, detectionDist)) {
			if (hit.collider.gameObject.GetComponent<DamageControl>() == null)
				turnMod += down;
			else if (hit.collider.gameObject.GetComponent<DamageControl>().Controller != gameObject)
				turnMod += down;
		}
		if (Physics.Raycast (transform.position, down, out hit, detectionDist)) {
			if (hit.collider.gameObject.GetComponent<DamageControl>() == null)
				turnMod += up;
			else if (hit.collider.gameObject.GetComponent<DamageControl>().Controller != gameObject)
				turnMod += up;
		}

		if (turnMod != Vector3.zero) {
			Quaternion rot = Quaternion.LookRotation (turnMod);
			transform.rotation = Quaternion.Slerp (transform.rotation, rot, rotationSmooth * 2 * Time.deltaTime);
		} else {
			if (targettingDelay == 0) {
				if (target != null) {
					Vector3 pos = target.transform.position - transform.position;
					Quaternion rot = Quaternion.LookRotation (pos);
					float rollAngle = Vector3.SignedAngle (pos, transform.forward, Vector3.up);
					rollAngle = Mathf.Clamp (rollAngle, -50, 50);
					shipTransform.Rotate (0, 0, -rollAngle * Time.deltaTime * rotationSmooth);
					transform.rotation = Quaternion.Slerp (transform.rotation, rot, rotationSmooth * Time.deltaTime);
				} else {
					CheckTargets ();
				}
			}
		}
	}

	void FormationMode () {
		if (player.health > 0) {
			Vector3 velocity = Vector3.zero;

			transform.position = Vector3.SmoothDamp (transform.position, formationTarget.position, ref velocity, Time.deltaTime, 300);
			transform.rotation = formationTarget.rotation;
			shipTransform.rotation = player.ship.rotation;

			Camera cam = Camera.main;
			formShootTarget = Vector3.forward;
			Ray ray = cam.ScreenPointToRay (CrossPlatformInputManager.mousePosition);
			RaycastHit hit;
			int layerMask = LayerMask.GetMask ("Radar", "RadarPoints", "Space", "Player");
			layerMask = ~layerMask;

			Aim ();

			if (CrossPlatformInputManager.GetButton ("Fire1")) {
			
				if (Physics.Raycast (ray, out hit, Mathf.Infinity, layerMask)) {
					formShootTarget = hit.point;
				} else {
					formShootTarget = cam.ScreenToWorldPoint (new Vector3 (CrossPlatformInputManager.mousePosition.x, CrossPlatformInputManager.mousePosition.y, cam.farClipPlane));
				}

				Fire ();
			}
		} else {
			inFormation = false;
			formationTarget = null;
		}
	}

	void Update () {
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

			if (!inFormation) {
				Pathfinding ();
				Move ();

				if (!leavingBattlefield) {
					if (Vector3.Distance (battleCenter.position, transform.position) > battleSize * 0.75f) {
						leavingBattlefield = true;
						target = battleCenter.gameObject;
					}
				} else {
					if (Vector3.Distance (battleCenter.position, transform.position) < battleSize * 0.5f) {
						leavingBattlefield = false;
						CheckTargets ();
					}
				}
			} else {
				FormationMode ();
			}

			if (heating >= Weapons [currentWeapon].maxHeating) {
				if (Weapons [currentWeapon].infinite == true)
					StartCoroutine (Overheat ());
			}

			if (heatDec) {
				if (heating > 0)
					heating -= 0.5f;
				else if (heating < 0)
					heating = 0;
			}

			if (!inFormation) {
				if (target == null)
					CheckTargets ();
				else
					Aim ();
			}

			if (fireTimer > 0)
				fireTimer -= 0.1f;
			else if (fireTimer < 0)
				fireTimer = 0;

			if (targettingDelay > 0) 
				targettingDelay -= 0.1f;
			else if (targettingDelay < 0) 
				targettingDelay = 0;

			if (changeTargetTimer > 0)
				changeTargetTimer -= 0.05f;
			else
				CheckTargets ();
		}
	}
}
