using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Block : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

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

	void Awake () {

		direction = Vector2.zero;
		touched = false;
		//Debug.Log ("awake touchpad");
	}

	void Start () {

		ps = player.GetComponent<PlayerState> ();
	}

	public void OnPointerDown (PointerEventData data) {

		//print ("OnPointerDown");

		if (!touched) 
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

						enemy = enemy1;
					}
				}

				player.transform.LookAt (enemy.transform);
			}

			touched = true;
			pointerID = data.pointerId;
			origin = data.position;
			anim.SetBool ("Block",true);

			ps.attacking = false;
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

			anim.SetBool ("Block",false);

			touched = false;
		}
	}

	public Vector2 GetDirection () {

		smoothDirection = Vector2.MoveTowards (smoothDirection, direction, smoothing);
		return smoothDirection;
	}
}