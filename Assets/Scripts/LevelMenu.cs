using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour {
	int levels;
	public GameObject buttonprefab;
	public List<GameObject> levelbuttons = new List<GameObject>();
	public GameObject mc;//maincanvas
	public SceneLoading sl;
	int buttonnum;
	public int currentfirst;
	public static int highestLevelSolved;
	public GameObject highestMarker;
	public static LevelMenu Instance;
	public GameObject worldTextHolder;
	public GameObject buttonBack;
	public GameObject buttonForward;

	public Image monthSprite;
	public Image yearSprite;
	// Use this for initialization
	void Awake(){
		Instance = this;
		//Debug.Log(LevelMenu.Instance);
	}
	void Start () {
		levels=20;
		sl = mc.GetComponent<SceneLoading>();
		/*for (int i = 1; i < 21; i++){
			createButton(i);

		}
		*/
		//currentfirst = 1;
		//Debug.Log("CURRENTFIRST" + currentfirst);
		//LevelMenu.highestLevelSolved = FindHighestSolved();
		//Debug.Log("CURHIGHEST IS"  + FindHighestSolved());
	}

	// Update is called once per frame
	public static int FindHighestSolved(){
		int curHighest = 0;
		int maxMaps = LevelStorer.leveldic.Count;
		for(int i=1; i<maxMaps+1; i++){
			string mystring = "Level"+i+"Rating";
			//Debug.Log(PlayerPrefs.GetInt(mystring));
			if(PlayerPrefs.GetInt(mystring)>0){
				curHighest = i;
			}	
		}
		//Debug.Log(curHighest+1 + " IS CURHIGHEST");
		return curHighest+1;	
	}
	void createPotdButton(int num){
		GameObject curbutton = Instantiate(buttonprefab, new Vector3(0, 0, 0), Quaternion.identity);
		curbutton.transform.SetParent(this.transform, false);
		curbutton.transform.localScale = new Vector3(1,1,1);
		Button btn = curbutton.GetComponent<Button>();
		buttonnum = num;

		//HAY QUE CAMBIAR ESTA FUNCION


		int world = int.Parse(DateChecker.Instance.mmyyyy);
		//int levelinworld = num;
		//txt.text = "World " + world.ToString() + "-" + levelinworld.ToString();		
		curbutton.transform.GetChild(0).GetComponent<Text>().text = (num+1).ToString();	

		//Debug.Log(LevelManager.adFree + "IS ADFREE");
		//if(ShouldShowPotd());
		//Debug.Log(DateChecker.Instance.dayInMonth + " " + num);
		//LevelManager.adFree = false;
		if(LevelManager.adFree){
			if(num+1 <= DateChecker.Instance.dayInMonth || DateChecker.currentMonthIndex<DateChecker.todayMonthIndex){
				btn.onClick.AddListener(delegate{sl.LoadPotdMap(num + 
				PotdHolder.monthBank[DateChecker.currentMonthIndex][0]);}); 
			}
			else{
				curbutton.transform.GetChild(1).gameObject.SetActive(true);	
			 	curbutton.transform.GetChild(2).gameObject.SetActive(true);	
			}
		}

		else{
			if(num+1 == DateChecker.Instance.dayInMonth){
				btn.onClick.AddListener(delegate{sl.LoadPotdMap(num + 
				PotdHolder.monthBank[DateChecker.currentMonthIndex][0]);}); 
			}
			else{
				curbutton.transform.GetChild(1).gameObject.SetActive(true);	
			 	curbutton.transform.GetChild(2).gameObject.SetActive(true);	
			}
		}

		// if(num+1 <= DateChecker.Instance.dayInMonth || DateChecker.currentMonthIndex<DateChecker.todayMonthIndex){

		// 	if(LevelManager.adFree  || num+1 == DateChecker.Instance.dayInMonth)
		// 		btn.onClick.AddListener(delegate{sl.LoadPotdMap(num + 
		// 		PotdHolder.monthBank[DateChecker.currentMonthIndex][0]);}); 
		// 	else{
		// 		curbutton.transform.GetChild(1).gameObject.SetActive(true);	
		// 	 	curbutton.transform.GetChild(2).gameObject.SetActive(true);					
		// 	}
		// }

		// else{
		//  	curbutton.transform.GetChild(1).gameObject.SetActive(true);	
		//  	curbutton.transform.GetChild(2).gameObject.SetActive(true);	
		// }
		

		levelbuttons.Add(curbutton);
	}

	void createButton(int num){
		GameObject curbutton = Instantiate(buttonprefab, new Vector3(0, 0, 0), Quaternion.identity);
		curbutton.transform.SetParent(this.transform, false);
		curbutton.transform.localScale = new Vector3(1,1,1);
		Button btn = curbutton.GetComponent<Button>();
		buttonnum = num;
		btn.onClick.AddListener(delegate{sl.LoadLevel(num);});
		int world = Mathf.FloorToInt((num-1)/40)  + 1;
		int levelinworld = num - ((world-1)*40);
		//txt.text = "World " + world.ToString() + "-" + levelinworld.ToString();		
		curbutton.transform.GetChild(0).GetComponent<Text>().text =levelinworld.ToString();	

		if(LevelStorer.leveldic[num].islocked == true && num != 0){
			curbutton.transform.GetChild(1).gameObject.SetActive(true);	
			curbutton.transform.GetChild(2).gameObject.SetActive(true);	
		}
		if(LevelStorer.leveldic[num].rating == 1){
			curbutton.transform.GetChild(3).GetChild(1).GetChild(1).gameObject.SetActive(true);
		}
		if(LevelStorer.leveldic[num].rating == 2){
			curbutton.transform.GetChild(3).GetChild(1).GetChild(0).gameObject.SetActive(true);
			curbutton.transform.GetChild(3).GetChild(1).GetChild(2).gameObject.SetActive(true);
		}
		if(LevelStorer.leveldic[num].rating == 3){
			curbutton.transform.GetChild(3).GetChild(1).GetChild(0).gameObject.SetActive(true);
			curbutton.transform.GetChild(3).GetChild(1).GetChild(1).gameObject.SetActive(true);
			curbutton.transform.GetChild(3).GetChild(1).GetChild(2).gameObject.SetActive(true);
		}			

		levelbuttons.Add(curbutton);
	}

	public void CheckDownUpButtons(int curfirst){
		//Debug.Log(curfirst);
		if(curfirst == 1){
			buttonBack.SetActive(false);
			buttonForward.SetActive(true);
			return;
		}
		else if(curfirst == 141){
			buttonBack.SetActive(true);
			buttonForward.SetActive(false);
		}
		else{
			buttonBack.SetActive(true);
			buttonForward.SetActive(true);
		}
	}

	public void AssignWorldText(int curfirst){
		CameraController.Fade(.2f,1f, curfirst);
		CheckDownUpButtons(curfirst);

	}
	void addFunction(){
		sl.LoadLevel(buttonnum);
	}

	public void clearMenu(){
		for (int i = 0; i < levelbuttons.Count; i++){
			levelbuttons[i].SetActive(false);
			Destroy(levelbuttons[i]);

		}
		levelbuttons.Clear();
	}
	public void populateMenuDown(){
		if(currentfirst >20){
			clearMenu();
			for (int i = currentfirst-20; i < currentfirst; i++){
				createButton(i);

			}
			currentfirst = currentfirst -20;	
		}
		AssignWorldText(currentfirst);
	}
	public void populateMenu(){
		//Debug.Log(currentfirst + " is starting first");
		if(currentfirst==0 | currentfirst==null){
			currentfirst = 1;
		}
		for (int i = currentfirst; i < currentfirst+20; i++){
			createButton(i);
		}		
		AssignWorldText(currentfirst);
	}

	public void CheckPotdUpDown(){
		if(DateChecker.todayMonthIndex>0){
			buttonBack.SetActive(true);
			buttonForward.SetActive(false);
		}
		if(DateChecker.currentMonthIndex<DateChecker.todayMonthIndex){
			buttonForward.SetActive(true);
			buttonBack.SetActive(false);
			if(DateChecker.currentMonthIndex>0)
			buttonBack.SetActive(true);
		}
		if(DateChecker.todayMonthIndex == 0){
			buttonBack.SetActive(false);
			buttonForward.SetActive(false);
		}
	}


	public void populateMenuUp(){
		if(currentfirst <181){
			clearMenu();
			for (int i = currentfirst+20; i < currentfirst+40; i++){
				createButton(i);

			}
			currentfirst = currentfirst + 20;	
		}
		AssignWorldText(currentfirst);
	}

	public void AssignMonthText(){
		int monthnum = (DateChecker.currentMonthIndex+10) % 12;
		Debug.Log((int)11/12);
		monthSprite.sprite = PotdHolder.Instance.monthSprites[monthnum];
		yearSprite.sprite = PotdHolder.Instance.yearSprites[(int)(DateChecker.currentMonthIndex+10)/12];
	}

	public void populatePotdMenu(){
		DateChecker.currentMonthIndex = DateChecker.todayMonthIndex;
		int potdFirst = PotdHolder.monthBank[DateChecker.todayMonthIndex][0];
		int numberOfLevels = PotdHolder.monthBank[DateChecker.todayMonthIndex][1];

		for(int i = 0; i<numberOfLevels; i++){
			createPotdButton(i);
		}
		AssignMonthText();
		CheckPotdUpDown();
	}

	public void populatePotdUp(){
		clearMenu();
		DateChecker.currentMonthIndex ++;
		//int potdFirst = PotdHolder.monthBank[DateChecker.currentMonthIndex][0]
		int numberOfLevels = PotdHolder.monthBank[DateChecker.currentMonthIndex][1];

		for(int i = 0; i<numberOfLevels;i++){
			createPotdButton(i);
		}
		AssignMonthText();
		CheckPotdUpDown();
	}

	public void populatePotdDown(){
		clearMenu();
		DateChecker.currentMonthIndex --;
		//int potdFirst = PotdHolder.monthBank[DateChecker.currentMonthIndex][0]
		int numberOfLevels = PotdHolder.monthBank[DateChecker.currentMonthIndex][1];

		for(int i = 0; i<numberOfLevels;i++){
			createPotdButton(i);
		}
		AssignMonthText();
		CheckPotdUpDown();
	}
}
