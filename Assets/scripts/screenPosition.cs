using UnityEngine;
using System.Collections;

public class screenPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.X)) {
			print(Camera.main.ScreenToWorldPoint(new Vector3(1, 1, 5)));
		}
		if (Input.GetKeyDown (KeyCode.A)) { //left side
			transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height / 2, 5));
			print(Screen.width + " " + Screen.height);		
		}
		if (Input.GetKeyDown (KeyCode.B)) {//right side
			transform.position = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height/2, 5));
			print(Screen.width + " " + Screen.height);		
		}
		if (Input.GetKeyDown (KeyCode.C)) {//top
			transform.position = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width/2, Screen.height, 5));
			print(Screen.width + " " + Screen.height);		
		}		
		if (Input.GetKeyDown (KeyCode.D)) {
			transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width / 2, Screen.height / 5, 5));
			print(Screen.width + " " + Screen.height);	
		}
	}
}
