using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantherFight : MonoBehaviour {

	private int pointerID;
	public GameObject player;
	public Animator anim;
	PlayerState ps;
	GameObject[] enemies;
	GameObject enemy;
	private float m_Speed = 5f;
	GameManager gm;

	void Start () {

		player = GameObject.Find ("GameController");
		ps = player.GetComponent<PlayerState> ();
		gm = GameObject.Find ("GameController").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButton("Evade") && !ps.jumping) 
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

}
