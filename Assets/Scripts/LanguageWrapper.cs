using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageWrapper : MonoBehaviour
{
    public void SetSpanish(){
    	LanguageHandler.Instance.GetSpanish();
    }
    public void SetEnglish(){
    	LanguageHandler.Instance.GetEnglish();
    }
}
