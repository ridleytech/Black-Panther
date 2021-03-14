using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
	public float health = 100f;							// How much health the player has left.
	//public AudioClip deathClip;							// The sound effect of the player dying.
	
	private Animator anim;								// Reference to the animator component.
	//private DonePlayerMovement playerMovement;			// Reference to the player movement script.
	private DoneHashIDs hash;							// Reference to the HashIDs.
	private float timer;								// A timer for counting to the reset of the level once the player is dead.
	public bool playerDead;							// A bool to show if the player is dead or not.
	private CapsuleCollider col;							// Reference to the sphere collider trigger component.
	public bool playerKnockedOut;
	public GameObject hitbox;
	GameManager gm;

	void Awake ()
	{
		// Setting up the references.
		anim = GetComponentInParent<Animator>();
		hash = GameObject.FindGameObjectWithTag(DoneTags.gameController).GetComponent<DoneHashIDs>();
		gm = GameObject.Find("GameController").GetComponent<GameManager>();
	}
	
	void Update ()
	{
		// If health is less than or equal to 0...
		if(health <= 0f)
		{
			// ... and if the player is not yet dead...
			if(!playerDead)
				// ... call the PlayerDying function.
				PlayerDying();
			else
			{
				// Otherwise, if the player is dead, call the PlayerDead and LevelReset functions.
				PlayerDead();
			}
		}
	}
	
	public void PlayerDying ()
	{
		//print ("dying");
		//print ("was shot");
		// The player is now dead.
		playerDead = true;
		
		// Set the animator's dead parameter to true also.
		
		//Debug.Log(hash.deadBool);
		//Debug.Log(playerDead);



		gm.currentTarget = null;
		//gameObject.tag = null;

		Destroy (GetComponent<Rigidbody> ());
		Destroy (hitbox);

		if(GetComponent<UnityEngine.AI.NavMeshAgent>())
		{
			UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

			agent.Stop ();
			//agent.enabled = false;
		}

		CapsuleCollider cap = GetComponent<CapsuleCollider>();
		cap.enabled = false;


		//StartCoroutine(RunSpawnerIteration(1.25f));



		// Play the dying sound effect at the player's location.
		//AudioSource.PlayClipAtPoint(deathClip, transform.position);
	}
		
	private IEnumerator RunSpawnerIteration(float waitTime)
	{
		anim.SetTrigger("Dead");

		yield return new WaitForSeconds(waitTime);

		AudioSource audio = GetComponent<AudioSource>();

		int rand = Random.Range (0, gm.deathClips.Length);

		audio.clip = gm.deathClips[rand];
		audio.Play();
	}
	
	void PlayerDead ()
	{
		// If the player is in the dying state then reset the dead parameter.

//		if (anim.GetCurrentAnimatorStateInfo (0).nameHash == hash.dyingState) 
//		{
//			anim.SetBool (hash.deadBool, false);
//			print ("reset");
//		}
		
		// Disable the movement.
		//anim.SetFloat(hash.speedFloat, 0f);
		//playerMovement.enabled = false;
	}
	
	public void TakeDamage (float amount)
	{
		// Decrement the player's health by amount.
		health -= amount;
	}
	
	void OnTriggerEnter (Collider other)
	{
		// If the bullet has entered the trigger sphere...
		
//		if (other.tag == "Bullet") 
//		{
//			playerDead = true;
//			PlayerDying();
//			//Debug.Log("die");
//		}
	}
}
