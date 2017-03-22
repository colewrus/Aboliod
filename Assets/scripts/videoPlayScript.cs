using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class videoPlayScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
		StartCoroutine (playRun ());

	}



	IEnumerator playRun(){		

		if (contant_Script.instance.spanish) {
			Handheld.PlayFullScreenMovie ("video_SP.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);

		} else if (!contant_Script.instance.spanish) {
			Handheld.PlayFullScreenMovie ("video_EN.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);

		}


		yield return new WaitForEndOfFrame();

		SceneManager.LoadScene ("MenuScreen");

	}

}
