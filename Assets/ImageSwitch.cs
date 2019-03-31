using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSwitch : MonoBehaviour {
	public Sprite imageone;
	public Sprite imagetwo;
	public string type;
	// Use this for initialization
	void Start () {
		initSprite(type);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void initSprite(string name){
		string path = name + " Source";
		if(GameObject.Find(path).GetComponent<AudioSource>().mute){
			GetComponent<Image>().sprite = imagetwo;
		}
		else{
			GetComponent<Image>().sprite = imageone;

		}
		Debug.Log(path);
		//GetComponent<Image>().sprite = imageone;
		//if(Scene)
	}
}
