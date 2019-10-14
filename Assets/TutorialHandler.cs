using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialHandler : MonoBehaviour
{

	public static TutorialHandler Instance;
	int indexNumber;
	public Sprite nextSprite;
	public Sprite closeSprite;
	bool readyToClose;
	string[] levelLineBank;

    // Start is called before the first frame update
    void Start()
    {
    	Instance = this;
    }

    public void PrepareTutorial(string[] lineBank, int curLine){
    	levelLineBank = lineBank;
    	indexNumber = curLine;
    	ShowTutorialText(lineBank[curLine]);
    	if(curLine<lineBank.Length-1){
    		readyToClose = false;
    	}
    	else{
    		readyToClose = true;
    	}
    }

    public void ShowTutorialText(string phrase){
		this.gameObject.transform.GetChild(1).GetComponent<Text>().text = phrase;
    }

    private void PrepareNextLine(){
    	
    }

    private void PrepareClose(){

    }
    public void TuTorialButton(){
    	if(readyToClose){
    		//PrepareTutorial
    	}
    	else{
    		//PrepareTutorial
    	}
    }
}
