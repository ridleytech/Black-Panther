using UnityEngine;
using System.Collections;

public class EnemyNav : MonoBehaviour {

	public Transform destination;
	public UnityEngine.AI.NavMeshAgent agent;

	// Use this for initialization
	void Start () {
	
		//agent.speed = .5f;
		agent.stoppingDistance = 1.0f;
		agent.SetDestination(destination.position);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
