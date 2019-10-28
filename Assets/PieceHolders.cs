using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Holder{
	public string name;
	public int maxpieces;
	public int currentpieces;
	public GameObject mygameobject;
	public bool active;


	public Holder(string newname, int newmaxpieces, int newcurrentpieces,
		GameObject newgameobject, bool isactive){
		name = newname;
		maxpieces = newmaxpieces;
		currentpieces = newcurrentpieces;
		mygameobject = newgameobject;
		active = isactive;

	}

}
public class PieceHolders : MonoBehaviour {
	public static PieceHolders Instance;
	public List<Holder> holders = new List<Holder>();
	public int holdersNumber;
	public GameObject pedroHolder;
	public GameObject leftHolder;
	public GameObject rightHolder;
	public GameObject upHolder;
	public GameObject downHolder;
	public GameObject wallSeedHolder;
	public GameObject leftSeedHolder;
	public GameObject rightSeedHolder;
	public GameObject upSeedHolder;
	public GameObject downSeedHolder;
	public int pedroNumber;
	public int leftNumber;
	public int rightNumber;
	public int upNumber;
	public int downNumber;
	public int leftSeedNumber;
	public int rightSeedNumber;
	public int upSeedNumber;
	public int downSeedNumber;
	public int wallSeedNumber;
	public static bool hinting;
	public static Dragger draggee;
	public static List<Dragger> placedpieces = new List<Dragger>();
	SceneLoading sl;
	public Text hintText;
	// Use this for initialization
	void Awake () {
		initValues();
		sl = GameObject.Find("Main Canvas").GetComponent<SceneLoading>();
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(LevelBuilder.tiles[6,4].isTaken);
	}
	public void initSize(){
		RectTransform rt = GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(holdersNumber*150, 150);
	}
	public void reset(){
		pedroHolder.SetActive(false);
		leftHolder.SetActive(false);
		rightHolder.SetActive(false);
		upHolder.SetActive(false);
		downHolder.SetActive(false);
		wallSeedHolder.SetActive(false);
		leftSeedHolder.SetActive(false);
		rightSeedHolder.SetActive(false);
		upSeedHolder.SetActive(false);
		downSeedHolder.SetActive(false);
		initValues();
		holders = new List<Holder>();
	}
	public void initValues(){
	pedroNumber = 0;
	leftNumber = 0;
	rightNumber = 0;
	upNumber = 0;
	downNumber = 0;
	wallSeedNumber = 0;
	leftSeedNumber =0;
	rightSeedNumber =0;
	upSeedNumber =0;
	downSeedNumber =0;
	holdersNumber = 0;
	pedroHolder.GetComponent<Image>().color = Color.white;
	leftHolder.GetComponent<Image>().color = Color.white;
	upHolder.GetComponent<Image>().color = Color.white;
	rightHolder.GetComponent<Image>().color = Color.white;
	downHolder.GetComponent<Image>().color = Color.white;
	wallSeedHolder.GetComponent<Image>().color = Color.white;
	leftSeedHolder.GetComponent<Image>().color = Color.white;
	rightSeedHolder.GetComponent<Image>().color = Color.white;
	upSeedHolder.GetComponent<Image>().color = Color.white;
	downSeedHolder.GetComponent<Image>().color = Color.white;
	}
	public void InitHolder(string type){
		if(type == "Pedro" || type ==  "Wall"){
			holders.Add(new Holder(type,1,1,pedroHolder,true));
		}
		if(type == "Left"){
			holders.Add(new Holder(type,1,1,leftHolder,true));
		}
		if(type == "Right"){
			holders.Add(new Holder(type,1,1,rightHolder,true));
		}
		if(type == "Up"){
			holders.Add(new Holder(type,1,1,upHolder,true));
		}
		if(type == "Down"){
			holders.Add(new Holder(type,1,1,downHolder,true));
		}
		if(type == "WallSeed"){
			holders.Add(new Holder(type,1,1,wallSeedHolder,true));
		}
		if(type == "LeftSeed"){
			holders.Add(new Holder(type,1,1,leftSeedHolder,true));
		}
		if(type == "RightSeed"){
			holders.Add(new Holder(type,1,1,rightSeedHolder,true));
		}
		if(type == "UpSeed"){
			holders.Add(new Holder(type,1,1,upSeedHolder,true));
		}
		if(type == "DownSeed"){
			holders.Add(new Holder(type,1,1,downSeedHolder,true));
		}
	}
	public void AddtoHolder(string type){
		//Debug.Log("Test");
		Holder currentholder= holders.Find(myItem => myItem.name == type);
		currentholder.maxpieces++;
		currentholder.currentpieces++;
//		Debug.Log(currentholder.name);

	}
	public void SubstractToHolder(string type){
		//Debug.Log("Test");
		Holder currentholder= holders.Find(myItem => myItem.name == type);
		currentholder.currentpieces--;
		//currentholder.
//		Debug.Log(currentholder.name);

	}
	public void AddPiece(string type){
		if(type == "Pedro" || type == "Wall"){
			if(pedroHolder.active){
				pedroNumber++;
				AddtoHolder(type);
			}
			else{
				InitHolder(type);
				pedroHolder.SetActive(true);
				holdersNumber++;
				pedroNumber++;
				initSize();
			}			
		}
		if(type == "Up"){
			if(upHolder.active){
				upNumber++;
				AddtoHolder(type);
			}
			else{
				InitHolder(type);
				upHolder.SetActive(true);
				holdersNumber++;
				upNumber++;
				initSize();
			}			
		}
		if(type == "Down"){
			if(downHolder.active){
				downNumber++;
				AddtoHolder(type);
			}
			else{
				InitHolder(type);
				downHolder.SetActive(true);
				holdersNumber++;
				downNumber++;
				initSize();
			}			
		}
		if(type == "Left"){
			if(leftHolder.active){
				leftNumber++;
				AddtoHolder(type);
			}
			else{
				InitHolder(type);
				leftHolder.SetActive(true);
				holdersNumber++;
				leftNumber++;
				initSize();
			}			
		}
		if(type == "Right"){
			if(rightHolder.active){
				rightNumber++;
				AddtoHolder(type);
			}
			else{
				InitHolder(type);
				rightHolder.SetActive(true);
				holdersNumber++;
				rightNumber++;
				initSize();
			}			
		}
		if(type == "WallSeed"){
			if(wallSeedHolder.active){
				wallSeedNumber++;
				AddtoHolder(type);
			}
			else{
				InitHolder(type);
				wallSeedHolder.SetActive(true);
				holdersNumber++;
				wallSeedNumber++;
				initSize();
			}			
		}
		if(type == "LeftSeed"){
			if(leftSeedHolder.active){
				leftSeedNumber++;
				AddtoHolder(type);
			}
			else{
				InitHolder(type);
				leftSeedHolder.SetActive(true);
				holdersNumber++;
				leftSeedNumber++;
				initSize();
			}			
		}
		if(type == "RightSeed"){
			if(rightSeedHolder.active){
				rightSeedNumber++;
				AddtoHolder(type);
			}
			else{
				InitHolder(type);
				rightSeedHolder.SetActive(true);
				holdersNumber++;
				rightSeedNumber++;
				initSize();
			}			
		}
		if(type == "UpSeed"){
			if(upSeedHolder.active){
				upSeedNumber++;
				AddtoHolder(type);
			}
			else{
				InitHolder(type);
				upSeedHolder.SetActive(true);
				holdersNumber++;
				upSeedNumber++;
				initSize();
			}			
		}
		if(type == "DownSeed"){
			if(downSeedHolder.active){
				downSeedNumber++;
				AddtoHolder(type);
			}
			else{
				InitHolder(type);
				downSeedHolder.SetActive(true);
				holdersNumber++;
				downSeedNumber++;
				initSize();
			}			
		}
		updateText(type);
	}
	public void updateValueUp(string type){
		if(type == "Pedro" || type == "Wall"){
			pedroNumber ++;
			pedroHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Wall x" + pedroNumber;
			pedroHolder.GetComponent<Image>().color = Color.white;

		}
		if(type == "Up"){
			upNumber++;
			upHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Up x" + upNumber;
			upHolder.GetComponent<Image>().color = Color.white;
		}
		if(type == "Down"){
			downNumber++;
			downHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Down x" + downNumber;
			downHolder.GetComponent<Image>().color = Color.white;
		}
		if(type == "Left"){
			leftNumber++;
			leftHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Left x" + leftNumber;
			leftHolder.GetComponent<Image>().color = Color.white;
		}
		if(type == "Right"){
			rightNumber++;
			rightHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Right x" + rightNumber;
			rightHolder.GetComponent<Image>().color = Color.white;
		}		
		if(type == "WallSeed"){
			wallSeedNumber++;
			wallSeedHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "WallSeed x" + wallSeedNumber;
			wallSeedHolder.GetComponent<Image>().color = Color.white;
		}
		if(type == "LeftSeed"){
			leftSeedNumber++;
			leftSeedHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "LeftSeed x" + leftSeedNumber;
			leftSeedHolder.GetComponent<Image>().color = Color.white;
		}
		if(type == "RightSeed"){
			rightSeedNumber++;
			rightSeedHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "RightSeed x" + rightSeedNumber;
			rightSeedHolder.GetComponent<Image>().color = Color.white;
		}
		if(type == "UpSeed"){
			upSeedNumber++;
			upSeedHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "UpSeed x" + upSeedNumber;
			upSeedHolder.GetComponent<Image>().color = Color.white;
		}
		if(type == "DownSeed"){
			downSeedNumber++;
			downSeedHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "DownSeed x" + wallSeedNumber;
			downSeedHolder.GetComponent<Image>().color = Color.white;
		}
	}
	public void unshadeImage(string type){
		if(type == "Pedro" || type == "Wall"){
			Color tempColor = pedroHolder.GetComponent<Image>().color;
			tempColor.a = 1f;
			pedroHolder.GetComponent<Image>().color = tempColor;
		}
		if(type == "Up"){
			Color tempColor = upHolder.GetComponent<Image>().color;
			tempColor.a = 1f;
			upHolder.GetComponent<Image>().color = tempColor;
		}
		if(type == "Down"){
			Color tempColor = downHolder.GetComponent<Image>().color;
			tempColor.a = 1f;
			downHolder.GetComponent<Image>().color = tempColor;
		}
		if(type == "Left"){
			Color tempColor = leftHolder.GetComponent<Image>().color;
			tempColor.a = 1f;
			leftHolder.GetComponent<Image>().color = tempColor;
		}
		if(type == "Right"){
			Color tempColor = rightHolder.GetComponent<Image>().color;
			tempColor.a = 1f;
			rightHolder.GetComponent<Image>().color = tempColor;
		}	
		if(type == "WallSeed"){
			Color tempColor = wallSeedHolder.GetComponent<Image>().color;
			tempColor.a = 1f;
			wallSeedHolder.GetComponent<Image>().color = tempColor;
		}		
		if(type == "LeftSeed"){
			Color tempColor = leftSeedHolder.GetComponent<Image>().color;
			tempColor.a = 1f;
			leftSeedHolder.GetComponent<Image>().color = tempColor;
		}	
		if(type == "RightSeed"){
			Color tempColor = rightSeedHolder.GetComponent<Image>().color;
			tempColor.a = 1f;
			rightSeedHolder.GetComponent<Image>().color = tempColor;
		}	
		if(type == "UpSeed"){
			Color tempColor = upSeedHolder.GetComponent<Image>().color;
			tempColor.a = 1f;
			upSeedHolder.GetComponent<Image>().color = tempColor;
		}	
		if(type == "DownSeed"){
			Color tempColor = downSeedHolder.GetComponent<Image>().color;
			tempColor.a = 1f;
			downSeedHolder.GetComponent<Image>().color = tempColor;
		}			
	}
	public bool isAvailable(string type){
		if(type == "Pedro" || type == "Wall"){
			if(pedroNumber!=0){
				return true;
			}
			else{
				return false;
			}
		}
		if(type == "Left"){
			if(leftNumber!=0){
				return true;
			}
			else{
				return false;
			}
		}
		if(type == "Right"){
			if(rightNumber!=0){
				return true;
			}
			else{
				return false;
			}
		}
		if(type == "Down"){
			if(downNumber!=0){
				return true;
			}
			else{
				return false;
			}
		}
		if(type == "Up"){
			if(upNumber!=0){
				return true;
			}
			else{
				return false;
			}
		}
		if(type == "WallSeed"){
			if(wallSeedNumber!=0){
				return true;
			}
			else{
				return false;
			}
		}
		if(type == "LeftSeed"){
			if(leftSeedNumber!=0){
				return true;
			}
			else{
				return false;
			}
		}
		if(type == "RightSeed"){
			if(rightSeedNumber!=0){
				return true;
			}
			else{
				return false;
			}
		}
		if(type == "UpSeed"){
			if(upSeedNumber!=0){
				return true;
			}
			else{
				return false;
			}
		}
		if(type == "DownSeed"){
			if(downSeedNumber!=0){
				return true;
			}
			else{
				return false;
			}
		}
		else return false;
	}
	public void updateValueDown(string type){
		if(type == "Pedro" || type == "Wall"){
			pedroNumber --;
			pedroHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Wall x" + pedroNumber;
			if(pedroNumber == 0){
				pedroHolder.GetComponent<Image>().color = Color.gray;
			}

		}
		if(type == "Up"){
			upNumber--;
			upHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Up x" + upNumber;
			if(upNumber == 0){
				upHolder.GetComponent<Image>().color = Color.gray;
			}
		}
		if(type == "Down"){
			downNumber--;
			downHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Down x" + downNumber;
			if(downNumber == 0){
				downHolder.GetComponent<Image>().color = Color.gray;
			}
		}
		if(type == "Left"){
			leftNumber--;
			leftHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Left x" + leftNumber;
			if(leftNumber == 0){
				leftHolder.GetComponent<Image>().color = Color.gray;
			}
		}
		if(type == "Right"){
			rightNumber--;
			rightHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Right x" + rightNumber;
			if(rightNumber == 0){
				rightHolder.GetComponent<Image>().color = Color.gray;
			}
		}	
		if(type == "WallSeed"){
			wallSeedNumber--;
			wallSeedHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "WallSeed x" + wallSeedNumber;
			if(wallSeedNumber == 0){
				wallSeedHolder.GetComponent<Image>().color = Color.gray;
			}
		}	
		if(type == "LeftSeed"){
			leftSeedNumber--;
			leftSeedHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "LeftSeed x" + leftSeedNumber;
			if(leftSeedNumber == 0){
				leftSeedHolder.GetComponent<Image>().color = Color.gray;
			}
		}	
		if(type == "RightSeed"){
			rightSeedNumber--;
			rightSeedHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "RightSeed x" + rightSeedNumber;
			if(rightSeedNumber == 0){
				rightSeedHolder.GetComponent<Image>().color = Color.gray;
			}
		}	
		if(type == "UpSeed"){
			upSeedNumber--;
			upSeedHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "UpSeed x" + upSeedNumber;
			if(upSeedNumber == 0){
				upSeedHolder.GetComponent<Image>().color = Color.gray;
			}
		}	
		if(type == "DownSeed"){
			downSeedNumber--;
			downSeedHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "DownSeed x" + downSeedNumber;
			if(downSeedNumber == 0){
				downSeedHolder.GetComponent<Image>().color = Color.gray;
			}
		}		
	}	
	public void updateText(string type){
		if(type == "Pedro" || type == "Wall"){
			pedroHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Wall x" + pedroNumber;
		}
		if(type == "Up"){
			upHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Up x" + upNumber;
		}
		if(type == "Down"){
			downHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Down x" + downNumber;
		}
		if(type == "Left"){
			leftHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Left x" + leftNumber;
		}
		if(type == "Right"){
			rightHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Right x" + rightNumber;
		}
		if(type == "WallSeed"){
			wallSeedHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "WallSeed x" + wallSeedNumber;
		}
		if(type == "RightSeed"){
			rightSeedHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "RightSeed x" + rightSeedNumber;
		}
		if(type == "LeftSeed"){
			leftSeedHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "LeftSeed x" + leftSeedNumber;
		}
		if(type == "UpSeed"){
			upSeedHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "UpSeed x" + upSeedNumber;
		}
		if(type == "DownSeed"){
			downSeedHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "DownSeed x" + downSeedNumber;
		}
	}


	public void AssignHint(){
		hinting = true;
		LevelManager.isdragging = true;	
		Swiping.canswipe = false;
		//RewardHint();
		if(HintAvailable()){
			if(LevelManager.adFree){
				RewardHint();
			}
			
			else{
				GoogleAds.Instance.UserOptToWatchAd();	
			}
			

		}
		else
			AnnounceNoHintAvailable();
	}


	public void AnnounceNoHintAvailable(){
		hinting = false;
		LevelManager.isdragging = false;	
		Swiping.canswipe = true;
		LevelBuilder.hintboard.SetActive(false);
	}


	public bool HintAvailable(){
		int rightones = 0;
		for(int i = 0; i < placedpieces.Count; i++){//first pass, to confirm the ones in right place
			if(PartofSolution(i)){//if placed in a correct solution, this is to find if any piece is in the right place.
				rightones++;
			}
		} 	
		if(rightones == placedpieces.Count){
			Debug.Log("ALL IN RIGHT PLACE");
			return false;
		}
		return true;
	}


	public void RewardHint(){
		StartCoroutine(HintWrapper());
	}


	public IEnumerator HintWrapper(){
		//make sure draggers are off and cant move fox.
		//turn off draggers
		//
		//yield return new WaitForSeconds(.1f);
		if(!LevelManager.configging)	{
			Debug.Log("CALLED WRAPPER");
			for(int i = 0; i <placedpieces.Count; i++){
				placedpieces[i].gameObject.GetComponent<BoxCollider>().enabled = false;
			}
			LevelBuilder.hintboard.SetActive(false);
			yield return new WaitForSeconds(.1f);
			Hint();			
		}	

	}


	public void Hint(){//right now works for one stored solution.


		Debug.Log("CALLING HINT");
		//LevelBuilder.hintboard.SetActive(false);
		Debug.Log(placedpieces.Count + "Pieces placed");
		Vector2 postogo;
		GameObject piece;
		string postype;

		if(TurnBehaviour.turn == 0){
		// for(int i = 0; i < placedpieces.Count; i++){
		// 	Debug.Log(placedpieces.Count);
		// 	Debug.Log("UNO");
		// postype = placedpieces[i].myType;

		// postogo = LookforPosition(postype);
		// Debug.Log(postogo);

		// removePiece(new Vector2(placedpieces[i].transform.position.x, -placedpieces[i].transform.position.z), placedpieces[i].myType);
						
		// PlaceHint(placedpieces[i], postogo);	
		// }
		// return;
			int rightones = 0;
			for(int i = 0; i < placedpieces.Count; i++){//first pass, to confirm the ones in right place
				//Debug.Log(placedpieces[i].myType + "" + placedpieces[i].transform.position.x + "" + -placedpieces[i].transform.position.z);
				if(PartofSolution(i)){//if placed in a correct solution, this is to find if any piece is in the right place.
				//filter out solutions without this piece
					Debug.Log("Piece in the right place");
					//CrownPiece(Dragger placedpieces[i]);
					placedpieces[i].transform.GetChild(1).GetComponent<Renderer>().material.color = Color.green;
					rightones++;
				}

			} 	
			if(rightones == placedpieces.Count){
				Debug.Log("ALL IN RIGHT PLACE");
			}

			for(int i = 0; i < placedpieces.Count; i++){//second pass, to correct the first one found in wrong place.
				if(!PartofSolution(i)){
					if(placedpieces[i].myType == "Seed"){
						postype =placedpieces[i].mySeedType + placedpieces[i].myType;
					}
					else{
						postype = placedpieces[i].myType;
					}
					Debug.Log(placedpieces[i].myType);
					//LookforPosition(placedpieces[i].myType);
					postogo = LookforPosition(postype);
					if(postogo != new Vector2(0,0)){
						Debug.Log(postogo + "POSTOGO");

						removePiece(new Vector2(placedpieces[i].transform.position.x, -placedpieces[i].transform.position.z), placedpieces[i].myType);
						PlaceHint(placedpieces[i], postogo);
						return;

					}
				}
			}
			for(int i=0; i < holders.Count; i++){//if none on board can be moved to a good one (because a wrong is there), look through holders
				Debug.Log("CASE 3 DO NOT WANT, MEANS ALL GOOD OR MISSING PIECES");
				// if(holders[i].currentpieces > 0){
				// 	postogo = LookforPosition(holders[i].name);
				// 	//Debug.Log("Instantiate a "+ holders[i].name);
				// 	if(postogo!= new Vector2(0,0)){
				// 		piece = holders[i].mygameobject.GetComponent<UIMonster>().Spawnit();
				// 		PlaceHint(piece.GetComponent<Dragger>(), postogo);
				// 		updateValueDown(holders[i].name);
				// 		placedpieces.Add(piece.GetComponent<Dragger>());
				// 		return;
				// 	}
				// }
			}
			// for(int i = 0; i < placedpieces.Count; i++){//second pass, to correct the first one found in wrong place.
			// 	if(!PartofSolution(i)){
			// 		if(placedpieces[i].myType == "Seed"){
			// 			postype =placedpieces[i].mySeedType + placedpieces[i].myType;
			// 		}
			// 		else{
			// 			postype = placedpieces[i].myType;
			// 		}
			// 		Debug.Log(placedpieces[i].myType);
			// 		//LookforPosition(placedpieces[i].myType);
			// 		postogo = LookforPosition(postype);
			// 		Debug.Log(postogo);
			// 		if(postogo == new Vector2(0,0)){
			// 			//Swap(i);
			// 			return;

			// 		}
			// 	}				
			// }
		}
		Debug.Log("End hint");
		hinting = false;
        for(int i=0; i<placedpieces.Count; i++){
        	placedpieces[i].gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        LevelManager.isdragging = false;
	}
	/*public void Swap(int place){
		for(int i =0; i<LevelManager.hints.Count; i++){
			if(type != LevelManager.hints[i].type){
				if(!Occupied(LevelManager.hints[i])){
					return new Vector2(LevelManager.hints[i].x, LevelManager.hints[i].y);
				}
			}
		}
		return new Vector2(0,0);		
	}*/
	public void CrownPiece(Dragger dg){
		
	}
	public void HintMenu(){
		LevelManager.isdragging = true;
		//hintText.text = "Hint x " + LevelManager.hintCurrency.ToString();
		for(int i = 0; i <placedpieces.Count; i++){
			placedpieces[i].gameObject.GetComponent<BoxCollider>().enabled = false;
		}
		LevelBuilder.hintboard.SetActive(true);
	}
	public void CloseHintMenu(){
		LevelBuilder.hintboard.SetActive(false);
		hinting = false;
        for(int i=0; i<placedpieces.Count; i++){
        	placedpieces[i].gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        LevelManager.isdragging = false;

	}
	public void WatchAd(){
		// LevelManager.hintCurrency = LevelManager.hintCurrency+3;
		// PlayerPrefs.SetInt("hintCurrency", LevelManager.hintCurrency);
		// hintText.text = "Hint x " + LevelManager.hintCurrency.ToString();
	}
	public void HintOrRestart(){
		if(TurnBehaviour.turn == 0&&!hinting){
			if(LevelManager.adFree){
				RewardHint();
			}
			else{
				HintMenu();

			}
		}
		if(TurnBehaviour.turn == 1){
			sl.ResetLevelButton();
		}
	}

	public void RestartAndHint(){
		StartCoroutine(RestartWithHint());
		//Debug.Log(placedpieces[0].gameObject.GetComponent<BoxCollider>().enabled);
	}
	public IEnumerator RestartWithHint(){
		SceneLoading.gamelost.SetActive(false);
		sl.ResetLevelButton();
		yield return new WaitForSeconds(1);
		if(LevelManager.adFree){
			RewardHint();
		}
		else{
			HintMenu();

		}
	}
	public IEnumerator HintTowards(Vector3 targetpos, Dragger dragger){
		dragger.transform.position = new Vector3(targetpos.x,0,targetpos.y);	
		yield break;
	}
	public void PlaceHint(Dragger dragger, Vector2 position){
		//dragger.transform.GetChild(1).gameObject.SetActive(true);
		draggee = dragger;
		dragger.GoToHint(new Vector3(position.x,0,-position.y));
		//hintText.text = "Hint x " + LevelManager.hintCurrency.ToString();
		// if(dragger.myType == "Wall" || dragger.myType == "Seed"){
		// 	placeNormal(position, dragger);
		// }
		// if(dragger.myType == "Left" || dragger.myType == "Up" || dragger.myType == "Down" || dragger.myType == "Right"){
		// 	placeIcarus(position,dragger);
		// }
		//placedpieces.Add(dragger);
	}
	public bool PartofSolution(int position){
		string postype;
		if(placedpieces[position].myType == "Seed"){
			postype =placedpieces[position].mySeedType + placedpieces[position].myType;
		}
		else{
			postype = placedpieces[position].myType;
		}
		for (int i = 0; i <LevelManager.hints.Count; i++){
		Debug.Log(placedpieces[position].myType + "" + LevelManager.hints[i].type);

			if(postype == LevelManager.hints[i].type && 
				placedpieces[position].transform.position.x == LevelManager.hints[i].x &&
				-placedpieces[position].transform.position.z == LevelManager.hints[i].y){

				return true;

			}
		}
		return false;
	}
	public int CountUnplaced(){
		int amount = 0;
		for (int i = 0; i<holders.Count; i++){
			amount = amount+holders[i].currentpieces;
		}
		return amount;
	}
	public void CountPlaced(){
		int amount = 0;
		for (int i=0; i<holders.Count; i++){

		}
	}
	public Vector2 LookforPosition(string type){
		for(int i =0; i<LevelManager.hints.Count; i++){
			if(type == LevelManager.hints[i].type){
				if(!Occupied(LevelManager.hints[i])){
					return new Vector2(LevelManager.hints[i].x, LevelManager.hints[i].y);
				}
			}
		}
		return new Vector2(0,0);
	}
	public bool Occupied(Hint hint){
		return LevelBuilder.tiles[hint.x,hint.y].isTaken;
		//return false;
	}

 	public void placeNormal(Vector2 positiontogo, Dragger td){
	 	if(LevelBuilder.tiles[(int)positiontogo.x, (int)positiontogo.y].type == "Left" || 
	 	LevelBuilder.tiles[(int)positiontogo.x, (int)positiontogo.y].type == "Down" ||
	 	LevelBuilder.tiles[(int)positiontogo.x, (int)positiontogo.y].type == "Up" ||
	 	LevelBuilder.tiles[(int)positiontogo.x, (int)positiontogo.y].type == "Right"){
	 		
	 		LevelBuilder.tiles[(int)positiontogo.x, (int)positiontogo.y].isSideways = LevelBuilder.tiles[(int)positiontogo.x, (int)positiontogo.y].type;
 		
 		}
//		Debug.Log(td.myType);
		LevelBuilder.tiles[(int)positiontogo.x, (int)positiontogo.y].type = td.myType;
		LevelBuilder.tiles[(int)positiontogo.x, (int)positiontogo.y].isTaken = true;
		LevelBuilder.tiles[(int)positiontogo.x, (int)positiontogo.y].tileObj = td.gameObject;	
		if(td.myType == "Seed"){
			LevelBuilder.tiles[(int)positiontogo.x, (int)positiontogo.y].seedType = td.mySeedType;
		}
		if(td.myType == "Wall"){
			td.gameObject.GetComponent<Animator>().SetInteger("Phase", 2);
		}
		
	}
	public void placeIcarus(Vector2 target, Dragger td){
		td.gameObject.GetComponent<Animator>().SetInteger("Phase", 2);
		if(td.myType == "Left"){
			LevelBuilder.tiles[(int)target.x, (int)target.y].type = "Wall";
			LevelBuilder.tiles[(int)target.x, (int)target.y].isTaken = true;
			LevelBuilder.tiles[(int)target.x, (int)target.y].tileObj = td.gameObject;
			if(LevelBuilder.tiles[(int)target.x-1, (int)target.y].type == "Ice"){
				LevelBuilder.tiles[(int)target.x-1, (int)target.y].type = td.myType;		
			}	

			else{
				LevelBuilder.tiles[(int)target.x-1, (int)target.y].isSideways = "Left";
			}
		

		} 
		if(td.myType =="Right"){
			LevelBuilder.tiles[(int)target.x, (int)target.y].type = "Wall";
			LevelBuilder.tiles[(int)target.x, (int)target.y].isTaken = true;
			LevelBuilder.tiles[(int)target.x, (int)target.y].tileObj = td.gameObject;
			if(LevelBuilder.tiles[(int)target.x+1, (int)target.y].type == "Ice"){
				LevelBuilder.tiles[(int)target.x+1, (int)target.y].type = td.myType;		
			}	
			else{
				LevelBuilder.tiles[(int)target.x+1, (int)target.y].isSideways = "Right";
			}	

		}
		if(td.myType == "Up" ){
			LevelBuilder.tiles[(int)target.x, (int)target.y].type = "Wall";
			LevelBuilder.tiles[(int)target.x, (int)target.y].isTaken = true;
			LevelBuilder.tiles[(int)target.x, (int)target.y].tileObj = td.gameObject;	
			if(LevelBuilder.tiles[(int)target.x, (int)target.y-1].type == "Ice"){
				LevelBuilder.tiles[(int)target.x, (int)target.y-1].type = td.myType;		
			}	
			else{
				LevelBuilder.tiles[(int)target.x, (int)target.y-1].isSideways = "Up";
			}
		}
		if(td.myType == "Down"){
			LevelBuilder.tiles[(int)target.x, (int)target.y].type = "Wall";
			LevelBuilder.tiles[(int)target.x, (int)target.y].isTaken = true;
			LevelBuilder.tiles[(int)target.x, (int)target.y].tileObj = td.gameObject;	
			if(LevelBuilder.tiles[(int)target.x, (int)target.y+1].type == "Ice"){
				LevelBuilder.tiles[(int)target.x, (int)target.y+1].type = td.myType;		
			}	
			else{
				LevelBuilder.tiles[(int)target.x, (int)target.y+1].isSideways = "Down";
			}
		}
	}
	public void removePiece(Vector2 target, string type){
		if(type == "Wall"){
			LevelBuilder.tiles[(int)target.x, (int)target.y].type = "Ice";
			LevelBuilder.tiles[(int)target.x, (int)target.y].isTaken = false;	
			if(LevelBuilder.tiles[(int)target.x, (int)target.y].isSideways != null){
				LevelBuilder.tiles[(int)target.x, (int)target.y].type = LevelBuilder.tiles[(int)target.x, (int)target.y].isSideways;
			}			
		}


		if(type == "Left" || type == "Right" || type == "Down" || type == "Up" )	{
			removeIcarus(type, target);
		}
		if(type == "Seed"){
			Debug.Log("SEEDED");
			LevelBuilder.tiles[(int)target.x, (int)target.y].type = "Ice";
			LevelBuilder.tiles[(int)target.x, (int)target.y].isTaken = false;	
			if(LevelBuilder.tiles[(int)target.x, (int)target.y].isSideways != null){
				LevelBuilder.tiles[(int)target.x, (int)target.y].type = LevelBuilder.tiles[(int)target.x, (int)target.y].isSideways;
			}				
		}
	}
	public void removeIcarus(string type, Vector3 target){
		Debug.Log(type);
		LevelBuilder.tiles[(int)target.x, (int)target.y].type = "Ice";
		LevelBuilder.tiles[(int)target.x, (int)target.y].isTaken = false;	
		if(LevelBuilder.tiles[(int)target.x, (int)target.y].isSideways != null){
				LevelBuilder.tiles[(int)target.x, (int)target.y].type = LevelBuilder.tiles[(int)target.x, -(int)target.z].isSideways;
		}	

		if(type == "Left"){
			if(LevelBuilder.tiles[(int)target.x-1, (int)target.y].type == type){
				LevelBuilder.tiles[(int)target.x-1, (int)target.y].type = "Ice";		
				LevelBuilder.tiles[(int)target.x-1, (int)target.y].isTaken = false;	
				LevelBuilder.tiles[(int)target.x-1, (int)target.y].isSideways = null;					
			}
			else if(LevelBuilder.tiles[(int)target.x-1, (int)target.y].isSideways == type){
				LevelBuilder.tiles[(int)target.x-1, (int)target.y].isSideways = null;

			}


		} 
		if(type =="Right"){
			if(LevelBuilder.tiles[(int)target.x+1, (int)target.y].type == type){
				LevelBuilder.tiles[(int)target.x+1, (int)target.y].type = "Ice";		
				LevelBuilder.tiles[(int)target.x+1, (int)target.y].isTaken = false;	
				LevelBuilder.tiles[(int)target.x+1, (int)target.y].isSideways = null;					
			}		
			else if(LevelBuilder.tiles[(int)target.x+1, (int)target.y].isSideways == type){
				LevelBuilder.tiles[(int)target.x+1, (int)target.y].isSideways = null;

			}
		}
		if(type == "Up" ){	
			if(LevelBuilder.tiles[(int)target.x, (int)target.y-1].type == type){
				LevelBuilder.tiles[(int)target.x, (int)target.y-1].type = "Ice";		
				LevelBuilder.tiles[(int)target.x, (int)target.y-1].isTaken = false;	
				LevelBuilder.tiles[(int)target.x, (int)target.y-1].isSideways = null;					
			}	
			else if(LevelBuilder.tiles[(int)target.x, (int)target.y-1].isSideways == type){
				LevelBuilder.tiles[(int)target.x, (int)target.y-1].isSideways = null;

			}	
			//else if (LevelBuilder.tiles[(int)target.x, (int)target.y+1].type == "Ice"){
			//	LevelBuilder.tiles[(int)target.x, (int)target.y+1].isSideways = null;//THESE CAN BE REPLACED BY NAMESIDEWAYS() WHICH CAN LOOK FOR LRUDS AROUND IT.
			//}
		}
		if(type == "Down"){
			if(LevelBuilder.tiles[(int)target.x, (int)target.y+1].type == type){
				LevelBuilder.tiles[(int)target.x, (int)target.y+1].type = "Ice";		
				LevelBuilder.tiles[(int)target.x, (int)target.y+1].isTaken = false;	
				LevelBuilder.tiles[(int)target.x, (int)target.y+1].isSideways = null;					
			}
			else if(LevelBuilder.tiles[(int)target.x, (int)target.y+1].isSideways == type){
				LevelBuilder.tiles[(int)target.x, (int)target.y+1].isSideways = null;

			}		
		}
	}
}
