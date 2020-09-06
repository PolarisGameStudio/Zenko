using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AwakeText : MonoBehaviour
{
	public Text[] texts;
	public enum TextSection{BuyMenu, Options, Tutorial, Hint, UnlockPotd};
	public TextSection mySection;
    LanguageHandler.Language currentLanguage;
    public bool scanOnUpdate;
    // Start is called before the first frame update
    void OnEnable()
    {
        SelectLanguage();
    }

    // Update is called once per frame
    void Start(){
        currentLanguage = LanguageHandler.Instance.userLanguage;
        if(LanguageHandler.IsEnglish())
        SelectEnglish();
        else
        SelectSpanish();
    }

    public void SelectLanguage(){
		//Debug.Log("About to sleect language");
    	if(LanguageHandler.Instance.userLanguage == LanguageHandler.Language.Spanish){
        	SelectSpanish();
        }
        else{
        	SelectEnglish();
        }
    }
    void SelectSpanish()
    {
    	switch (mySection){
    		case TextSection.BuyMenu:
    			for(int i = 0; i<texts.Length; i++)
    			{
    				texts[i].text = TutorialDictionary.Instance.buyMenu_sp[i];
    			}
    			break;
    		case TextSection.Options:
    				texts[0].text = "Musica";
    				texts[1].text = "Sfx";
    			break;
    		case TextSection.Tutorial:
    			break;
    		case TextSection.Hint:
//    		Debug.Log("Ahora en español");
    			for(int i = 0; i<texts.Length; i++)
    			{
    				texts[i].text = TutorialDictionary.Instance.hint_sp[i];
    			}
    			break;
            case TextSection.UnlockPotd:
                texts[0].text = "Ver Anuncio para Abrir";
                texts[1].text = "Cancelar";
                texts[2].text = "Desbloquear Todos";
                break;
    	}
    }
    void SelectEnglish(){
    	switch (mySection){
    		case TextSection.BuyMenu:
    			for(int i = 0; i<texts.Length; i++)
    			{
    				texts[i].text = TutorialDictionary.Instance.buyMenu[i];
    			}    			
    			break;
    		case TextSection.Options:
					texts[0].text = "Music";
    				texts[1].text = "Sfx";
    			break;
    		case TextSection.Tutorial:

    			break;
    		case TextSection.Hint:
//    		Debug.Log("Ahora en ingles");
	    		for(int i = 0; i<texts.Length; i++)
	    			{
	    				texts[i].text = TutorialDictionary.Instance.hint[i];
	    			}
	    		break;
            case TextSection.UnlockPotd:
                texts[0].text = "Watch Ad to Unlock";
                texts[1].text = "Cancel";
                texts[2].text = "Unlock Archive";
                break;
    	}
    }
}
