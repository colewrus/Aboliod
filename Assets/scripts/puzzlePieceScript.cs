﻿using UnityEngine;
using System.Collections;


	

public class puzzlePieceScript : MonoBehaviour {

	public GameObject outlineBKG;
	GameObject slot;
	float tempX;
	float tempY;
	float volume;
	Vector3 tv;
	bool moveActive;
	float clickTimer;
	bool clickBool;



	// Use this for initialization
	void Start () {
		moveActive = false;
		clickBool = false;
		clickTimer = 0;
		slot = null;
		slot = photoPuzzle.instance.slot;

		if (contant_Script.instance.sound_mute) {
			volume = 0;
		} else {
			volume = 0.5f;
		}
		Color outTMP = outlineBKG.GetComponent<SpriteRenderer> ().color;
		outTMP.a = 0;
		outlineBKG.GetComponent<SpriteRenderer> ().color = outTMP;
	}
	
	// Update is called once per frame
	void Update () {
		slot = photoPuzzle.instance.slot;
		if (clickTimer < 5) {
			clickTimer += 1 * Time.deltaTime;
		} else {
			clickBool = true;
		}
			

		if (moveActive) {
			transform.localPosition = Vector3.MoveTowards (transform.localPosition, tv, 1.0f);
			slot.transform.position = Vector3.MoveTowards (slot.transform.position, new Vector3 (tempX, tempY, -9), 1.0f);
			if (transform.localPosition == tv) {
				moveActive = false;
				photoPuzzle.instance.slot = null;	
			}
		}

		if (photoPuzzle.instance.slot == null) {
			Color outTMP = outlineBKG.GetComponent<SpriteRenderer> ().color;
			outTMP.a = 0;
			outlineBKG.GetComponent<SpriteRenderer> ().color = outTMP;
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
			outlineBKG.GetComponent<SpriteRenderer> ().sortingOrder = 0;
		}
	}

	void OnMouseUp(){

		if(clickBool && !photoPuzzle.instance.winBool){
			if (photoPuzzle.instance.slot == null) {
				photoPuzzle.instance.slot = gameObject;
				Color outTMP = outlineBKG.GetComponent<SpriteRenderer> ().color;
				outTMP.a = 1;
				outlineBKG.GetComponent<SpriteRenderer> ().color = outTMP;
				gameObject.GetComponent<SpriteRenderer> ().sortingOrder = 3;
				outlineBKG.GetComponent<SpriteRenderer> ().sortingOrder = 2;
			} else if (gameObject == photoPuzzle.instance.slot) {
				Color outTMP = outlineBKG.GetComponent<SpriteRenderer> ().color;
				outTMP.a = 0;
				outlineBKG.GetComponent<SpriteRenderer> ().color = outTMP;
				gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
				outlineBKG.GetComponent<SpriteRenderer> ().sortingOrder = 0;
				photoPuzzle.instance.slot = null;
			}else {
				if (Vector2.Distance (transform.position, slot.transform.position) < (1 + GetComponent<Renderer> ().bounds.size.x)) {			
					tempX = transform.localPosition.x;
					tempY = transform.localPosition.y;
					tv = new Vector3 (slot.transform.position.x, slot.transform.position.y, -9);
					moveActive = true;
					PlaySingle (photoPuzzle.instance.slideSound, volume, photoPuzzle.instance.soundSource);
				}
			}
			if (!photoPuzzle.instance.firstClick) {
				photoPuzzle.instance.firstClick = true;
			}
		}
	}


	void PlaySingle(AudioClip clip, float volume, AudioSource source){
		source.volume = volume;
		source.clip = clip;
		source.Play ();
	}
}
