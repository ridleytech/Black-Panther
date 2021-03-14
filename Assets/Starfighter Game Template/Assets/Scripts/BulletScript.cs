using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[AddComponentMenu("Starfighter Game Template/Bullet Script")]
public class BulletScript : MonoBehaviour {

	public float damage;
	public bool ray;
	public float speed;
	public float rotationSpeed;
	[Tooltip("Prefab of an impact effect")]
	public GameObject impact;
	public string team;
	[HideInInspector]
	public bool player = false;
	[HideInInspector]
	public Transform target;

	void Start () {
		if (ray) {
			GetComponent<LineRenderer> ().SetPosition (0, transform.position);
			if (player) {
				Ray r = Camera.main.ScreenPointToRay (CrossPlatformInputManager.mousePosition);
				RaycastHit hit;
				int layerMask = LayerMask.GetMask ("Radar", "RadarPoints", "Space", "Player");
				layerMask = ~layerMask;

				if (Physics.Raycast (r, out hit, Mathf.Infinity, layerMask)) {
					GetComponent<LineRenderer> ().SetPosition (1, hit.point);
					if (hit.transform.gameObject.GetComponent<DamageControl> () != null) {
						if (hit.transform.gameObject.GetComponent<DamageControl> ().team != team) {
							hit.transform.gameObject.GetComponent<DamageControl> ().DealDamage (damage, player);
							Instantiate (impact, hit.point, transform.rotation);
						}
					} else {
						Instantiate (impact, hit.point, transform.rotation);
					}
				} else {
					GetComponent<LineRenderer> ().SetPosition (1, Camera.main.ScreenToWorldPoint (new Vector3 (CrossPlatformInputManager.mousePosition.x, CrossPlatformInputManager.mousePosition.y, Camera.main.farClipPlane)));
				}
			} else {
				Ray r = new Ray (transform.position, transform.forward);
				RaycastHit hit;
				int layerMask = LayerMask.GetMask ("Radar", "RadarPoints", "Space");
				layerMask = ~layerMask;
				if (Physics.Raycast (r, out hit, Mathf.Infinity, layerMask)) {
					GetComponent<LineRenderer> ().SetPosition (1, hit.point);
					if (hit.transform.gameObject.GetComponent<DamageControl> () != null) {
						if (hit.transform.gameObject.GetComponent<DamageControl> ().team != team) {
							hit.transform.gameObject.GetComponent<DamageControl> ().DealDamage (damage, player);
							Instantiate (impact, hit.point, transform.rotation);
						}
					} else {
						Instantiate (impact, hit.point, transform.rotation);
					}
				} else {
					GetComponent<LineRenderer> ().SetPosition (1, transform.forward * 500);
				}
			}
		}
	}

	void Update () {
		if (Time.timeScale > 0) {
			if (!ray) {
				transform.Translate (Vector3.forward * speed);

				if (target != null) {
					Vector3 pos = target.position - transform.position;
					Quaternion rot = Quaternion.LookRotation (pos);
					transform.rotation = Quaternion.Slerp (transform.rotation, rot, Time.deltaTime * rotationSpeed);
				}
			} else {
				Color sCol = GetComponent<LineRenderer> ().startColor;
				Color eCol = GetComponent<LineRenderer> ().endColor;
				sCol.a = Mathf.Lerp (sCol.a, 0, Time.deltaTime * 15);
				eCol.a = Mathf.Lerp (eCol.a, 0, Time.deltaTime * 15);
				GetComponent<LineRenderer> ().startColor = sCol;
				GetComponent<LineRenderer> ().endColor = eCol;
				if (GetComponent<LineRenderer> ().endColor.a == 0)
					Destroy (gameObject);
			}
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.GetComponent<DamageControl> () != null) {
			if (other.gameObject.GetComponent<DamageControl> ().team != team) {
				other.gameObject.GetComponent<DamageControl> ().DealDamage (damage, player);
				Instantiate (impact, transform.position, transform.rotation);
				Destroy (gameObject);
			}
		} else {
			Instantiate (impact, transform.position, transform.rotation);
			Destroy (gameObject);
		}

	}
}
