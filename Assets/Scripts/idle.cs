using UnityEngine;
using System.Collections;

public class idle : MonoBehaviour {

	float timeForIdle = 5f;
	float idleTimer;
	bool playIdle;
	bool noInteractions;

	void Start()
	{
		idleTimer = timeForIdle;
	}

	void Update()
	{
		if(noInteractions && !playIdle)
		{
			// Frame rate dependent
			idleTimer -= Time.deltaTime;

			if(idleTimer <= 0)
			{
				idleTimer = 0;
				playIdle = true;
				PlayIdleAnimations();
			}
		}
		else if(!noInteractions)
		{
			idleTimer = timeForIdle;
			playIdle = false;
		}
	}

	void PlayIdleAnimations()
	{
//		if(!playIdle)
//			return;
//
//		animation.CrossFade("idle1");
//
//		yield return WaitForSeconds(animation["idle1"].time);
//
//		animation.CrossFade("idle2");
//
//		yield return WaitForSeconds(animation["idle2"].time);
//
//		animation.CrossFade("idle3");
//
//		yield return WaitForSeconds(animation["idle3"].time);
	}
}
