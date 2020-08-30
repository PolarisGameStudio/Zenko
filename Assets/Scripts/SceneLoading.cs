using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CompleteProject;


public class SceneLoading : MonoBehaviour {
	int numberOfAdventureMaps = 200; //change this when expanding levels
	public static bool adFree; //true if theyve paid for ad-free
	public int num;
	public static GameObject gamewon;
	public static GameObject gamelost;
	public int testnum;
	public GameObject textHolder;
	Dragger td;
	Dragger td2;
	public static SceneLoading Instance;
	public bool level;
	public GameObject buyMenu;
	bool canOpen;
	public static string menuState;
	public bool isMenu; // checks if its world select scene
	public GameObject PotdShortcut;
	//public bool isPotdOpen;
//	public IceTileHandler myhandler;
	void Awake(){
		Instance = this;
	}
	void Start(){
		menuState = "Start";
		canOpen = true;
		Instance = this;
//		Debug.Log("SceneLoadingStart");
		//Application.targetFrameRate = 30;

		//if in world select
		if (!level){ 
			//Start music
			MusicHandler.PlayTitleTheme();
			isMenu = true;
//			Debug.Log("Starting World Select Scene");
		}
		//else if loading level scene
		else{
			isMenu = false;

			//DO I REALLY WANT TO BE SAVING EVERYTIME A NEW MAP IS OPENING?
			PlayServices.instance.SaveLocal();
			PlayServices.instance.SaveData();
			//DO I?

//			Debug.Log("Starting Level Scene with level number " + LevelManager.levelnum);

			//NOT SURE WHY I DO THIS. PROBABLY TO PREVENT BUGS
			if(LevelManager.levelnum == 0 || LevelManager.levelnum ==null){
			LevelManager.levelnum = 103;				
			}
			//PROBABLY TO PREVENT BUG INDEED

			//assign appropriate ui LevelName
			FillLevelNameUI();

			//initialize rating and rating UI
			RatingBehaviour.InitializeRating();
		}
	}

	//Assigns UI Assets that correspond to the level name. calls either assignlevelname or assignpotdname
	public void FillLevelNameUI(){
		if(!LevelManager.ispotd)
		AssignLevelName();
		else
		AssignPotdName();
	}
	
	//Assigns name according to current level in format "World 1-20"
	public void AssignLevelName(){
		int world = Mathf.FloorToInt((LevelManager.levelnum-1)/40)  + 1;
		int levelinworld = LevelManager.levelnum - ((world-1)*40);
		WorldName.Instance.AssignLevelName(world, levelinworld);
	}
	
	//Assigns name depending on date of PoTD in format "11/11/2011"
	public void AssignPotdName(){
		WorldName.Instance.AssignPotdDate(DateChecker.Instance.currentIndex);
	}

	//Called from UI, opens Adventure Mode Selection
	public void adventureMode(){
		Transform levelBox = transform.Find("Level_Box");
		Transform buttonHolder = levelBox.Find("ButtonHolder");
		Transform menuHolder = transform.Find("MenuHolder");
		LevelMenu levelMenuScript = buttonHolder.GetComponent<LevelMenu>();
		int curfirst = getCurFirst(LevelStorer.highestSolved);
		if(canOpen){
			menuState = "Adventure";
			levelBox.gameObject.SetActive(true);
			levelMenuScript.clearMenu();
			
			PlayerPrefs.SetInt("CurrentFirst", curfirst); //sets in format 1,21,41,61 etc. 
			PlayerPrefs.Save();
			levelMenuScript.currentfirst = curfirst;
			levelMenuScript.populateMenu();
			LevelMenu.Instance.CheckDownUpButtons(curfirst);

			GameModeHandler.TurnOff();
			menuHolder.Find("Menu").gameObject.SetActive(false);
			menuHolder.Find("CloseLevel_Box").gameObject.SetActive(true);
			menuHolder.Find("Config").gameObject.SetActive(false);
			if(MenuButton.open){
				MenuButton.thisMB.closeMenu();
			}
			canOpen = false;
		}
	}
	
	//Returns 1,21,41,61,81 etc depending on where the fed int lies
	//Can change for loop
	public int getCurFirst(int highest){
		int candidate = 1;
		if (highest == 0 || highest == null || highest == 1){
			return 1;
		}
		for (int i = 1; i<numberOfAdventureMaps-1; i+=20){
			if(i<highest+1){
				candidate = i;
			}
			else{
				return candidate;
			}
		}
		return candidate;
	}
	
	//Called from UI, Closes Adventure Mode Selection
	public void closeAdventureMode(){
		Transform levelBox = transform.Find("Level_Box");
		Transform menuHolder = transform.Find("MenuHolder");
		menuState = "Start";
		levelBox.gameObject.SetActive(false);
		//remove curbuttons
		levelBox.Find("ButtonHolder").GetComponent<LevelMenu>().currentfirst = PlayerPrefs.GetInt("CurrentFirst");
		if(isMenu){
			menuHolder.Find("Menu").gameObject.SetActive(true);
			menuHolder.Find("CloseLevel_Box").gameObject.SetActive(false);
			GameModeHandler.Return();		
			CameraController.Fade(.2f,0f);	
		}
		canOpen = true;
	}
	
	//Called from UI, Opens Puzzle of the Day Mode Selection
	public void PuzzleOfTheDayMenu(){
		if(menuState == "Start"){
			if(canOpen){
				Transform potdBox = transform.Find("PoTD_Box");
				LevelMenu levelmenuScript = potdBox.Find("ButtonHolder").GetComponent<LevelMenu>();
				Transform menuHolder = transform.Find("MenuHolder");

				menuState = "Potd";
				potdBox.gameObject.SetActive(true);

				levelmenuScript.clearMenu();
				//GET DATE
				//USE DATE
				levelmenuScript.populatePotdMenu();
				canOpen = false;

				TurnOffIfAdFree.Instance.SetImages();

				if(isMenu){
					GameModeHandler.TurnOff();
					menuHolder.Find("Menu").gameObject.SetActive(false);
					menuHolder.Find("ClosePotd_Box").gameObject.SetActive(true);
					menuHolder.Find("Config").gameObject.SetActive(false);

					if(MenuButton.open){
						MenuButton.thisMB.closeMenu();
					}

					CameraController.Fade(.2f,1f, 1);
					PotdShortcut.SetActive(false);
				}
				if(!isMenu){
					LevelManager.configging = true;
        			Swiping.canswipe = false;

					menuHolder.Find("Menu").gameObject.SetActive(false);
					menuHolder.Find("Config").gameObject.SetActive(false);
					menuHolder.Find("ClosePotd_Box").gameObject.SetActive(true);
					
					if(MenuButton.open){
						MenuButton.thisMB.closeMenu();
					}

					CameraController.Fade(.2f,1f, 1);
					transform.Find("NOADS").gameObject.SetActive(true);
				}
			}	
		}
		else if(menuState == "Potd"){

		}
		else if(menuState == "Adventure"){
			closeAdventureMode();
			if(canOpen){
				menuState = "Potd";
				transform.Find("PoTD_Box").gameObject.SetActive(true);

				transform.Find("PoTD_Box").Find("ButtonHolder").GetComponent<LevelMenu>().clearMenu();

				//GET DATE
				//USE DATE
				transform.Find("PoTD_Box").Find("ButtonHolder").GetComponent<LevelMenu>().populatePotdMenu();


				GameModeHandler.TurnOff();
				transform.Find("MenuHolder").Find("Menu").gameObject.SetActive(false);
				transform.Find("MenuHolder").Find("ClosePotd_Box").gameObject.SetActive(true);
				transform.Find("MenuHolder").Find("Config").gameObject.SetActive(false);
				canOpen = false;			
				
				if(MenuButton.open){
					MenuButton.thisMB.closeMenu();
				}
				CameraController.Fade(.2f,1f, 1);

				if(LevelManager.adFree){

				}
				else{
					if(HasUnlocked()){

					}
				}
				PotdShortcut.SetActive(false);
			}
		}
	}
	
	//Called from UI, closes Puzzle of the Day Mode Selection
	public void ClosePotdMode(){
		menuState = "Start";
		transform.Find("PoTD_Box").Find("ButtonHolder").GetComponent<LevelMenu>().CloseUnlockMenu();
		transform.Find("PoTD_Box").gameObject.SetActive(false);

		if(isMenu){
			transform.Find("PoTD_Box").Find("ButtonHolder").GetComponent<LevelMenu>().currentfirst = PlayerPrefs.GetInt("CurrentFirst");

			transform.Find("MenuHolder").Find("Menu").gameObject.SetActive(true);
			transform.Find("MenuHolder").Find("ClosePotd_Box").gameObject.SetActive(false);
			GameModeHandler.Return();		

			CameraController.Fade(.2f,0f);
			PotdShortcut.SetActive(true);			
		}
		else{
			transform.Find("PoTD_Box").Find("ButtonHolder").GetComponent<LevelMenu>().currentfirst = PlayerPrefs.GetInt("CurrentFirst");
			transform.Find("MenuHolder").Find("Menu").gameObject.SetActive(true);
			transform.Find("MenuHolder").Find("ClosePotd_Box").gameObject.SetActive(false);
			CameraController.Fade(.2f,0f);	
			LevelManager.configging = false;
        	Swiping.canswipe = true;
        	Swiping.mydirection = "Null";
        	TutorialHandler.Instance.TutorialClosebutton();
	        if(Input.touchCount>0){
	            Touch t = Input.GetTouch(0);
	            Swiping.firstPressPos = new Vector2(t.position.x,t.position.y);

	        }
	        else{
	            Swiping.firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
	        }
	        transform.Find("NOADS").gameObject.SetActive(false);

		}
		//remove curbuttons
		canOpen = true;
	}
	
	//This is called by UI if pressed a adventure level button
	public void LoadLevel(int num){
		if(LevelStorer.leveldic[num].islocked == false || num==1 ){
			LevelManager.levelnum = num;
			Swiping.mydirection = "Null";
			TurnCounter.turncount = 0;
			LevelStorer.Lookfor(num);
			LevelManager.levelnum = num;
			LevelManager.readytodraw = true;
			LevelManager.ispotd = false;
			SceneManager.LoadScene(1);
			MusicHandler.PlayInitialLoop();
		}
	}
	
	//Opens World Select Scene
	public void LoadMenu(){
		SceneManager.LoadScene(0);
	}
	
	//Opens Remove Ads Interface
	public void OpenRemoveAdsMenu(){
		buyMenu.SetActive(true);
	}
	
	//Closes Remove Ads Interface
	public void CloseRemoveAdsMenu(){
		buyMenu.SetActive(false);
		if(!PieceHolders.hintMenuOpen){
			LevelManager.isdragging = false;
		}
	}
	
	//Calls NoAdsCommand
	public void RemoveAds(){
		Purchaser.Instance.BuyNoAds();
	}
	
	//Called from next won, either after winning or in editor ui
	public void NextlevelButton(){
		Swiping.mydirection = "Null";
		LevelManager.israndom = false;

		LevelManager.levelnum++;
		AssignLevelName();

		LevelStorer.UnlockLevel (LevelManager.levelnum);
		LevelStorer.Lookfor (LevelManager.levelnum);

		TurnCounter.turncount = 0;
		LevelManager.NextLevel(LevelManager.levelnum);
		TurnGraphics.SetTurnCounter(LevelStorer.efficientturns);
		
		Swiping.mydirection = "Null";
		RatingBehaviour.RestartRating();
	}
	
	//Called from UI, opens help	
	public void TutorialButton(){
		Debug.Log("PUSHED TUTORIAL BUTTON");
		TutorialHandler.Instance.HelpButton();
		MenuButton.thisMB.closeMenu();
	}
	
	//Called from UI when pressing "next" on victory screen, starts next level and shows ad if required
	public void NextWon(){
		Swiping.mydirection = "Null";
		if(LevelManager.ispotd){
			Potd();
			GoogleAds.Instance.ShowInterstitial();
		}
		if(!LevelManager.ispotd){
			NextlevelButton();
			GoogleAds.Instance.ShowInterstitial();
		}
	}

	//Loads Level Scene with potd after preparing for it
	public void LoadPotdMap(int index){
		Swiping.mydirection = "Null";
		TurnCounter.turncount = 0;
		PlayerPrefs.SetInt("PoTD", index);
		DateChecker.Instance.currentIndex = index;
		LevelStorer.potdDic[DateChecker.Instance.currentIndex].islocked = false;
		LevelStorer.potdDic[DateChecker.Instance.currentIndex].isNew = false;
		LevelManager.ispotd = true;
		SceneManager.LoadScene(1);
		MusicHandler.PlayInitialLoop();
	}
	//Called from UI
	public void CloseTryAgainScreen(){
		GameObject.Find("TryAgainScreen").transform.GetChild(0).gameObject.SetActive(false);
	}
	
	//Called from nextwon
	public void Potd(){
		Swiping.mydirection = "Null";
		TurnCounter.turncount = 0;
		LevelManager.ispotd = true;
		LevelManager.NextPotd();
		TurnGraphics.SetTurnCounter(LevelStorer.efficientturns);
		RatingBehaviour.RestartRating();
		AssignPotdName();
	}	
	public void PotdSpecific(int num){
		Swiping.mydirection = "Null";
		TurnCounter.turncount = 0;
		LevelManager.ispotd = true;
		PlayerPrefs.SetInt("PoTD", num);
		DateChecker.Instance.currentIndex = num;
		LevelStorer.potdDic[num].islocked = false;
		LevelStorer.potdDic[num].isNew = false;
		PotdShortcut.GetComponent<PotdShortcut>().AssignPotdShortcutAssets(PotdUnlocker.Instance.keysAvailable);
		PotdUnlocker.Instance.Initiate();
		LevelManager.SpecificPotd(num);
		TurnGraphics.SetTurnCounter(LevelStorer.efficientturns);
		RatingBehaviour.RestartRating();
		AssignPotdName();
		ClosePotdMode();
		ChangeSprites();
	}	
	public void ChangeSprites(){
		//transform.Find("Level_Box").Find("ButtonHolder").GetComponent<LevelMenu>().clearMenu();
	}
	
	//for testing changes to potd map
	public void ReBringCurrentPotd(){
		Swiping.mydirection = "Null";
		TurnCounter.turncount = 0;
		LevelManager.ispotd = true;
		LevelManager.RePotd();
		TurnGraphics.SetTurnCounter(LevelStorer.efficientturns);
		RatingBehaviour.RestartRating();
		AssignPotdName();		
	}


	public static void SetStars(int rating){

		if(rating == 1){
			RatingPopUp.starholder.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
			RatingPopUp.starholder.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
			RatingPopUp.starholder.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
		
		}
		else if(rating == 2){
			RatingPopUp.starholder.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
			RatingPopUp.starholder.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
			RatingPopUp.starholder.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
		}
		else if(rating == 3){
			RatingPopUp.starholder.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
			RatingPopUp.starholder.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
			RatingPopUp.starholder.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);
		}
		else if (rating == 0){
			RatingPopUp.starholder.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
			RatingPopUp.starholder.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
			RatingPopUp.starholder.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);			
		}
	}
	public void PlaceHint(){



		int mynum = LevelManager.hintnum; 
		//Code to remove type,taken,obj
		td = LevelManager.piecetiles[mynum].gameObject.GetComponent<Dragger>();

		if(td.gameObject.transform.position.x< 8){ //check to see if its on board (then check to see if its in the right place)
			
			//Tile tiletoleave = LevelBuilder.tiles[(int)td.gameObject.transform.x]


		}


		Vector3 Place = new Vector3 (LevelManager.myhints[mynum].x, 0, -LevelManager.myhints[mynum].y);
		LevelManager.piecetiles[mynum].position = Place;

		LevelBuilder.tiles[(int)Place.x, -(int)Place.z].type = td.myType;
		LevelBuilder.tiles[(int)Place.x, -(int)Place.z].isTaken = true;
		LevelBuilder.tiles[(int)Place.x, -(int)Place.z].tileObj = td.gameObject;
		Debug.Log(Place.z);

		if(td.myType == "Seed"){
			LevelBuilder.tiles[(int)Place.x, -(int)Place.z].seedType = td.mySeedType;	
		} 
		LevelManager.hintnum++;
	}

	
	public void PreviousLevelButton(){
		Swiping.mydirection = "Null";
		LevelManager.levelnum--;
		LevelStorer.Lookfor (LevelManager.levelnum);
		TurnCounter.turncount = 0;
		LevelManager.NextLevel (LevelManager.levelnum);
	}
	public void ResetLevelButton(){
		AssignLevelName();
		TurnCounter.turncount = 0;
		LevelManager.ResetLevel();
		LevelManager.UnPop();
		ProgressBar.InitializeProgressBar(LevelStorer.efficientturns);
		DotHandler.InitializeDots(LevelStorer.efficientturns);
		Swiping.mydirection = "Null";
		RatingBehaviour.RestartRating();
		MenuButton.CloseMenu();
	}
	public void ResetAllButton(){
		AssignLevelName();
		TurnCounter.turncount = 0;
		LevelManager.NextLevel (LevelManager.levelnum);
		TurnGraphics.SetTurnCounter(LevelStorer.efficientturns);
		Swiping.mydirection = "Null";
		MenuButton.CloseMenu();
	}
	public void Testnum(int num){
		Swiping.mydirection = "Null";
		LevelManager.levelnum = num;		
		LevelStorer.Lookfor (num);
		TurnCounter.turncount = 0;
		LevelManager.NextLevel (num);

	}
	public void GoToWorld(int worldnumber){
		LevelBuilder.iscreated = false;
		//LevelManager.worldnum = worldnumber;
		SceneManager.LoadScene(worldnumber);
		//change camera position depending on 
	}

	public void unlockAll(){
		LevelStorer.UnlockAllLevels();
	}

	public void LockAll(){
		LevelStorer.LockAllLevels();
	}
	//This is called by button click if pressed a puzzle of the day button
	public void LoadPotd(int index){
		Swiping.mydirection = "Null";

		TurnCounter.turncount = 0;
		LevelManager.ispotd = true;

		LevelManager.NextPotd();
		TurnGraphics.SetTurnCounter(LevelStorer.efficientturns);
		RatingBehaviour.RestartRating();		
	}
	public void Plus(){
		LevelManager.levelnum ++;
	}
	public void Minus(){
		LevelManager.levelnum--;
	}
	//probably old
	public void GoToLevelSelect(){
		if (LevelManager.levelnum < 34) {
			SceneManager.LoadScene (1);
		} else if (LevelManager.levelnum < 67) {
			SceneManager.LoadScene (2);
		} else if (LevelManager.levelnum < 101) {
			SceneManager.LoadScene (3);
		}
	}
	bool HasUnlocked(){
		return true;
	}
	public void GoToWorldSelect(){
			SceneManager.LoadScene (1);
			MusicHandler.running = false;
	}
	public void placeIcarus(string type, Vector3 target){
		if(type == "Left"){
			LevelBuilder.tiles[(int)target.x, -(int)target.z].type = "Wall";
			LevelBuilder.tiles[(int)target.x, -(int)target.z].isTaken = true;
			LevelBuilder.tiles[(int)target.x, -(int)target.z].tileObj = td.gameObject;
			if(LevelBuilder.tiles[(int)target.x-1, -(int)target.z].type == "Ice"){
				LevelBuilder.tiles[(int)target.x-1, -(int)target.z].type = type;		
			}	
			else{
				LevelBuilder.tiles[(int)target.x-1, -(int)target.z].isSideways = "Left";
			}
		} 
		if(type =="Right"){
			LevelBuilder.tiles[(int)target.x, -(int)target.z].type = "Wall";
			LevelBuilder.tiles[(int)target.x, -(int)target.z].isTaken = true;
			LevelBuilder.tiles[(int)target.x, -(int)target.z].tileObj = td.gameObject;
			if(LevelBuilder.tiles[(int)target.x+1, -(int)target.z].type == "Ice"){
				LevelBuilder.tiles[(int)target.x+1, -(int)target.z].type = type;		
				//LevelBuilder.tiles[(int)target.x+1, -(int)target.z].isTaken = true;				
			}	
			else{
				LevelBuilder.tiles[(int)target.x+1, -(int)target.z].isSideways = "Right";
			}	

		}
		if(type == "Up" ){
			LevelBuilder.tiles[(int)target.x, -(int)target.z].type = "Wall";
			LevelBuilder.tiles[(int)target.x, -(int)target.z].isTaken = true;
			LevelBuilder.tiles[(int)target.x, -(int)target.z].tileObj = td.gameObject;	
			if(LevelBuilder.tiles[(int)target.x, -(int)target.z-1].type == "Ice"){
				LevelBuilder.tiles[(int)target.x, -(int)target.z-1].type = type;		
				//LevelBuilder.tiles[(int)target.x, -(int)target.z-1].isTaken = true;				
			}	
			else{
				LevelBuilder.tiles[(int)target.x, -(int)target.z-1].isSideways = "Up";
			}
		}
		if(type == "Down"){
			LevelBuilder.tiles[(int)target.x, -(int)target.z].type = "Wall";
			LevelBuilder.tiles[(int)target.x, -(int)target.z].isTaken = true;
			LevelBuilder.tiles[(int)target.x, -(int)target.z].tileObj = td.gameObject;	
			if(LevelBuilder.tiles[(int)target.x, -(int)target.z+1].type == "Ice"){
				LevelBuilder.tiles[(int)target.x, -(int)target.z+1].type = type;		
				//LevelBuilder.tiles[(int)target.x, -(int)target.z+1].isTaken = true;				
			}	
			else{
				LevelBuilder.tiles[(int)target.x, -(int)target.z+1].isSideways = "Down";
			}
		}
	}
	public void removePiece(Vector3 target, string type){
		if(type == "Wall"){
			LevelBuilder.tiles[(int)target.x, -(int)target.z].type = "Ice";
			LevelBuilder.tiles[(int)target.x, -(int)target.z].isTaken = false;	
			if(LevelBuilder.tiles[(int)target.x, -(int)target.z].isSideways != null){
				LevelBuilder.tiles[(int)target.x, -(int)target.z].type = LevelBuilder.tiles[(int)target.x, -(int)target.z].isSideways;
			}			
		}


		if(type == "Left" || type == "Right" || type == "Down" || type == "Up" )	{
			removeIcarus(type, target);
		}
		if(type == "Seed"){
			LevelBuilder.tiles[(int)target.x, -(int)target.z].type = "Ice";
			LevelBuilder.tiles[(int)target.x, -(int)target.z].isTaken = false;	
			if(LevelBuilder.tiles[(int)target.x, -(int)target.z].isSideways != null){
				LevelBuilder.tiles[(int)target.x, -(int)target.z].type = LevelBuilder.tiles[(int)target.x, -(int)target.z].isSideways;
			}				
		}
	}
	public void removeIcarus(string type, Vector3 target){
		Debug.Log(type);
		LevelBuilder.tiles[(int)target.x, -(int)target.z].type = "Ice";
		LevelBuilder.tiles[(int)target.x, -(int)target.z].isTaken = false;	
		if(LevelBuilder.tiles[(int)target.x, -(int)target.z].isSideways != null){
				LevelBuilder.tiles[(int)target.x, -(int)target.z].type = LevelBuilder.tiles[(int)target.x, -(int)target.z].isSideways;
		}	

		if(type == "Left"){
			if(LevelBuilder.tiles[(int)target.x-1, -(int)target.z].type == type){
				LevelBuilder.tiles[(int)target.x-1, -(int)target.z].type = "Ice";		
				LevelBuilder.tiles[(int)target.x-1, -(int)target.z].isTaken = false;	
				LevelBuilder.tiles[(int)target.x-1, -(int)target.z].isSideways = null;					
			}
			else if(LevelBuilder.tiles[(int)target.x-1, -(int)target.z].isSideways == type){
				LevelBuilder.tiles[(int)target.x-1, -(int)target.z].isSideways = null;

			}


		} 
		if(type =="Right"){
			if(LevelBuilder.tiles[(int)target.x+1, -(int)target.z].type == type){
				LevelBuilder.tiles[(int)target.x+1, -(int)target.z].type = "Ice";		
				LevelBuilder.tiles[(int)target.x+1, -(int)target.z].isTaken = false;	
				LevelBuilder.tiles[(int)target.x+1, -(int)target.z].isSideways = null;					
			}		
			else if(LevelBuilder.tiles[(int)target.x+1, -(int)target.z].isSideways == type){
				LevelBuilder.tiles[(int)target.x+1, -(int)target.z].isSideways = null;

			}
		}
		if(type == "Up" ){	
			if(LevelBuilder.tiles[(int)target.x, -(int)target.z-1].type == type){
				LevelBuilder.tiles[(int)target.x, -(int)target.z-1].type = "Ice";		
				LevelBuilder.tiles[(int)target.x, -(int)target.z-1].isTaken = false;	
				LevelBuilder.tiles[(int)target.x, -(int)target.z-1].isSideways = null;					
			}	
			else if(LevelBuilder.tiles[(int)target.x, -(int)target.z-1].isSideways == type){
				LevelBuilder.tiles[(int)target.x, -(int)target.z-1].isSideways = null;

			}	
		}
		if(type == "Down"){
			if(LevelBuilder.tiles[(int)target.x, -(int)target.z+1].type == type){
				LevelBuilder.tiles[(int)target.x, -(int)target.z+1].type = "Ice";		
				LevelBuilder.tiles[(int)target.x, -(int)target.z+1].isTaken = false;	
				LevelBuilder.tiles[(int)target.x, -(int)target.z+1].isSideways = null;					
			}
			else if(LevelBuilder.tiles[(int)target.x, -(int)target.z+1].isSideways == type){
				LevelBuilder.tiles[(int)target.x, -(int)target.z+1].isSideways = null;

			}		
		}
	}
	void placeNormal(Vector3 positiontogo){
		if(LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].type == "Left" || 
		LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].type == "Down" ||
		LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].type == "Up" ||
		LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].type == "Right")
		{
			LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].isSideways = LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].type;

		}
		Debug.Log(td.myType);
		LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].type = td.myType;
		LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].isTaken = true;
		LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].tileObj = td.gameObject;	
	}
	public void PlaceHint2(){
		for(int i = 0; i<LevelManager.piecetiles.Count; i++){ //First go through the pieces outside of the board.
			td = LevelManager.piecetiles[i].gameObject.GetComponent<Dragger>();
			Debug.Log(td.gameObject.transform.position);
			if(td.gameObject.transform.position.z < -7){//if piece is outside:
				Debug.Log("Piece number "+ i + "is outside and good to go"); 
				//need to check if taken by someone of similar type.
				//If no one is on their assigned (ideal) tile, place it. and return.

				Vector3 Place = new Vector3 (LevelManager.myhints[i].x, 0, -LevelManager.myhints[i].y);
				if (LevelBuilder.tiles[(int)Place.x, -(int)Place.z].isTaken == false){ //if it's intended place is free
					LevelManager.piecetiles[i].position = Place;

					if(td.myType == "Left" || td.myType == "Right" || td.myType == "Up" || td.myType == "Down"){
						placeIcarus(td.myType, Place);

						}
						else{
							placeNormal(Place);
							//LevelBuilder.tiles[(int)Place.x, -(int)Place.z].type = td.myType;
							//LevelBuilder.tiles[(int)Place.x, -(int)Place.z].isTaken = true;
							//LevelBuilder.tiles[(int)Place.x, -(int)Place.z].tileObj = td.gameObject;				
						}

					td.gameObject.GetComponent<BoxCollider>().enabled = false;	
					
					//LevelBuilder.tiles[(int)Place.x, -(int)Place.z].type = td.myType;
					//LevelBuilder.tiles[(int)Place.x, -(int)Place.z].isTaken = true;
					//LevelBuilder.tiles[(int)Place.x, -(int)Place.z].tileObj = td.gameObject;
					//td.gameObject.GetComponent<BoxCollider>().enabled = false;					//disable collider so piece stays there.
					
					Debug.Log(Place.z);

					if(td.myType == "Seed"){
						LevelBuilder.tiles[(int)Place.x, -(int)Place.z].seedType = td.mySeedType;	
					} 

					LevelManager.hintnum++;
					LevelManager.hintsgiven.Add(i);
					if(td.gameObject.GetComponent<Animator>() != null){
						td.gameObject.GetComponent<Animator>().SetInteger("Phase", 2);

					}
					return;
					//place it
				}
				else{
					Debug.Log("Someone in my place");
					//Check if one on my place is good or bad.
					if(td.myType == LevelBuilder.tiles[(int)Place.x, -(int)Place.z].type && td.mySeedType == LevelBuilder.tiles[(int)Place.x, -(int)Place.z].seedType){//if someone of the same type is on it's ideal place. 
						for(int j = 0; j<LevelManager.piecetiles.Count; j++){//search for a new spot (probably gonna end up being the one who has this one's place unless there's more than 2 of the same type)
							if(j!=i){//skip self
								td2 = LevelManager.piecetiles[j].gameObject.GetComponent<Dragger>();
								if(td2.myType == td.myType && td.mySeedType == td2.mySeedType){		//check if the selected piece is of same type
									Debug.Log("Same person in my place");
									Vector3 Place2 = new Vector3 (LevelManager.myhints[j].x, 0, -LevelManager.myhints[j].y);	//place 2 is hint solution assigned to piece being tested
									Debug.Log(Place2);		
									if(LevelBuilder.tiles[(int)Place2.x, -(int)Place2.z].isTaken == false){ //Check if the selected piece of the same type has it's ideal spot open.
										Debug.Log("new target free");
										
										//Place tile and return.
										LevelManager.piecetiles[i].position = Place2;
										if(td.myType == "Left" || td.myType == "Right" || td.myType == "Up" || td.myType == "Down"){
											placeIcarus(td.myType, Place2);

											}
											else{
												placeNormal(Place2);			
											}

										td.gameObject.GetComponent<BoxCollider>().enabled = false;					//disable collider so piece stays there.
										
										Debug.Log(Place2.z + "Testcloseearly");

										if(td.myType == "Seed"){
											LevelBuilder.tiles[(int)Place2.x, -(int)Place2.z].seedType = td.mySeedType;	
										} 
										if(td.gameObject.GetComponent<Animator>() != null){
											td.gameObject.GetComponent<Animator>().SetInteger("Phase", 2);

										}
										LevelManager.hintnum++;
										LevelManager.hintsgiven.Add(i);

										return;										
									
									}							//check that their position is open
																//take their position
//Debug.Log("Same person in my place");
								}
							}
						}
					//place this one on the other's place

					}
					else{//if other is in bad place
					//skip

					}

				}
		
			}
		}
		for(int i=0; i < LevelManager.piecetiles.Count; i++){//If no pieces are outside the board.
			Debug.Log("Trying for " + i);
			bool alternative = false;
			td = LevelManager.piecetiles[i].gameObject.GetComponent<Dragger>();
			string piecetype = td.myType;
			string seedtype = td.mySeedType;
			Vector3 Place = new Vector3 (LevelManager.myhints[i].x, 0, -LevelManager.myhints[i].y); //target from hint.
			Vector2 myv2 = new Vector2(td.gameObject.transform.position.x, -td.gameObject.transform.position.z);
			Debug.Log(LevelManager.myhints[i] +"" +  myv2);
			if(LevelManager.myhints[i] != myv2){//check if same position as same hint position
				alternative = false;
				Debug.Log("Time to work");
				for(int j=0; j<LevelManager.piecetiles.Count; j++){//See if it is on a tile that also has same type.
					if(i!=j){
						td2 = LevelManager.piecetiles[j].gameObject.GetComponent<Dragger>();
						if(myv2 == LevelManager.myhints[j] && td.myType == td2.myType && td.mySeedType == td.mySeedType){
							Debug.Log("Piece " + i +  " is SITTING ON A GOOD ALTERNATIVE");
							alternative = true;
							break;
						}
					}
				}
				if(alternative == false){//If not on an alternative (then move), look for it's hint.

					Debug.Log("Time to place");
					//this needs work.
					if(LevelBuilder.tiles[(int)Place.x, -(int)Place.z].isTaken == false){ //if hint place is free
						Debug.Log("Locked on target");
						//remove
						removePiece(td.gameObject.transform.position, td.myType);
						LevelManager.piecetiles[i].position = Place;

						if(td.myType == "Left" || td.myType == "Right" || td.myType == "Up" || td.myType == "Down"){
							placeIcarus(td.myType, Place);

						}
						else{
								placeNormal(Place);
						}
						td.gameObject.GetComponent<BoxCollider>().enabled = false;					//disable collider so piece stays there.
						
						Debug.Log(Place.z);

						if(td.myType == "Seed"){
							LevelBuilder.tiles[(int)Place.x, -(int)Place.z].seedType = td.mySeedType;	
						} 

						LevelManager.hintnum++;
						LevelManager.hintsgiven.Add(i);
						if(td.gameObject.GetComponent<Animator>() != null){
							td.gameObject.GetComponent<Animator>().SetInteger("Phase", 2);

						}
						return;
					}
					else{ //go through all to find alternative

					}
					//return;

				}

			}

			//check if it's on the right one or one of same type.

			//return;
		}
		for(int i=0; i < LevelManager.piecetiles.Count; i++){ //If none inside have a free alternative or choice done. (Basically if they are all wrong in each other's)
			td = LevelManager.piecetiles[i].gameObject.GetComponent<Dragger>();
			string piecetype = td.myType;
			string seedtype = td.mySeedType;
			Vector3 Place = new Vector3 (LevelManager.myhints[i].x, 0, -LevelManager.myhints[i].y); //target from hint.
			Vector2 myv2 = new Vector2(td.gameObject.transform.position.x, -td.gameObject.transform.position.z);			


		}

	}

	//Opens level with number assigned
	public void NextSpecificButton(int level){
		LevelManager.levelnum = level;
		Swiping.mydirection = "Null";
		LevelManager.israndom = false;

		LevelManager.levelnum++;
		AssignLevelName();

		LevelStorer.UnlockLevel (LevelManager.levelnum);
		LevelStorer.Lookfor (LevelManager.levelnum);

		TurnCounter.turncount = 0;
		LevelManager.NextLevel(LevelManager.levelnum);
		TurnGraphics.SetTurnCounter(LevelStorer.efficientturns);
		
		Swiping.mydirection = "Null";
		RatingBehaviour.RestartRating();
	}

	//Test For Unlocking An Achievement
	public void ShowAchievementsUI(){
		PlayServices.UnlockWorldAchievement(1);
	}
	//Makes adfree false (for testing purposes)
	public void RemoveAdFree(){
		LevelManager.adFree = false;
	}
	
	//Called from UI, probably Test
	public void TestLevel(){
		Swiping.mydirection = "Null";
		LevelManager.israndom = false;

		LevelManager.levelnum++;
		AssignLevelName();

		TurnCounter.turncount = 0;
		LevelManager.NextLevel(161);
		TurnGraphics.SetTurnCounter(161);
		
		Swiping.mydirection = "Null";
		RatingBehaviour.RestartRating();
	}
	
	//Called from UI, probably unused
	public void muteMusic(){
		AudioSource ms = GameObject.Find("Music Source").GetComponent<AudioSource>();
		ms.mute = !ms.mute;
		if(ms.mute){
			EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = 
			EventSystem.current.currentSelectedGameObject.GetComponent<ImageSwitch>().imagetwo;			
		}
		else{
			EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = 
			EventSystem.current.currentSelectedGameObject.GetComponent<ImageSwitch>().imageone;
		}
	}
	
	//Called from UI, probably unused
	public void muteSfx(){
		AudioSource sfxs = GameObject.Find("Sfx Source").GetComponent<AudioSource>();
		sfxs.mute = !sfxs.mute;
		if(sfxs.mute){
			EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = 
			EventSystem.current.currentSelectedGameObject.GetComponent<ImageSwitch>().imagetwo;			
		}
		else{
			EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = 
			EventSystem.current.currentSelectedGameObject.GetComponent<ImageSwitch>().imageone;
		}
	}
}