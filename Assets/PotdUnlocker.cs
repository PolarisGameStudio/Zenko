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
	//public int newkeys;
    // Start is called before the first frame update
    public void Awake(){
    	if(Instance != this){
    		Instance = this;

    		if(activated){
    			Initiate();
    		}

    		activated = true;

    	}

    	
    }
    public void Initiate()
    {
    	string lastDate;
        Debug.Log(DateChecker.todayDate);
        Debug.Log(PlayerPrefs.GetString("LastKeyDate"));
        if(!PlayerPrefs.HasKey("KeysAvailable")){
        	Debug.Log("initing keys");
        	PlayerPrefs.SetInt("KeysAvailable", 3);
            keysAvailable = 3;
            PlayerPrefs.SetString("LastKeyDate", DateChecker.todayDate);
            keysText.text = "x" + keysAvailable.ToString();
            return;
        }
        if(PlayerPrefs.HasKey("LastKeyDate")){
        	Debug.Log("haslastkeydate");
        	lastDate = PlayerPrefs.GetString("LastKeyDate");
        	if(lastDate == DateChecker.todayDate){
        		Debug.Log("SAME DTE");
        		keysAvailable = PlayerPrefs.GetInt("KeysAvailable");
        	}
        	else{
        		Debug.Log("diff date than ppefs");
        		PlayerPrefs.SetString("LastKeyDate", DateChecker.todayDate);
        		keysAvailable = 3;
                PlayerPrefs.SetInt("KeysAvailable", 3);
        	}
        }
        keysText.text = "x" + keysAvailable.ToString();

       // Debug.Log("keys : " + keysAvailable);
        
    }

    // Update is called once per frame
    void Update()
    {
    	//PlayerPrefs.SetString

    }

    void GetKeysFromDate(string mmyyyy){

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
