using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialHandler : MonoBehaviour
{

	public static TutorialHandler Instance;
	int indexNumber;
    public GameObject NextButton;
    public GameObject CloseButton;
	bool readyToClose;
	string[] levelLineBank;

    // Start is called before the first frame update
    void Awake()
    {
    	Instance = this;
    }
    public void TutorialCheck(int levelnumber){
        if (TutorialDictionary.tutDic.ContainsKey(levelnumber))
            PrepareTutorial(TutorialDictionary.tutDic[levelnumber], 0);

    }
    public void PrepareTutorial(string[] lineBank, int curLine){
        Debug.Log("PREPARING TUTORIAL");
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
    	levelLineBank = lineBank;
    	indexNumber = curLine;
    	ShowTutorialText(lineBank[curLine]);
    	if(curLine<lineBank.Length-1){
    		NextButton.SetActive(true);
            CloseButton.SetActive(false);
    	}
    	else{
    		NextButton.SetActive(false);
            CloseButton.SetActive(true);
    	}
    }

    public void ShowTutorialText(string phrase){
		this.gameObject.transform.GetChild(1).GetComponent<Text>().text = phrase;
    }

    private void PrepareNextLine(){
    	
    }

    private void PrepareClose(){

    }
    public void TutorialNextButton(){
        indexNumber++;
        PrepareTutorial(levelLineBank, indexNumber);
    }
    public void TutorialClosebutton(){
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        CloseButton.SetActive(false);
        NextButton.SetActive(false);
    }
}
