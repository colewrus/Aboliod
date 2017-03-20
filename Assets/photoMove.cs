using UnityEngine;
using System.Collections;

public class photoMove : MonoBehaviour {

	public GameObject slot;
	public float distToSlot;
	float xtemp;
	float ytemp;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {


	}

	void OnMouseUp(){
		Debug.Log (Vector2.Distance(transform.localPosition,slot.transform.position));
		//If theDistance==1 between tiles then swap tiles
		if(Vector2.Distance(transform.localPosition,slot.transform.position) < distToSlot){
			
			xtemp = transform.localPosition.x;
			ytemp = transform.localPosition.y;
			transform.localPosition = new Vector3(slot.transform.position.x, slot.transform.position.y, 0);
			slot.transform.position = new Vector3(xtemp, ytemp, 0);

		}	

	}
}﻿