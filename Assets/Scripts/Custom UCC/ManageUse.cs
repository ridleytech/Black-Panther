using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Opsive.UltimateCharacterController.Inventory;
using Opsive.UltimateCharacterController.Events;
using Opsive.UltimateCharacterController.Items;
using Opsive.UltimateCharacterController.Character.Abilities.Items;
using Opsive.UltimateCharacterController.Character.Abilities;

using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Utility;


using BehaviorDesigner.Runtime.Tasks.UltimateCharacterController;

public class ManageUse : MonoBehaviour
{
    int itemNum;

    public string m_AbilityType;
    public UltimateCharacterLocomotion m_CharacterLocomotion;
    private Ability m_Ability;
    int m_PriorityIndex = -1;
    public bool attacking;


    //https://opsive.com/support/documentation/ultimate-character-controller/character/abilities/

    public void Start() {

        m_CharacterLocomotion = GetComponent<UltimateCharacterLocomotion>(); 

        //var abilities = m_CharacterLocomotion.GetAbilities(TaskUtility.GetTypeWithinAssembly("RevCombat"));

        //if (abilities.Length > 1) {
        //    // If there are multiple abilities found then the priority index should be used, otherwise set the ability to the first value.
        //    if (m_PriorityIndex != -1) {
        //        for (int i = 0; i < abilities.Length; ++i) {
        //            if (abilities[i].Index == m_PriorityIndex.Value) {
        //                m_Ability = abilities[i];
        //                break;
        //            }
        //        }
        //    } else {
        //        m_Ability = abilities[0];
        //    }
        //}

        //print("m_Ability: "+m_Ability);
    
        //EventHandler.RegisterEvent<Item, int>(gameObject, "OnInventoryEquipItem", showSword);
    }

    public void OnItemAbilityActive(ItemAbility item, bool activated)
    {
        //print("item: " + item);

        ////attacking = true;

        //var jumpAbility = m_CharacterLocomotion.GetAbility<RevCombat>();
        //// Tries to start the jump ability. There are many cases where the ability may not start, 
        //// such as if it doesn't have a high enough priority or if CanStartAbility returns false.
        ////m_CharacterLocomotion.TryStartAbility(jumpAbility);

        //// Stop the jump ability if it is active.
        //if (jumpAbility.IsActive)
        //{
        //    m_CharacterLocomotion.TryStopAbility(jumpAbility);
        //}
    }
}