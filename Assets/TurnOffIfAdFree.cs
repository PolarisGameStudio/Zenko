using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurnOffIfAdFree : MonoBehaviour
{
	public Image image;
	public Text text;
    // Start is called before the first frame update
    void Start()
    {
        if(LevelManager.adFree){
        	image.enabled = false;
        	text.enabled = false;
        }
        else{
        	this.gameObject.SetActive(false);
        	text.gameObject.SetActive(false);
        }
    }

}
