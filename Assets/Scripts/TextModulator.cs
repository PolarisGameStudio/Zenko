using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextModulator : MonoBehaviour
{
	public static int hue;
	public static int s;
	public static int v;

//tutorialhandler references this
    void Start()
    {
        hue =131;
       	s = 23;
       	v = 82;
    }

    void FixedUpdate()
    {
    hue++;
		if(hue>359){
			hue = 0;
		}
    }
}
