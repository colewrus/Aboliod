using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class photoPuzzle : MonoBehaviour {
	public AudioClip slideSound;
	public AudioSource soundSource;
	public AudioSource musicSource;

	public GameObject picture;
	public GameObject slot;
	public Text tutorialText;
	public string nextLevel;
	public GameObject winImage;
	public GameObject textPanel;
	public GameObject exit_Botton;
	public List<GameObject> slides;
	public List<Sprite> images;
	public List<Vector3> positions;

	public static photoPuzzle instance = null;

	public int shuffleDelay;

	float tempX;
	float tempY;
	Vector2 S;
	float spacing;
	bool startFadeIn;
	bool shuffleBool;
	public bool continueBool;
	public bool winBool;
	public bool firstClick;
    //enables tap control of puzzle
    public bool clickBool;
	public bool instruct_Bool;
	public bool exit_Bool;
	public Sprite final_img_lvl1_EN;
	public Sprite final_img_lvl2_EN;
	public Sprite final_img_lvl1_SP;
	public Sprite final_img_lvl2_SP;

	Color tmpPan;

	IEnumerator loadPanel;
	IEnumerator loadText;
	IEnumerator loadButton;
	IEnumerator panelI;
	IEnumerator exitI;
	IEnumerator textI;

    bool shuffleA = false;
    bool shuffleB = false;
    bool shuffleC = false;
    bool shuffleD = false;
    bool shuffleE = false;
    bool shuffleF = false;

    void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

	}
	// Use this for initialization
	void Start () {

        clickBool = false;

		//Set the screen resolution
		//Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
		//Screen.SetResolution(600, 800, false);


		//set language
		if (contant_Script.instance.spanish) {
			tutorialText.text = "Tocá para reacomodar las piezas y completar la imagen.";
			if (SceneManager.GetActiveScene ().name == "slideLoad1") {
				winImage.GetComponent<SpriteRenderer> ().sprite = final_img_lvl1_SP;
			} else if (SceneManager.GetActiveScene ().name == "slideLoad2") {
				winImage.GetComponent<SpriteRenderer> ().sprite = final_img_lvl2_SP;
			}

		} else if (!contant_Script.instance.spanish) {
			tutorialText.text = "Tap to reshuffle the pieces and make the image whole.";
			if (SceneManager.GetActiveScene ().name == "slideLoad1") {
				winImage.GetComponent<SpriteRenderer> ().sprite = final_img_lvl1_EN;
			} else if (SceneManager.GetActiveScene ().name == "slideLoad2") {
				winImage.GetComponent<SpriteRenderer> ().sprite = final_img_lvl2_EN;
			}
		} 


		//start our loop enders to false - this end the loops that generate the images during setup
		startFadeIn = false;
		shuffleBool = false;
		continueBool = false;
		exit_Bool = false;
		//music mute
		if (contant_Script.instance.music_mute) {
			musicSource.volume = 0;
		} else {
			musicSource.volume = 0.5f;
		}
		//declare Ienumerators
		//print (SceneManager.GetActiveScene().buildIndex);

		instruct_Bool = false;
		//Panel Stuff
		Color tmpPanel = textPanel.GetComponent<Image>().color;
		tmpPanel.a = 0;
		textPanel.GetComponent<Image> ().color = tmpPanel;
		Color tmpButton = exit_Botton.GetComponent<Image> ().color;
		tmpButton.a = 0;
		exit_Botton.GetComponent<Image> ().color = tmpButton;


		//tutorial highlight prior to the first player move
		firstClick = false;
		//set the final image to inactive
		winImage.SetActive (false);
		Color tmpImg = winImage.GetComponent<SpriteRenderer> ().color;
		tmpImg.a = 0;
		winImage.GetComponent<SpriteRenderer> ().color = tmpImg;
		//set the tutorial text to transparent
		Color tmpText = tutorialText.GetComponent<Text>().color;
		tmpText.a = 0;
		tutorialText.GetComponent<Text> ().color = tmpText;

		
		slides = new List<GameObject> ();
		positions = new List<Vector3> ();

		for (int y = 0; y < 3; y++) {
			for (int x = 0; x < 3; x++) {
				slides.Add ((GameObject)Instantiate (picture));
				int slideC = slides.Count-1;
				slides [slideC].GetComponent<SpriteRenderer> ().sprite = images [slideC];
				Color tmp = slides[slideC].GetComponent<SpriteRenderer> ().color;
				tmp.a = 0;
				slides [slideC].GetComponent<SpriteRenderer> ().color = tmp;
				spacing = slides [0].GetComponent<Renderer> ().bounds.size.x;

				Vector3 posEnd = Camera.main.WorldToScreenPoint(new Vector3(slides[0].GetComponent<Renderer> ().bounds.max.x, slides[0].GetComponent<Renderer> ().bounds.max.y, slides[0].GetComponent<Renderer> ().bounds.max.z));
				float widthTemp1 = Screen.width - (posEnd.x*3);
				Vector3 padding = Camera.main.ScreenToWorldPoint(slides[0].transform.position);
				float widthTemp2 = widthTemp1 + padding.x;
				float startPosx = widthTemp2 / 9;
				//print (spacing + " : " + Screen.width + " : " + startPosx);


				slides [slideC].GetComponent<BoxCollider2D> ().size = (slides [slideC].GetComponent<SpriteRenderer> ().bounds.size / 2);
		
				slides [slideC].transform.position = new Vector3 ((spacing/9) + x * spacing, y * -spacing, -9);	
				positions.Add (slides [slideC].transform.position);	
			}
		}

	}


	void Update(){
		IEnumerator setupI = Setup();
		if (!exit_Bool) {
			StartCoroutine (setupI);
		}

		if (continueBool) {
			StopCoroutine (setupI);
		}
		if (SceneManager.GetActiveScene().name == "slideLoad1") {			
			if (!instruct_Bool && !exit_Bool) {
				loadPanel = FadeIn_Img (textPanel, 3f);
				loadText = FadeIn_Text (tutorialText, 3f);
				loadButton = FadeIn_Img (exit_Botton, 3.5f);
				StartCoroutine (loadPanel);
				StartCoroutine (loadText);
				StartCoroutine (loadButton);
				Color tmpPan2 = textPanel.GetComponent<Image> ().color;
				if (tmpPan2.a > 0.8f) {
					instruct_Bool = true;
				}
			}

			if (instruct_Bool) {
				StopCoroutine (setupI);
				StopCoroutine (loadPanel);
				StopCoroutine (loadText);
				StopCoroutine (loadButton);
			}

			if (exit_Bool) {
				textPanel.SetActive (false);
			}
		}

		if (shuffleBool) {
			
			winBool = true;
			for (int i = 0; i < slides.Count; i++) {
				if (slides [i].transform.position != positions [i]) {
					winBool = false;
					break;
				} 
			}
			if (winBool) {				
				winImage.SetActive (true);
				Color tmp = winImage.GetComponent<SpriteRenderer> ().color;
				tmp.a = Mathf.Lerp (tmp.a, 1, Time.deltaTime * 2);
				winImage.GetComponent<SpriteRenderer> ().color = tmp;
				if (Input.GetMouseButtonUp (0)) { //tap & on release move on to the next level
					nextLevel_forInvoke ();
				}
				Invoke ("nextLevel_forInvoke", 10); // load the next level after waiting for a bit
			}
		}

	}

	void nextLevel_forInvoke(){
		if (SceneManager.GetActiveScene ().buildIndex == 4) {
			SceneManager.LoadScene (1);
		}
		if (SceneManager.GetActiveScene ().buildIndex == 3) {
			SceneManager.LoadScene (2);
		}
	}

	public void Exit_Press(){
		exit_Bool = true;
	}

	IEnumerator FadeOut(GameObject target, float delay){
		yield return new WaitForSeconds (delay);
		Color tmp = target.GetComponent<SpriteRenderer> ().color;
		if (tmp.a > 0f) {
			tmp.a = Mathf.Lerp (tmp.a, 0, Time.deltaTime * 4);
			target.GetComponent<SpriteRenderer> ().color = tmp;
		}
	}

	IEnumerator FadeIn(GameObject target, float delay){
		yield return new WaitForSeconds (delay);
		Color tmp = target.GetComponent<SpriteRenderer> ().color;
		if (tmp.a < 1f) {
			tmp.a = Mathf.Lerp (tmp.a, 1, Time.deltaTime * 4);
			target.GetComponent<SpriteRenderer> ().color = tmp;
		}

	}

	IEnumerator FadeIn_Img(GameObject target, float delay){
		yield return new WaitForSeconds (delay);
		Color tmp = target.GetComponent<Image>().color;
		if (tmp.a < 1f) {
			tmp.a = Mathf.Lerp (tmp.a, 1, Time.deltaTime * 12);
			target.GetComponent<Image> ().color = tmp;
		}
	}

	IEnumerator FadeOut_Img(GameObject target, float delay){
		yield return new WaitForSeconds (delay);
		Color tmp = target.GetComponent<Image>().color;
		if (tmp.a > 0f) {
			tmp.a = Mathf.Lerp (tmp.a, 0, Time.deltaTime * 5);
			target.GetComponent<Image>().color = tmp;
		}

	}

	IEnumerator FadeOut_Text(Text target, float delay){
		yield return new WaitForSeconds (delay);
		Color tmp = target.GetComponent<Text> ().color;
		if (tmp.a > 0f) {
			tmp.a = Mathf.Lerp (tmp.a, 0, Time.deltaTime * 3);
			target.GetComponent<Text> ().color = tmp;
		}
	}

	IEnumerator FadeIn_Text(Text target, float delay){
		yield return new WaitForSeconds (delay);
		Color tmp = target.GetComponent<Text> ().color;
		if (tmp.a < 1f) {
			tmp.a = Mathf.Lerp (tmp.a, 1, Time.deltaTime * 6);
			target.GetComponent<Text> ().color = tmp;
		}
	}

	IEnumerator Setup(){
		if (!startFadeIn) {
			yield return new WaitForSeconds (0.5f);
			for (int i = 0; i < slides.Count; i++) {
				StartCoroutine (FadeIn (slides [i], 1));
				if (i == slides.Count - 1) {
					startFadeIn = true;
				}
			}
		}

		int tempInt = 0;

		//maybe play countdown sound
		yield return new WaitForSeconds (shuffleDelay);
        
		//shuffle the images
		if (!shuffleBool) {
            if (!shuffleA)
            {
                slot = slides[1];
                slides[0].GetComponent<puzzlePieceScript>().setMovement = true;
                slides[0].GetComponent<puzzlePieceScript>().MovePiece(slot);
                shuffleA = true;
            }

            yield return new WaitForSeconds(0.75f);

            if (!shuffleB)
            {
                slot = slides[6];
                slides[3].GetComponent<puzzlePieceScript>().setMovement = true;
                slides[3].GetComponent<puzzlePieceScript>().MovePiece(slot);
                shuffleB = true;
            }

            yield return new WaitForSeconds(0.5f);
            if (!shuffleC)
            {
                slot = slides[5];
                slides[2].GetComponent<puzzlePieceScript>().setMovement = true;
                slides[2].GetComponent<puzzlePieceScript>().MovePiece(slot);
                shuffleC = true;
            }
            yield return new WaitForSeconds(0.25f);
            if (!shuffleD)
            {
                slot = slides[4];
                slides[2].GetComponent<puzzlePieceScript>().setMovement = true;
                slides[2].GetComponent<puzzlePieceScript>().MovePiece(slot);
                shuffleD = true;
            }
            yield return new WaitForSeconds(0.25f);
            if (!shuffleE)
            {
                slot = slides[4];
                slides[8].GetComponent<puzzlePieceScript>().setMovement = true;
                slides[8].GetComponent<puzzlePieceScript>().MovePiece(slot);
                shuffleE = true;
            }
            yield return new WaitForSeconds(0.25f);
            if (!shuffleF)
            {
                slot = slides[7];
                slides[3].GetComponent<puzzlePieceScript>().setMovement = true;
                slides[3].GetComponent<puzzlePieceScript>().MovePiece(slot);
                shuffleF = true;
            }

            shuffleBool = true;
            clickBool = true;
        }
    }

	public void NextLevel(){
		if (continueBool) {
			SceneManager.LoadScene (nextLevel);
		}

	}

}
