using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Opsive.UltimateCharacterController.Character.Abilities;
using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Utility;
using Opsive.UltimateCharacterController.Character.MovementTypes;

public class AlternateCombat : Ability
{
    UltimateCharacterLocomotion script;

    public virtual void AbilityStarted() {

        script = GameObject.Find("bp").GetComponent<UltimateCharacterLocomotion>();

        //script.SetMovementType("Opsive.UltimateCharacterController.ThirdPersonController.Character.MovementTypes.Combat");

        //script.enabled = false;

        //script.ThirdPersonMovementTypeFullName = "Opsive.UltimateCharacterController.ThirdPersonController.Character.MovementTypes.Combat";

        ////print("combat");
        //script.MovementTypeFullName = "Opsive.UltimateCharacterController.ThirdPersonController.Character.MovementTypes.Combat";
        ////Opsive.UltimateCharacterController.ThirdPersonController.Character.MovementTypes.Combat
        script.SetMovementType(UnityEngineUtility.GetType("Opsive.UltimateCharacterController.ThirdPersonController.Character.MovementTypes.Combat"));
    }

    /// <summary>
    /// The ability has stopped running.
    /// </summary>
    public virtual void AbilityStopped() {

        //script.MovementTypeFullName = "Opsive.UltimateCharacterController.ThirdPersonController.Character.MovementTypes.Adventure";

        ////print("adventure");
        //script.SetMovementType(UnityEngineUtility.GetType("Opsive.UltimateCharacterController.ThirdPersonController.Character.MovementTypes.Adventure"));
    }
}
