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
	int buttonnum;
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
		// foreach(Transform child in transform){
		// 	levelbuttons.Add(child.gameObject);
		// }
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
	public static void UnlockPotdLevel(int num){
		Debug.Log("UNLOCK ACT");
		PotdUnlocker.Instance.keysAvailable--;
		Debug.Log(PotdUnlocker.Instance.keysAvailable);
		PlayerPrefs.SetInt("KeysAvailable", PotdUnlocker.Instance.keysAvailable);	
		SceneLoading.Instance.LoadPotdMap(num);
	}
	public void OpenUnlockMenu(int num, int starter){
		//UnlockMenu = GameObject.Find("UnlockPotdMenu");
		UnlockMenu.transform.Find("Title").GetComponent<Text>().text = "UNLOCK LEVEL " + (num+1).ToString() + "?";
		UnlockMenu.transform.Find("UnlockAdButton").transform.Find("contador").GetComponent<Text>().text = "x " + PotdUnlocker.Instance.keysAvailable.ToString();
		//GameObject.Find()
		//buyMenu = GameObject.Find("")
		buyMenu.GetComponent<BuyMenu>().AssignTitle("Unlock Archive?");
		if(PotdUnlocker.Instance.keysAvailable>0){
			levelToUnlock = num+starter;
			//UnlockMenu.transform.Find("UnlockAdButton").GetComponent<Button>().onClick.AddListener(delegate{GoogleAds.Instance.UserOptToOpenPotd(num+starter);});
			
		}
		else{
			UnlockMenu.transform.Find("UnlockAdButton").transform.Find("Text").GetComponent<Text>().text = "COME BACK TOMORROW";
			UnlockMenu.transform.Find("UnlockAdButton").GetComponent<Button>().interactable = false;

		}
		UnlockMenu.SetActive(true);
		//GoogleAds.Instance.RequestPotdAd();

	}

	public void CloseUnlockMenu(){
		UnlockMenu.SetActive(false);
		buyMenu.GetComponent<BuyMenu>().AssignTitle("Disable Ads?");
	}


	void createPotdButton(int num){
		//GameObject curbutton = Instantiate(buttonprefab, new Vector3(0, 0, 0), Quaternion.identity);
		GameObject curbutton = levelbuttons[num];
		curbutton.SetActive(true);
		curbutton.transform.GetChild(7).gameObject.SetActive(false);
		curbutton.transform.GetChild(6).gameObject.SetActive(false);
		//curbutton.transform.SetParent(this.transform, false);
		//curbutton.transform.localScale = new Vector3(1,1,1);
		Button btn = curbutton.GetComponent<Button>();
		buttonnum = num;

		//HAY QUE CAMBIAR ESTA FUNCION

		//Debug.Log(DateChecker.Instance.mmyyyy + "is mmyyyy world");
		int world = int.Parse(DateChecker.Instance.mmyyyy);
		curbutton.transform.GetChild(2).GetComponent<Text>().text = (num+1).ToString();	

		if(LevelManager.adFree){
			if(num+1 <= DateChecker.Instance.dayInMonth || DateChecker.currentMonthIndex<DateChecker.todayMonthIndex){
				LevelStorer.potdDic[num+potdFirst].islocked = false;
				//btn.onClick.AddListener(delegate{sl.LoadPotdMap(num + potdFirst);}); 
				curbutton.GetComponent<LevelButton>().level = num+potdFirst;
				curbutton.GetComponent<LevelButton>().type = "Potd";
				curbutton.transform.GetChild(3).gameObject.SetActive(false);	
			}
			else{

				curbutton.transform.GetChild(3).gameObject.SetActive(true);	
			 	curbutton.transform.GetChild(6).gameObject.SetActive(true);	
			 	//curbutton.transform.GetChild(7).gameObject.SetActive(true);	
			}
		}

		else{
			//Debug.Log(num + " num plus " + DateChecker.Instance.dayInMonth + DateChecker.currentMonthIndex +DateChecker.todayMonthIndex + potdFirst );
			if((num+1 == DateChecker.Instance.dayInMonth && DateChecker.currentMonthIndex == DateChecker.todayMonthIndex) 
				|| !LevelStorer.potdDic[num+potdFirst].islocked){
				//Debug.Log("UNLOCKING " + num + " in month " + DateChecker.currentMonthIndex );
				LevelStorer.potdDic[num+potdFirst].islocked = false;
				//btn.onClick.AddListener(delegate{sl.LoadPotdMap(num + potdFirst); 
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
		 			//btn.onClick.AddListener(delegate{OpenUnlockMenu(num,potdFirst);});
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
		
		//levelbuttons.Add(curbutton);
	}

	void createButton(int num){
		//GameObject curbutton = Instantiate(buttonprefab, new Vector3(0, 0, 0), Quaternion.identity);
		//curbutton.transform.SetParent(this.transform, false);
		//curbutton.transform.localScale = new Vector3(1,1,1);
		GameObject curbutton = levelbuttons[counter];
		curbutton.SetActive(true);
		curbutton.transform.GetChild(7).gameObject.SetActive(false);
		curbutton.transform.GetChild(6).gameObject.SetActive(false);
		curbutton.transform.GetChild(3).gameObject.SetActive(false);	
		Button btn = curbutton.GetComponent<Button>();
		buttonnum = num;
		//btn.onClick.AddListener(delegate{sl.LoadLevel(num);});
		curbutton.GetComponent<LevelButton>().level = num;
		curbutton.GetComponent<LevelButton>().type = "Adventure";
		int world = Mathf.FloorToInt((num-1)/40)  + 1;
		int levelinworld = num - ((world-1)*40);
		curbutton.transform.GetChild(5).GetComponent<Image>().sprite = snowSprites[world-1];
		//txt.text = "World " + world.ToString() + "-" + levelinworld.ToString();		
		curbutton.transform.GetChild(2).GetComponent<Text>().text =levelinworld.ToString();	

		if(LevelStorer.leveldic[num].islocked == true && num != 0){
			curbutton.transform.GetChild(3).gameObject.SetActive(true);	
			curbutton.transform.GetChild(6).gameObject.SetActive(true);	


		}
		// if(!LevelStorer.leveldic[num].islocked){
		// 	//Debug.Log("NOT LOCKED");
		// }
		if(LevelStorer.leveldic[num].rating == 1){
			curbutton.transform.GetChild(4).GetChild(1).GetChild(1).gameObject.SetActive(true);
		}
		if(LevelStorer.leveldic[num].rating == 2){
			curbutton.transform.GetChild(4).GetChild(1).GetChild(0).gameObject.SetActive(true);
			curbutton.transform.GetChild(4).GetChild(1).GetChild(2).gameObject.SetActive(true);
		}
		if(LevelStorer.leveldic[num].rating == 3){
			curbutton.transform.GetChild(4).GetChild(1).GetChild(0).gameObject.SetActive(true);
			curbutton.transform.GetChild(4).GetChild(1).GetChild(1).gameObject.SetActive(true);
			curbutton.transform.GetChild(4).GetChild(1).GetChild(2).gameObject.SetActive(true);
		}			

		//levelbuttons.Add(curbutton);
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
			levelbuttons[i].GetComponent<LevelButton>().type = null;
			levelbuttons[i].SetActive(false);
			levelbuttons[i].transform.GetChild(4).GetChild(1).GetChild(0).gameObject.SetActive(false);
			levelbuttons[i].transform.GetChild(4).GetChild(1).GetChild(1).gameObject.SetActive(false);
			levelbuttons[i].transform.GetChild(4).GetChild(1).GetChild(2).gameObject.SetActive(false);
			//Destroy(levelbuttons[i]);

		}
		counter = 0;
		//levelbuttons.Clear();
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
		//Debug.Log(currentfirst + " is starting first")
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
		if(currentfirst <181){
			clearMenu();
			for (int i = currentfirst+20; i < currentfirst+40; i++){
				//Debug.Log(i);
				createButton(i);
				counter++;
			}
			currentfirst = currentfirst + 20;	
		}
		AssignWorldText(currentfirst);
	}

	public void AssignMonthText(){
		int monthnum = (DateChecker.currentMonthIndex+10) % 12;
		//Debug.Log((int)11/12);
		monthSprite.sprite = PotdHolder.Instance.monthSprites[monthnum];
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
		PrepareUnlockLowest();
	}

	public void populatePotdUp(){
		CloseUnlockMenu();
		lowestLocked = 0;
		clearMenu();
		DateChecker.currentMonthIndex ++;
		potdFirst = PotdHolder.monthBank[DateChecker.currentMonthIndex][0];
		int numberOfLevels = PotdHolder.monthBank[DateChecker.currentMonthIndex][1];

		for(int i = 0; i<numberOfLevels;i++){
			createPotdButton(i);
		}
		AssignMonthText();
		CheckPotdUpDown();
		PrepareUnlockLowest();
	}

	public void populatePotdDown(){
		CloseUnlockMenu();
		lowestLocked = 0;
		clearMenu();
		DateChecker.currentMonthIndex --;
		potdFirst = PotdHolder.monthBank[DateChecker.currentMonthIndex][0];
		int numberOfLevels = PotdHolder.monthBank[DateChecker.currentMonthIndex][1];

		for(int i = 0; i<numberOfLevels;i++){
			createPotdButton(i);
		}
		AssignMonthText();
		CheckPotdUpDown();
		PrepareUnlockLowest();
	}

	public void PrepareUnlockLowest(){

	}
}
