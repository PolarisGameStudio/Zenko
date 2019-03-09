using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class HueModifier : MonoBehaviour {
//	Image myimage;
//	Color mycolor;
	float modifier;
	int hue;
	int s;
	int v;
	// Use this for initialization
	void Start () {
		//mycolor = myimage.Color;
		hue = 0;
		s = 13;
		v = 202;
		
	}
	
	// Update is called once per frame
	void Update () {
		hue++;
		if(hue>359){
			hue = 0;
		}
		this.transform.GetComponent<Image>().color = Color.HSVToRGB((float)hue/359,(float)13/255, (float)202/255);
		Debug.Log(hue);

	}


}
