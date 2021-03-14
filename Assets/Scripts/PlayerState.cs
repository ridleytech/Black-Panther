using UnityEngine;
using System.Collections;
public class PlayerState : MonoBehaviour {

	public bool attacking;
	public bool swingingSword;
	public bool isSwordAttack;
	public bool isFootAttack;
	public bool knockedDown;
	public bool dead;
	public int energy;
	public Animator anim;
	public bool jumping;
    GameManager gm;
    public int playerNumber;

	GameObject[] enemies;
	GameObject enemy;
	public float jumpForce;

	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;

	//[RequireComponent(typeof(CharacterController))]
	public CharacterController controller;
	public string enemyTag;
	public float moveSpeed;
	public int attackType;

    void Start () {

        gm = GameObject.Find("GameController").GetComponent<GameManager>();
        anim = GetComponent<Animator> ();
		energy = 100;
	}

//	void OnTriggerEnter (Collider other) {
//
//		if(other.name == "floor" && gm.playerPossession != playerNumber)
//		{
//            anim.SetBool("onDefense", true);
//            //anim.SetBool("blocking", false);
//            print ("landed");
//			jumping = false;
//
//            if (this.name == "man")
//            {
//
//            }
//		}
//	}



	void landedFlip () {

		jumping = false;

		//to do randall

		if (gm.currentTarget) {
		
			//enable enemey emeraldAI

			print ("enable emerald");

//			Emerald_AI EmeraldComponent = gm.currentTarget.GetComponent<Collider>().gameObject.GetComponent<Emerald_AI> ();
//			EmeraldComponent.enabled = true;

			Quaternion rotation = transform.rotation;
			//Vector3 result = transform.forward;
			transform.rotation = rotation;

		}
	}

	void OnTriggerEnter (Collider other) {

		//print ("foot collider name: "+other.gameObject.name);

		if (other.gameObject.name.Contains("Terrain"))
		{
			print ("landed");
			jumping = false;
		}
	}

	void startSwing () {

		//print ("startSwing");

		swingingSword = true;
	}

	void endSwing () {

		//print ("endSwing");
		swingingSword = false;
		attacking = false;
		anim.ResetTrigger("Attack");
	}

	void Update() {

		if(Input.GetButton("Jump"))
		{
			print ("jump");
//			Rigidbody playerRB = GetComponent<Rigidbody>();
//			playerRB.AddForce(transform.up * jumpForce);

			if (controller.isGrounded && Input.GetButton("Jump")) {
				moveDirection.y = jumpSpeed;
			}

			moveDirection.y -= gravity * Time.deltaTime;
			controller.Move(moveDirection * Time.deltaTime);


			jumping = true;
		}
		else if (Input.GetButton("Evade") && !jumping) 
		{
			//print ("attack");

			enemies = GameObject.FindGameObjectsWithTag ("Emerald AI");

			if(enemies.Length > 0)
			{
				float closest = 300f;

				foreach (GameObject enemy1 in enemies)
				{
					float distance = Vector3.Distance (enemy1.transform.position, transform.position);

					if (distance < closest) {

						gm.currentTarget = enemy1;
						closest = distance;
					}
				}

				transform.LookAt (gm.currentTarget.transform);
			}

			jumping = true;
			anim.SetTrigger ("Evade");

			attacking = false;

			//to do randall

			if(gm.currentTarget)
			{
				float distance1 = Vector3.Distance (gm.currentTarget.transform.position, transform.position);

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
		else if (Input.GetButton("Punch") && !attacking) 
		{
			print ("attack");

			attacking = true;
			enemies = GameObject.FindGameObjectsWithTag (enemyTag);

			if(enemies.Length > 0)
			{
				float closest = 300f;

				foreach (GameObject enemy1 in enemies)
				{
					EnemyHealth eh = enemy1.GetComponentInParent<EnemyHealth>();

					if (eh.health > 0) {
						
						float distance = Vector3.Distance (enemy1.transform.position, transform.position);

						if (distance < closest) {

							gm.currentTarget = enemy1;
							closest = distance;
						}
					}
				}
			}

			//print (gm.currentTarget.name);

			if (attackType == 0) {

				anim.SetTrigger ("Attack");
				isSwordAttack = true;
			}
			else if (attackType == 1) {

				//anim.SetTrigger ("crawlSweepAttack");
				anim.SetBool ("crawl",true);
			}

			if (gm.currentTarget) {
				
				gm.currentTarget.transform.LookAt (transform);
			}
		}

		if (attacking && gm.currentTarget) {

			transform.LookAt (gm.currentTarget.transform);
			//gm.currentTarget.transform.LookAt (transform);

			if (attackType == 1) 
			{
				MoveTowardsTarget (gm.currentTarget.transform.position);
			}
		}

//		float knockdownCurve = anim.GetFloat(Animator.StringToHash("knockdownCurve"));
//		float getUpCurve = anim.GetFloat(Animator.StringToHash("getUpCurve"));
//
//		//		if (kickCurve > .0075f) {
//		//
//		//			//print ("kickCurve: "+kickCurve);
//		//
//		//			//print ("knockdown");
//		//
//		//			//opponentanim.SetTrigger ("Knockdown");
//		//		} 
//
//		if (knockdownCurve > 0 || getUpCurve > 0) {
//			
//			knockedDown = true;
//		} else {
//			knockedDown = false;
//		}
	}

	void MoveTowardsTarget(Vector3 target) {

		//print ("moving to target");
		
		var cc = GetComponent<CharacterController>();
		var offset = target - transform.position;
		//Get the difference.
		if(offset.magnitude > .1f) {
			//If we're further away than .1 unit, move towards the target.
			//The minimum allowable tolerance varies with the speed of the object and the framerate. 
			// 2 * tolerance must be >= moveSpeed / framerate or the object will jump right over the stop.
			offset = offset.normalized * moveSpeed;
			//normalize it and account for movement speed.
			cc.Move(offset * Time.deltaTime);
			//actually move the character.
		}
	}


}
