using UnityEngine;
using System.Collections;

public class persistentVariables : MonoBehaviour {

	public bool directPhysics;


	// Use this for initialization
	void Awake(){
		DontDestroyOnLoad (transform.gameObject);
	}



	public void togglePhysics(){
		if (directPhysics == true) {
			directPhysics = false;
		} else {
			directPhysics = true;
		}
	}


}
