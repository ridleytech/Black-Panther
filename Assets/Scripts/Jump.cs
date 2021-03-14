using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Jump : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

	//public float smoothing;
	private Vector2 origin;
	private Vector2 direction;
	private Vector2 smoothDirection;
	public bool touched;
	private int pointerID;
	public GameObject defender;
    public GameObject player;
    public Animator anim;
    public Animator defAnim;
    //PlayerState ps;
    public Rigidbody rb;
	public float jumpForce;
	public PlayerState defenderPS;
    GameManager gm;

    void Awake () {

		direction = Vector2.zero;
		touched = false;
		//Debug.Log ("awake touchpad");
	}

	void Start () {

        gm = GameObject.Find("GameController").GetComponent<GameManager>();
//        defenderPS = defender.GetComponent<PlayerState> ();
//        defAnim = defender.GetComponent<Animator>();
    }

	public void OnPointerDown (PointerEventData data) {

		//print ("OnPointerDown");

		if (!touched) 
		{
			print ("jump");

            //defenderPS.jumping = true;
            //anim.SetBool("onDefense", false);
            
			touched = true;
			pointerID = data.pointerId;
			origin = data.position;

            //rb.AddForce (transform.up * jumpForce);
            //anim.SetBool("blocking", true);
            //defAnim.SetTrigger("blockTrigger");

            Rigidbody playerRB = player.GetComponent<Rigidbody>();
            playerRB.AddForce(transform.up * jumpForce);            
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

            //Debug.Log ("OnPointerUp weapon");

            //anim.ResetTrigger("Attack");
//            gm.ballShot = true;
//            defAnim.SetBool("onDefense", false);
//            defAnim.SetBool("runToPosition", true);
            //ps.attacking = false;

            touched = false;
		}
	}
}