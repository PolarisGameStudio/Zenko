using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour {
	int levels;
	public GameObject buttonprefab;
	public List<GameObject> levelbuttons;
	public GameObject mc;//maincanvas
	public SceneLoading sl;
	public int currentfirst;
	public static int highestLevelSolved;
	public GameObject highestMarker;
	public static LevelMenu Instance;
	public GameObject worldTextHolder;
	public GameObject buttonBack;
	public GameObject buttonForward;
	public Sprite[] snowSprites;
	public Image monthSprite;
	public Image yearSprite;
	public int potdFirst;
	public int lowestLocked;
	public GameObject UnlockMenu;
	public GameObject buyMenu;
	private int counter;
	public static int levelToUnlock;

	void Awake(){
		Instance = this;
	}

	void Start () {
		levels=20;
		sl = mc.GetComponent<SceneLoading>();
	}

	//Find level number that is highest solved, for purposes of choosing which screen to spawn in
	public static int FindHighestSolved(){
		int curHighest = 0;
		int maxMaps = LevelStorer.leveldic.Count;
		for(int i=1; i<maxMaps+1; i++){
			string mystring = "Level"+i+"Rating";
			if(PlayerPrefs.GetInt(mystring)>0){
				curHighest = i;
			}	
		}
		return curHighest+1;	
	}
	
	//Called from Google Ads, Unlocks and opens selected potd
	public static void UnlockPotdLevel(int num){
		PotdUnlocker.Instance.keysAvailable--;
		PlayerPrefs.SetInt("KeysAvailable", PotdUnlocker.Instance.keysAvailable);	
		if(SceneLoading.Instance.isMenu){ //if in world select scene
			SceneLoading.Instance.LoadPotdMap(num);
		}
		else{ // if in level scene
			SceneLoading.Instance.PotdSpecific(num);
		}
	}
	
	//Called from UI when pressing a "tap to unlock"
	public void OpenUnlockMenu(int num, int starter){
		if(LanguageHandler.IsEnglish())
		UnlockMenu.transform.Find("Title").GetComponent<Text>().text = "UNLOCK LEVEL " + (num+1).ToString() + "?";
		else
		UnlockMenu.transform.Find("Title").GetComponent<Text>().text = "ABRIR NIVEL " + (num+1).ToString() + "?";
		UnlockMenu.transform.Find("UnlockAdButton").transform.Find("contador").GetComponent<Text>().text = "x " + PotdUnlocker.Instance.keysAvailable.ToString();
		buyMenu.GetComponent<BuyMenu>().AssignTitle("Unlock Archive?");
		if(PotdUnlocker.Instance.keysAvailable>0){
			levelToUnlock = num+starter;			
		}
		else{
			UnlockMenu.transform.Find("UnlockAdButton").transform.Find("Text").GetComponent<Text>().text = "COME BACK TOMORROW";
			UnlockMenu.transform.Find("UnlockAdButton").GetComponent<Button>().interactable = false;

		}
		UnlockMenu.SetActive(true);

	}

	public void CloseUnlockMenu(){
		UnlockMenu.SetActive(false);
		buyMenu.GetComponent<BuyMenu>().AssignTitle("Disable Ads?");
	}


	void createPotdButton(int num){
		GameObject curbutton = levelbuttons[num];
		curbutton.SetActive(true);
		curbutton.transform.GetChild(7).gameObject.SetActive(false);
		curbutton.transform.GetChild(6).gameObject.SetActive(false);
		Button btn = curbutton.GetComponent<Button>();
		int world = int.Parse(DateChecker.Instance.mmyyyy);
		curbutton.transform.GetChild(2).GetComponent<Text>().text = (num+1).ToString();	
		if(LevelManager.adFree){
			if(num+1 <= DateChecker.Instance.dayInMonth || DateChecker.currentMonthIndex<DateChecker.todayMonthIndex){
				LevelStorer.potdDic[num+potdFirst].islocked = false;
				curbutton.GetComponent<LevelButton>().level = num+potdFirst;
				curbutton.GetComponent<LevelButton>().type = "Potd";
				curbutton.transform.GetChild(3).gameObject.SetActive(false);	
			}
			else{
				curbutton.transform.GetChild(3).gameObject.SetActive(true);	
			 	curbutton.transform.GetChild(6).gameObject.SetActive(true);	
			}
		}
		else{
			if((num+1 == DateChecker.Instance.dayInMonth && DateChecker.currentMonthIndex == DateChecker.todayMonthIndex) 
				|| !LevelStorer.potdDic[num+potdFirst].islocked){
				LevelStorer.potdDic[num+potdFirst].islocked = false;
				curbutton.GetComponent<LevelButton>().level = num+potdFirst;
				curbutton.GetComponent<LevelButton>().type = "Potd";
				curbutton.transform.GetChild(3).gameObject.SetActive(false);	
			}
			else{

				curbutton.transform.GetChild(3).gameObject.SetActive(true);	
			 	curbutton.transform.GetChild(6).gameObject.SetActive(true);	
			 	if(num+1 <= DateChecker.Instance.dayInMonth || DateChecker.currentMonthIndex<DateChecker.todayMonthIndex){
		 			curbutton.transform.GetChild(7).gameObject.SetActive(true);	
		 			curbutton.transform.GetChild(6).gameObject.SetActive(false);
		 			curbutton.GetComponent<LevelButton>().level = num;
		 			curbutton.GetComponent<LevelButton>().potdFirst = potdFirst;
					curbutton.GetComponent<LevelButton>().type = "PotdUnlock";
			 	}
			}
		}
		if(LevelStorer.potdDic[num+potdFirst].rating == 1){
			curbutton.transform.GetChild(4).GetChild(1).GetChild(1).gameObject.SetActive(true);
		}
		else if(LevelStorer.potdDic[num+potdFirst].rating == 2){
			curbutton.transform.GetChild(4).GetChild(1).GetChild(0).gameObject.SetActive(true);
			curbutton.transform.GetChild(4).GetChild(1).GetChild(2).gameObject.SetActive(true);
		}
		else if(LevelStorer.potdDic[num+potdFirst].rating == 3){
			curbutton.transform.GetChild(4).GetChild(1).GetChild(0).gameObject.SetActive(true);
			curbutton.transform.GetChild(4).GetChild(1).GetChild(1).gameObject.SetActive(true);
			curbutton.transform.GetChild(4).GetChild(1).GetChild(2).gameObject.SetActive(true);
		}				
	}

	void createButton(int num){
		GameObject curbutton = levelbuttons[counter];
		curbutton.SetActive(true);

		curbutton.transform.GetChild(7).gameObject.SetActive(false);//taptounlock
		curbutton.transform.GetChild(6).gameObject.SetActive(false);//lock
		curbutton.transform.GetChild(3).gameObject.SetActive(false);//glass overlay

		LevelButton currentLevelButton = curbutton.GetComponent<LevelButton>();
		currentLevelButton.level = num;
		currentLevelButton.type = "Adventure";

		int world = Mathf.FloorToInt((num-1)/40)  + 1;  
		curbutton.transform.GetChild(5).GetComponent<Image>().sprite = snowSprites[world-1];//assigns snow sprite according to world (higher world, more melt)                                                                                                                                     
		int levelinworld = num - ((world-1)*40);
		curbutton.transform.GetChild(2).GetComponent<Text>().text =levelinworld.ToString();	//assigns level number

		Transform starHolder = curbutton.transform.GetChild(4).GetChild(1);
		if(LevelStorer.leveldic[num].islocked == true && num != 0){//if locked
			curbutton.transform.GetChild(3).gameObject.SetActive(true);//glass overlay
			curbutton.transform.GetChild(6).gameObject.SetActive(true);//lock
		}
		if(LevelStorer.leveldic[num].rating == 1){
			starHolder.GetChild(1).gameObject.SetActive(true);
		}
		else if(LevelStorer.leveldic[num].rating == 2){
			starHolder.GetChild(0).gameObject.SetActive(true);
			starHolder.GetChild(2).gameObject.SetActive(true);
		}
		else if(LevelStorer.leveldic[num].rating == 3){
			starHolder.GetChild(0).gameObject.SetActive(true);
			starHolder.GetChild(1).gameObject.SetActive(true);
			starHolder.GetChild(2).gameObject.SetActive(true);
		}			
	}

	public void CheckDownUpButtons(int curfirst){
		if(curfirst == 1){
			buttonBack.SetActive(false);
			buttonForward.SetActive(true);
			return;
		}
		else if(curfirst == 181){
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

	public void clearMenu(){
		for (int i = 0; i < levelbuttons.Count; i++){
			levelbuttons[i].GetComponent<LevelButton>().type = null;
			levelbuttons[i].SetActive(false);
			levelbuttons[i].transform.GetChild(4).GetChild(1).GetChild(0).gameObject.SetActive(false);
			levelbuttons[i].transform.GetChild(4).GetChild(1).GetChild(1).gameObject.SetActive(false);
			levelbuttons[i].transform.GetChild(4).GetChild(1).GetChild(2).gameObject.SetActive(false);
		}
		counter = 0;
	}

	public void populateMenuDown(){
		if(currentfirst >20){
			clearMenu();

			for (int i = currentfirst-20; i < currentfirst; i++){
				createButton(i);
				counter++;
			}
			currentfirst = currentfirst -20;	
		}
		AssignWorldText(currentfirst);
	}

	public void populateMenu(){
		clearMenu();
		if(currentfirst==0 | currentfirst==null){
			currentfirst = 1;
		}
		for (int i = currentfirst; i < currentfirst+20; i++){
			createButton(i);
			counter++;
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
		if(currentfirst <221){
			clearMenu();
			for (int i = currentfirst+20; i < currentfirst+40; i++){
				createButton(i);
				counter++;
			}
			currentfirst = currentfirst + 20;	
		}
		AssignWorldText(currentfirst);
	}

	public void AssignMonthText(){
		int monthnum = (DateChecker.currentMonthIndex+10) % 12;
		if(LanguageHandler.IsEnglish())
		{
			monthSprite.sprite = PotdHolder.Instance.monthSprites[monthnum];
		}
		else
		{
			monthSprite.sprite = PotdHolder.Instance.monthSpritesSpanish[monthnum];
		}
		yearSprite.sprite = PotdHolder.Instance.yearSprites[(int)(DateChecker.currentMonthIndex+10)/12];
	}

	public void populatePotdMenu(){
		clearMenu();
		lowestLocked = 0;
		DateChecker.currentMonthIndex = DateChecker.todayMonthIndex;
		potdFirst = PotdHolder.monthBank[DateChecker.todayMonthIndex][0];
		int numberOfLevels = PotdHolder.monthBank[DateChecker.todayMonthIndex][1];
		for(int i = 0; i<numberOfLevels; i++){
			createPotdButton(i);
		}
		PlayServices.instance.SaveLocal();
		PlayServices.instance.SaveData();
		AssignMonthText();
		CheckPotdUpDown();
	}

	public void populatePotdUp(){
		CloseUnlockMenu();
		lowestLocked = 0;
		clearMenu();
		DateChecker.currentMonthIndex ++;
		potdFirst = PotdHolder.monthBank[DateChecker.currentMonthIndex][0];
		int numberOfLevels = PotdHolder.monthBank[DateChecker.currentMonthIndex][1];
		for(int i = 0; i<numberOfLevels;i++)
		{
			createPotdButton(i);
		}
		AssignMonthText();
		CheckPotdUpDown();
	}

	public void populatePotdDown(){
		CloseUnlockMenu();
		lowestLocked = 0;
		clearMenu();
		DateChecker.currentMonthIndex --;
		potdFirst = PotdHolder.monthBank[DateChecker.currentMonthIndex][0];
		int numberOfLevels = PotdHolder.monthBank[DateChecker.currentMonthIndex][1];
		for(int i = 0; i<numberOfLevels;i++)
		{
			createPotdButton(i);
		}
		AssignMonthText();
		CheckPotdUpDown();
	}
}
