using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintMenuHandler : MonoBehaviour
{
	public static HintMenuHandler Instance;
	public static int hintsAvailable;
	public Text titleText;
	public Text adText;
    public Text counterText;
	public Button adButton;
	public bool isAdLoaded;
	public string lastDate;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Initialize(){
    	PopulateKeys();
    	titleText.text = "Hint";

    	//if(!GoogleAds.Instance.rewardVideo.IsLoaded()){
		counterText.GetComponent<Text>().text = "x " + hintsAvailable.ToString();

    	//}
    	if(hintsAvailable==0){
    		adButton.interactable = false;
    		adText.text = "More ads\navailable tomorrow";

    	}
    	else{
    		adButton.interactable = true;
    	}


    }
    private void PopulateKeys(){
		if(!PlayerPrefs.HasKey("HintsAvailable")){
        	Debug.Log("initing hints");
        	PlayerPrefs.SetInt("HintsAvailable", 3);
        }
        if(PlayerPrefs.HasKey("HintsAvailable")){
        	Debug.Log("haslastkeydate");
        	lastDate = PlayerPrefs.GetString("LastKeyDate");
        	if(lastDate == DateChecker.todayDate){
        		Debug.Log("SAME DTE");
        		hintsAvailable = PlayerPrefs.GetInt("HintsAvailable");
        	}
        	else{
        		Debug.Log("diff date than ppefs");
        		PlayerPrefs.SetString("LastKeyDate", DateChecker.todayDate);
        		hintsAvailable = 3;
        	}
        }
        else{
        	Debug.Log("no keydate in ppefs");
        	PlayerPrefs.SetString("LastKeyDate", DateChecker.todayDate);
        	hintsAvailable = 3;
        }
    }
}
