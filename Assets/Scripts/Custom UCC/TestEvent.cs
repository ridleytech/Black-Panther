using UnityEngine;
using Opsive.UltimateCharacterController.Events;
using Opsive.UltimateCharacterController.Character.Abilities;

public class TestEvent : MonoBehaviour
{
    /// <summary>
    /// Initialize the default values.
    /// </summary>
    public void Awake()
    {
        EventHandler.RegisterEvent<Ability, bool>(gameObject, "OnCharacterAbilityActive", OnAbilityActive);
    }

    /// <summary>
    /// The specified ability has started or stopped.
    /// </summary>
    /// <param name="ability">The ability that has been started or stopped.</param>
    /// <param name="activated">Was the ability activated?</param>
    private void OnAbilityActive(Ability ability, bool activated)
    {
        Debug.Log(ability + " activated: " + activated);
    }

    /// <summary>
    /// The GameObject has been destroyed.
    /// </summary>
    public void OnDestroy()
    {
        EventHandler.UnregisterEvent<Ability, bool>(gameObject, "OnCharacterAbilityActive", OnAbilityActive);
    }
}