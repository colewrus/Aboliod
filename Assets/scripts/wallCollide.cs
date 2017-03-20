using UnityEngine;
using System.Collections;

public class wallCollide : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}


	void OnCollisionEnter2D(Collision2D other){


		ContactPoint2D hit = other.contacts [0];
		Vector2 direction = Vector2.Reflect (other.rigidbody.velocity, hit.normal);
		if (direction.y < 40) {
			direction.y = 135;
		}
		other.gameObject.GetComponent<Rigidbody2D> ().AddForce (direction);
	}
}
