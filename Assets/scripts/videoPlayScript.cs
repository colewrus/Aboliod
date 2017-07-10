using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class videoPlayScript : MonoBehaviour {

    public GameObject test;
    public GameObject screen; //object to hold the movie texture

	// Use this for initialization
	void Start () {
        test.SetActive(false);
        Invoke("playRun", 0.5f);
    }




	
	void playRun(){
		print ("hello");
		if (contant_Script.instance.spanish) {
			Handheld.PlayFullScreenMovie ("video_SP.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
            //test.SetActive(true);
        } else if (!contant_Script.instance.spanish) {
			Handheld.PlayFullScreenMovie ("video_EN.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
           // test.SetActive(true);
        }

        SceneManager.LoadScene("MenuScreen");

        //yield return new WaitForEndOfFrame();

    }
    

}
