using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Opsive.UltimateCharacterController.Character.Abilities
{
    public class DamageVisualizationStomach : DamageVisualization
    {
        /// <summary>
        /// Returns the value that the AbilityIntData parameter should be set to.
        /// </summary>
        /// <param name="amount">The amount of damage taken.</param>
        /// <param name="position">The position of the damage.</param>
        /// <param name="force">The amount of force applied to the character.</param>
        /// <param name="attacker">The GameObject that damaged the character.</param>
        /// <returns>The value that the AbilityIntData parameter should be set to. A value of -1 will prevent the ability from starting.</returns>
        protected override int GetDamageTypeIndex(float amount, Vector3 position, Vector3 force, GameObject attacker)
        {
            //if (amount > 20)
            //{
            //    return 4;
            //    //print("show custom damage");
            //}

            ////return base.GetDamageTypeIndex(amount,position,force,attacker);
            //return -1;



            var direction = m_Transform.InverseTransformPoint(position);
            if (direction.z > 0)
            {
                if (direction.x > 0)
                {
                    return (int)TakeDamageIndex.FrontRight;
                }
                return (int)TakeDamageIndex.FrontLeft;
            }
            else if (direction.z < 0)
            {
                if (direction.x > 0)
                {
                    return (int)TakeDamageIndex.BackRight;
                }
                return (int)TakeDamageIndex.BackLeft;
            }

            return -1;


        }
    }
}
