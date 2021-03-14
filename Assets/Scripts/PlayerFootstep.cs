using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstep : MonoBehaviour {
	/**
Script made by OMA [www.oma.netau.net] **/
	public CharacterController controller; 
	public AudioClip[] concrete ; 
	public AudioClip[] wood ; 
	public AudioClip[] dirt ; 
	public AudioClip[] metal ; 
	public AudioClip[] glass ;
	public AudioClip[] sand; 
	public AudioClip[] snow; 
	public AudioClip[] floor; 
	public AudioClip[] grass;
	public bool step; 
	float audioStepLengthWalk = 0.45f; 
	float audioStepLengthRun = 0.25f;
	AudioSource audio;

	void Start()
	{
		controller = GetComponent <CharacterController>();
		audio = GetComponent <AudioSource>();
        step = true;

    }

	void OnControllerColliderHit ( ControllerColliderHit hit) {

		//print ("mag: " + controller.velocity.magnitude);
		
		if (controller.isGrounded && controller.velocity.magnitude < 7 && controller.velocity.magnitude > 5 && hit.gameObject.tag == "Untagged" && step == true ) {
			WalkOnConcrete();
		} else if (controller.isGrounded && controller.velocity.magnitude > 8 && hit.gameObject.tag == "Concrete" && step == true || controller.isGrounded && controller.velocity.magnitude > 8 && hit.gameObject.tag == "Untagged" && step == true) {
			RunOnConcrete();
		} else if (controller.isGrounded && controller.velocity.magnitude < 7 && controller.velocity.magnitude > 5 && hit.gameObject.tag == "Wood" && step == true) {
			WalkOnWood();
		} else if (controller.isGrounded && controller.velocity.magnitude > 8 && hit.gameObject.tag == "Wood" && step == true) {
			RunOnWood();
		} else if (controller.isGrounded && controller.velocity.magnitude < 7 && controller.velocity.magnitude > 5 && hit.gameObject.tag == "Dirt" && step == true) {
			WalkOnDirt();
		} else if (controller.isGrounded && controller.velocity.magnitude > 8 && hit.gameObject.tag == "Dirt" && step == true) {
			RunOnDirt();
		} else if (controller.isGrounded && controller.velocity.magnitude < 7 && controller.velocity.magnitude > 5 && hit.gameObject.tag == "Metal" && step == true) {
			WalkOnMetal();
		} else if (controller.isGrounded && controller.velocity.magnitude > 8 && hit.gameObject.tag == "Metal" && step == true) {
			RunOnMetal();        
		} else if (controller.isGrounded && controller.velocity.magnitude < 7 && controller.velocity.magnitude > 5 && hit.gameObject.tag == "Glass" && step == true) {
			WalkOnGlass();
		} else if (controller.isGrounded && controller.velocity.magnitude > 8 && hit.gameObject.tag == "Glass" && step == true) {
			RunOnGlass();
		} else if (controller.isGrounded && controller.velocity.magnitude < 7 && controller.velocity.magnitude > 5 && hit.gameObject.tag == "Sand" && step == true) {
			WalkOnSand();
		} else if (controller.isGrounded && controller.velocity.magnitude > 8 && hit.gameObject.tag == "Sand" && step == true) {
			RunOnSand();            
		} else if (controller.isGrounded && controller.velocity.magnitude < 7 && controller.velocity.magnitude > 5 && hit.gameObject.tag == "Snow" && step == true) {
			WalkOnSnow();
		} else if (controller.isGrounded && controller.velocity.magnitude > 8 && hit.gameObject.tag == "Snow" && step == true) {
			RunOnSnow();
		} else if (controller.isGrounded && controller.velocity.magnitude < 7 && controller.velocity.magnitude > 5 && hit.gameObject.tag == "Floor" && step == true) {
			WalkOnFloor();
		} else if (controller.isGrounded && controller.velocity.magnitude > 8 && hit.gameObject.tag == "Floor" && step == true) {
			RunOnFloor();    
		} else if (controller.isGrounded && controller.velocity.magnitude < 3 && controller.velocity.magnitude > .5 && (hit.gameObject.tag == "Grass" || hit.gameObject.tag == "Terrain") && step == true) {

			print ("walk");
			WalkOnGrass();
		} else if (controller.isGrounded && controller.velocity.magnitude > 8 && (hit.gameObject.tag == "Grass" || hit.gameObject.tag == "Terrain") && step == true) {

			print ("run");
			RunOnGrass();                    

		}        
	}


	IEnumerator WaitForFootSteps(float stepsLength) { step = false; yield return new WaitForSeconds(stepsLength); step = true; }

	void WalkOnConcrete() {
	audio.clip = concrete[Random.Range(0, concrete.Length)];
	audio.volume = 0.1f;
	audio.Play();
	StartCoroutine(WaitForFootSteps(audioStepLengthWalk));

}
void RunOnConcrete() {
	audio.clip = concrete[Random.Range(0, concrete.Length)];
	audio.volume = 0.3f;
	audio.Play();
	StartCoroutine(WaitForFootSteps(audioStepLengthWalk));

}
void WalkOnWood() {
audio.clip = wood[Random.Range(0, wood.Length)];
audio.volume = 0.1f;
audio.Play();
StartCoroutine(WaitForFootSteps(audioStepLengthWalk));

}
void RunOnWood() {
	audio.clip = wood[Random.Range(0, wood.Length)];
	audio.volume = 0.3f;
	audio.Play();
	StartCoroutine(WaitForFootSteps(audioStepLengthRun));

}
void WalkOnDirt() {
audio.clip = dirt[Random.Range(0, dirt.Length)];
audio.volume = 0.1f;
audio.Play();
StartCoroutine(WaitForFootSteps(audioStepLengthWalk));

}
void RunOnDirt() {
	audio.clip = dirt[Random.Range(0, dirt.Length)];
	audio.volume = 0.3f;
	audio.Play();
	StartCoroutine(WaitForFootSteps(audioStepLengthRun));

}
void WalkOnMetal() {
audio.clip = metal[Random.Range(0, metal.Length)];
audio.volume = 0.1f;
audio.Play();
StartCoroutine(WaitForFootSteps(audioStepLengthWalk));

}
void RunOnMetal() {
	audio.clip = metal[Random.Range(0, metal.Length)];
	audio.volume = 0.3f;
	audio.Play();
	StartCoroutine(WaitForFootSteps(audioStepLengthRun));

}
 void WalkOnGlass() {
audio.clip = glass[Random.Range(0, glass.Length)];
audio.volume = 0.1f;
audio.Play();
StartCoroutine(WaitForFootSteps(audioStepLengthWalk));

}
void RunOnGlass() {
	audio.clip = glass[Random.Range(0, glass.Length)];
	audio.volume = 0.3f;
	audio.Play();
	StartCoroutine(WaitForFootSteps(audioStepLengthRun));

}
void WalkOnSand() {
audio.clip = sand[Random.Range(0, sand.Length)];
audio.volume = 0.1f;
audio.Play();
StartCoroutine(WaitForFootSteps(audioStepLengthWalk));

}
void RunOnSand() {
	audio.clip = sand[Random.Range(0, sand.Length)];
	audio.volume = 0.3f;
	audio.Play();
	StartCoroutine(WaitForFootSteps(audioStepLengthRun));

}
void WalkOnSnow() {
audio.clip = snow[Random.Range(0, snow.Length)];
audio.volume = 0.1f;
audio.Play();
StartCoroutine(WaitForFootSteps(audioStepLengthWalk));

}
void RunOnSnow() {
	audio.clip = snow[Random.Range(0, snow.Length)];
	audio.volume = 0.3f;
	audio.Play();
	StartCoroutine(WaitForFootSteps(audioStepLengthRun));

}
void WalkOnFloor() {
audio.clip = floor[Random.Range(0, floor.Length)];
audio.volume = 0.1f;
audio.Play();
StartCoroutine(WaitForFootSteps(audioStepLengthWalk));

}
void RunOnFloor() {
	audio.clip = floor[Random.Range(0, floor.Length)];
	audio.volume = 0.3f;
	audio.Play();
	StartCoroutine(WaitForFootSteps(audioStepLengthRun));

}

void WalkOnGrass() {
audio.clip = grass[Random.Range(0, grass.Length)];
audio.volume = 0.8f;
audio.Play();
StartCoroutine(WaitForFootSteps(audioStepLengthWalk));

}
void RunOnGrass() {
	audio.clip = grass[Random.Range(0, grass.Length)];
	audio.volume = 1f;
	audio.Play();
	StartCoroutine(WaitForFootSteps(audioStepLengthRun));

}
}
