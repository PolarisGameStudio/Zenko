using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotdShortcut : MonoBehaviour
{
	public Image newPotd;
	public Image keyNumber;
	public Sprite[] keySprites;
	public static PotdShortcut Instance;
    // Start is called before the first frame update
    void Awake(){
    	Instance = this;
    }
    void Start()
    {
        AssignPotdShortcutAssets(PotdUnlocker.Instance.keysAvailable);
    }
    bool IsTodayNew(){
    	return true;
    }
    public void AssignPotdShortcutAssets(int keys)
    {
    	if(LevelManager.adFree){
    		keyNumber.enabled = false;
    	}
    	else{
	    	if(keys == 0){
	    	keyNumber.sprite = null;
	    	keyNumber.enabled = false;
	    	}
	    	else{
	    		keyNumber.sprite = keySprites[keys-1];
	    		keyNumber.enabled = true;
	    	}    		
    	}
    	if(LevelStorer.potdDic[DateChecker.Instance.todayIndex].islocked == true){
            if(LevelStorer.potdDic[DateChecker.Instance.todayIndex].isNew == true)
                newPotd.enabled = true;
            else
                newPotd.enabled = false;
    	}
    	else{
    		newPotd.enabled = false;
    	}
    }

}
