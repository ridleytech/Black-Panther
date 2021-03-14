using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[AddComponentMenu("Starfighter Game Template/Space Scene Manager")]
public class SpaceSceneManager : MonoBehaviour {

	public PlayerSpaceship player;
	[Tooltip("Camera of the space scene")]
	public Camera spaceCam;
	public GameObject hyperSpace;
	public bool startSceneWithHyperspace;
	public float hyperspaceDurationSecs;
	public Image fader;
	Color tCol;
	[Tooltip("An array of the GameObjects that will be activated when hyperspace effect ends")]
	public GameObject[] activateOnStart;

	void Awake() {
		Cursor.lockState = CursorLockMode.Locked;
		if (!startSceneWithHyperspace)
			ActivateObjects ();
		else {
			tCol = fader.color;
			tCol.a = 1;
			hyperSpace.SetActive (true);
			StartCoroutine (HyperspaceTimer());
		}
	}

	IEnumerator HyperspaceTimer() {
		yield return new WaitForSeconds (hyperspaceDurationSecs);
		hyperSpace.SetActive (false);
		tCol.a = 0.5f;
		ActivateObjects ();
	}

	void ActivateObjects() {
		player.enabled = true;
		Cursor.lockState = CursorLockMode.None;
		for (int i = 0; i < activateOnStart.Length; i++) {
			activateOnStart [i].SetActive (true);
		}
	}

	public void OpenScene(int s) {
		Time.timeScale = 1;
		SceneManager.LoadScene (s);
	}

	public void QuitGame() {
		Application.Quit();
	}

	void Update () {
		if (fader != null) {
			if (tCol.a > 0)
				tCol.a -= 0.02f;
			fader.color = tCol;
		}
		spaceCam.fieldOfView = player.cam.fieldOfView;
		spaceCam.transform.rotation = player.cam.transform.rotation;
	}
}
