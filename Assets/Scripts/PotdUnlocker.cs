using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotdUnlocker : MonoBehaviour
{	
	public static PotdUnlocker Instance;
	public int keysAvailable;
	public Text keysText;
	public static bool activated;

    public void Awake(){
    	if(Instance != this){
    		Instance = this;
    		Initiate();
    	}
    }

    public void Initiate()
    {
		Debug.Log("Initiating with " + keysAvailable + " available keys.");
    	string lastDate;
        if(!PlayerPrefs.HasKey("KeysAvailable")){
        	PlayerPrefs.SetInt("KeysAvailable", 3);
            keysAvailable = 3;
            PlayerPrefs.SetString("LastKeyDate", DateChecker.todayDate);
            keysText.text = "x" + keysAvailable.ToString();
            return;
        }
        if(PlayerPrefs.HasKey("LastKeyDate")){
        	lastDate = PlayerPrefs.GetString("LastKeyDate");
        	if(lastDate == DateChecker.todayDate){
        		keysAvailable = PlayerPrefs.GetInt("KeysAvailable");
        	}
        	else{
        		PlayerPrefs.SetString("LastKeyDate", DateChecker.todayDate);
        		keysAvailable = 3;
                PlayerPrefs.SetInt("KeysAvailable", 3);
        	}
        }
        keysText.text = "x" + keysAvailable.ToString();        
		Debug.Log("Finished initiating with " + keysAvailable + " keys available.");
    }

    public void UnlockCurrent(){
        GoogleAds.Instance.UserOptToOpenPotd();
    }

    public void RemoveOneKey(){
    	if(keysAvailable!=0){
	    	keysAvailable --;
	    	PlayerPrefs.SetInt("KeysAvailable", keysAvailable);	
	    	keysText.text = "x" + keysAvailable.ToString();
    	}
    }
}
