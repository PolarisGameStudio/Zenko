using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorModulator : MonoBehaviour {
	public List<GameObject> ImageDudes = new List<GameObject>();
	public List<GameObject> LetterDudes = new List<GameObject>();
	public List <GameObject> LightDudes = new List<GameObject>();
	int hue;
	public GameObject ButtonParent;
	LevelMenu ButtonHolder;
	public GameObject Star;
	bool up;
	
	void Start () {
		ButtonHolder = ButtonParent.GetComponent<LevelMenu>();
		up = true;
		hue = 30;
	}
	
	void FixedUpdate () {
		hue++;
		if(hue>359){
			hue = 0;
		}
		modifyAll();
	}
	void modifyAll(){
		for(int i = 0; i<ImageDudes.Count; i++){
			ImageDudes[i].GetComponent<Image>().color = Color.HSVToRGB((float)hue/359,(float)10/255, (float)255/255);
		}
		for(int i = 0; i<ButtonHolder.levelbuttons.Count; i++){
			ButtonHolder.levelbuttons[i].transform.Find("GlassOverlay").GetComponent<Image>().color = Color.HSVToRGB((float)hue/359,(float)13/255, (float)255/255);
			ButtonHolder.levelbuttons[i].transform.GetComponent<Image>().color = Color.HSVToRGB((float)hue/359,(float)13/255, (float)255/255);
		
		}
		for(int i = 0; i< LetterDudes.Count; i++){
			Color thecolor = Color.HSVToRGB((float)hue/359,(float)43/255, (float)248/255);
			thecolor.a = (float)167/255;
			LetterDudes[i].GetComponent<Text>().color = thecolor;
		}
		Star.GetComponent<Image>().color = Color.HSVToRGB((float)hue/359,(float)10/255, (float)255/255);
	}
}
