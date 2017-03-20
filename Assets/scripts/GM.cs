using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GM : MonoBehaviour {

	//Game Variables
    public int lives = 3;
    public int bricks;
    public float resetDelay = 2f;
    public Text livesText;
    public bool RecayNow;
	private Vector3 temp;
	public string nextLevelString;
	public bool gameWon;
	public bool endFade;


	//GameObjects
    public GameObject gameOver;
	public Sprite lose_EN;
	public Sprite lose_SP;
    public GameObject youWon;
	public Sprite win_EN;
	public Sprite win_SP;
    public GameObject bricksPrefab;
    public GameObject paddle;
    public GameObject deathParticles;
	private GameObject clonePaddle;
    public static GM instance = null;
	public GameObject brickPrefab;
	public GameObject pepeWalk;


	//Pepe animation variables
	public Vector3 pepeStart;
	public Vector3 pepeEnd;
	public Vector3 jumpStart;
	public Vector3 jumpEnd;
	public float walkTimer;
	public GameObject pepeWin;
	public GameObject brickWin;
	GameObject pepeClone;

	//AudioSources
    public AudioSource musicSource;
    public AudioSource efxSource;    

	//UI stuff

	public GameObject PausePanel;
    public bool efxMute;
    public bool musicMute;
    public Image efxImage;
    public Image musicImage;
    public Sprite musicTrue;
    public Sprite musicFalse;
    public Sprite efxTrue;
    public Sprite efxFalse;
	public Sprite onSprite;
	public Sprite offSprite;
    public GameObject blackoutCurtain;
	public GameObject instruction_Panel;

	//Walls
	public GameObject leftWall;
	public GameObject rightWall;
	public GameObject topWall;
       

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
       
        Setup();
    }

    public void Setup()
    {
		//Set the screen resolution
		//Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
		Screen.SetResolution(600, 800, false);

		//Set the language
		if (contant_Script.instance.spanish) {

			PausePanel.transform.GetChild (2).transform.GetChild (0).GetComponent<Text> ().text = "Finalizar Juego";
			//need to adjust size of text above
			PausePanel.transform.GetChild (3).transform.GetChild (0).GetComponent<Text> ().text = "Reiniciar";
			PausePanel.transform.GetChild (4).transform.GetChild (1).GetComponent<Text> ().text = "Musica";
			PausePanel.transform.GetChild (5).transform.GetChild (1).GetComponent<Text> ().text = "Sonida";
			youWon.GetComponent<Image> ().sprite = win_SP;
			gameOver.GetComponent<Image> ().sprite = lose_SP;
		} else if (!contant_Script.instance.spanish) {
			
			PausePanel.transform.GetChild (2).transform.GetChild (0).GetComponent<Text> ().text = "Quit Game";
			//need to adjust size of text above
			PausePanel.transform.GetChild (3).transform.GetChild (0).GetComponent<Text> ().text = "Resume";
			//need to adjust size of text above
			PausePanel.transform.GetChild (4).transform.GetChild (1).GetComponent<Text> ().text = "Music";
			PausePanel.transform.GetChild (5).transform.GetChild (1).GetComponent<Text> ().text = "Sound";
			youWon.GetComponent<Image> ().sprite = win_EN;
			gameOver.GetComponent<Image> ().sprite = lose_EN;
		}

		//check settings to see if music & sounds are muted
		if (contant_Script.instance.music_mute) {
			
			musicMute = true;
			musicSource.volume = 0;            
			musicImage.GetComponent<Image>().sprite = offSprite;
		} else {
			musicMute = false;
			musicSource.volume = 0.4f;
			musicImage.GetComponent<Image>().sprite = onSprite;
		}

		if (contant_Script.instance.sound_mute) {
			efxMute = true;
			efxImage.GetComponent<Image>().sprite = offSprite;
		} else {
			efxMute = false;
			efxImage.GetComponent<Image>().sprite = onSprite;
		}

		//set pepe start based on fraction of screen size
		pepeStart = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width/10, Screen.height / 10, 5));
		pepeEnd = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width / 10, Screen.height - (Screen.height / 10), 5));

		//set the walls
		leftWall.transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height / 2, 5));
		rightWall.transform.position = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height/2, 5));
		topWall.transform.position = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width/2, Screen.height, 5));


        //Set the default audio image settings - need to add save state
		musicImage.GetComponent<Image>().sprite = onSprite;
		efxImage.GetComponent<Image>().sprite = onSprite;

        //set the text for the lives
        livesText.text = "x" + lives;
		gameWon = false;
		livesText.color = hexToColor ("#0056fff");
        //On resume time decay bool
        RecayNow = false;

		//Instantiate the level objects
		Vector3 levelOnePlace = new Vector3(-24, 48, 0);
		Vector3 levelTwoPlace = new Vector3 (-40, 53, 0);

		if (SceneManager.GetActiveScene ().name == "phoneLayout") {
			temp = levelOnePlace;
			instruction_Panel.SetActive (true);
		}
		if (SceneManager.GetActiveScene ().name == "level2") {
			temp = levelTwoPlace;
			instruction_Panel.SetActive (false);
		}
		clonePaddle = Instantiate (paddle, transform.position, Quaternion.identity) as GameObject;
		Instantiate(bricksPrefab, temp, Quaternion.identity);

        //count the number of bricks in given level and set to the win number
        bricks = GameObject.FindGameObjectsWithTag("bricks").Length;

		//ensure the game pause menu is invisible and time is set to 1
		PausePanel.SetActive(false);
		Time.timeScale = 1f;

		endFade = false;

		pepeClone = (GameObject) Instantiate (pepeWalk, pepeStart, pepeWalk.transform.rotation);

    }

    public void CheckGameOver()
    {
        
		if (bricks == 1) {
			Destroy (pepeClone);
			Color tmpBrickAlpha = GameObject.FindGameObjectWithTag ("bricks").GetComponent<SpriteRenderer> ().color;
			tmpBrickAlpha.a = 0;
			print (tmpBrickAlpha);
			GameObject.FindGameObjectWithTag ("bricks").GetComponent<SpriteRenderer> ().color = tmpBrickAlpha;
			Color tmpBallAlpha = GameObject.FindGameObjectWithTag ("ball").GetComponent<SpriteRenderer> ().color;
			tmpBallAlpha.a = 0;
			GameObject.FindGameObjectWithTag ("ball").GetComponent<SpriteRenderer> ().color = tmpBallAlpha;
			GameObject.FindGameObjectWithTag ("ball").GetComponent<Rigidbody2D> ().isKinematic = true;
			brickWin.SetActive (true);
			//Instantiate (pepeWin, Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width / 2, Screen.height / 2, 5)), Quaternion.identity);
			Instantiate (brickWin, Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width / 2, (Screen.height/2)+(2* pepeWin.GetComponent<SpriteRenderer>().bounds.size.y), 5)), Quaternion.identity);
			GameObject.FindGameObjectWithTag ("bricks").SetActive (false);	
		
		}

		if (bricks < 1)
        {			
			gameWon = true;
			SoundManager.Instance.PlaySingle(SoundManager.Instance.wonSound, 0.15f);
            youWon.SetActive(true);
			endFade = true;

			//set for the sake of the fadeControl script, basically releases it to be reset to active
			endFade = true;
			blackoutCurtain.SetActive (true);
			Invoke ("NextLevel", resetDelay);
        }

        if (lives < 0)
        {
            SoundManager.Instance.PlaySingle(SoundManager.Instance.overSound, 0.15f);
            gameOver.SetActive(true);
			endFade = true;
			blackoutCurtain.SetActive (true);
			Time.timeScale = 1f;
            Invoke("Reset", resetDelay);
        }
		Color tmp = blackoutCurtain.GetComponent<Image> ().color;
		tmp.a = 0.3f;
		blackoutCurtain.GetComponent<Image> ().color = tmp;			
    }

	//Reset the level
    void Reset()
    {
        Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	public void NextLevel(){
		Time.timeScale = 1f;
		SceneManager.LoadScene (nextLevelString);
	}

    public void LoseLife()
    {
        SoundManager.Instance.PlaySingle(SoundManager.Instance.deathSound, 0.1f);
        lives--;
		if (lives >= 0) {
			livesText.text = "x" + lives;
		} else {
			livesText.text = "x0";
		}
        if (GameObject.Find("ball"))
        {
            Instantiate(deathParticles, clonePaddle.transform.position, Quaternion.identity);
        }

        Destroy(clonePaddle);
        Destroy(GameObject.Find("Ball"));
        Invoke("SetupPaddle", 0.3f);
        CheckGameOver();

		if (lives == 1) {
			livesText.color = Color.red;
		}

		//!---------------------ADD A HOLD ON TIMER AND PEPE MOVEMENT------------------!
    }

    void SetupPaddle()
    {		
        clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity) as GameObject;
    }

    public void DestroyBrick()
    {
        SoundManager.Instance.PlaySingle(SoundManager.Instance.blockHit, 0.1f);
        bricks--;
        CheckGameOver();
    }

 
	//Open the in game menu
	public void InGameMenu(){
        SoundManager.Instance.PlaySingle (SoundManager.Instance.buttonPress, 0.3f);
		Time.timeScale = 0f;
		PausePanel.SetActive (true);
	}

	//resume game and close the in game menu
	public void ResumeGame(){
        SoundManager.Instance.PlaySingle (SoundManager.Instance.buttonPress, 0.3f);
        //Time.timeScale = 0.25f;
        //RecayNow = true;
        Time.timeScale = 1f;  
        PausePanel.SetActive (false);
	}
		


	//quit the game and return to menu
	public void QuitCurrentGame(){
        SoundManager.Instance.PlaySingle (SoundManager.Instance.buttonPress, 0.3f);
        Invoke("LoadMenu", 0.2f);
		Time.timeScale = 1f;
	}

    //Mute sound effects and music
    public void MuteSounds()
    {
        if (!efxMute)
        {
            SoundManager.Instance.PlaySingle(SoundManager.Instance.muted, 0.3f);
            efxMute = true;
			efxImage.GetComponent<Image>().sprite = offSprite;
        }
        else{            
            efxMute = false;
			efxImage.GetComponent<Image>().sprite = onSprite;
            SoundManager.Instance.PlaySingle(SoundManager.Instance.buttonPress, 0.3f);
        }
    }

    public void MuteMuzak()
    {
        if (!musicMute)
        {
			SoundManager.Instance.PlaySingle(SoundManager.Instance.muted, 0.3f);
			musicMute = true;
			musicSource.volume = 0;            
			musicImage.GetComponent<Image>().sprite = offSprite;
        }
        else
        {
            musicMute = false;
            musicSource.volume = 0.4f;
			musicImage.GetComponent<Image>().sprite = onSprite;
			SoundManager.Instance.PlaySingle(SoundManager.Instance.buttonPress, 0.3f);
        }
    }

    //For invoke method, should load the main menu
    void LoadMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }


	public static Color hexToColor(string hex)
	{
		hex = hex.Replace ("0x", "");//in case the string is formatted 0xFFFFFF
		hex = hex.Replace ("#", "");//in case the string is formatted #FFFFFF
		byte a = 255;//assume fully visible unless specified in hex
		byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
		//Only use alpha if the string has enough characters
		if(hex.Length == 8){
			a = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
		}
		return new Color32(r,g,b,a);
	}

}
