﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoAdScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(LevelManager.adFree);
        if(LevelManager.adFree){
        
        	//this.gameObject.SetActive(false);
            this.GetComponent<Image>().color = Color.blue;
            this.gameObject.SetActive(false);

        }
        if(LevelManager.adFree == false){
            this.GetComponent<Image>().color = Color.white;
        }
    }
}
