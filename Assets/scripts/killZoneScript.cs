using UnityEngine;
using System.Collections;

public class killZoneScript : MonoBehaviour {

	void Start(){
		transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width / 2, 0, 5));
	}

    void OnTriggerEnter2D(Collider2D col)
    {
		if(col.gameObject.name == "Ball" && GM.instance.gameWon == false)
        {
            GM.instance.LoseLife();
        }
        
    }
}
