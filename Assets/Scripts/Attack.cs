using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Attack : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

	public float smoothing;
	private Vector2 origin;
	private Vector2 direction;
	private Vector2 smoothDirection;
	public bool touched;
	private int pointerID;
	public GameObject player;
	public Animator anim;
	PlayerState ps;
	GameObject[] enemies;
	GameObject enemy;
	private float m_Speed = 5f;
	GameManager gm;

	void Awake () {

		direction = Vector2.zero;
		touched = false;
		//Debug.Log ("awake touchpad");
	}

	void Start () {

		ps = player.GetComponent<PlayerState> ();
		gm = GameObject.Find ("GameController").GetComponent<GameManager>();

		//		player = GameObject.Find ("granny2");
		//
		//		print ("player: "+player);
		//
		//		anim = player.GetComponent<Animator> ();
		//
		//		print ("anim: "+anim);
	}

	public void OnPointerDown (PointerEventData data) {

		//print ("OnPointerDown");

		if (!touched && !ps.attacking) 
		{
			//print ("attack");

			ps.attacking = true;
			enemies = GameObject.FindGameObjectsWithTag ("Emerald AI");

			if(enemies.Length > 0)
			{
				float closest = 300f;

				foreach (GameObject enemy1 in enemies)
				{
					float distance = Vector3.Distance (enemy1.transform.position, player.transform.position);

					if (distance < closest) {

						gm.currentTarget = enemy1;
						closest = distance;
					}
				}

				player.transform.LookAt (gm.currentTarget.transform);
			}

			//print (gm.currentTarget.name);

			touched = true;
			pointerID = data.pointerId;
			origin = data.position;
			anim.SetTrigger ("Attack");
		}
	}

//	void Update()
//	{
//		if (touched && enemy) {
//
//			print ("smooth");
//			
//			Vector3 lTargetDir = enemy.transform.position - player.transform.position;
//			lTargetDir.y = 0.0f;
//			transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.LookRotation (lTargetDir), Time.time * m_Speed);
//		}
//	}

	public void OnDrag (PointerEventData data) {

		if (data.pointerId == pointerID) 
		{
			Vector2 currentPosition = data.position;
			Vector2 directionRaw = currentPosition - origin;
			direction = directionRaw.normalized;

			//Debug.Log ("direction: "+direction);
		}
	}

	public void OnPointerUp (PointerEventData data) {

		if (data.pointerId == pointerID) 
		{
			direction = Vector3.zero;

			//Debug.Log ("OnPointerUp weapon");

			//anim.ResetTrigger("Attack");

			//ps.attacking = false;

			touched = false;
		}
	}

	public Vector2 GetDirection () {

		smoothDirection = Vector2.MoveTowards (smoothDirection, direction, smoothing);
		return smoothDirection;
	}
}