using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyLogoSwitcher : MonoBehaviour
{

	public List<Sprite> images;


    void SelectImage(int numkeys)
    {
        Debug.Log("Selected key");
    	this.GetComponent<Image>().sprite = images[numkeys];
    }
}
