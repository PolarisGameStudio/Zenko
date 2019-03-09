using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuroraModulator : MonoBehaviour {
	public List<GameObject> LightDudes = new List<GameObject>();
	int hue;
	public int s;
	public int v;

	// Use this for initialization
	void Start () {
		s=44;
		v=255;
	}
	
	// Update is called once per frame
	void Update () {
		hue++;
		if(hue>359){
			hue = 0;
		}
		Modify();
	}

	void Modify(){
		LightDudes[0].GetComponent<Light>().color = Color.HSVToRGB((float)hue/359,(float)s/255, (float)v/255);
	}
}
