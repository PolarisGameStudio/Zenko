using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeHandler : MonoBehaviour
{
	public GameObject[] initialObjects;
	public GameObject[] Modes;
	public GameObject addButton;
    public GameObject menuButton;
	public static GameModeHandler Instance;
	bool initialized;
	int currentMode;
    // Start is called before the first frame update
    void Start()
    {
    	Instance = this;   
    	initialized = false;
    	currentMode = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!initialized){
	        if(Input.GetMouseButtonDown(0)){
                Debug.Log("INITIALIZED");
	        	TurnOn();
	        	initialized = true;
	        	foreach(GameObject go in initialObjects){
	        		go.SetActive(false);
	        	}
                CameraController.Fade(.2f,.4f, 1);
	        }        	
        }
    }
    public void NextMode(){
    	Modes[currentMode].SetActive(false);
    	Modes[nextInCycle(currentMode)].SetActive(true);
    	currentMode=nextInCycle(currentMode);

    }
    public void PreviousMode(){
    	Modes[currentMode].SetActive(false);
    	Modes[previousInCycle(currentMode)].SetActive(true);
    	currentMode=previousInCycle(currentMode);
    }
    public static void TurnOff(){
    	Instance.transform.GetChild(0).gameObject.SetActive(false);
    	//Instance.addButton.SetActive(false);
        //Instance.menuButton.SetActive(false);
    }
    public static void TurnOn(){
    	Instance.transform.GetChild(0).gameObject.SetActive(true);
    	Instance.addButton.SetActive(true);
        Instance.menuButton.SetActive(true);

    }

    public static void Return(){
        Instance.transform.GetChild(0).gameObject.SetActive(true);
        //Instance.addButton.SetActive(true);
        //Instance.menuButton.SetActive(true);        
    }
    public int nextInCycle(int curPlace){
    	if(curPlace < Modes.Length-1){
    		return curPlace+1;
    	}
    	else{
    		return 0;
    	}
    }
    public int previousInCycle(int curPlace){
    	if(curPlace > 0){
    		return curPlace-1;
    	}
    	else{
    		return Modes.Length-1;
    	}
    }
}
