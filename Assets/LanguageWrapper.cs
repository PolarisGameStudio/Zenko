using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageWrapper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetSpanish(){
    	LanguageHandler.Instance.GetSpanish();
    }
    public void SetEnglish(){
    	LanguageHandler.Instance.GetEnglish();
    }
}
