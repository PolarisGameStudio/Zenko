using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextModulator : MonoBehaviour
{
	public static int hue;
	public static int s;
	public static int v;
    // Start is called before the first frame update
    void Start()
    {
        hue =131;
       	s = 23;
       	v = 82;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    hue++;
		if(hue>359){
			hue = 0;
		}
    }
}
