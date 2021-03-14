using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour {

    GameManager gm;

    void Start () {

        gm = GameObject.Find("GameController").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "man")
        {
            gm.inPaint = true;
            //print("in the paint");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "man")
        {
            gm.inPaint = false;
            //print("out of paint");
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
