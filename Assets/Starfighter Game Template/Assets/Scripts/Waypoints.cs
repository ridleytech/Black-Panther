using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Waypoints : MonoBehaviour
{

    public StarshipAI em;
    public Transform next;

    // Start is called before the first frame update
    void Start()
    {
        
    }

void OnTriggerEnter (Collider other) {

print("enter");

        em.battleCenter = next;

}

    // Update is called once per frame
    void Update()
    {
        
    }
}
