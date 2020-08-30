using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Hint{
	public string type;
	public int x;
	public int y;
	public Hint(string newtype, int newx, int newy){
		type = newtype;
		x = newx;
		y = newy;
	}
}

public class LevelManager : MonoBehaviour {
	public static bool newicarus;
	public static int levelnum;
	public static LevelBuilder levelselector;
	//static int levelwidth;
	public static bool readytodraw;
	public static int worldnum;
	public static int myefficientturns;
	public static string[,] placedPieces = new string[10,10];
	public static List<Vector2> myhints = new List<Vector2>();
	public static List<string> mypieces = new List<string>();
	public static List<Transform> piecetiles = new List<Transform>();
	public static bool israndom;
	public static int hintnum;
	public static List<int> hintsgiven = new List<int>();
	public static bool isdragging;
	public static bool ispotd;
	public static List<Hint> hints = new List<Hint>();
	public static bool adFree;
	public static bool configging;
	public static Transform playert;
	public bool freeIt;
	public static LevelManager Instance = null;

	void Awake(){
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
			return;
		}
		Destroy(this.gameObject);
	}

	public static void NextLevel(int mynum){
		myhints = new List<Vector2>();
		LevelStorer.Lookfor (mynum);
		levelselector.DestroyAllExceptCamera ();
		TileKeeper.Instance.Shuffle();
		TileKeeper.Instance.Reset();
		EnvironmentKeeper.Instance.Shuffle();
		EnvironmentKeeper.Instance.Reset();
		if(!LevelBuilder.iscreated){
			levelselector.CreateBase ();
		}
		levelselector.drawNormal(mynum);
	}

	public static void NextPotd(){
		myhints = new List<Vector2>();
		hintsgiven = new List<int>();
		levelselector.DestroyAllExceptCamera ();
		TileKeeper.Instance.Shuffle();
		TileKeeper.Instance.Reset();
		EnvironmentKeeper.Instance.Shuffle();
		EnvironmentKeeper.Instance.Reset();
		PlayerPrefs.SetInt("PoTD", PlayerPrefs.GetInt("PoTD") + 1);
		DateChecker.Instance.currentIndex = PlayerPrefs.GetInt("PoTD");
		LevelStorer.potdDic[PlayerPrefs.GetInt("PoTD")].isNew = false;
		levelselector.drawPotd(PlayerPrefs.GetInt("PoTD"));
	}

	public static void SpecificPotd(int num){
		myhints = new List<Vector2>();
		hintsgiven = new List<int>();
		levelselector.DestroyAllExceptCamera ();
		TileKeeper.Instance.Shuffle();
		TileKeeper.Instance.Reset();
		EnvironmentKeeper.Instance.Shuffle();
		EnvironmentKeeper.Instance.Reset();
		PlayerPrefs.SetInt("PoTD", num);
		DateChecker.Instance.currentIndex = PlayerPrefs.GetInt("PoTD");
		levelselector.drawPotd(PlayerPrefs.GetInt("PoTD"));
	}

	//This is used when testing and need to refresh potd.
	public static void RePotd(){
		myhints = new List<Vector2>();
		hintsgiven = new List<int>();
		levelselector.DestroyAllExceptCamera ();
		TileKeeper.Instance.Shuffle();
		TileKeeper.Instance.Reset();
		EnvironmentKeeper.Instance.Shuffle();
		EnvironmentKeeper.Instance.Reset();
		DateChecker.Instance.currentIndex = PlayerPrefs.GetInt("PoTD");
		MapsHolder.RePotd();
	}

	public static void ResetLevel(){
		levelselector.ResetPlayer();
	}

	public static void UnPopIcarus(string type, Dragger dragger){
		if(type == "Left"){
			LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z].type = "Seed";	
			if(LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x-1, -(int)dragger.gameObject.transform.position.z].type == type){
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x-1, -(int)dragger.gameObject.transform.position.z].type = "Ice";
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x-1, -(int)dragger.gameObject.transform.position.z].isTaken = false;
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x-1, -(int)dragger.gameObject.transform.position.z].isSideways = null;
			}
			else{
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x-1, -(int)dragger.gameObject.transform.position.z].isSideways = null;
			}	
		}
		if(type == "Right"){
			LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z].type = "Seed";	
			if(LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x+1, -(int)dragger.gameObject.transform.position.z].type == type){
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x+1, -(int)dragger.gameObject.transform.position.z].type = "Ice";
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x+1, -(int)dragger.gameObject.transform.position.z].isTaken = false;
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x+1, -(int)dragger.gameObject.transform.position.z].isSideways = null;
			}
			else{
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x+1, -(int)dragger.gameObject.transform.position.z].isSideways = null;
			}
		}
		if(type == "Up"){
			LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z].type = "Seed";	
			if(LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z-1].type == type){
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z-1].type = "Ice";
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z-1].isTaken = false;
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z-1].isSideways = null;
			}
			else{
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z-1].isSideways = null;
			}		
		}
		if(type == "Down"){
			LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z].type = "Seed";	
			if(LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z+1].type == type){
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z+1].type = "Ice";
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z+1].isTaken = false;
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z+1].isSideways = null;
			}
			else{
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z+1].isSideways = null;
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
				dragger.convertWhenReady = false;
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
			}
		}
		GameObject[] fragiles = GameObject.FindGameObjectsWithTag("Fragile");
		foreach(GameObject fragile in fragiles){
			fragile.GetComponent<FragileBehaviour>().readytolava = false;
			fragile.GetComponent<Animator>().SetInteger("Phase",0);
			LevelBuilder.tiles[(int)fragile.transform.position.x, -(int)fragile.transform.position.z].type = "Fragile";
		}
	}
}
