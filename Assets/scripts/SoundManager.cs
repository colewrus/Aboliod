using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SoundManager : MonoBehaviour {

    public static SoundManager Instance;


    public AudioClip blockHit;
    public AudioClip deathSound;
    public AudioClip wonSound;
    public AudioClip overSound;
    public AudioClip buttonPress;
    public AudioClip muted;



    //Developer Tools - UI
    public Slider speedSlider;
    public GameObject developerConsole;
    public Text speedVal;
    public Slider bounceSlider;
    public Text bounceVal;
    public InputField speedBox;
    public GameObject blackoutCurtain;


    //Developer Tools - variables
    bool consoleActive;
    public float speedNum;
    public float updatedSpeed;
    public float bounceNum;
    public float updatedBounce;
    private bool useBar;
	public Button physicsButton;

	float tmpTimer;

    // Use this for initialization
    void Awake () {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        developerConsole.SetActive(false);
        consoleActive = false;
        updatedBounce = 1;
        useBar = true;

    }

    void Start()
    {
		tmpTimer = 0;
        //set values on other gameObject variables
        speedNum = paddle.instance.paddleSpeed;
        bounceNum = GameObject.Find("Ball").GetComponent<Rigidbody2D>().mass;
        blackoutCurtain.SetActive(true);

		//set the physics button in the developers tool to red
		physicsButton.image.color = hexToColor ("#FF0000");
    }

    public void PlaySingle(AudioClip clip, float volume)
    {
        if (!GM.instance.efxMute)
        {
            GM.instance.efxSource.volume = volume;
        }
        else
        {
            GM.instance.efxSource.volume = 0;
        }
        GM.instance.efxSource.clip = clip;
        GM.instance.efxSource.Play();
    }

    void Update()
    {


		if (GM.instance.bricks == 1) {
			endLevel ();
		}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!consoleActive)
            {
                developerConsole.SetActive(true);
                Time.timeScale = 0;
                consoleActive = true;
            }else if(consoleActive)
            {
				Time.timeScale = 1;
				developerConsole.SetActive(false);
                consoleActive = false;
                
            }
        }


        speedBox.onValueChanged.AddListener(delegate { UseBoxCheck(); });

            
        if (useBar)
        {
            updatedSpeed = speedNum * speedSlider.value;
            speedVal.text = "Paddle Speed " + Mathf.FloorToInt(updatedSpeed);
        }
        else
        {
            updatedSpeed = float.Parse(speedBox.text);
            speedVal.text = "Paddle Speed " + Mathf.FloorToInt(updatedSpeed);            
        }

	


        updatedBounce = bounceNum * bounceSlider.value;
        bounceVal.text = "Ball Mass " + updatedBounce;
    }


	void endLevel(){
		tmpTimer += 1 * Time.deltaTime;
		if (tmpTimer > 2.1f) {
			GM.instance.bricks = 0;
			GM.instance.CheckGameOver ();
		}
	}

    public void UseBoxCheck()
    {
        useBar = false;
    }

    public void AddLife()
    {
        GM.instance.lives += 1;
        GM.instance.livesText.text = "Lives: " + GM.instance.lives;
    }

    public void MassReset()
    {
        GM.instance.LoseLife();            
	}

	public void setPhysics(){


		//temporary just win the level button
		GM.instance.bricks = 0;
		GM.instance.CheckGameOver();
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
