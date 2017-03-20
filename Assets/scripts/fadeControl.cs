using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class fadeControl : MonoBehaviour {

    public float alpha;
    public float fadeSpeed;
	public bool awakeStart;
	public bool fadeIn;
	public bool isThisText;
	public float fadeOutSpeed;
	// Use this for initialization
	void Start () {
		if (!fadeIn) {
			alpha = 1;        
		}else {
			alpha = 0;
			if (isThisText) {
				gameObject.GetComponent<Text> ().color = new Color (0, 0, 0, alpha);
			}
		}
    }
	
	// Update is called once per frame
	void Update () {
        
		if (!awakeStart) {
			if (alpha < 1) {
				alpha += fadeSpeed * Time.deltaTime;
				gameObject.GetComponent<Text> ().color = new Color (1, 1, 1, alpha);
			}
			if (paddle.instance.launchBool) {
				awakeStart = true;
			}
		}

		if (awakeStart && !GM.instance.endFade)
        {
			if(alpha > 0){
	            alpha -= fadeSpeed * Time.deltaTime;
				if (!isThisText) {
					gameObject.GetComponent<Image> ().color = new Color (0, 0, 0, alpha);
				} else {
					gameObject.GetComponent<Text> ().color = new Color (1, 1, 1, alpha);
				}
			}
        }

		if(alpha < 0.01f && !fadeIn && !GM.instance.endFade){
            gameObject.SetActive(false);
        }
    }
}
