using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Opsive.UltimateCharacterController.Character.Abilities;

public class RevCombat : DetectObjectAbilityBase
{
    ManageUse mu;


    public override void OnTriggerEnter(Collider other)
    {
        //string hi = "hi";

        //mu = GameObject.Find("bp").GetComponent<ManageUse>();

        //if (!mu.attacking)
        //{

        //}

        base.OnTriggerEnter(other);

        StartAbility();

    }

    public override void OnTriggerExit(Collider other)
    {
        // The detected object will be set when the ability starts and contains a reference to the object that allowed the ability to start.
        if (other.gameObject == m_DetectedObject)
        {
            //base.print("exit");
            StopAbility();
        }

        base.OnTriggerExit(other);
    }

    public void EndAbility () {

        StopAbility();
    }
}
