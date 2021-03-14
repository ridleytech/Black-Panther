using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FreeLookController : MonoBehaviour {

    //public GameObject ultMoveStickGO;
    public UltimateJoystick ultMoveStick;

    public float h;
    public float v;
    public CinemachineFreeLook flc;

    void Awake()
    {
        //ultMoveStick = GameObject.Find("UltimateLookJoystick").GetComponent<UltimateJoystick>();
    }

    void FixedUpdate()
    {
        // Cache the inputs.
        //		float h = Input.GetAxis("Horizontal");
        //		float v = Input.GetAxis("Vertical");

        //if (Input.GetAxis("Horizontal") != 0f)
        //{

        //    h = Input.GetAxis("Horizontal");

        //}
        //else
        //{

        //    h = ultMoveStick.horizontalValue;
        //}

        //if (Input.GetAxis("Vertical") != 0f)
        //{

        //    v = Input.GetAxis("Vertical");

        //}
        //else
        //{

        //    v = ultMoveStick.verticalValue;
        //}


        
        

//		if (Input.GetAxis ("Mouse X") != 0f) {
//			
//			h = Input.GetAxis ("Mouse X");
//			
//		} else {
//			
//			h = ultMoveStick.horizontalValue;
//		}
//			
//		if (Input.GetAxis ("Mouse Y") != 0f) {
//			
//			v = Input.GetAxis ("Mouse Y");
//
//		} else {
//
//			v = ultMoveStick.verticalValue;
//		}

//		h = Input.GetAxis ("Mouse X");
//		v = Input.GetAxis ("Mouse Y");

//		h = ultMoveStick.horizontalValue;
//		v = ultMoveStick.verticalValue;
//
//		flc.m_XAxis.Value = h;
//		flc.m_YAxis.Value = v;

        //input axis name in inspector : Mouse X

        //MovementManagement(h, v, sneak);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
