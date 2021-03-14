using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Evade : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

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
	}

	public void OnPointerDown (PointerEventData data) {

		//print ("OnPointerDown");

		if (!touched && !ps.jumping) 
		{
			//print ("attack");

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

			ps.jumping = true;
			touched = true;
			pointerID = data.pointerId;
			origin = data.position;
			anim.SetTrigger ("Evade");

			ps.attacking = false;

			//to do randall

			if(gm.currentTarget)
			{
				float distance1 = Vector3.Distance (gm.currentTarget.transform.position, player.transform.position);

				if (distance1 < 2f) {

					//disable enemey emeraldAI

					if (gm.currentTarget.GetComponent<Collider>().gameObject.GetComponent<Emerald_AI> () != null) {

						print ("disable emerald");

						Emerald_AI EmeraldComponent = gm.currentTarget.GetComponent<Collider>().gameObject.GetComponent<Emerald_AI> ();
						EmeraldComponent.enabled = false;

						//set enemy animation to ducking
					}
				}
			}
		}
	}

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

			anim.ResetTrigger ("Evade");

			touched = false;
		}
	}

	public Vector2 GetDirection () {

		smoothDirection = Vector2.MoveTowards (smoothDirection, direction, smoothing);
		return smoothDirection;
	}
}