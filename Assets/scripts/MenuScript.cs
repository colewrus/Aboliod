using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuScript : MonoBehaviour {

	public AudioClip buttonPress;
	public AudioSource efxSource;
    public string startGame;
	public GameObject aboutPanel;
	public GameObject nextButton;
	public GameObject backButton;
	public GameObject defaultPanel;
	public GameObject exitButton;
	public Text aboutText;

	//Settings Menu
	public Image musicImage;
	public Image efxImage;
	public Sprite onSprite;
	public Sprite offSprite;
	public GameObject settingsPanel;
	bool showSettings;
	public Text settingsTitle;
	public Text musicText;
	public Text soundsText;
	public GameObject exitButton_2;
	public Text language_Text;
	public Text lang_title_text;
	public Sprite title_Image_SP;
	public Sprite title_Image_EN;
	public Image title_Image_GameObj;

	public string aboutString_1_SP;
	public string aboutString_2_SP;
	public string aboutString_3_SP;
	public string aboutString_4_SP;
	public string aboutString_1_EN;
	public string aboutString_2_EN;
	public string aboutString_3_EN;
	public string aboutString_4_EN;
	public List<string> aboutList;


	float volume;

	//button bools
	bool aboutShow;
	bool nextRun;
	bool backRun;
	//track list position
	int list_Tracker;

	void Start(){
		Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
		//Screen.SetResolution(600, 800, false);
		Color tmp = aboutPanel.GetComponent<Image>().color;
		tmp.a = 0;
		aboutPanel.GetComponent<Image> ().color = tmp;

		Color textTMP = aboutText.GetComponent<Text> ().color;
		textTMP.a = 0;
		aboutText.GetComponent<Text> ().color = textTMP;

		Color nextButtonTMP = nextButton.GetComponent<Image> ().color;
		nextButtonTMP.a = 0;
		nextButton.GetComponent<Image> ().color = nextButtonTMP;

		Color backButtonTMP = nextButton.GetComponent<Image> ().color;
		backButtonTMP.a = 0;
		nextButton.GetComponent<Image> ().color = backButtonTMP;

		volume = 0.5f;

		//set the text
		aboutList = new List<string>();

		aboutList.Add (aboutString_1_SP);
		aboutList.Add (aboutString_2_SP);
		aboutList.Add (aboutString_3_SP);
		aboutList.Add (aboutString_4_SP);
		list_Tracker = 0;
		aboutText.text = aboutList [list_Tracker];

		aboutPanel.SetActive (false);
		settingsPanel.SetActive (false);
		showSettings = false;
		aboutShow = false;
		nextRun = false;
		backRun = false;
		language_Text.text = "Spanish";

	}

	void Update(){
		if (Input.GetKey (KeyCode.X)) {
			print (contant_Script.instance.spanish);
			print (settingsPanel.transform.GetChild (4).gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().color.a);
		}
		if (aboutShow) {
			StartCoroutine (FadeIn (aboutPanel, 0));
			StartCoroutine (FadeIn_Text (aboutText, 0.1f));
			if (list_Tracker < 3) {
				StartCoroutine (FadeIn (nextButton, 0.1f));
			}else{
				StartCoroutine (FadeOut (nextButton, 0.1f));
			}
			StartCoroutine (FadeIn (exitButton, 0.1f));
			defaultPanel.SetActive (false);
			if (list_Tracker > 0) {
				StartCoroutine (FadeIn (backButton, 0.1f));
			}
		} else {
			StartCoroutine (FadeOut (aboutPanel, 0.3f));
			StartCoroutine (FadeOut_Text (aboutText, 0.1f));
			StartCoroutine (FadeOut (nextButton, 0));
			StartCoroutine (FadeOut (backButton, 0));
			StartCoroutine (FadeOut (exitButton, 0));
			Color tmp = aboutPanel.GetComponent<Image> ().color;
			if (tmp.a <= 0.1f) {
				aboutPanel.SetActive (false);
				defaultPanel.SetActive (true);

			}
		}

		if (showSettings) {
			StartCoroutine (FadeIn (settingsPanel, 0));
			StartCoroutine (FadeIn_Img (musicImage, 0));
			StartCoroutine (FadeIn_Img (efxImage, 0));
			StartCoroutine (FadeIn_Text (settingsPanel.transform.GetChild (4).gameObject.transform.GetChild (0).GetComponent<Text> (), 0)); //language text
			StartCoroutine (FadeIn_Img (settingsPanel.transform.GetChild (4).gameObject.transform.GetChild (1).GetComponent<Image> (), 0)); //button background
			StartCoroutine (FadeIn_Text (settingsPanel.transform.GetChild (4).gameObject.transform.GetChild (1).transform.GetChild(0).GetComponent<Text>(), 0)); //text on the button
			StartCoroutine (FadeIn_Text (settingsTitle, 0));
			StartCoroutine (FadeIn_Text (musicText, 0));
			StartCoroutine (FadeIn_Text (soundsText, 0));
			StartCoroutine (FadeIn (exitButton_2, 0));
			defaultPanel.SetActive (false);
		} else {
			StartCoroutine (FadeOut (settingsPanel, 0));
			StartCoroutine (FadeOut_Img (musicImage, 0));
			StartCoroutine (FadeOut_Img (efxImage, 0));
			StartCoroutine (FadeOut_Text (settingsTitle, 0));
			StartCoroutine (FadeOut_Text (musicText, 0));
			StartCoroutine (FadeOut_Text (soundsText, 0));
			StartCoroutine (FadeOut (exitButton_2, 0));	
			StartCoroutine (FadeOut_Text (settingsPanel.transform.GetChild (4).gameObject.transform.GetChild (0).GetComponent<Text> (), 0)); //language text
			//need to adjust size of text above
			StartCoroutine (FadeOut_Img (settingsPanel.transform.GetChild (4).gameObject.transform.GetChild (1).GetComponent<Image> (), 0)); //button background
			StartCoroutine (FadeOut_Text (settingsPanel.transform.GetChild (4).gameObject.transform.GetChild (1).transform.GetChild(0).GetComponent<Text>(), 0)); //text on the button
			Color tmp = settingsPanel.GetComponent<Image> ().color;
			if (tmp.a <= 0.01f) {
				settingsPanel.SetActive (false);
			}
		}

		//Next and back transition
		if (nextRun) {
			textTransition (list_Tracker);
		}
		if (backRun) {
			textTransition (list_Tracker);
		}
		if (list_Tracker == 0) {
			StartCoroutine (FadeOut (backButton, 0.1f));
		}

		//local mute
		if (contant_Script.instance.sound_mute) {
			efxSource.volume = 0;
			volume = 0;
		} else {
			efxSource.volume = 0.5f;
			volume = 0.5f;
		}


		//whole bunch of CoRoutine stops;



	}

	void textTransition(int currentSpot){
		StartCoroutine (FadeOut_Text (aboutText, 0.1f));
		StartCoroutine (Text_Change (0.2f, currentSpot));
		StartCoroutine (FadeIn_Text (aboutText, 0.3f));
		StartCoroutine (Button_Bool (0.5f));
	}


	public void PlaySingle(AudioClip clip, float volume)
	{
		efxSource.volume = volume;
		efxSource.clip = clip;
		efxSource.Play();
	}


	public void PlayGame(){		
		PlaySingle (buttonPress, volume);
        Invoke("NextLevel", 0.2f);        
	}

    void NextLevel()
    {
        SceneManager.LoadScene(startGame);
    }

	public void QuitGame(){
		PlaySingle (buttonPress, volume);	
		Application.Quit ();
	}

	//Buttons and the various screens they load

	public void ShowAbout(){
		aboutPanel.SetActive (true);
		aboutShow = true;
	}

	public void Close_About(){
		aboutShow = false;
		list_Tracker = 0;
	}

	public void Next(){
		if (list_Tracker < 4) {
			list_Tracker += 1;
		}
		nextRun = true;
	}

	public void Back_Button(){
		print ("test");
		if (list_Tracker > 0) {
			list_Tracker -= 1;
		}
		backRun = true;
	}

	//Settings panel
	public void Show_Settings(){
		settingsPanel.SetActive (true);
		showSettings = true;
	}

	public void Close_Settings(){
		showSettings = false;
	}


	public void Toggle_Music(){
		contant_Script.instance.Music_Func ();
		if (contant_Script.instance.music_mute)
		{
			musicImage.GetComponent<Image>().sprite = offSprite;
		}
		else
		{				
			musicImage.GetComponent<Image>().sprite = onSprite;
		}
	}

	public void Toggle_Sound(){
		contant_Script.instance.Sounds_Func ();
		if (contant_Script.instance.music_mute)
		{
			efxImage.GetComponent<Image>().sprite = offSprite;
		}
		else
		{			
			efxImage.GetComponent<Image>().sprite = onSprite;
		}
	}


	public void Toggle_Language(){
		if (!contant_Script.instance.spanish) {			
			language_Text.text = "Spanish";
			//reset list to spanish About
			aboutList.Clear();
			aboutList.Add (aboutString_1_SP);
			aboutList.Add (aboutString_2_SP);
			aboutList.Add (aboutString_3_SP);
			aboutList.Add (aboutString_4_SP);
			aboutText.text = aboutList [0];
			settingsTitle.text = "Adjustes";
			musicText.text = "Musica";
			soundsText.text = "Sonido";
			lang_title_text.text = "Lengua";
			title_Image_GameObj.GetComponent<Image> ().sprite = title_Image_SP;
			settingsPanel.transform.GetChild (4).gameObject.transform.GetChild (0).GetComponent<Text> ().fontSize = 105; //change size of the language text
			contant_Script.instance.spanish = true;
		} else {			
			language_Text.text = "English";
			//reset list to english About
			aboutList.Clear();
			aboutList.Add (aboutString_1_EN);
			aboutList.Add (aboutString_2_EN);
			aboutList.Add (aboutString_3_EN);
			aboutList.Add (aboutString_4_EN);
			aboutText.text = aboutList [0];
			aboutText.text = aboutList [0];
			title_Image_GameObj.GetComponent<Image> ().sprite = title_Image_EN;
			settingsPanel.transform.GetChild (4).gameObject.transform.GetChild (0).GetComponent<Text> ().fontSize = 85; //change size of the language text
			settingsTitle.text = "Settings";
			musicText.text = "Music";
			soundsText.text = "Sound";
			lang_title_text.text = "Language";
			contant_Script.instance.spanish = false;
		}
	}


	IEnumerator Button_Bool(float delay){
		yield return new WaitForSeconds (delay);
		nextRun = false;
		backRun = false;
	}


	IEnumerator Text_Change(float delay, int listSpot){
		yield return new WaitForSeconds (delay);
		aboutText.text = aboutList [listSpot];
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
