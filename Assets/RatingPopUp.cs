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
		string prefname = "Level" + LevelManager.levelnum + "Rating";
		int previousrating = PlayerPrefs.GetInt (prefname);
		if (myrating > previousrating) {
			PlayerPrefs.SetInt (prefname, myrating);

		}
		//PlayerPrefs.Save();
		ready = true;
		//Debug.Log ("GIVEN");
		text1.text = "You got " + RatingBehaviour.rating + " Stars";
		text2.text = "You finished level " + LevelManager.levelnum + "!";
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
