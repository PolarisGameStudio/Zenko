using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuroraModulator : MonoBehaviour {
	public List<GameObject> LightDudes = new List<GameObject>();
	int hue;
	public int s;
	public int v;
	public GameObject Star;

	// Use this for initialization
	void Start () {
		s=80;
		v=240;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		hue++;
		if(hue>359){
			hue = 0;
		}
		Modify();
		Star.GetComponent<Image>().color = Color.HSVToRGB((float)hue/359,(float)10/255, (float)255/255);

	}

	void Modify(){
		LightDudes[0].GetComponent<Light>().color = Color.HSVToRGB((float)hue/359,(float)s/255, (float)v/255);
	}
}
