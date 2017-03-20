using UnityEngine;
using System.Collections;

public class finalBrickScript : MonoBehaviour {
	float timer;
	public GameObject brickParticle;
	// Use this for initialization
	void Start () {
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (GM.instance.bricks == 1) {
			timer += 1 * Time.deltaTime;
		}
		if (timer > 1.5f) {
			final_Brick_Break ();
		}
	}

	void final_Brick_Break(){
		Instantiate(brickParticle, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
