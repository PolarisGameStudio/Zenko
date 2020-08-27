using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageHandler : MonoBehaviour
{
	public static LanguageHandler Instance;
	public enum Language{Spanish, English};
	public Language userLanguage = Language.English;

	void Awake()
	{
        if(Instance == null){
            Instance = this;
        }
		
        else{
            Destroy(this);
        }
	}
    // Start is called before the first frame update
    void Start()
    {
    	if(PlayerPrefs.HasKey("Language")){
			if(PlayerPrefs.GetString("Language") == "Spanish")
	        {
	        	GetSpanish();
	        }
	        else
	        {
	        	GetEnglish();
	        }	
    	}
        
    	else{
    		if(Application.systemLanguage == SystemLanguage.Spanish){
    			GetSpanish();
    		}
    		else
    		{
    			GetEnglish();
    		}
    	}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetSpanish()
    {
    	userLanguage = Language.Spanish;
        if(!SceneLoading.Instance.isMenu)
        {
            if(!LevelManager.ispotd)
            SceneLoading.Instance.AssignLevelName();
            else
            SceneLoading.Instance.AssignPotdName();
        }
        else
        {

        }
        PlayerPrefs.SetString("Language", "Spanish");
        AwakeText[] awakeTexts = FindObjectsOfType(typeof(AwakeText)) as AwakeText[];
        foreach(AwakeText at in awakeTexts){
            at.SelectLanguage();
        }
        SpriteSwitcher[] spriteSwitchers = FindObjectsOfType(typeof(SpriteSwitcher)) as SpriteSwitcher[];
        foreach(SpriteSwitcher ss in spriteSwitchers){
            ss.AssignSprite();
        }
    }
    public void GetEnglish()
    {

        userLanguage = Language.English;
        if(!SceneLoading.Instance.isMenu)
        {
            if(!LevelManager.ispotd)
            SceneLoading.Instance.AssignLevelName();
            else
            SceneLoading.Instance.AssignPotdName();
        }
        else
        {

        }
        PlayerPrefs.SetString("Language", "English");
        AwakeText[] awakeTexts = FindObjectsOfType(typeof(AwakeText)) as AwakeText[];
        foreach(AwakeText at in awakeTexts){
            at.SelectLanguage();
        }
        SpriteSwitcher[] spriteSwitchers = FindObjectsOfType(typeof(SpriteSwitcher)) as SpriteSwitcher[];
        foreach(SpriteSwitcher ss in spriteSwitchers){
            ss.AssignSprite();
        }
    }
    public void WriteTexts()
    {

    }
    public static bool IsEnglish(){
        if(Instance== null){
            return true;
        }
        else{
            if(Instance.userLanguage == Language.English)
                return true;
            else
                return false;
        }

    }

    
}
