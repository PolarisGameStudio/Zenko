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
	/*int ImageS;
	int LetterS;
	int LightS;*/

	// Use this for initialization
	void Start () {
		ButtonHolder = ButtonParent.GetComponent<LevelMenu>();
		up = true;
		hue = 30;
	}
	
	// Update is called once per frame
	void Update () {
		hue++;
		if(hue>359){
			hue = 0;
		}
		/*if(up){
			if(hue<200){
				hue++;
			}
			else{
				up = false;
			}
		}
		else if(!up){
			if(hue>30){
				hue--;
			}
			else{
				up =true;
			}
		}*/

		modifyAll();
//		Debug.Log(ButtonHolder.levelbuttons.Count);
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
			//LetterDudes[i].GetComponent<Text>().color = Color.HSVToRGB((float)hue/359,(float)43/255, (float)248/255);
			//LetterDudes[i].GetComponent<Text>().color.a = (float)167/255;

		}
		Star.GetComponent<Image>().color = Color.HSVToRGB((float)hue/359,(float)10/255, (float)255/255);
		//for
	}
}
