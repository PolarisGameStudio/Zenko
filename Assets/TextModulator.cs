using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextModulator : MonoBehaviour
{
	int hue;
	public int s;
	public int v;
	public Text text;
    // Start is called before the first frame update
    void Start()
    {
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
		text.color = Color.HSVToRGB((float)hue/359,(float)s/99, (float)v/99);

    }
}
