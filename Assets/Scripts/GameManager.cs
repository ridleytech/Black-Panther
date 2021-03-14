using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public bool rescued;
	public GameObject[] family;
	public GameObject[] currentEnemies;
    public bool ballShot;
    public int gameMode;
    public GameObject ball;
    public int playerPossession;
    public bool shotMissed;
	public bool missionStarted;
    public bool inPaint;
	public GameObject currentTarget;
	public AudioClip[] deathClips;
	public AudioClip[] hitClips;

    public int enemiesKilled;
    public GameObject[] enemyGroups;
    int enemyIndex;

    void Start () {

        gameMode = 1;
        playerPossession = 1;

        ball = GameObject.Find("ball");

        GameObject enemies = enemyGroups[1] as GameObject;
        enemies.SetActive(false);
    }

	public void playHitClip () {
	
		AudioSource audio = GetComponent<AudioSource>();

		int rand = Random.Range (0, hitClips.Length);

		//yield return new WaitForSeconds(audio.clip.length);

		if (!audio.isPlaying) {
			audio.clip = hitClips[rand];
			audio.Play();
		}
	
	}

	public void playYellClip () {

		AudioSource audio = GetComponent<AudioSource>();

		int rand = Random.Range (0, hitClips.Length);

		//yield return new WaitForSeconds(audio.clip.length);

		if (!audio.isPlaying) {
			audio.clip = hitClips[rand];
			audio.Play();
		}

	}


	public void playDeathClip () {

        enemiesKilled++;

        if (enemiesKilled == 2)
        {
            enemyIndex++;
            GameObject enemies = enemyGroups[enemyIndex] as GameObject;
            enemies.SetActive(true);
        }

        AudioSource audio = GetComponent<AudioSource>();

		int rand = Random.Range (0, deathClips.Length);

		//yield return new WaitForSeconds(audio.clip.length);

		if (!audio.isPlaying) {
			audio.clip = deathClips[rand];
			audio.Play();
		}

	}
}
