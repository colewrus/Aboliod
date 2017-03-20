using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Panel_Text_Control : MonoBehaviour {

	public Text textVar;
	List<string> Instructions_List;


	public string instructions_1_SP;
	public string instructions_2_SP;
	public string instructions_3_SP;

	public string instructions_1_EN;
	public string instructions_2_EN;
	public string instructions_3_EN;
	public bool changeBool;
	public static bool launch_release;
	int list_Tracker;
	float timer;
	float fadeTimer;

	// Use this for initialization
	void Start () {
		Instructions_List = new List<string> ();

		if (contant_Script.instance.spanish) {
			Instructions_List.Add (instructions_1_SP);
			Instructions_List.Add (instructions_2_SP);
			Instructions_List.Add (instructions_3_SP);
		} else if(!contant_Script.instance.spanish) {
			Instructions_List.Add (instructions_1_EN);
			Instructions_List.Add (instructions_2_EN);
			Instructions_List.Add (instructions_3_EN);
			
		}
		list_Tracker = 0;
		timer = 0;
		launch_release = false;
		textVar.text = Instructions_List [0];
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.X)) {
			print (Instructions_List [0]);
		}
		if (timer < 21) {
			timer += 1 * Time.deltaTime;
		}


		if (list_Tracker == 3) {
			this.gameObject.SetActive (false);
			paddle.instance.launchBool = true;
			changeBool = false;
			launch_release = true;
		}
		if (changeBool) {
			textTransition (list_Tracker);
		}
	}


	void textTransition(int currentSpot){		
		//Color tmp = textVar.GetComponent<Text> ().color;
		IEnumerator cTmp = FadeOut_Text (textVar, 0.1f);
		IEnumerator cTmp2 = Text_Change (0.2f, currentSpot, Instructions_List);
		StartCoroutine(cTmp);
		StartCoroutine (cTmp2);
		//StartCoroutine (Text_Change (0.2f, currentSpot, Instructions_List));
		if (textVar.text == Instructions_List [currentSpot]) {
			StopCoroutine (cTmp);
			StopCoroutine (cTmp2);
		}
		StartCoroutine (FadeIn_Text (textVar, 0.3f));
	}

	public void next_Button(){
		list_Tracker += 1;
		changeBool = true;
	}

	IEnumerator Text_Change(float delay, int listSpot, List<string> textList){
		yield return new WaitForSeconds (delay);
		textVar.text = textList [listSpot];
	}



	IEnumerator FadeIn(GameObject target, float delay){
		yield return new WaitForSeconds (delay);
		Color tmp = target.GetComponent<Image> ().color;
		if (tmp.a < 1f) {
			tmp.a = Mathf.Lerp (tmp.a, 1, Time.deltaTime * 2);
			target.GetComponent<Image> ().color = tmp;
		}
	}

	IEnumerator FadeIn_Img(Image target, float delay){
		yield return new WaitForSeconds (delay);
		Color tmp = target.GetComponent<Image>().color;
		if (tmp.a < 1f) {
			tmp.a = Mathf.Lerp (tmp.a, 1, Time.deltaTime * 2);
			target.GetComponent<Image>().color = tmp;
		}
	}

	IEnumerator FadeIn_Text(Text target, float delay){
		yield return new WaitForSeconds (delay);
		Color tmp = target.GetComponent<Text> ().color;
		if (tmp.a < 1f) {
			tmp.a = Mathf.Lerp (tmp.a, 1, Time.deltaTime * 2);
			target.GetComponent<Text> ().color = tmp;
		}
	}



	IEnumerator FadeOut_Text(Text target, float delay){
		yield return new WaitForSeconds (delay);
		Color tmp = target.GetComponent<Text> ().color;
		if (tmp.a > 0f) {
			tmp.a = Mathf.Lerp (tmp.a, 0, Time.deltaTime * 2);
			target.GetComponent<Text> ().color = tmp;
		}
	}

	IEnumerator FadeOut_Img(Image target, float delay){
		yield return new WaitForSeconds (delay);
		Color tmp = target.GetComponent<Image>().color;
		if (tmp.a > 0f) {
			tmp.a = Mathf.Lerp (tmp.a, 0, Time.deltaTime * 2);
			target.GetComponent<Image>().color = tmp;
		}
	}

	IEnumerator FadeOut(GameObject target, float delay){
		yield return new WaitForSeconds (delay);
		Color tmp = target.GetComponent<Image> ().color;
		if (tmp.a > 0f) {
			tmp.a = Mathf.Lerp (tmp.a, 0, Time.deltaTime * 2);
			target.GetComponent<Image> ().color = tmp;
		}
	}
}
