using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Starfighter Game Template/Damage Control")]
public class DamageControl : MonoBehaviour {

	public GameObject Controller;
	public string team;

	void Awake () {
		if (Controller.GetComponent<PlayerSpaceship> () != null)
			Controller.GetComponent<PlayerSpaceship> ().myTeam = team;
	}

	public void DealDamage (float d, bool p) {
		if (Controller.GetComponent<PlayerSpaceship> () != null)
			Controller.GetComponent<PlayerSpaceship> ().ModifyHealth (d);
		
		if (Controller.GetComponent<AsteroidScript> () != null)
			Controller.GetComponent<AsteroidScript> ().ModifyHealth (d);
		
		if (Controller.GetComponent<StarshipAI> () != null)
			Controller.GetComponent<StarshipAI> ().ModifyHealth (d, p);
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.GetComponent<DamageControl> () == null) {
			if (Controller.GetComponent<PlayerSpaceship> () != null)
				Controller.GetComponent<PlayerSpaceship> ().ModifyHealth (Controller.GetComponent<PlayerSpaceship> ().maxHealth+Controller.GetComponent<PlayerSpaceship> ().maxShield);

			if (Controller.GetComponent<StarshipAI> () != null)
				Controller.GetComponent<StarshipAI> ().ModifyHealth (Controller.GetComponent<StarshipAI> ().maxHealth+Controller.GetComponent<StarshipAI> ().maxShield, false);
		}
	}

}
