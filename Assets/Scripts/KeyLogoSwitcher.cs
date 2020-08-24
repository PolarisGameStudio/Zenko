using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyLogoSwitcher : MonoBehaviour
{

	public List<Sprite> images;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectImage(int numkeys)
    {
    	this.GetComponent<Image>().sprite = images[numkeys];
    }
}
