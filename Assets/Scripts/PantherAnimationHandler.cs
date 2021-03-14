using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantherAnimationHandler : MonoBehaviour {

	public GameObject spawnPoint;
	public int spawnCount;
	public float distance;

	void Start () {

		spawnCircle ();
	}

	public void spawnCircle () {

		Vector3 center = transform.position;


		for (int i = 0; i < spawnCount; i++) {

			Vector3 pos = RandomCircle(center, distance);
			Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center-pos);
			GameObject ob = Instantiate(spawnPoint, new Vector3(pos.x,Terrain.activeTerrain.SampleHeight(pos) + 1f,pos.z), rot);
			ob.tag = "surroundPoint";
			ob.name = "SP"+ i.ToString();
			ob.transform.parent = transform;
		}
	}

	Vector3 RandomCircle ( Vector3 center ,   float radius  ){
		
		float ang = Random.value * 360;
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.y = center.y;
		pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);

		return pos;
	}

	void startSwing () {

	}

	void endSwing () {

	}

    void startedSwing()
    {

    }

    void endedSwing()
    {

    }
}
