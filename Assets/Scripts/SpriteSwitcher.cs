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
        if(mySpriteHolder == null)
        mySpriteHolder = this.GetComponent<Image>();  
        currentLanguage = LanguageHandler.Instance.userLanguage;
        AssignSprite();
    }
    void OnEnable(){
        AssignSprite();
    }

    public void AssignSprite(){
        //Debug.Log("assigning sprite");
        if(mySpriteHolder == null)
        mySpriteHolder = this.GetComponent<Image>();
        if(LanguageHandler.IsEnglish())
        mySpriteHolder.sprite = englishSprite;
        else
        mySpriteHolder.sprite = spanishSprite;    	
    }
}
