﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPad : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		HideIfClickedOutside(this.gameObject);
	}
    private void HideIfClickedOutside(GameObject panel) {
    	Debug.Log( Input.mousePosition);
    	//Debug.Log()
        if (Input.GetMouseButton(0) && panel.activeSelf && 
            !RectTransformUtility.RectangleContainsScreenPoint(
                panel.GetComponent<RectTransform>(), 
                Input.mousePosition, 
                Camera.main)) {
            panel.SetActive(false);
        }
    }
}
