using UnityEngine;
using System.Collections;

public class desktopVideo_script : MonoBehaviour {

	public MovieTexture english;
	public MovieTexture spanish;


	// Use this for initialization
	void Start () {
		if (contant_Script.instance.spanish) {
			GetComponent<Renderer> ().material.mainTexture = spanish;
			GetComponent<AudioSource> ().clip = spanish.audioClip;
			english.Play ();
		} else if (!contant_Script.instance.spanish) {
			GetComponent<Renderer> ().material.mainTexture = english;
			GetComponent<AudioSource> ().clip = english.audioClip;
			spanish.Play ();
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
