using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngagePlayer : MonoBehaviour
{
    Animator anim;
    public GameObject engagedEnemy;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("collider tag: " + other.transform.parent.parent.gameObject);

        if (other.transform.parent.parent.gameObject.tag == "enemy" && engagedEnemy == null)
        {
            //print("engaged enemy");
            //anim.SetBool("engaged", true);
            engagedEnemy = other.transform.parent.parent.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //print("collider tag: " + other.transform.parent.parent.gameObject);

        if (other.transform.parent.parent.gameObject.tag == "enemy" && engagedEnemy == other.transform.parent.parent.gameObject)
        {
            //print("enemy not engaged");
            //anim.SetBool("engaged", false);
            engagedEnemy = null;
        }
    }
}
