using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bricks : MonoBehaviour {

    public GameObject brickParticle;
	public List<Sprite> SpriteList;

	void Awake(){
		int ranNum = Random.Range (0, 2);
		gameObject.GetComponent<SpriteRenderer> ().sprite = SpriteList [ranNum];
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        Instantiate(brickParticle, transform.position, Quaternion.identity);
        GM.instance.DestroyBrick();
        Destroy(gameObject);
    }
}
