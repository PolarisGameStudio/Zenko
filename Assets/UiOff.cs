using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiOff : MonoBehaviour
{
	public List<Image> images = new List<Image>();
	public Transform dotHolder;
	public Transform otherName;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    	#if UNITY_STANDALONE || UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.X)){
        	TurnOff();
        }
        if(Input.GetKeyDown(KeyCode.Z)){
        	TurnOn();
        }  
        #endif      
    }
    public void TurnOff(){
    	foreach(Image image in images){
    		image.enabled = false;
    	}
    	Image[] allChildren = dotHolder.GetComponentsInChildren<Image>();
    	foreach(Image child in allChildren){
    		child.enabled = false;
    	}
    	allChildren = otherName.GetComponentsInChildren<Image>();
    	foreach(Image child in allChildren){
    		child.enabled = false;
    	}
    }
    public void TurnOn(){
    	foreach(Image image in images){
    		image.enabled = true;
    	}
    	Image[] allChildren = dotHolder.GetComponentsInChildren<Image>();
    	foreach(Image child in allChildren){
    		child.enabled = true;
    	}
    	allChildren = otherName.GetComponentsInChildren<Image>();
    	foreach(Image child in allChildren){
    		child.enabled = true;
    	}
    }
}
