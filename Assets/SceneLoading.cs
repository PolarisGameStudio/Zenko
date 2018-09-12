using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoading : MonoBehaviour {
	public int num;
	public static GameObject gamewon;
	public static GameObject gamelost;
	public int testnum;
//	public IceTileHandler myhandler;

	public void LoadScene(int num){
		Swiping.mydirection = "Null";
		TurnCounter.turncount = 0;
		Debug.Log ("Going to Scene at level " + num);
		LevelManager.levelnum = num;
		LevelManager.readytodraw = true;
		SceneManager.LoadScene(1);
		//NextlevelButton();
	}
	public void LoadMenu(){
		SceneManager.LoadScene(0);
	}
	public void NextlevelButton(){
		Debug.Log("Next button");
		Swiping.mydirection = "Null";
		LevelManager.levelnum++;
		//LevelManager.levelnum = Random.Range(0,66);

		LevelStorer.UnlockLevel (LevelManager.levelnum);
		LevelStorer.Lookfor (LevelManager.levelnum);
		TurnCounter.turncount = 0;
		//LevelManager.NextLevel (LevelManager.levelnum);
		//LevelManager.levelnum = Random.Range(0,66);
		LevelManager.NextLevel(LevelManager.levelnum);

		//myhandler.GiveIce();

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
		Swiping.mydirection = "Null";
		LevelStorer.Lookfor (LevelManager.levelnum);
		TurnCounter.turncount = 0;
		LevelManager.NextLevel (LevelManager.levelnum);
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
	public void TestButton(){
		Debug.Log("Buttoned");
		LoadScene(1);
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
