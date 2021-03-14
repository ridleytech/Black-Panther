using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Character.Abilities;
using Opsive.UltimateCharacterController.Character.Abilities.Items;
using Opsive.UltimateCharacterController.Character.Abilities.AI;
using Opsive.UltimateCharacterController.Events;
using BehaviorDesigner.Runtime;

public class PlayerAbilityManager : MonoBehaviour
{
    [SerializeField] protected GameObject m_Character;
    [SerializeField] protected GameObject m_Enemy;
    public Animator anim;
    public Transform armPosition;
    bool e_BehaviorTreeEnabled;
    public BehaviorTree e_BehaviorTree;
    UnityEngine.AI.NavMeshAgent agent;

    private void Start()
    {
        m_Character = gameObject;
        anim = m_Character.GetComponent<Animator>();
        e_BehaviorTree = m_Enemy.GetComponent<BehaviorTree>();
        agent = m_Enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void OnAnimatorIK(int layerIndex)
    {
        if(anim)
        {
            //Randall
            //print("reach");

            float reach = anim.GetFloat("RightHandReach");
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, reach);
            anim.SetIKPosition(AvatarIKGoal.RightHand, armPosition.position);

            float reachL = anim.GetFloat("LeftHandReach");
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, reachL);
            anim.SetIKPosition(AvatarIKGoal.RightHand, armPosition.position);
        }
    }

    public void Awake()
    {
        EventHandler.RegisterEvent<Ability, bool>(gameObject, "OnCharacterAbilityActive", OnAbilityActive1);
    }

    private void OnAbilityActive1(Ability ability, bool activated)
    {
        //Debug.Log(ability + " activated: " + activated);

        //if (ability.ToString() == "ThrowOpponent1" && activated == true && anim.GetBool("engaged") == true)
           if (ability.ToString() == "ThrowOpponent1" && activated == true)
        {
            //grab enemy arm

            //reach();

            //m_Enemy.transform.LookAt(gameObject.transform);

            flipEnemy();

            //disable behavior tree

            //e_BehaviorTreeEnabled = BehaviorManager.instance.IsBehaviorEnabled(e_BehaviorTree);

            //if (e_BehaviorTreeEnabled)
            //{
            //    Debug.Log("disable tree");
            //    e_BehaviorTree.DisableBehavior();
            //}
        }
        else if (ability.ToString() == "ThrowOpponent1" && activated == false)
        {
            //disableReach();

            //print("stop reaching");
        }

    }

    void flipEnemy()
    {
        //set enemy rotation

        Debug.Log("flipEnemy");

        var enemyLocomotion = m_Enemy.GetComponent<UltimateCharacterLocomotion>();

        if (enemyLocomotion != null)
        {
            Quaternion rot = gameObject.transform.rotation;

            //characterLocomotion.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            enemyLocomotion.SetRotation(Quaternion.Inverse(rot));
            //characterLocomotion.SetActive(false);
        }

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

        //start enemy get thrown animation

        var getThrownAbility = enemyLocomotion.GetAbility<GetThrown>();

        enemyLocomotion.TryStartAbility(getThrownAbility);
    }

    void EndSlam()
    {
        print("EndSlam1");

        //manually stop throw ability

        var characterLocomotion = m_Character.GetComponent<UltimateCharacterLocomotion>();
        var throwAbility = characterLocomotion.GetAbility<ThrowOpponent1>();

        // Tries to start the jump ability. There are many cases where the ability may not start, 
        // such as if it doesn't have a high enough priority or if CanStartAbility returns false.
        //characterLocomotion.TryStartAbility(jumpAbility);

        // Stop the throw ability if it is active.
        if (throwAbility.IsActive)
        {
            characterLocomotion.TryStopAbility(throwAbility);
        }
    }

    void reach ()
    {
        anim.SetFloat("RightHandReach", 1.0f);
        anim.SetFloat("LeftHandReach", 1.0f);
    }

    void disableReach()
    {
        anim.SetFloat("RightHandReach", 0f);
        anim.SetFloat("LeftHandReach", 0f);
    }

    /// <summary>
    /// The GameObject has been destroyed.
    /// </summary>
    public void OnDestroy()
    {
        EventHandler.UnregisterEvent<ThrowOpponent1, bool>(gameObject, "OnCharacterAbilityActive", OnAbilityActive1);
    }
}