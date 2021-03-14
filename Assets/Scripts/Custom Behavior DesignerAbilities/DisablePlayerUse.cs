using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Character.Abilities;
using Opsive.UltimateCharacterController.Character.Abilities.Items;
using Opsive.UltimateCharacterController.Character.Abilities.AI;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class DisablePlayerUse : Action
{
    public GameObject m_Character;

    // Start is called before the first frame update
    void Start()
    {
        //m_Character = GameObject.FindWithTag("Player");
    }

    void disableAbility()
    {
        var characterLocomotion = m_Character.GetComponent<UltimateCharacterLocomotion>();
        var useAbility = characterLocomotion.GetAbility<Use>().Enabled = false;

        //if (throwAbility.IsActive)
        //{
        //    characterLocomotion.TryStopAbility(throwAbility);
        //}
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}