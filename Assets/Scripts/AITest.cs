using UnityEngine;
using System.Collections;

public class AITest : MonoBehaviour {

	public GameObject player;
	public UnityEngine.AI.NavMeshAgent agent;

	// Use this for initialization
	void Start () {
	
		agent.destination = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
