using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModulateThis : MonoBehaviour
{
    public Image image;
	public Text text;
    public bool isImage;
    // Start is called before the first frame update


    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isImage)
		text.color = Color.HSVToRGB((float)TextModulator.hue/359,(float)TextModulator.s/99, (float)TextModulator.v/99);
        else
        image.color = Color.HSVToRGB((float)TextModulator.hue/359,(float)TextModulator.s/99, (float)TextModulator.v/99);
    }
}
