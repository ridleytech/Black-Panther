using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Defense : MonoBehaviour {

	public GameObject playerAssignment;
	public float defenseRating;
	public float offset;
	public GameObject rim;
	public Vector3 defPosition;
	DonePlayerMovementOrig playerMovement;
	Animator anim;
	PlayerState ps;
    NavMeshAgent agent;
    float lastDistance;
    GameManager gm;
    bool agentStarted;
    public bool ballRebounded;
    public Vector3 destination;

    void Start () {
	
		ps = GetComponent<PlayerState> ();
		playerMovement = playerAssignment.GetComponent<DonePlayerMovementOrig> ();
		anim = GetComponent<Animator> ();
        agent = GetComponent<NavMeshAgent>();
        gm = GameObject.Find("GameController").GetComponent<GameManager>();

		anim.SetBool ("onDefense", true);
        agent.speed = 10;
	}

	void Update () {

        float distance = Vector3.Distance(playerAssignment.transform.position, transform.position);

        if (distance != lastDistance)
        {
            //print("distance: " + distance);
            lastDistance = distance;
        }

        float jumpCurve = anim.GetFloat(Animator.StringToHash("jumpContest3"));

        //print("jumpCurve: " + jumpCurve);

        if (gm.ballShot == false)
        {
            if (playerMovement.h != 0 || playerMovement.v != 0)
            {
                //print ("moving");
                //print("h: "+ playerMovement.h);

                anim.SetBool("shuffling", true);

                if (playerMovement.h < 0)
                {
                    anim.SetBool("moveLeft", true);
                    anim.SetBool("moveRight", false);
                }
                else
                {
                    anim.SetBool("moveLeft", false);
                    anim.SetBool("moveRight", true);
                }
            }
            else
            {
                anim.SetBool("shuffling", false);
            }

            Vector3 v = GetPoint(playerAssignment.transform.position, rim.transform.position);
            Vector3 v2 = GetPoint(v, playerAssignment.transform.position);
            Vector3 v3 = GetPoint(v2, playerAssignment.transform.position);

            //print("distance: " + distance);

            if (gm.inPaint)
            {
               // if (distance > 1.5f)
               // {
                //    transform.position = Vector3.Lerp(transform.position, v3, .01f);
                //}
                //else
                //{
                //    transform.position = v3;
                //}

                transform.position = v3;
            }
            else
            {
                transform.position = v2;
            }

            //agent.destination = v2;

            if (jumpCurve == 0f)
            {
                //print("jumpCurve: " + jumpCurve);

                //anim.SetBool ("blocking", false);

                //transform.position = Vector3.Lerp(transform.position, v2, Time.deltaTime);

                //transform.position = v3;
            }
            else
            {
                print("jumping");

                //anim.SetBool("onDefense", false);
                //anim.SetBool ("shuffling", false);
                // anim.SetBool("blocking", true);
            }
            
            transform.LookAt(playerAssignment.transform);
        }
        else //ball was shot
        {
            if (!agentStarted && jumpCurve == 0f) // go for rebound
            {
                transform.LookAt(rim.transform);
                agent.destination = rim.transform.position;

                destination = rim.transform.position;

                agentStarted = true;

                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    agent.speed = 5;
                    anim.SetFloat("Speed", 5f);
                }
            }            

            if (agent.remainingDistance < agent.stoppingDistance) // if at rim/ball
            {
                //print("remainingDistance: " + agent.remainingDistance);
                //print("stop");

                anim.SetFloat("Speed", 0f);
                agent.speed = 0f;
                agent.Stop();

                if(gm.shotMissed)
                {
                    anim.SetBool("rebound", true);
                }                
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "ball" && gm.ballShot)
        {
            print("rebound");
            ballRebounded = true;
            gm.ball.transform.parent = gameObject.transform;

            if (gm.playerPossession == 1)
            {
                gm.playerPossession = 2;
            }
            else
            {
                gm.playerPossession = 1;
            }
        }
    }

    Vector3 GetPoint(Vector3 pos1, Vector3 pos2)
	{
		//get the positions of our transforms
		//Vector3 pos1 = playerAssignment.transform.position ;
		//Vector3 pos2 = rim.transform.position ;

		//get the direction between the two transforms -->
		Vector3 dir = (pos2 - pos1).normalized ;

		//get a direction that crosses our [dir] direction
		//NOTE! : this can be any of a buhgillion directions that cross our [dir] in 3D space
		//To alter which direction we're crossing in, assign another directional value to the 2nd parameter
		Vector3 perpDir = Vector3.Cross(dir, Vector3.right) ;

		//get our midway point
		Vector3 midPoint = (pos1 + pos2) / 2f ;

		//get the offset point
		//This is the point you're looking for.
		Vector3 offsetPoint = midPoint + (perpDir * offset) ;

        Vector3 new1 = new Vector3(offsetPoint.x,transform.position.y,offsetPoint.z);

		return new1;
	}
}