using UnityEngine;
using System.Collections;


	

public class puzzlePieceScript : MonoBehaviour {

	public GameObject outlineBKG;
	public GameObject slot;
	public float tempX;
	public float tempY; //this object's position to feed movement target for the slotted slide
	float volume;
	Vector3 tv;
	public bool moveActive;
	float clickTimer;


    public bool setMovement;

	// Use this for initialization
	void Start () {
		moveActive = false;
		clickTimer = 0;
		slot = null;

        setMovement = false;
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
	void FixedUpdate () {
		slot = photoPuzzle.instance.slot;

		if (moveActive) {
			
			transform.localPosition = Vector3.MoveTowards (transform.localPosition, tv, 1.0f);
			slot.transform.position = Vector3.MoveTowards (slot.transform.position, new Vector3 (tempX, tempY, -9), 1.0f);
            Debug.Log(tv + " : " + transform.localPosition);
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

    public Vector3 SetTarget(GameObject slotObj)
    {
        return tv = new Vector3(slot.transform.position.x, slot.transform.position.y, -9);
    }


    public void MovePiece(GameObject slotObj)
    {
        if (setMovement)
        {
            tv = new Vector3(slot.transform.position.x, slot.transform.position.y, -9);
            tempX = transform.localPosition.x;
            tempY = transform.localPosition.y;
            moveActive = true;
            Debug.Log("move function");
            setMovement = false;
        } 
  
    }

	void OnMouseUp(){

		if(photoPuzzle.instance.clickBool && !photoPuzzle.instance.winBool){
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
