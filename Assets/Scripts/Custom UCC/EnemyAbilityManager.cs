using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Character.Abilities;
using Opsive.UltimateCharacterController.Character.Abilities.Items;
using Opsive.UltimateCharacterController.Character.Abilities.AI;
using Opsive.UltimateCharacterController.Events;
using BehaviorDesigner.Runtime;

public class EnemyAbilityManager : MonoBehaviour
{
    [SerializeField] protected GameObject m_Enemy;
    public UltimateCharacterLocomotion enemyLocomotion;


    // Start is called before the first frame update
    void Start()
    {
        m_Enemy = gameObject;
        enemyLocomotion = m_Enemy.GetComponent<UltimateCharacterLocomotion>();
    }

    public void Awake()
    {
        EventHandler.RegisterEvent<Ability, bool>(gameObject, "OnCharacterAbilityActive", OnAbilityActive1);
    }

    private void OnAbilityActive1(Ability ability, bool activated)
    {
        //Debug.Log("enemy " + ability + " activated: " + activated);


        if (ability.ToString() == "Opsive.UltimateCharacterController.Character.Abilities.DamageVisualization" && activated == true)
        {
            //Debug.Log("damage viz");
            //if (enemyLocomotion != null)
            //{
            //    Quaternion rot = gameObject.transform.rotation;

            //    //enemyLocomotion.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            //    enemyLocomotion.SetRotation(Quaternion.Inverse(rot));
            //    //enemyLocomotion.SetActive(false);
            //}

            //stop enemy nav mesh agent movement ability

            var navAbility = enemyLocomotion.GetAbility<NavMeshAgentMovement>();

            if (navAbility.IsActive)
            {
                enemyLocomotion.TryStopAbility(navAbility);
                Debug.Log("stop nav");
            }

            // disable enemy use (attack) ability

            var useAbility = enemyLocomotion.GetAbility<Use>();

            enemyLocomotion.TryStopAbility(useAbility);
        }
    }

    void EndThrow()
    {
        //get up from being thrown

        print("EndThrow");

        var thrownAbility = enemyLocomotion.GetAbility<GetThrown>();

        if (thrownAbility.IsActive)
        {
            enemyLocomotion.TryStopAbility(thrownAbility);
        }
    }

    void GetUp()
    {
        //start nav mesh again after being thronw

        print("GetUp");

        var navAbility = enemyLocomotion.GetAbility<NavMeshAgentMovement>();

        enemyLocomotion.TryStartAbility(navAbility);
    }
}
