using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurnOffIfAdFree : MonoBehaviour
{
	public Image image;
	public Text text;
    public static TurnOffIfAdFree Instance;
    void Awake(){
        Instance = this;
    }
    void Start()
    {
        SetImages();
    }
    public void SetImages(){
//        Debug.Log(LevelManager.adFree);
        if(LevelManager.adFree){
            this.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
        }
        else{
            image.enabled = true;
            text.enabled = true;
            PotdUnlocker.Instance.Initiate();
            
        }        

    }

}
