using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MackandalAnimationManager : MonoBehaviour {

	GameManager gm;
	public bool isPunchSound;
	public bool isKickSound;
	public bool isSweepSound;
	public bool isSwordSound;
	public AudioSource audio;
	EnemyHealth eh;

	void Start () {

		gm = GameObject.Find ("GameController").GetComponent<GameManager>();
		audio = GameObject.Find ("pantherSword").GetComponent<AudioSource>();
		eh = GetComponent<EnemyHealth>();
	}
	
	void startSwing () {
	}

	void endSwing () {
	}

	void SwordAndShieldSlashStart () {
	}

	void SwordAndShieldSlashEnd () {
	}

	void playAttackSound () {
	}

	void playHitSound () {

		print ("playHitSound");

		audio.GetComponent<AudioSource>();

		int rand = Random.Range (0, gm.hitClips.Length);

		//yield return new WaitForSeconds(audio.clip.length);

		if (!audio.isPlaying) {
			
			audio.clip = gm.hitClips[rand];
			audio.Play();
		}

        //StartCoroutine(GamePauser());
        StartCoroutine(GamePauser1(0.07f));

        //		if (eh.health <= 0) {
        //		
        //			StartCoroutine(RunSpawnerIteration(1.25f));
        //		}

        //		Animator anim = GetComponent<Animator> ();
        //		anim.SetTrigger ("takeDamage");
    }

    public IEnumerator GamePauser()
    {
        //Debug.Log("Inside PauseGame()");
        yield return new WaitForSeconds(1);
        //Debug.Log("Done with my pause");
    }

    public IEnumerator GamePauser1(float pauseTime)
    {
        //Debug.Log("Inside PauseGame()");
        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + pauseTime;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1f;
        //Debug.Log("Done with my pause");
        //PauseEnded();
    }

    private IEnumerator RunSpawnerIteration(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);

		AudioSource audio = GetComponent<AudioSource>();

		int rand = Random.Range (0, gm.deathClips.Length);

		audio.clip = gm.deathClips[rand];
		audio.Play();
	}

	void finishedDuck () {

		if (gameObject.GetComponent<Emerald_AI> () != null) {

			//Emerald Damage from UFPS Randall

			Emerald_AI EmeraldComponent = GetComponent<Collider>().gameObject.GetComponent<Emerald_AI> ();
			EmeraldComponent.enabled = true;
		}
	}
}
