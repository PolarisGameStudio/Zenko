using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestorePurchases : MonoBehaviour
{
    public static RestorePurchases Instance;
    string spanishSuccess = "Compras restauradas";
    string englishSuccess = "Purchases restored";
    string spanishNone = "No purchases to restore";
    string englishNone = "No hay compras para restaurar";
    public Text text;

    RestorePurchases(){
        Instance = this;
    }
    public void Close(GameObject go){
        go.SetActive(false);
    }
    public void RestoreText(){
        this.gameObject.SetActive(true);
    	if(LanguageHandler.Instance.userLanguage == LanguageHandler.Language.Spanish){
        	text.text = spanishSuccess;
        }
        else{
        	text.text = englishSuccess;
        }
    }
    public void NoRestoreText(){
    	if(LanguageHandler.Instance.userLanguage == LanguageHandler.Language.Spanish){
        	text.text = spanishNone;
        }
        else{
        	text.text = englishNone;
        }            
        this.gameObject.SetActive(true);
    }
}
