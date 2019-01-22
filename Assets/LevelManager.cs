using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelManager : MonoBehaviour {

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

	public static void NextLevel(int mynum){
		myhints = new List<Vector2>();
		Debug.Log("GONNAGETICE");
		LevelStorer.Lookfor (mynum);
//		TurnCounter.turncount = 0;
		levelselector.DestroyAllExceptCamera ();
		if(!LevelBuilder.iscreated){
			levelselector.CreateBase ();
		}

		levelselector.PlaceBase();
		//Debug.Log("GONNAGETICE");

		levelselector.DrawNextLevel (mynum);
//		Debug.Log("GONNAGETICE");
//		myicehandler.GiveIce();
	}

	public static void NextRandomLevel(){
		Debug.Log(LevelManager.levelnum);
		myhints = new List<Vector2>();
		Debug.Log("GONNAGETICE");
		levelselector.DestroyAllExceptCamera ();
		if(!LevelBuilder.iscreated){
			levelselector.CreateBase ();
		}

		levelselector.PlaceBase();
		levelselector.DrawNextLevel (-7); // -4 means random
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
	public static void UnPop(){
		Dragger[] draggers = (Dragger[]) GameObject.FindObjectsOfType(typeof(Dragger));
		foreach(Dragger dragger in draggers){
			if(dragger.mySeedType != "None"){
				Debug.Log(dragger.mySeedType);
				dragger.myshrinker.SetActive(true);
				dragger.myBigger.SetActive(false);
				Debug.Log((int)dragger.gameObject.transform.position.x);
				Debug.Log(-(int)dragger.gameObject.transform.position.z);
				if(dragger.gameObject.transform.position.x<8){
				LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z].type = "Seed";

				}
				//LevelBuilder.tiles[(int)dragger.gameObject.transform.position.x, -(int)dragger.gameObject.transform.position.z].seedType = mySeedType;

			}
		}
		GameObject[] fragiles = GameObject.FindGameObjectsWithTag("Fragile");
		foreach(GameObject fragile in fragiles){
			//Debug.Log(fragile);
			LevelBuilder.tiles[(int)fragile.transform.position.x, -(int)fragile.transform.position.z].type = "Fragile";
			MeshRenderer mymesh = fragile.GetComponentInChildren<MeshRenderer>();
			FragileProperties myproperties = fragile.GetComponent<FragileProperties>();
			myproperties.myhole.SetActive(false);
			myproperties.myfragile.SetActive(true);
//			Debug.Log(myproperties.mypink);
			//Color mycolor = myproperties.mypink;
			//mymesh.material.color = mycolor;
		}

	}
	void Update(){
	}
}
