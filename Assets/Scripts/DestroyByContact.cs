using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	//public int hp;
	//private int damage;
	void OnTriggerEnter2D(Collider2D other) {

		//if (other.tag == "Boundary" || other.tag == "MainCamera" || other.tag == "Player") {
			//return;
		//}
		//damage++;

		Destroy (other.gameObject);
		//if (damage == hp) {
			//Destroy (gameObject);
		//}
	}
}
