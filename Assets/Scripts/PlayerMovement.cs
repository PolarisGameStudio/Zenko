using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
	//public KeySimulator mykeysimulator;
	// Use this for initialization
	void Start () {
		//current tile works as a target to move to
		animationController = GetComponent<PlayerAnimation>();
		startingposition = transform.position;
		currenttile = transform.position;
		cantakeinput = true;
		//canmove = true;
		nextaction = null;
		beingdragged = false;
		lastFragile = null;
		isspeeding = false;
		levelWonBoard = GameObject.Find ("GameWon");
		if (levelWonBoard != null) {
			SceneLoading.gamewon = levelWonBoard;
			levelWonBoard.SetActive (false);

		}
		LevelLostBoard = GameObject.Find ("GameLost");
		if (LevelLostBoard != null) {
			SceneLoading.gamelost = LevelLostBoard;
			LevelLostBoard.SetActive (false);

		}
		//Debug.Log(LevelLostBoard);
		//LevelLostBoard.SetActive (false);
		Debug.Log ("TURNEDTOFF");
		outofmap = false;
		hasmoved = false;
		TG = GameObject.Find("ProgressBar").GetComponent<ProgressBar>();
	}
	
	// Update is called once per frame
	void Update () {
		if(canmove){
			this.transform.GetChild(3).GetComponent<Animator>().SetInteger("Phase", 0);

		}
		if(!canmove && !GoalBehaviour.goaling){
			//Debug.Log("SETTING");
			this.transform.GetChild(3).GetComponent<Animator>().SetInteger("Phase", 1);
		}
		if (Input.GetKeyDown (KeyCode.G)){
			Debug.Log(LevelBuilder.tiles[0,0].isTaken);
		}
		myswipe = Swiping.mydirection;
//		Debug.Log(Swiping.mydirection);
		//Debug.Log (levelWonBoard);
		if (levelWonBoard == null) {
			levelWonBoard = SceneLoading.gamewon;
			levelWonBoard.SetActive(false);
		}
		if (LevelLostBoard == null) {
			LevelLostBoard = SceneLoading.gamelost;
			LevelLostBoard.SetActive (false);
		}
//		Debug.Log (TurnBehaviour.turn + "BEHAV");
		if (transform.position != startingposition && TurnBehaviour.turn == 0) {
			foreach (Dragger piece in PieceHolders.placedpieces){
				piece.gameObject.transform.GetComponent<BoxCollider>().enabled = false;
			}
			TurnBehaviour.turn = 1;
		}
		if (TurnBehaviour.turn == 1 && transform.position == startingposition) {
			TurnBehaviour.turn = 0; 
		}
		// if(Vector3.Distance(currenttile, transform.position) <.5f && !boop && 
		// 	transform.position != startingposition && nextaction != "Goal_Action" && nextaction != "Hole_Action"){
		// 	if(lastFragile != null && Vector3.Distance(lastFragile.transform.position,transform.position) <.5f){

		// 	}
		// 	else{
		// 		Debug.Log(lastbooped + " " + currenttile);
		// 		Debug.Log(boop);
		// 		if(lastbooped != currenttile){
		// 			boop = true;
		// 			lastbooped = currenttile;
		// 			Debug.Log("booped");
		// 		}
		// 		Debug.Log("stopped");				
		// 	}
		// }
		if(Vector3.Distance(currenttile, transform.position) <.6f && !boop && 
			transform.position != startingposition && nextaction !="Goal_Action"){
			Debug.Log(lastbooped + " " + currenttile);
			Debug.Log(boop);
			if(lastbooped != currenttile){
				boop = true;
				lastbooped = currenttile;
				Debug.Log("booped");
				AssignShakeOrientation(character_direction);
			}
			Debug.Log("stopped");				
		}
		// if(Vector3.Distance(currenttile, transform.position) <.6f && !boopout && 
		// 	transform.position != startingposition && nextaction == "Goal_Action"){
		// 	//StartCoroutine(ShakeOut());
		// 	Debug.Log(lastbooped + " " + currenttile);
		// 	Debug.Log(boop);
		// 	if(lastbooped != currenttile){
		// 		boopout = true;
		// 		lastbooped = currenttile;
		// 		Debug.Log("booped");
		// 	}
		// 	Debug.Log("stopped");	
		// }
		// if(Vector3.Distance(currenttile, transform.position) <.5f && !boopout && 
		// 	transform.position != startingposition && 
		// 	(nextaction == "Hole_Action" || (lastFragile != null && Vector3.Distance(lastFragile.transform.position,transform.position) <.5f))){
		// 	if(nextaction == "Left_Action" || nextaction == "Right_Action" || nextaction == "Down_Action" || nextaction == "Up_Action"){

		// 	}
		// 	else{
		// 		Debug.Log(lastbooped + " " + currenttile);
		// 		Debug.Log(boop);
		// 		if(lastbooped != currenttile){
		// 			boopout = true;
		// 			lastbooped = currenttile;
		// 			Debug.Log("booped");
		// 		}
		// 		Debug.Log("stopped");				
		// 	}
		// }
		if (currenttile == transform.position) {//do this when reached currenttile

			GoalBehaviour.isstatic = true;
//			Debug.Log(nextaction);
			//Debug.Log (tilescript.myTaker.tag);
			if (lastFragile != null && lastFragile.transform.position == transform.position && nextaction== null) {
				Debug.Log ("UNUL");
				//this.enabled = false;

				//int nextlevel = LevelManager.levelnum;
				//LevelManager.NextLevel (nextlevel);

				this.enabled=false;

				Debug.Log("Hole");
				StartCoroutine(PopLose());
				StartCoroutine(animationController.Disappear(.3f));

			}

			else if (nextaction == null) {
//				Debug.Log("Null");
				cantakeinput = true;
				canmove = true;
				isspeeding = false;
			}  
			else if (nextaction == "Goal_Action") {
				RatingPopUp.GiveRating ();
 				SceneLoading.SetStars(RatingBehaviour.currentrating);
 				this.enabled = false;
				StartCoroutine(PopWin());
			}			
			else if (nextaction == "Hole_Action") {

				this.enabled=false;
				Debug.Log("Hole");
				StartCoroutine(PopLose());
				StartCoroutine(animationController.Disappear(.3f));


				//int nextlevel = LevelManager.levelnum;
				//LevelManager.NextLevel (nextlevel);
				//here popup "Gameover" try again

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
				}
				if (nextaction == "Right_Action") {
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
				}
				if (nextaction == "Down_Action") {
					nextaction = null;
				}
			}
		}
			Movement ();
	}
	// IEnumerator ShakeOut(){
	// 	yield return new WaitForSeconds(.1f);
	// 	Debug.Log(lastbooped + " " + currenttile);
	// 	Debug.Log(boop);
	// 	if(lastbooped != currenttile){
	// 		boopout = true;
	// 		lastbooped = currenttile;
	// 		Debug.Log("booped");
	// 	}
	// 	Debug.Log("stopped");				
	// }
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
		if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || myswipe == "Up" /*|| mykeysimulator.W*/ ) {
			tiletotest = currenttile;
			Debug.Log(tiletotest);
			if (canmove == true) {
				firstmove = true;
				boop = false;
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
			tiletotest = currenttile;
			if (canmove == true) {
				firstmove = true;
				boop = false;
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
			tiletotest = currenttile;
			if (canmove == true) {
				firstmove = true;
				boop = false;
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
			tiletotest = currenttile;
			if (canmove == true) {
				firstmove = true;
				boop = false;
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
	/*void FindTileTag(){
		Collider2D[] colliders = Physics2D.OverlapCircleAll(tiletotest, .1f); ///Presuming the object you are testing also has a collider 0 otherwise{
		if(colliders.Length == 0){
				istiletaken = true;
				outofmap = true;
		}
		else{
			foreach (Collider2D component in colliders) {
				if (component.tag == "Ground") {
					tileobject = component.gameObject;
					tilescript = tileobject.GetComponent<TileHandler> ();
					istiletaken = tilescript.isTaken;
				} 
			}
		}
	}*/
	void FindTileTag(){
//		Debug.Log("FINDING TAG");
	//Collider2D[] colliders = Physics2D.OverlapCircleAll(tiletotest, .1f); ///Presuming the object you are testing also has a collider 0 otherwise{
		
//		Debug.Log((int)tiletotest.x + "+" + -(int)tiletotest.z);
		//Debug.Log(LevelBuilder.tiles[-2,5]);
		//tilescript = LevelBuilder.tiles[(int)tiletotest.x,-(int)tiletotest.z];
//		Debug.Log(tilescript.type);
//		Debug.Log(tilescript.type);
		if(inside()){
				tilescript = LevelBuilder.tiles[(int)tiletotest.x,-(int)tiletotest.z];
				/*
				foreach (Collider2D component in colliders) {
					if (component.tag == "Ground") {
						tileobject = component.gameObject;
						tilescript = tileobject.GetComponent<TileHandler> ();
						istiletaken = tilescript.isTaken;
					} */
				//tileobject = LevelBuilder.tiles[(int)tiletotest.x, (int)tiletotest.z].tileObj;	
				//istiletaken = LevelBuilder.tiles[(int)tiletotest.x, (int)tiletotest.z].isTaken;
				istiletaken = tilescript.isTaken;			
		}
		else{
				//Debug.Log("square null");
				istiletaken = true;
				outofmap = true;
				//Debug.Log("no collider");
		}
		
	}
	public bool inside(){
//		Debug.Log(tiletotest.x);
//		Debug.Log(tiletotest.z);
		if((int)tiletotest.x>=0 && (int)tiletotest.x <= LevelBuilder.totaldimension && 
			-(int)tiletotest.z>=0 && -(int)tiletotest.z <= LevelBuilder.totaldimension){
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
			//Debug.Log("move");
			GoalBehaviour.readytomove = true;
			transform.position = Vector3.MoveTowards (transform.position, currenttile, Time.deltaTime * speed); 
			cantakeinput = false;
			Swiping.mydirection = "Null";
		}
	}
	void Count(){
		if(firstmove == true && canmove == true){
			if(TurnCounter.turncount == 0){
//				Debug.Log("Anim");
				LevelBuilder.starttransform.GetComponentInChildren<Animator>().SetInteger("Phase",1);
			}
			TurnCounter.turncount++;
//			Debug.Log (TurnCounter.turncount);
			ProgressBar.TakeTurn(TurnCounter.turncount);
		}
		if(firstmove == true){
			firstmove = false;
		}
		RatingBehaviour.CalculateRating ();
	}
	void PopSeed(string type){
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
		Swiping.mydirection = "Null";
		/*if (istiletaken == false) {
			//move and keep moving i	f theres nothing but ice
			Count ();
			currenttile = tiletotest;
		} */

//			Debug.Log(tilescript.type);
			if(outofmap == true){
				canmove = false;
				outofmap = false;
			}

			else if (tilescript.type == "Ice"){
				Count ();
				currenttile = tiletotest;				
			}
			else if (tilescript.type == "Wall" || tilescript.type == "Start") {
				//the desired tile is the previous one and u stop looking for next tiles.
				//Swiping.mydirection = "Null";
				canmove = false;
				Debug.Log("canmove is false now");
				Count ();

			} else if (tilescript.type == "Goal") {
				//you'll stop in the tile you checked and stop moving.
				if (TurnCounter.turncount == 0) {
					Debug.Log("0 turns");
					canmove = false;
				} else {
					if(tilenumber == 0){
					GoalBehaviour.goaling = true;
					this.transform.GetChild(3).GetComponent<Animator>().SetInteger("Phase", 2);
					StartCoroutine(animationController.Disappear(.4f));
					speed = 3;
					}

					Count ();
					currenttile = tiletotest;
					canmove = false;
					//Qeue up an action when reaching the tile
					Debug.Log("aqui?");
					nextaction = "Goal_Action";
				}
			} else if (tilescript.type == "Hole") {
				//you'll stop in the tile you checked and stop moving.
				currenttile = tiletotest;
				canmove = false;

				//Qeue up an action when reaching the tile
				nextaction = "Hole_Action";
			} else if (tilescript.type == "Wood") {
				Count ();
				currenttile = tiletotest;
				//Debug.Log ("Pink");
				//canmove = true;
				if(tilescript.isSideways!= null){
					//Count ();
					//currenttile = tiletotest;
					canmove = false;
					nextaction = tilescript.isSideways+ "_Action";
					Debug.Log(nextaction);
					isspeeding = true;				
				}
			} else if (tilescript.type == "Left") {

					Count ();
					currenttile = tiletotest;
					canmove = false;
					nextaction = "Left_Action";
					isspeeding = true;

			
				
			} else if (tilescript.type == "Right") {

					Count ();
					currenttile = tiletotest;
					canmove = false;
					nextaction = "Right_Action";
					isspeeding = true;

			} else if (tilescript.type == "Up") {

					Count ();
					currenttile = tiletotest;
					canmove = false;
					nextaction = "Up_Action";
					isspeeding = true;
	

			} else if (tilescript.type == "Down") {

					Count ();
					currenttile = tiletotest;
					canmove = false;
					nextaction = "Down_Action";
					isspeeding = true;


			} else if (tilescript.type == "Fragile") {
				Count ();
				currenttile = tiletotest;
				lastFragile = tilescript.tileObj;
				Vector3 overlapV3 = new Vector3(currenttile.x, currenttile.y, currenttile.z);
				//Debug.Log(overlapV3);
				Collider[] colliders = Physics.OverlapSphere(overlapV3, .5f);
				foreach (Collider component in colliders) {
					if (component.tag == "Fragile") {
//						Debug.Log("Fragile");
//						Debug.Log(component);
						//tileobject = component.gameObject;
						//MeshRenderer tilerenderer = tileobject.GetComponentInChildren<MeshRenderer> ();
						//tilerenderer.material.color = fragilered;
						component.GetComponent<FragileBehaviour>().readytolava = true;
						component.GetComponent<FragileBehaviour>().player = this.gameObject;
//						Debug.Log(component.GetComponent<FragileBehaviour>().player);	
						//component.GetComponent<FragileProperties>().myred = fragilered;

					} 
				}
				if(tilescript.isSideways!= null){
					//Count ();
					//currenttile = tiletotest;
					canmove = false;
					nextaction = tilescript.isSideways+ "_Action";
					Debug.Log(nextaction);
					isspeeding = true;				
				}
				else{

				}
				tilescript.type = "Hole";
		
			} else if (tilescript.type == "Quicksand") {
				currenttile = tiletotest;
				lastFragile = tilescript.tileObj;
				Count ();
				if (isspeeding == false) {
					currenttile = tiletotest;
					canmove = false;
					nextaction = "Hole_action";
				}

			}
			else if (tilescript.type == "Seed") {
				currenttile = tiletotest;
				lastSeed = tilescript.tileObj;
				Debug.Log(tilescript.tileObj);
				//Debug.Log("Check");
				//tilescript.type = tilescript.seedType;
				PopSeed(tilescript.seedType);
				
				if(tilescript.isSideways!= null){
					//Count ();
					//currenttile = tiletotest;
					canmove = false;
					nextaction = tilescript.isSideways+ "_Action";
					Debug.Log(nextaction);
					isspeeding = true;				
				}
				//tilescript.seedType = "SeededTile";
				//Vector3 scale = lastSeed.GetComponent<Dragger>().myshrinker.transform.localScale;
				//scale.Set(.33f,.33f,.33f);

				//lastSeed.GetComponent<Dragger>().myshrinker.transform.localScale = scale;
				
				//Debug.Log(lastSeed.GetComponent<Dragger>().myshrinker.transform.localScale);//lastSeed.GetComponent<Transform>().localscale.y = 1;
				//myseedbehaviour = lastSeed.GetComponent<Seed_Behaviour> ();
				//myseedbehaviour.Unseed ();


			} else if (tilescript.type == "Boss") {
				currenttile = tiletotest;
				canmove = false;
				

				/*if (character_direction == "Up") {
					Boss_Behaviour.bosstile.y++;
				}
				if (character_direction == "Right") {
					Boss_Behaviour.bosstile.x++;
				}
				if (character_direction == "Left") {
					Boss_Behaviour.bosstile.x--;
				}
				if (character_direction == "Down") {
					Boss_Behaviour.bosstile.y--;
				}*/


			}
				else {
				Debug.Log(tilescript.type);
				Debug.Log ("Dong");
				canmove = false;
			}
			Count ();
	}
	IEnumerator PopWin(){
		yield return new WaitForSeconds(1);
		levelWonBoard.SetActive (true);


	}
	IEnumerator PopLose(){
		float fadetime = 1.5f;
		float initpos = 0;
		float finalpos = -4;
		bool hasdowned = false;
		for(float t = 0.0f; t<fadetime; t+= Time.deltaTime){
			float normalizedTime = t/fadetime;
			//Debug.Log(Mathf.Lerp(lowValue,highValue,normalizedTime));//kick
			transform.position = new Vector3(transform.position.x, Mathf.Lerp(initpos,finalpos,normalizedTime), transform.position.z);
			if(transform.position.y < -1.5f && !hasdowned && !LevelBuilder.resetting){
				hasdowned = true;
				LevelLostBoard.SetActive (true);
			}
			yield return null;
		}
		//Debug.Log(highValue);
		//dissolveMat.SetFloat("Vector1_B5CA3B27", highValue);
		//yield return new WaitForSeconds(.5f);
		//LevelLostBoard.SetActive (true);
	}

}
