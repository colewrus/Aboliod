using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class paddle : MonoBehaviour {
    public static paddle instance = null;
    public float paddleSpeed;
	public float paddleDeflect;



    private Vector2 playerPos = new Vector2(-5, -75);
    private Vector3 mousePosition;
    private Vector3 cameraPos;
	public bool launchBool;




    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
		//launchBool = GameObject.Find ("persistentManager").GetComponent<persistentVariables> ().directPhysics;
		launchBool = false;
    }


    void Start () {
      	transform.position = playerPos;   
		launchBool = false;
	}
	
	// Update is called once per frame
	void Update () {
		
        paddleSpeed = SoundManager.Instance.updatedSpeed;   

        if (Input.GetMouseButton(0) )
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            
            if(EventSystem.current.IsPointerOverGameObject() == false && launchBool == true)
            {
				Vector3 constrain_Min = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 5));
				Vector3 constrain_Max = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, 0, 5));
				playerPos = new Vector2(Mathf.Clamp(mousePosition.x, constrain_Min.x + (this.GetComponent<SpriteRenderer>().bounds.size.x/2), constrain_Max.x-(this.GetComponent<SpriteRenderer>().bounds.size.x/2)), -75);           
                transform.position = Vector2.MoveTowards(transform.position, playerPos, paddleSpeed * Time.deltaTime);
            }
			if (Panel_Text_Control.launch_release == true) {
				launchBool = true;
			}
            
        }

        if (GM.instance.RecayNow) {
            if(Time.timeScale < 1)
            {
                Time.timeScale += 0.5f * Time.deltaTime;                     
            }
        }       

	}

    /*
	void OnCollisionEnter2D(Collision2D other){
		//float difference = other.contacts [0].point.x - transform.position.x;
		Vector2 delta = other.transform.position - transform.position;
		///!--------------IS YOUR yDelta SET TO BETWEEN 0 & 1??!?!?!?!--------------------------!
		Vector2 direction = new Vector2 (Mathf.Clamp (delta.normalized.x, -0.8f, 0.8f), Mathf.Clamp(delta.normalized.y, 0.85f, 1.0f));
		other.gameObject.GetComponent<Rigidbody2D> ().velocity = direction * paddleDeflect;
	}
    */


}
