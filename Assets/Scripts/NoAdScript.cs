using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoAdScript : MonoBehaviour
{
    bool givenAdFree;
    // Start is called before the first frame update
    void Start()
    {
        if(!LevelManager.adFree){
            this.transform.GetComponent<Image>().enabled = true;
        }
        else{
            this.transform.GetComponent<Image>().enabled = false;
            this.gameObject.SetActive(false);
            givenAdFree = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelManager.adFree && !givenAdFree){
            this.gameObject.SetActive(false);
            givenAdFree = true;
        }
    }
}
