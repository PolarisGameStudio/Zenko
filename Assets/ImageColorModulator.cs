using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageColorModulator : MonoBehaviour
{
	int hue;
	public int s;
	public int v;
	public Image image;
    // Start is called before the first frame update
    void Start()
    {
        hue =131;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    hue++;
		if(hue>359){
			hue = 0;
		}
		image.color = Color.HSVToRGB((float)hue/359,(float)s/99, (float)v/99);

    }
}
