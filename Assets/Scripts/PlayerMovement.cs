using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Color;

public class PlayerMovement : MonoBehaviour {

	public Vector3 currenttile;
	public Vector3 startingposition;
	public bool cantakeinput;
	public int speed;
	public string direction;
	bool istiletaken;
	public GameObject tileobject;
//	Tilehandler tilescript;
	Vector3 tiletotest;
	public bool canmove;
	GameObject tiletaker;
	public string nextaction;
	public bool beingdragged;
	public GameObject lastFragile;
	public static bool isspeeding;
	public static string character_direction;
	public GameObject lastSeed;
//	Seed_Behaviour myseedbehaviour;
	bool firstmove; //used to count turns
	public GameObject levelWonBoard;
	public GameObject LevelLostBoard;
	//RatingPopUp PopupScript;
	string myswipe;
	bool outofmap;
	Tile tilescript;
	bool hasmoved;
	Color fragilered = new Color(253/255f,65/255f,65/255f,255/255f);
	ProgressBar TG;
	string type;
	int tilenumber;
	public PlayerAnimation animationController;
	public static bool boop;
	public Vector3 lastbooped;
	public static bool boopout;
	public static Vector3 shakeNoise;
	public static bool longgoal;
	GameObject menuButton;
	GameObject hintButton;
	bool hasstopped;
	GameObject wallToHit;

	//public KeySimulator mykeysimulator;
	// Use this for initialization
	public void Start () {
		//current tile works as a target to move to
		LevelManager.playert = this.gameObject.transform;

		hasstopped = false;
		animationController = GetComponent<PlayerAnimation>();
		startingposition = transform.position;
		currenttile = transform.position;
		cantakeinput = true;
		nextaction = null;
		beingdragged = false;
		lastFragile = null;
		isspeeding = false;
		levelWonBoard = LevelBuilder.winboard;
		if (levelWonBoard != null) {
			SceneLoading.gamewon = levelWonBoard;
			levelWonBoard.SetActive (false);

		}
		LevelLostBoard = LevelBuilder.loseboard;
		if (LevelLostBoard != null) {
			SceneLoading.gamelost = LevelLostBoard;
			LevelLostBoard.SetActive (false);

		}
		outofmap = false;
		hasmoved = false;
		TG = GameObject.Find("ProgressBar").GetComponent<ProgressBar>();
		menuButton = GameObject.Find("Menu");
		hintButton = GameObject.Find("Hint");
	}
	void ResetBoards(){
		if (levelWonBoard == null) {
			levelWonBoard = SceneLoading.gamewon;
			levelWonBoard.SetActive(false);
		}

		if (LevelLostBoard == null) {
			LevelLostBoard = SceneLoading.gamelost;
			LevelLostBoard.SetActive (false);
		}		
	}
	void TurnChanger(){
		if (transform.position != startingposition && TurnBehaviour.turn == 0) {

			TurnBehaviour.turn = 1;
			menuButton.GetComponent<Image>().sprite = menuButton.GetComponent<ImageHolder>().imagetwo;
			hintButton.GetComponent<Image>().sprite = hintButton.GetComponent<ImageHolder>().imagetwo;
			
			LevelBuilder.ChangeBackground("Color_A7A46709", new Color(154f/255f,53f/255f,53f/255f,0),.3f);
		}
		if (TurnBehaviour.turn == 1 && transform.position == startingposition) {
			TurnBehaviour.turn = 0; 
			menuButton.GetComponent<Image>().sprite = menuButton.GetComponent<ImageHolder>().imageone;
			hintButton.GetComponent<Image>().sprite = hintButton.GetComponent<ImageHolder>().imageone;
	
		}		
	}
	// Update is called once per frame
	void Update () {
//		Debug.Log(PieceHolders.placedpieces.Count);
//		Debug.Log(PieceHolders.placedpieces[0].transform.position);
//		Debug.Log(PieceHolders.placedpieces[1].transform.position);
		if(canmove){
			this.transform.GetChild(0).GetComponent<Animator>().SetInteger("Phase", 0);

		}
		if(!canmove && !GoalBehaviour.goaling){
			this.transform.GetChild(0).GetComponent<Animator>().SetInteger("Phase", 1);
		}

		myswipe = Swiping.mydirection;

		ResetBoards();

		TurnChanger();

		if(Vector3.Distance(currenttile, transform.position) <.6f && !boop && 
			transform.position != startingposition && nextaction !="Goal_Action"){
			if(lastbooped != currenttile){
				boop = true;
				lastbooped = currenttile;
				AssignShakeOrientation(character_direction);
			}
		}
		if(Vector3.Distance(currenttile, transform.position) <.8f && !boop && 
			transform.position != startingposition && nextaction !="Goal_Action"){
			if(wallToHit!= null){
				wallToHit.GetComponent<Animator>().SetTrigger("Hit");
				wallToHit = null;				
			}
		}		


		if (currenttile == transform.position /*&& !hasstopped*/) {//do this when reached currenttile
			//Debug.Log("WHAT IS NEXTACTION BABY DONT " + nextaction + " ME");
			//Debug.Log("On tile");
			Debug.Log("GOT TO TILE");
			ActOnStopped();
		}
			Movement ();
	}
	void ActOnStopped(){
		GoalBehaviour.isstatic = true;
		if (lastFragile != null && lastFragile.transform.position == transform.position && nextaction== null) {
			Debug.Log ("UNUL");
			this.enabled=false;
			Debug.Log("Hole");
			StartCoroutine(PopLose());
			StartCoroutine(animationController.Disappear(.3f));
			SfxHandler.Instance.StopSlide();
			SfxHandler.Instance.PlayHole();

		}

		else if (nextaction == null && !canmove) {
			//Debug.Log("NullNULLNULLNULL");
			cantakeinput = true;
			canmove = true;
			isspeeding = false;
			hasstopped = true;
			if(hasmoved){
//				Debug.Log(tiletotest);
			SfxHandler.Instance.PlayWallHit((int)tiletotest.x, (int)-tiletotest.z);
			SfxHandler.Instance.StopSlide();					
			}
		}  
		else if (nextaction == "Goal_Action") {
			RatingPopUp.GiveRating ();//stores to playerprefs
				SceneLoading.SetStars(RatingBehaviour.currentrating);
				this.enabled = false;
			StartCoroutine(PopWin());
		}			
		else if (nextaction == "Hole_Action") {
			SfxHandler.Instance.StopSlide();
			SfxHandler.Instance.PlayHole();
			this.enabled=false;
			Debug.Log("Hole");
			StartCoroutine(PopLose());
			StartCoroutine(animationController.Disappear(.3f));
		}
		else if (nextaction == "Left_Action") {
			Debug.Log(nextaction);
			tiletotest = currenttile;
			canmove = true;
			while (canmove == true) {
				character_direction = "Left";
				tiletotest += Vector3.left;
				this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
				FindTileTag ();
				ActOnTile ();
				isspeeding = true;
				SfxHandler.Instance.PlayIcarus();
			}
			if (nextaction == "Left_Action") {
				nextaction = null;
			}
		}
		else if (nextaction == "Right_Action") {
			Debug.Log(nextaction);
			tiletotest = currenttile;
			canmove = true;
			while (canmove == true) {
				character_direction = "Right";
				tiletotest += Vector3.right;
				this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
				FindTileTag ();
				ActOnTile ();
				isspeeding = true;
				SfxHandler.Instance.PlayIcarus();
			}
			Debug.Log("CURRENT NEXT ACTION IS " + nextaction);
			if (nextaction == "Right_Action") {
				Debug.Log("MAKING NULL");
				nextaction = null;
			}
		}
		else if (nextaction == "Up_Action") {
							Debug.Log(nextaction);
			character_direction = "Up";
			tiletotest = currenttile;
			canmove = true;
			while (canmove == true) {
				this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
				tiletotest += Vector3.forward;
				FindTileTag ();
				ActOnTile ();
				isspeeding = true;
				SfxHandler.Instance.PlayIcarus();
			}
			if (nextaction == "Up_Action") {
				nextaction = null;
			}
		}
		else if (nextaction == "Down_Action") {
			Debug.Log(nextaction);
			character_direction = "Down";
			tiletotest = currenttile;
			canmove = true;
			while (canmove == true) {
				this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,270,0));
				tiletotest += Vector3.back;
				FindTileTag ();
				ActOnTile ();
				isspeeding = true;
				SfxHandler.Instance.PlayIcarus();
			}
			if (nextaction == "Down_Action") {
				nextaction = null;
			}
		}
		else if (nextaction == "Portal_Action")
		{
			Debug.Log("TRIGGERING PORTAL ACTION");
			GameObject newPortal = GetMatchingPortal();
			Vector3 newPosition = newPortal.transform.position;
			transform.position = newPosition;
			tiletotest = transform.position;
			currenttile = tiletotest;
			Debug.Log("tiletotest is " + tiletotest);
			string newDirection = newPortal.GetComponent<Dragger>().portalType;
			character_direction = newDirection;
			canmove = true;
			
			NewPortalStart(character_direction);

		}
		if(wallToHit != null){
			wallToHit.GetComponent<Animator>().SetTrigger("Hit");
			wallToHit = null;
			hasstopped = true;

		}
	}
	GameObject GetMatchingPortal(){
		foreach(GameObject portal in LevelBuilder.Portals)
		{
			if(portal.transform.position.x == transform.position.x && portal.transform.position.y == transform.position.y){

			}
			else{
				Debug.Log("Portal selected type is " + portal.GetComponent<Dragger>().portalType);
				return portal;
			}
		}
		return null;

	}

	void NewPortalStart(string direction){
		nextaction = null;
		Debug.Log(transform.position + " " + direction + currenttile + tiletotest);
		switch(direction){
			case "Up":
				while (canmove == true) {
					this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
					tiletotest += Vector3.forward;
					FindTileTag ();
					ActOnTile ();
				}
				break;
			case "Down":
				while (canmove == true) {
					this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,270,0));
					tiletotest += Vector3.back;
					FindTileTag ();
					ActOnTile ();
				}
				break;
			case "Right":
				while (canmove == true) {
					this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
					tiletotest += Vector3.right;
					FindTileTag ();
					ActOnTile ();
				}
				break;
			case "Left":
				while (canmove == true) {
					this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
					tiletotest += Vector3.left;
					FindTileTag ();
					ActOnTile ();
				}
				break;
		}

	}
	public void AssignShakeOrientation(string shakeDirection){
		if(shakeDirection == "Up"){
			shakeNoise = new Vector3(0,0,-.5f);
		}
		if(shakeDirection == "Down"){
			shakeNoise = new Vector3(0,0,.5f);
		}
		if(shakeDirection == "Left"){
			shakeNoise = new Vector3(.5f,0,0);
		}
		if(shakeDirection == "Right"){
			shakeNoise = new Vector3(-.5f,0,0);
		}
		if(nextaction == "Up_Action"){
			shakeNoise = new Vector3(0,0,-.5f);
		}
		if(nextaction == "Down_Action"){
			shakeNoise = new Vector3(0,0,.5f);
		}
		if(nextaction == "Left_Action"){
			shakeNoise = new Vector3(.5f,0,0);
		}
		if(nextaction == "Right_Action"){
			shakeNoise = new Vector3(-.5f,0,0);
		}
	}
	void QWERTYMove(){
		//Debug.Log(LevelManager.isdragging);
		if(!LevelManager.isdragging){
			if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || myswipe == "Up" /*|| mykeysimulator.W*/ ) {
				hasmoved = true;
				tiletotest = currenttile;
//				Debug.Log(tiletotest);
				if (canmove == true) {
					hasstopped = false;
					firstmove = true;
					boop = false;
					MenuButton.CloseMenu();
				}
				tilenumber = 0;
				while (canmove == true) {
					character_direction = "Up";
					this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
					tiletotest += Vector3.forward;
					FindTileTag ();
					ActOnTile ();
					tilenumber++;
				}
				//mykeysimulator.W = false;
			}
			if (Input.GetKeyDown (KeyCode.A)|| Input.GetKeyDown(KeyCode.LeftArrow) || myswipe == "Left" /*|| mykeysimulator.A*/) {
				hasmoved = true;
				tiletotest = currenttile;
				if (canmove == true) {
					hasstopped = false;

					firstmove = true;
					boop = false;
					MenuButton.CloseMenu();
				}
				tilenumber = 0;
				while (canmove == true) {	
					character_direction = "Left";
					this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
					tiletotest += Vector3.left;
					FindTileTag ();
					ActOnTile ();
					tilenumber++;
	//				Debug.Log(canmove);

				}
				//mykeysimulator.A = false;
			}
			if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || myswipe == "Down" /*|| mykeysimulator.S */) {
				hasmoved = true;
				tiletotest = currenttile;
				if (canmove == true) {
					hasstopped = false;
					firstmove = true;
					boop = false;
					MenuButton.CloseMenu();
				}
				tilenumber =0;
				while (canmove == true) {
					character_direction = "Down";
					this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,270,0));
					tiletotest += Vector3.back;
					FindTileTag ();
					ActOnTile ();
					tilenumber++;
				}
				//mykeysimulator.S = false;
			}
			if (Input.GetKeyDown (KeyCode.D)|| Input.GetKeyDown(KeyCode.RightArrow) || myswipe == "Right" /*|| mykeysimulator.D*/) {
				hasmoved = true;
				tiletotest = currenttile;
				if (canmove == true) {
					hasstopped = false;
					firstmove = true;
					boop = false;
					MenuButton.CloseMenu();
				}
				tilenumber = 0;
				while (canmove == true) {
					character_direction = "Right";
					this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
					tiletotest += Vector3.right;
					FindTileTag ();
					ActOnTile ();
					tilenumber++;
				}
				//mykeysimulator.D = false;
			}			
		}
		
	}

	void FindTileTag(){

		if(inside()){
				tilescript = LevelBuilder.tiles[(int)tiletotest.x,-(int)tiletotest.z];
				istiletaken = tilescript.isTaken;			
		}
		else{
				istiletaken = true;
				outofmap = true;
		}
		
	}
	public bool inside(){
		//Debug.Log("x is " + tiletotest.x + "y is" + tiletotest.z);
		//Debug.Log(LevelBuilder.totaldimension);
		if((int)tiletotest.x>=0 && (int)tiletotest.x < LevelBuilder.totaldimension && 
			-(int)tiletotest.z>=0 && -(int)tiletotest.z < LevelBuilder.totaldimension){
			return true;
		}
		else 
		return false;
	}
	void Movement(){  
		//check for input when its your turn.
		if (cantakeinput) {
			QWERTYMove ();
		}
		//if the desired tile is not the place you're standing in it moves there
		if (currenttile != transform.position && beingdragged == false) {
			GoalBehaviour.readytomove = true;
			transform.position = Vector3.MoveTowards (transform.position, currenttile, Time.deltaTime * speed); 
			cantakeinput = false;
			Swiping.mydirection = "Null";
		}
	}
	void SpawnStatue(){
		LevelBuilder.starttransform.transform.GetChild(0).gameObject.SetActive(true);
	}
	void Count(){
		//Debug.Log(canmove + "CANMOVE");
		if(firstmove == true && canmove == true){
			if(TurnCounter.turncount == 0){
				//LevelBuilder.starttransform.GetComponentInChildren<Animator>().SetInteger("Phase",1);
				SpawnStatue();
			}
			SfxHandler.Instance.PlaySlide();
			TurnCounter.turncount++;
//			Debug.Log("TURNINGTURNINGTURNING AHOOOOOOOGA");
			TG.TakeTurn(TurnCounter.turncount);
			DotHandler.TakeTurn(TurnCounter.turncount);
		}
		if(firstmove == true){
			firstmove = false;
		}
		RatingBehaviour.CalculateRating ();
	}
	void PopSeed(string type){
		SpawnStatue();
		Debug.Log(type);
		Debug.Log(tilescript.seedType);
		Debug.Log(tiletotest.x + " " + tiletotest.z);
		if(tilescript.seedType == "Wall"){
			tilescript.type = tilescript.seedType;

		}
		if(tilescript.seedType == "Left"){
			tilescript.type = "Wall";
			if(LevelBuilder.tiles[(int)currenttile.x-1,-(int)currenttile.z].type == "Ice"){
				LevelBuilder.tiles[(int)currenttile.x-1,-(int)currenttile.z].type = "Left";
				LevelBuilder.tiles[(int)currenttile.x-1,-(int)currenttile.z].isTaken = true;				
			}
			else{
				LevelBuilder.tiles[(int)currenttile.x-1, -(int)currenttile.z].isSideways = "Left";				
			}

			Debug.Log(LevelBuilder.tiles[(int)currenttile.x-1,-(int)currenttile.z].type);
		}
		if(tilescript.seedType == "Right"){
			tilescript.type = "Wall";
			if(LevelBuilder.tiles[(int)currenttile.x+1,-(int)currenttile.z].type == "Ice"){
				LevelBuilder.tiles[(int)currenttile.x+1,-(int)currenttile.z].type = "Right";
				LevelBuilder.tiles[(int)currenttile.x+1,-(int)currenttile.z].isTaken = true;
			}
			else{
				LevelBuilder.tiles[(int)currenttile.x+1,-(int)currenttile.z].isSideways = "Right";
			}
			Debug.Log(LevelBuilder.tiles[(int)currenttile.x+1,-(int)currenttile.z].type);
		}
		if(tilescript.seedType == "Up"){
			tilescript.type = "Wall";
			if(LevelBuilder.tiles[(int)currenttile.x,-(int)currenttile.z-1].type == "Ice"){
				LevelBuilder.tiles[(int)currenttile.x,-(int)currenttile.z-1].type = "Up";
				LevelBuilder.tiles[(int)currenttile.x,-(int)currenttile.z-1].isTaken = true;
			}
			else{
				LevelBuilder.tiles[(int)currenttile.x,-(int)currenttile.z-1].isSideways = "Up";
			}
			Debug.Log(LevelBuilder.tiles[(int)currenttile.x,-(int)currenttile.z-1].type);
			LevelBuilder.tiles[(int)currenttile.x,-(int)currenttile.z-1].isSideways = "Up";


		}
		if(tilescript.seedType == "Down"){
			tilescript.type = "Wall";
			if(LevelBuilder.tiles[(int)currenttile.x,-(int)currenttile.z+1].type == "Ice"){
				LevelBuilder.tiles[(int)currenttile.x,-(int)currenttile.z+1].type = "Down";
				LevelBuilder.tiles[(int)currenttile.x,-(int)currenttile.z+1].isTaken = true;
			}
			else{
				LevelBuilder.tiles[(int)currenttile.x,-(int)currenttile.z+1].isSideways = "Down";
			}
			Debug.Log(LevelBuilder.tiles[(int)currenttile.x,-(int)currenttile.z+1].type);
			LevelBuilder.tiles[(int)currenttile.x,-(int)currenttile.z+1].isSideways = "Down";
		}
		lastSeed.GetComponent<Dragger>().readyToPop = true;
		lastSeed.GetComponent<Dragger>().playert = this.gameObject.transform;
	}
	//Individual Behaviours to be stored in the following.


	void ActOnTile(){
		Debug.Log(tilescript.type);
		Swiping.mydirection = "Null";
		if(outofmap == true)
		{
			canmove = false;
			outofmap = false;
		}

		else if (tilescript.type == "Ice")
		{
			Count ();
			currenttile = tiletotest;				
		}
		else if (tilescript.type == "Wall" || tilescript.type == "Start") 
		{
			Vector3 overlapV3 = tiletotest;	
//				Debug.Log(tiletotest);
			Collider[] colliders = Physics.OverlapSphere(overlapV3, .5f);
//				Debug.Log("LF PEDRO");
			foreach (Collider component in colliders) 
			{
				if (component.tag == "Pedro") 
				{
					Debug.Log("Pedro");
					wallToHit = component.gameObject;
					wallToHit.GetComponent<Animator>().ResetTrigger("Hit");
				} 
				if (component.tag == "PedroSeed") 
				{
					Debug.Log("PedroSeed");
					wallToHit = component.transform.GetChild(0).gameObject;
					wallToHit.GetComponent<Animator>().ResetTrigger("Hit");
				} 
			}

			canmove = false;
//				Debug.Log("canmove is false now");
			Count ();
		} 
		else if (tilescript.type == "Goal") 
		{
			//you'll stop in the tile you checked and stop moving.
			if (TurnCounter.turncount == 0) 
			{
				Debug.Log("0 turns");
				canmove = false;
			} 
			else 
			{
				if(transform.position == currenttile)
				{
					longgoal = false;
				}
				else
				{
					longgoal = true;
				}
				if(tilenumber == 0)
				{
					GoalBehaviour.goaling = true;
					this.transform.GetChild(0).GetComponent<Animator>().SetInteger("Phase", 2);
					StartCoroutine(animationController.Disappear(.4f));
					speed = 3;
				}
				Count ();
				currenttile = tiletotest;
				canmove = false;
				//Qeue up an action when reaching the tile
//					Debug.Log("aqui?");
				nextaction = "Goal_Action";
			}
		} 
		else if (tilescript.type == "Hole") 
		{
			//you'll stop in the tile you checked and stop moving.
			Count();
			currenttile = tiletotest;
			canmove = false;

			//Qeue up an action when reaching the tile
			nextaction = "Hole_Action";
		} 
		else if (tilescript.type == "Wood") 
		{
			Count ();
			currenttile = tiletotest;
			if(tilescript.isSideways!= null)
			{
				canmove = false;
				nextaction = tilescript.isSideways+ "_Action";
				Debug.Log(nextaction);
				isspeeding = true;				
			}
		} 
		else if (tilescript.type == "Left") 
		{

				Count ();
				currenttile = tiletotest;
				canmove = false;
				nextaction = "Left_Action";
				isspeeding = true;
		} 
		else if (tilescript.type == "Right") 
		{
				Count ();
				currenttile = tiletotest;
				canmove = false;
				nextaction = "Right_Action";
				isspeeding = true;
		} 
		else if (tilescript.type == "Up") 
		{
				Count ();
				currenttile = tiletotest;
				canmove = false;
				nextaction = "Up_Action";
				isspeeding = true;
		} 
		else if (tilescript.type == "Down") 
		{
				Count ();
				currenttile = tiletotest;
				canmove = false;
				nextaction = "Down_Action";
				isspeeding = true;
		} 
		else if (tilescript.type == "Fragile") 
		{
			Count ();
			currenttile = tiletotest;
			lastFragile = tilescript.tileObj;
			Vector3 overlapV3 = new Vector3(currenttile.x, currenttile.y, currenttile.z);
			Collider[] colliders = Physics.OverlapSphere(overlapV3, .5f);
			foreach (Collider component in colliders) 
			{
				if (component.tag == "Fragile") 
				{
					component.GetComponent<FragileBehaviour>().readytolava = true;
					component.GetComponent<FragileBehaviour>().player = this.gameObject;
				} 
			}
			if(tilescript.isSideways!= null)
			{
				canmove = false;
				nextaction = tilescript.isSideways+ "_Action";
				Debug.Log(nextaction);
				isspeeding = true;			

			}
			else
			{

			}
			tilescript.type = "Hole";
	
		} 
		else if (tilescript.type == "Quicksand") 
		{
			currenttile = tiletotest;
			lastFragile = tilescript.tileObj;
			Count ();
			if (isspeeding == false) 
			{
				currenttile = tiletotest;
				canmove = false;
				nextaction = "Hole_action";
			}

		}
		else if (tilescript.type == "Seed") 
		{
			currenttile = tiletotest;
			lastSeed = tilescript.tileObj;
			Debug.Log(tilescript.tileObj);
			PopSeed(tilescript.seedType);
			Count();
			
			if(tilescript.isSideways!= null)
			{

				canmove = false;
				nextaction = tilescript.isSideways+ "_Action";
				Debug.Log(nextaction);
				isspeeding = true;				
			}
		} 
		else if (tilescript.type == "Boss") 
		{
			currenttile = tiletotest;
			canmove = false;
		}
		else if(tilescript.type == "Portal")
		{
			Debug.Log(character_direction +"." + tilescript.portalType);
			if(Opposite(character_direction, tilescript.portalType) && !PathBlocked())
			{
				Count ();
				Debug.Log("ACtedonportaltileportalstyle");
				currenttile = tiletotest;
				canmove = false;
				nextaction = "Portal_Action";
				
			}
			else{
				canmove = false;
				Count ();
			}
			
		}
		else 
		{
			Debug.Log(tilescript.type);
			Debug.Log ("Dong");
			canmove = false;
		}
		Count ();
	}
	bool PathBlocked()
	{
		return false;
	}
	bool Opposite(string d1, string d2){
		if((d1 == "Left" && d2 == "Right") || (d1 == "Right" && d2 == "Left") ||
			(d1 == "Up" && d2 == "Down") || (d1 == "Down" && d2 == "Up")){
			return true;
		}
		else 
			return false;
	}
	public void CheckAchievement(){
		if(!LevelManager.ispotd){
			if(LevelManager.levelnum == 40){
				PlayServices.UnlockWorldAchievement(1);
			}
			if(LevelManager.levelnum == 80){
				PlayServices.UnlockWorldAchievement(2);
			}
			if(LevelManager.levelnum == 120){
				PlayServices.UnlockWorldAchievement(3);
			}
			if(LevelManager.levelnum == 160){
				PlayServices.UnlockWorldAchievement(4);
			}			
		}
	}
	IEnumerator PopWin(){
		CheckAchievement();
		int world = Mathf.FloorToInt((LevelManager.levelnum-1)/40)  + 1;
		if(longgoal){
			yield return new WaitForSeconds(.5f);
			levelWonBoard.SetActive (true);
			if(MenuButton.open){
				MenuButton.CloseMenu();
			}
		}
		else{
			yield return new WaitForSeconds(.62f);	
			levelWonBoard.SetActive (true);
			if(MenuButton.open){
				MenuButton.CloseMenu();
			}
		}
		WinMessage.Instance.AssignMessage(world, RatingPopUp.myrating);

	}


	IEnumerator PopLose(){
		float fadetime = 1.5f;
		float initpos = 0;
		float finalpos = -4;
		bool hasdowned = false;
		for(float t = 0.0f; t<fadetime; t+= Time.deltaTime){
			float normalizedTime = t/fadetime;
			transform.position = new Vector3(transform.position.x, Mathf.Lerp(initpos,finalpos,normalizedTime), transform.position.z);
			if(transform.position.y < -1.5f && !hasdowned && !LevelBuilder.resetting){
				hasdowned = true;
				LevelLostBoard.SetActive (true);
				if(MenuButton.open){
					MenuButton.CloseMenu();
				}
				LoseMessage.Instance.AssignLoseSprite(Mathf.FloorToInt((LevelManager.levelnum-1)/40)  + 1);
			}
			yield return null;
		}
	}


}
