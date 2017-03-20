using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class contant_Script : MonoBehaviour {

	public bool music_mute;
	public bool sound_mute;
	public bool spanish;


	public static contant_Script instance = null;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad (gameObject);

		music_mute = false;
		sound_mute = false;
		spanish = true;
	}

	public void Music_Func(){
		if (!music_mute) {
			music_mute = true;
		} else {
			music_mute = false;
		}
	}

	public void Sounds_Func(){
		if (!sound_mute) {
			sound_mute = true;
		} else {
			sound_mute = false;
		}		
	}


	public void Spanish_Toggle(){
		if (!spanish) {
			spanish = true;
		} else {
			spanish = false;
		}
	}
}
