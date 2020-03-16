using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwitcher : MonoBehaviour
{
	public Sprite englishSprite;
	public Sprite spanishSprite;
	public Image mySpriteHolder;
	//public enum Language{Spanish, English};
	LanguageHandler.Language currentLanguage;
    // Start is called before the first frame update
    void Start()
    {
        currentLanguage = LanguageHandler.Instance.userLanguage;
        AssignSprite();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentLanguage != LanguageHandler.Instance.userLanguage){
        	AssignSprite();
        	currentLanguage = LanguageHandler.Instance.userLanguage;
        }
    }
    void AssignSprite(){
        if(LanguageHandler.IsEnglish())
        mySpriteHolder.sprite = englishSprite;
        else
        mySpriteHolder.sprite = spanishSprite;    	
    }
}
