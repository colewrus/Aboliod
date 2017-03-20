using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public static Ball instance;
    public float ballInitialVelocity;
	public float speed;
    private Rigidbody2D rb;
    public bool ballInPlay;
    private Vector3 mousePosition;
    public float ballSpeed;

    // Use this for initialization
    void Awake () {
        rb = GetComponent<Rigidbody2D>();	
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<Rigidbody2D>().mass = SoundManager.Instance.updatedBounce;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 ballStart = new Vector2(transform.position.x, transform.position.y);

		//print (this.gameObject.GetComponent<Rigidbody2D> ().velocity.y);
        if (Input.GetButtonDown("Fire1") && ballInPlay == false)
        {
			if (GM.instance.blackoutCurtain.activeInHierarchy == false && paddle.instance.launchBool) {
				transform.parent = null;
				ballInPlay = true;
				rb.isKinematic = false;
				Vector2 direction = mousePos - ballStart;
				rb.velocity = direction.normalized * ballSpeed;
			}
            

        }


		if (ballInPlay == true) {
			
			transform.Rotate (Vector3.forward * 5);
			float tempY = this.gameObject.GetComponent<Rigidbody2D> ().velocity.y;
			if (tempY > 0 && tempY < 90) {
				rb.AddForce (new Vector2 (0, 95f));
			} else if (tempY < 0 && tempY > -90) {
				rb.AddForce (new Vector2 (0, -95));
			}
		}
			
	}

}
