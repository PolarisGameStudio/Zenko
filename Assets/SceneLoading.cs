using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneLoading : MonoBehaviour {
	public int num;
	public static GameObject gamewon;
	public static GameObject gamelost;
	public int testnum;
	public Text txt;
	public Text txt2;
//	public IceTileHandler myhandler;
	void Start(){
		if (txt2 == null){
		}
		else{
//			Debug.Log(levelnum + "level");
			if(LevelManager.levelnum == 0 || LevelManager.levelnum ==null)
			LevelManager.levelnum = -4;
			//LevelStorer.Lookfor(LevelManager.levelnum);
			txt2.text = ("Efficient turns is " + LevelStorer.efficientturns);
			txt.text = LevelManager.levelnum.ToString();
//			Debug.Log(LevelManager.levelnum);
		}
	}
	public void LoadScene(int num){
		Swiping.mydirection = "Null";
		TurnCounter.turncount = 0;
		Debug.Log ("Going to Scene at level " + num);
		LevelStorer.Lookfor(num);
		LevelManager.levelnum = num;
		LevelManager.readytodraw = true;
		SceneManager.LoadScene(1);
		//NextlevelButton();
	}
	public void LoadMenu(){
		SceneManager.LoadScene(0);
	}
	public void NextlevelButton(){
		LevelManager.israndom = false;
		Debug.Log("Next button");
		Swiping.mydirection = "Null";
		LevelManager.levelnum++;
		txt.text = LevelManager.levelnum.ToString();
		//txt2.text = ("Efficient turns is " + LevelStorer.efficientturns);
		//LevelManager.levelnum = Random.Range(0,66);

		LevelStorer.UnlockLevel (LevelManager.levelnum);
		LevelStorer.Lookfor (LevelManager.levelnum);
		txt2.text = ("Efficient turns is " + LevelStorer.efficientturns);

		TurnCounter.turncount = 0;
		//LevelManager.NextLevel (LevelManager.levelnum);
		//LevelManager.levelnum = Random.Range(0,66);
		LevelManager.NextLevel(LevelManager.levelnum);
		TurnGraphics.SetTurnCounter(LevelStorer.efficientturns);
		

		//TurnGraphics.SetTurnCounter(LevelStorer.efficientturns);


		//myhandler.GiveIce();
	}
	public void RandomLevel(){
		Swiping.mydirection = "Null";
		LevelManager.israndom = true;
		LevelManager.levelnum = Random.Range(0,105);
		Debug.Log(LevelManager.levelnum);
		txt.text = LevelManager.levelnum.ToString();
		LevelManager.NextRandomLevel();
		//TurnGraphics.SetRandomTurnCounter();
		
		
	}

	public void RandomLevel2(){
		Swiping.mydirection = "Null";
		LevelManager.israndom = true;
		LevelManager.levelnum = Random.Range(0,102);
		Debug.Log(LevelManager.levelnum);
		txt.text = LevelManager.levelnum.ToString();
		LevelManager.NextRandomLevel2();
		//TurnGraphics.SetRandomTurnCounter();
		
		
	}
	public void PlaceHint(){



		int mynum = LevelManager.hintnum; 
		//Code to remove type,taken,obj
		Dragger td = LevelManager.piecetiles[mynum].gameObject.GetComponent<Dragger>();

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

	public void PlaceHint2(){
		//Debug.Log
		for(int i = 0; i<LevelManager.piecetiles.Count; i++){ //First go through the pieces outside of the board.
			Dragger td = LevelManager.piecetiles[i].gameObject.GetComponent<Dragger>();
			if(td.gameObject.transform.position.x>7){//if piece is outside:
				Debug.Log("Piece number "+ i + "is outside and good to go"); 
				//need to check if taken by someone of similar type.

				Vector3 Place = new Vector3 (LevelManager.myhints[i].x, 0, -LevelManager.myhints[i].y);
				if (LevelBuilder.tiles[(int)Place.x, -(int)Place.z].isTaken == false){

					LevelManager.piecetiles[i].position = Place;

					LevelBuilder.tiles[(int)Place.x, -(int)Place.z].type = td.myType;
					LevelBuilder.tiles[(int)Place.x, -(int)Place.z].isTaken = true;
					LevelBuilder.tiles[(int)Place.x, -(int)Place.z].tileObj = td.gameObject;
					td.gameObject.GetComponent<BoxCollider>().enabled = false;					//disable collider so piece stays there.
					
					Debug.Log(Place.z);

					if(td.myType == "Seed"){
						LevelBuilder.tiles[(int)Place.x, -(int)Place.z].seedType = td.mySeedType;	
					} 

					LevelManager.hintnum++;
					LevelManager.hintsgiven.Add(i);
					//td.gameObject.GetComponent<Animator>().SetInteger("Phase", 2);
					return;
					//place it
				}
				else{
					Debug.Log("Someone in my place");
					//Check if one on my place is good or bad.
					if(td.myType == LevelBuilder.tiles[(int)Place.x, -(int)Place.z].type){//if it's good alternative. 
						for(int j = 0; j<LevelManager.piecetiles.Count; j++){//search for a new spot
							if(j!=i){//skip self
								Dragger td2 = LevelManager.piecetiles[j].gameObject.GetComponent<Dragger>();
								if(td2.myType == td.myType){		//check if the selected piece is of same type
									Debug.Log("Same person in my place");
									Vector3 Place2 = new Vector3 (LevelManager.myhints[j].x, 0, -LevelManager.myhints[j].y);	
									Debug.Log(Place2);		
									//Debug.Log(LevelBuilder.tiles[(int)Place2.x, -(int)Place2.z].isTaken)	;				
									if(LevelBuilder.tiles[(int)Place2.x, -(int)Place2.z].isTaken == false){ 
										Debug.Log("new target free");
										LevelManager.piecetiles[i].position = Place2;

										LevelBuilder.tiles[(int)Place2.x, -(int)Place2.z].type = td.myType;
										LevelBuilder.tiles[(int)Place2.x, -(int)Place2.z].isTaken = true;
										LevelBuilder.tiles[(int)Place2.x, -(int)Place2.z].tileObj = td.gameObject;
										td.gameObject.GetComponent<BoxCollider>().enabled = false;					//disable collider so piece stays there.
										
										Debug.Log(Place2.z + "Testcloseearly");

										if(td.myType == "Seed"){
											LevelBuilder.tiles[(int)Place2.x, -(int)Place2.z].seedType = td.mySeedType;	
										} 
										//td.gameObject.GetComponent<Animator>().SetInteger("Phase", 2);
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
			Dragger td = LevelManager.piecetiles[i].gameObject.GetComponent<Dragger>();
			string piecetype = td.myType;
			string seedtype = td.mySeedType;
			Vector3 targetv2 = new Vector3 (LevelManager.myhints[i].x, 0, -LevelManager.myhints[i].y);
			Vector2 myv2 = new Vector2(td.gameObject.transform.position.x, -td.gameObject.transform.position.z);
			Debug.Log(LevelManager.myhints[i] +"" +  myv2);
			//if()//check if same position as same hint position

			//check if it's on the right one or one of same type.

			return;

			for(int j=0; j< LevelManager.piecetiles.Count; j++){

			}
		}

	}


	public void PreviousLevelButton(){
		Swiping.mydirection = "Null";
		LevelManager.levelnum--;
		LevelStorer.Lookfor (LevelManager.levelnum);
		TurnCounter.turncount = 0;
		LevelManager.NextLevel (LevelManager.levelnum);
		//LevelManager.NextLevel(Random.Range(0,100));
		//myhandler.GiveIce();

	}
	public void ResetLevelButton(){
		txt.text = LevelManager.levelnum.ToString();

		Swiping.mydirection = "Null";
		//LevelStorer.UnlockLevel (LevelManager.levelnum);
		//LevelStorer.Lookfor (LevelManager.levelnum);
		TurnCounter.turncount = 0;
		LevelManager.ResetLevel();
		LevelManager.UnPop();
		//TurnGraphics.ClearCounters();
		TurnGraphics.SetTurnCounter(LevelStorer.efficientturns);
		//LevelManager.NextLevel (LevelManager.levelnum);
		//myhandler.GiveIce();
	}
	public void ResetAllButton(){
		txt.text = LevelManager.levelnum.ToString();

		Swiping.mydirection = "Null";
		//LevelStorer.UnlockLevel (LevelManager.levelnum);
		//LevelStorer.Lookfor (LevelManager.levelnum);
		TurnCounter.turncount = 0;
		//LevelManager.ResetLevel();
		LevelManager.NextLevel (LevelManager.levelnum);
		TurnGraphics.SetTurnCounter(LevelStorer.efficientturns);
		//myhandler.GiveIce();
	}
	public void Testnum(int num){
		//initializevalues
//		AIBrain.pieces.Clear();
		Swiping.mydirection = "Null";
		LevelManager.levelnum = num;		
		LevelStorer.Lookfor (num);
		TurnCounter.turncount = 0;
		LevelManager.NextLevel (num);
		//myhandler.GiveIce();

	}
	public void GoToWorld(int worldnumber){


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
	public void LoadLevel(int num){
		//LevelManager.levelnum = 1;
		//Debug.Log(LevelStorer.leveldic.Count);
		if(LevelStorer.leveldic[num].islocked == false || num==1 ){
			Debug.Log("Going to Level "+ num);
			LevelManager.levelnum = num;
			LoadScene(LevelManager.levelnum);
		}

	}
	public void Plus(){
		LevelManager.levelnum ++;
		txt.text = LevelManager.levelnum.ToString();


	}
	public void Minus(){
		LevelManager.levelnum--;
		txt.text = LevelManager.levelnum.ToString();

	}

	public void GoToLevelSelect(){
		if (LevelManager.levelnum < 34) {
			SceneManager.LoadScene (1);
		} else if (LevelManager.levelnum < 67) {
			SceneManager.LoadScene (2);
		} else if (LevelManager.levelnum < 101) {
			SceneManager.LoadScene (3);
		}
	}

	public void GoToWorldSelect(){
			SceneManager.LoadScene (1);
	}
	void Update(){
//		Debug.Log (gamewon);
	}
}
