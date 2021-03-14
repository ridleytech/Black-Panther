using UnityEngine;
using System.Collections;

public class RandomIdleAnimBehavior : StateMachineBehaviour {

	public string paramName = "idleAnimID";
	public int[] stateIDarray = {0,1,2,3,4,5,6};
	public int lastIndex;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
	//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called before OnStateExit is called on any state inside this state machine
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called before OnStateMove is called on any state inside this state machine
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called before OnStateIK is called on any state inside this state machine
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMachineEnter is called when entering a statemachine via its Entry Node
	override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash){

		if (stateIDarray.Length <= 0) {
			
			animator.SetInteger (paramName, 0);

		} else {

			int rand = Random.Range (0, stateIDarray.Length);

			//Debug.Log (this.name + ": " + rand);

			if(animator.GetInteger(paramName) == rand)
			{
				rand = pickAnother (rand);
				//Debug.Log ("pick another");
			}

			animator.SetInteger(paramName, stateIDarray[rand]);
		}
	}

	int pickAnother (int param) {

		int rand = Random.Range (0, stateIDarray.Length);

		//Debug.Log (this.name + ": " + rand);

		if(param == rand)
		{
			return pickAnother (param);
			//return null;
		}

		return rand;
	}

	// OnStateMachineExit is called when exiting a statemachine via its Exit Node
	override public void OnStateMachineExit(Animator animator, int stateMachinePathHash) {
	
		//Debug.Log ("OnStateMachineExit");
	}
}
