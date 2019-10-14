using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextModulator : MonoBehaviour
{
	int hue;
	public int s;
	public int v;
	public static TextModulator Instance;
    // Start is called before the first frame update
    void Start()
    {
    	Instance = this;
       	s = 23;
       	v = 69;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       	hue++;
		if(hue>359){
			hue = 0;
		}
		this.GetComponent<Text>().color = Color.HSVToRGB((float)hue/359,(float)s/99, (float)v/99);

    }
}
