using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class EnemyManager : MonoBehaviour
{
    //public BehaviorTree e_BehaviorTree;
    [SerializeField] protected GameObject m_Enemy;

    void Start()
    {
        if(m_Enemy)
        {
            GlobalVariables.Instance.SetVariable("engagingEnemy", (SharedGameObject)m_Enemy);
        }
    }
}
