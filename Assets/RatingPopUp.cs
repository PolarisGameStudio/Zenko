using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingPopUp : MonoBehaviour {
	public Text myText;
	public static Text text1;
	public static Text text2;
	static int myrating;
	public Text mySecondText;
	static bool ready;
	//int previous rating;

	// Use this for initialization

	void Start(){
		text1=myText;
		text2=mySecondText;
	}

	public static void GiveRating () {
		
		RatingBehaviour.CalculateRating ();
		myrating = RatingBehaviour.rating;
		Debug.Log("My rating is" + myrating);
		Debug.Log("level "+ LevelManager.levelnum);
		string prefname = "Level" + LevelManager.levelnum + "Rating";
		int previousrating = PlayerPrefs.GetInt (prefname);
		Debug.Log("previousrating" + previousrating);
		//Debug.Log(prefname);
		//Debug.Log(previousrating);
		//Debug.Log(PlayerPrefs.GetInt("Player1Rating"));
		if (myrating > previousrating) {
			PlayerPrefs.SetInt (prefname, myrating);
			//Debug.Log("gave "+ prefname + "rating" + myrating);
			//Debug.Log(PlayerPrefs.GetInt(prefname));
			LevelStorer.leveldic[LevelManager.levelnum].rating = myrating;
		}
		//PlayerPrefs.Save();
		ready = true;
		//Debug.Log ("GIVEN");
		text1.text = "You got " + RatingBehaviour.rating + " Stars";
		text2.text = "You finished level " + LevelManager.levelnum + "!";
		PlayerPrefs.Save();
	}

	/*void OnEnable(){
		GiveRating ();
		myText.text = "You got " + RatingBehaviour.rating + " Stars";
		mySecondText.text = "You finished level " + LevelManager.levelnum + "!";
	}*/
	// Update is called once per frame
	void Update () {
		if (myrating != RatingBehaviour.rating) {
			myText.text = "You got " + RatingBehaviour.rating + " Stars";
			mySecondText.text = "You finished level " + LevelManager.levelnum + "!";
		}
		if (ready) {
			myText.text = "You got " + RatingBehaviour.rating + " Stars";
			mySecondText.text = "You finished level " + LevelManager.levelnum + "!";
			ready = false;
		}
	}
}
