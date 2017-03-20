using UnityEngine;
using System.Collections;

public class particleDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Destroy(gameObject, 2.0f);
	}
}
