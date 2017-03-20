using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class animatedPepe : MonoBehaviour {

	float travelTime;

	float rate;
	Vector3 startPos;
	public Vector3 endPos;
	//Vector3 jumpStart;
	//Vector3 jumpEnd;
	float index;
	public bool jumpBool;
	public GameObject pepeSprite;
	public float timer;
	// Use this for initialization
	int life_b;
	//IEnumerator startIdle;


	void Start () {
		endPos = GM.instance.pepeEnd;
		travelTime = GM.instance.walkTimer;
		rate = 1 / travelTime;
		startPos = transform.position;
		index = 0;
		timer = 0;
		//pepeSprite.GetComponent<Animator> ().Stop();
		jumpBool = false;

		transform.localScale = new Vector3 (1, 1, 1);
		life_b = 0;
	}

	// Update is called once per frame
	void Update () {
		
		if (transform.position != endPos && paddle.instance.launchBool) {
			transform.position = Vector3.Lerp (startPos, endPos, index);
			index += rate * Time.deltaTime;
			pepeSprite.GetComponent<Animator> ().Play ("walkAnim");
			life_b = 0;

		} else {			
			Idle_Func ();
		}

		if (transform.position == endPos) {
			Life_Reset ();
		} 


	}

	void Idle_Func(){
		pepeSprite.GetComponent<Animator> ().Play ("idle");
		timer += 1 * Time.deltaTime;
	}

	void Life_Reset(){
		if (life_b == 0) {
			travelTime = GM.instance.walkTimer;
			rate = 1 / travelTime;
			index = 0;
			timer = 0;
			transform.position = startPos;
			GM.instance.LoseLife ();

			life_b += 1;
		}
	}

	IEnumerator Idling(){		
		yield return new WaitForSeconds (5);
		pepeSprite.GetComponent<Animator> ().Play ("idle");
	}
}
