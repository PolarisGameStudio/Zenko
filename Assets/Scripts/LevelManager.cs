using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelManager : MonoBehaviour {

	public static bool newicarus;
	public static int levelnum;
	public static LevelBuilder levelselector;
	//static int levelwidth;
	public static bool readytodraw;
	public static int worldnum;
	public static int myefficientturns;
	public static List<Vector2> myhints = new List<Vector2>();
	public static List<Transform> piecetiles = new List<Transform>();
	public static bool israndom;
	public static int hintnum;
	public static List<int> hintsgiven = new List<int>();
	public static bool isdragging;
	public static bool ispotd;
	//public static IceTileHandler myicehandler;


	//private static LevelManager instance = null;



	/*void Awake(){
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
			return;
		}
		Destroy(this.gameObject);
	}*/
	public static void PopulateLists(){

	}

	public static void NextLevel(int mynum){
		myhints = new List<Vector2>();
		Debug.Log("GONNAGETICE");
		LevelStorer.Lookfor (mynum);
//		TurnCounter.turncount = 0;
		levelselector.DestroyAllExceptCamera ();
		if(!LevelBuilder.iscreated){
			levelselector.CreateBase ();
		}
		levelselector.drawNormal(mynum);
		/*levelselector.CreateOuterBase();
		levelselector.PlaceBase();
		//Debug.Log("GONNAGETICE");

		levelselector.DrawNextLevel (mynum);*/
//		Debug.Log("GONNAGETICE");
//		myicehandler.GiveIce();
	}

	public static void NextRandomLevel(){
		Debug.Log(LevelManager.levelnum);
		myhints = new List<Vector2>();
		hintsgiven = new List<int>();
		Debug.Log("GONNAGETICE");
		levelselector.DestroyAllExceptCamera ();
		if(!LevelBuilder.iscreated){
			levelselector.CreateBase ();
		}
		levelselector.CreateOuterBase();
		levelselector.PlaceBase();
		levelselector.DrawNextLevel (-19); // -4 means random
	}


	public static void NextRandomLevel2(){
		Debug.Log(LevelManager.levelnum);
		myhints = new List<Vector2>();
		hintsgiven = new List<int>();
		Debug.Log("GONNAGETICE");
		levelselector.DestroyAllExceptCamera ();
		if(!LevelBuilder.iscreated){
			levelselector.CreateBase ();
		}

		levelselector.PlaceBase();
		levelselector.CreateOuterBase();

		levelselector.DrawNextLevel (-105); // -4 means random
	}

	public static void NextPotd(){
		myhints = new List<Vector2>();
		hintsgiven = new List<int>();
		Debug.Log("GONNAGETICE");
		levelselector.DestroyAllExceptCamera ();
		levelselector.drawPotd(Random.Range(0,LevelBuilder.startersPotd.Count));
	}

	public static void ResetLevel(){
		levelselector.ResetPlayer();
	}

		public void niuNextLevel(int mynum){
		Debug.Log(mynum);
		LevelStorer.Lookfor (mynum);
//		TurnCounter.turncount = 0;
		levelselector.DestroyAllExceptCamera ();
		levelselector.CreateBase ();
		//Debug.Log("GONNAGETICE");

		levelselector.DrawNextLevel (mynum);
//		Debug.Log("GONNAGETICE");
//		myicehandler.GiveIce();
	}
	public static void UnPopIcarus(string type, Dragger dragger){
		if(type == "Left"){
			LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z].type = "Seed";	
			if(LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x-1, -(int)dragger.gameObject.transform.position.z].type == type){
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x-1, -(int)dragger.gameObject.transform.position.z].type = "Ice";
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x-1, -(int)dragger.gameObject.transform.position.z].isTaken = false;

			}
			else{
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x-1, -(int)dragger.gameObject.transform.position.z].isSideways = null;
				//LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x-1, -(int)dragger.gameObject.transform.position.z].isTaken = false;
			}
					
		}
		if(type == "Right"){
			LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z].type = "Seed";	
			if(LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x+1, -(int)dragger.gameObject.transform.position.z].type == type){
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x+1, -(int)dragger.gameObject.transform.position.z].type = "Ice";
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x+1, -(int)dragger.gameObject.transform.position.z].isTaken = false;

			}
			else{
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x+1, -(int)dragger.gameObject.transform.position.z].isSideways = null;
				//LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x+1, -(int)dragger.gameObject.transform.position.z].isTaken = false;
			}
		}
		if(type == "Up"){
			LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z].type = "Seed";	
			if(LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z-1].type == type){
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z-1].type = "Ice";
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z-1].isTaken = false;

			}
			else{
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z-1].isSideways = null;
				//LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z+1].isTaken = false;
			}		
		}
		if(type == "Down"){
			LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z].type = "Seed";	
			if(LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z+1].type == type){
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z+1].type = "Ice";
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z+1].isTaken = false;

			}
			else{
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z+1].isSideways = null;
				//LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z].isTaken = false;
			}		
		}
	}
	public static void UnPop(){
		Dragger[] draggers = (Dragger[]) GameObject.FindObjectsOfType(typeof(Dragger));
		foreach(Dragger dragger in draggers){
			if(dragger.mySeedType != "None"){
				Debug.Log(dragger.mySeedType);
				dragger.myshrinker.SetActive(true);
				dragger.myBigger.SetActive(false);
				Debug.Log((int)dragger.gameObject.transform.position.x);
				Debug.Log(-(int)dragger.gameObject.transform.position.z);
				if(dragger.gameObject.transform.position.z > -LevelBuilder.totaldimension){//checks for pieces inside board

					if(dragger.mySeedType == "Wall"){
						LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z].type = "Seed";
						Debug.Log(LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z].type);						
					}
					else if(dragger.mySeedType == "Left" || dragger.mySeedType == "Right" || dragger.mySeedType == "Up" || dragger.mySeedType == "Down"){
						UnPopIcarus(dragger.mySeedType, dragger);
					}
				}
				//LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z].seedType = mySeedType;

			}
		}
		GameObject[] fragiles = GameObject.FindGameObjectsWithTag("Fragile");
		//Debug.Log(fragiles.Length);

		foreach(GameObject fragile in fragiles){
			//Debug.Log(fragile);
			//LevelBuilder.tiles[(int)fragile.transform.position.x, -(int)fragile.transform.position.z].type = "Fragile";
			//MeshRenderer mymesh = fragile.GetComponentInChildren<MeshRenderer>();
			//FragileBehaviour myproperties = fragile.GetComponent<FragileBehaviour>();
			//myproperties.myhole.SetActive(false);
			//myproperties.myfragile.SetActive(true);
			//myproperties.lavaWhenReady = false;
			fragile.GetComponent<FragileBehaviour>().readytolava = false;
			fragile.GetComponent<Animator>().SetInteger("Phase",0);
			LevelBuilder.tiles[(int)fragile.transform.position.x, -(int)fragile.transform.position.z].type = "Fragile";
//			Debug.Log(myproperties.mypink);
			//Color mycolor = myproperties.mypink;
			//mymesh.material.color = mycolor;
		}

	}
	void Update(){
	}
}