using UnityEngine;
using System.Collections;

public class EnemyLocator : MonoBehaviour {

	public GameObject[] enemyList;
	public GameObject[] stationList;
	public Camera fpc;
	public Camera mapCamera;
	public Texture yTexture;
	public Texture wTexture;
	public Texture gTexture;
	GameManager gameManager;
	GameObject player;

	void Start () {
	
		//fpc = GameObject.Find ("FirstPersonCamera").GetComponent<Camera> ();
		//mapCamera = GameObject.Find ("MapCam").GetComponent<Camera> ();
		//enemyList = GameObject.FindGameObjectsWithTag ("Zombie");
		//stationList = GameObject.FindGameObjectsWithTag ("Station");
		gameManager = GameObject.Find ("GameController").GetComponent<GameManager>();
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnGUI() {
		//		if (gameManager.packageList.Length > 0) 
//		{
//			//packageIndicatorMap ();
//			packageIndicators ();
//		}
//
//		myIndicator ();



		if (gameManager.currentEnemies.Length > 0) 
		{
			enemyIndicators ();
		}

//		//enemyIndicatorMap ();
//
//		if (gameManager.enemiesInCustody > 0) 
//		{
//			stationIndicators ();
//		}
	}

	void stationIndicators () {

		GUIStyle style = new GUIStyle();
		style.fontSize = 10;
		style.normal.textColor = Color.blue;
		style.fontStyle = FontStyle.Bold;

		for (int i = 0; i < stationList.Length; i++) {

			GameObject enemy = (GameObject)stationList[i];

			Vector3 playerCoords = enemy.transform.position;
			Vector3 screenCords;
			Vector3 worldToViewPos;

			screenCords = fpc.WorldToScreenPoint (playerCoords);
			worldToViewPos = fpc.WorldToViewportPoint (playerCoords);

			//print ("worldToViewPos.y: " + worldToViewPos.y);
			//print ("screenCords.y: " + screenCords.y);

			float offset = screenCords.y - Screen.height/2;

			float distance = Vector3.Distance(enemy.transform.position,transform.position);

			distance = Mathf.Round(distance);
			string name;

			name = enemy.name + "\nDistance:" + distance.ToString();

			float width = 100;
			float height = 40;

			if (worldToViewPos.z > 0) {

				if(worldToViewPos.y >= 1)
				{
					GUI.Label (new Rect (screenCords.x, 20, width, height), name, style);
				}
				else if(worldToViewPos.y <= 0)
				{
					GUI.Label (new Rect (screenCords.x, Screen.height - 20, width, height), name, style);
				}
				else
				{
					GUI.Label (new Rect (screenCords.x, screenCords.y - (offset * 2), width, height), name, style);
				}
			}
		}
	}

	void myIndicator () {

		GUIStyle style = new GUIStyle();
		style.fontSize = 12;
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = Color.white;

		Vector3 playerCoords = transform.position;
		Vector3 screenCords;
		Vector3 worldToViewPos;

		screenCords = mapCamera.WorldToScreenPoint (playerCoords);
		worldToViewPos = mapCamera.WorldToViewportPoint (playerCoords);

		float offset = screenCords.y - Screen.height/2;

		string name = "Player";
		float width = 6;
		float height = 6;

//		if(worldToViewPos.y >= 1)
//		{
//			GUI.Label (new Rect (screenCords.x, 20, width, height), name, style);
//		}
//		else if(worldToViewPos.y <= 0)
//		{
//			GUI.Label (new Rect (screenCords.x, Screen.height - 20, width, height), name, style);
//		}
//		else
//		{
//			GUI.Label (new Rect (screenCords.x, screenCords.y - (offset * 2), width, height), name, style);
//		}

		if (worldToViewPos.z > 0) {

			if(worldToViewPos.y >= 1)
			{
				GUI.DrawTexture(new Rect(screenCords.x, 20, width, height), wTexture);
			}
			else if(worldToViewPos.y <= 0)
			{
				GUI.DrawTexture(new Rect(screenCords.x, Screen.height - 20, width, height), wTexture);
			}
			else
			{
				GUI.DrawTexture(new Rect(screenCords.x, screenCords.y - (offset * 2), width, height), wTexture);
			}
		}
	}

	void enemyIndicators () {

		GUIStyle style = new GUIStyle();
		style.fontSize = 10;
		style.normal.textColor = Color.yellow;
		style.fontStyle = FontStyle.Bold;

		for (int i = 0; i < gameManager.currentEnemies.Length; i++) {

			GameObject enemy = (GameObject)gameManager.currentEnemies[i];

			if(enemy)
			{
				EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth> ();

				if(enemyHealth.health > 0)
				{
					//DoneEnemySight es = enemy.GetComponent<DoneEnemySight>();
					//EnemyHealth eh = player.GetComponentInChildren<EnemyHealth>();

					Vector3 playerCoords = enemy.transform.position;
					Vector3 screenCords;
					Vector3 worldToViewPos;

					//				if(!mapCamera.enabled)
					//				{
					//					screenCords = fpc.WorldToScreenPoint (playerCoords);
					//					worldToViewPos = fpc.WorldToViewportPoint (playerCoords);
					//				}
					//				else
					//				{
					//					screenCords = mapCamera.WorldToScreenPoint (playerCoords);
					//					worldToViewPos = mapCamera.WorldToViewportPoint (playerCoords);
					//				}

					screenCords = mapCamera.WorldToScreenPoint (playerCoords);
					worldToViewPos = mapCamera.WorldToViewportPoint (playerCoords);

					//print ("worldToViewPos.y: " + worldToViewPos.y);
					//print ("screenCords.y: " + screenCords.y);

					float offset = screenCords.y - Screen.height/2;

					//print ("screenCords.y: " + screenCords.y + " offset: " + offset);

					//			if (worldToViewPos.z < 0) 
					//			{
					//				return; //print ("Object is behind the camera: " + worldToViewPos);
					//			}

					float distance = Vector3.Distance(enemy.transform.position,player.transform.position);

					distance = Mathf.Round(distance);

					string name;

		//			if(!mapCamera.enabled)
		//			{
		//				name = enemy.name;
		//			}
		//			else
		//			{
		//				name = enemy.name + "\nDistance:" + distance.ToString();
		//			}

					if(distance > 50)
					{
						name = "Enemy" + "\nDistance:" + distance.ToString();
					}
					else
					{
						name = "Enemy";
					}



					float width = 100;
					float height = 40;

					if (worldToViewPos.z > 0) {

						if(worldToViewPos.y >= 1)
						{
							GUI.Label (new Rect (screenCords.x, 20, width, height), name, style);
						}
						else if(worldToViewPos.y <= 0)
						{
							GUI.Label (new Rect (screenCords.x, Screen.height - 20, width, height), name, style);
						}
						else
						{
							GUI.Label (new Rect (screenCords.x, screenCords.y - (offset * 2), width, height), name, style);
						}
					}
				}
			}
		}
	}

//	void humanIndicatorMap () {
//
//		GUIStyle style = new GUIStyle();
//		style.fontSize = 10;
//		style.normal.textColor = Color.yellow;
//		style.fontStyle = FontStyle.Bold;
//
//		for (int i = 0; i < gameManager.humanList.Length; i++) {
//
//			GameObject human = (GameObject)gameManager.humanList[i];
//
//			if(human && human.name != "zombie")
//			{
//				Vector3 playerCoords = human.transform.position;
//				Vector3 screenCords;
//				Vector3 worldToViewPos;
//
//				screenCords = mapCamera.WorldToScreenPoint (playerCoords);
//				worldToViewPos = mapCamera.WorldToViewportPoint (playerCoords);
//
//				//print ("worldToViewPos.y: " + worldToViewPos.y);
//				//print ("screenCords.y: " + screenCords.y);
//
//				float offset = screenCords.y - Screen.height/2;
//
////				float distance = Vector3.Distance(enemy.transform.position,transform.position);
////
////				distance = Mathf.Round(distance);
//
//
//				float width = 3;
//				float height = 3;
//
//				if (worldToViewPos.z > 0) {
//
//					if(worldToViewPos.y >= 1)
//					{
//						//GUI.Label (new Rect (screenCords.x, 20, width, height), name, style);
//						GUI.DrawTexture(new Rect(screenCords.x, 20, width, height), wTexture);
//					}
//					else if(worldToViewPos.y <= 0)
//					{
//						//GUI.Label (new Rect (screenCords.x, Screen.height - 20, width, height), name, style);
//						GUI.DrawTexture(new Rect(screenCords.x, Screen.height - 20, width, height), wTexture);
//					}
//					else
//					{
//						//GUI.Label (new Rect (screenCords.x, screenCords.y - (offset * 2), width, height), name, style);
//						GUI.DrawTexture(new Rect(screenCords.x, screenCords.y - (offset * 2), width, height), wTexture);
//					}
//				}
//			}
//		}
//	}

//	void enemyIndicatorMap () {
//
//		GUIStyle style = new GUIStyle();
//		style.fontSize = 10;
//		style.normal.textColor = Color.yellow;
//		style.fontStyle = FontStyle.Bold;
//
//		for (int i = 0; i < gameManager.enemyList.Length; i++) {
//
//			GameObject enemy = (GameObject)gameManager.enemyList[i];
//
//			if(enemy && enemy.name != "zombie")
//			{
//				Vector3 playerCoords = enemy.transform.position;
//				Vector3 screenCords;
//				Vector3 worldToViewPos;
//
//				screenCords = mapCamera.WorldToScreenPoint (playerCoords);
//				worldToViewPos = mapCamera.WorldToViewportPoint (playerCoords);
//
//				//print ("worldToViewPos.y: " + worldToViewPos.y);
//				//print ("screenCords.y: " + screenCords.y);
//
//				float offset = screenCords.y - Screen.height/2;
//
//				float distance = Vector3.Distance(enemy.transform.position,transform.position);
//
//				distance = Mathf.Round(distance);
//
//
//				float width = 5;
//				float height = 5;
//
//				if (worldToViewPos.z > 0) {
//
//					if(worldToViewPos.y >= 1)
//					{
//						//GUI.Label (new Rect (screenCords.x, 20, width, height), name, style);
//						GUI.DrawTexture(new Rect(screenCords.x, 20, width, height), yTexture);
//					}
//					else if(worldToViewPos.y <= 0)
//					{
//						//GUI.Label (new Rect (screenCords.x, Screen.height - 20, width, height), name, style);
//						GUI.DrawTexture(new Rect(screenCords.x, Screen.height - 20, width, height), yTexture);
//					}
//					else
//					{
//						//GUI.Label (new Rect (screenCords.x, screenCords.y - (offset * 2), width, height), name, style);
//						GUI.DrawTexture(new Rect(screenCords.x, screenCords.y - (offset * 2), width, height), yTexture);
//					}
//				}
//			}
//		}
//	}

//	void packageIndicators () {
//
//		GUIStyle style = new GUIStyle();
//		style.fontSize = 10;
//		style.normal.textColor = Color.green;
//		style.fontStyle = FontStyle.Bold;
//
//		for (int i = 0; i < gameManager.packageList.Length; i++) {
//
//			GameObject enemy = (GameObject)gameManager.packageList[i];
//
//			Vector3 playerCoords = enemy.transform.position;
//			Vector3 screenCords;
//			Vector3 worldToViewPos;
//
//			screenCords = fpc.WorldToScreenPoint (playerCoords);
//			worldToViewPos = fpc.WorldToViewportPoint (playerCoords);
//
//			//print ("worldToViewPos.y: " + worldToViewPos.y);
//			//print ("screenCords.y: " + screenCords.y);
//
//			float offset = screenCords.y - Screen.height/2;
//
//			float distance = Vector3.Distance(enemy.transform.position,transform.position);
//
//			distance = Mathf.Round(distance);
//			string name;
//
//			name = enemy.name + "\nDistance:" + distance.ToString();
//
//			float width = 100;
//			float height = 40;
//
//			if (worldToViewPos.z > 0) {
//
//				if(worldToViewPos.y >= 1)
//				{
//					GUI.Label (new Rect (screenCords.x, 20, width, height), name, style);
//				}
//				else if(worldToViewPos.y <= 0)
//				{
//					GUI.Label (new Rect (screenCords.x, Screen.height - 20, width, height), name, style);
//				}
//				else
//				{
//					GUI.Label (new Rect (screenCords.x, screenCords.y - (offset * 2), width, height), name, style);
//				}
//			}
//		}
//	}

//	void packageIndicatorMap () {
//
//		GUIStyle style = new GUIStyle();
//		style.fontSize = 10;
//		style.normal.textColor = Color.green;
//		style.fontStyle = FontStyle.Bold;
//
//		for (int i = 0; i < gameManager.packageList.Length; i++) {
//
//			GameObject package = (GameObject)gameManager.enemyList[i];
//
//			Vector3 playerCoords = package.transform.position;
//			Vector3 screenCords;
//			Vector3 worldToViewPos;
//
//			screenCords = mapCamera.WorldToScreenPoint (playerCoords);
//			worldToViewPos = mapCamera.WorldToViewportPoint (playerCoords);
//
//			float width = 5;
//			float height = 5;
//
//			float offset = screenCords.y - Screen.height/2;
//
//			if (worldToViewPos.z > 0) {
//
//				if(worldToViewPos.y >= 1)
//				{
//					//GUI.Label (new Rect (screenCords.x, 20, width, height), name, style);
//					GUI.DrawTexture(new Rect(screenCords.x, 20, width, height), gTexture);
//				}
//				else if(worldToViewPos.y <= 0)
//				{
//					//GUI.Label (new Rect (screenCords.x, Screen.height - 20, width, height), name, style);
//					GUI.DrawTexture(new Rect(screenCords.x, Screen.height - 20, width, height), gTexture);
//				}
//				else
//				{
//					//GUI.Label (new Rect (screenCords.x, screenCords.y - (offset * 2), width, height), name, style);
//					GUI.DrawTexture(new Rect(screenCords.x, screenCords.y - (offset * 2), width, height), gTexture);
//				}
//			}
//		}
//	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
