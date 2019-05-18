using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingBehaviour : MonoBehaviour {
	static float totturns;
	static float curturns;
	static float turnratio;
	public static int rating;
	private static RatingBehaviour instance = null;
	public static int currentrating;
	static GameObject star1;
	static GameObject star2;
	static GameObject star3;

	// Use this for initialization
	void Awake(){
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
			return;
		}
		Destroy(this.gameObject);
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//CalculateRating ();
	}

	static public void CalculateRating(){
		totturns = LevelStorer.efficientturns;
		curturns = TurnCounter.turncount;
		turnratio = curturns / totturns;
		if (turnratio <= 1) {
			rating = 3;
		} else if (turnratio <= 2) {
			rating = 2;
		} else if (turnratio< 3){
			rating = 1;
		}
		else{
			rating = 0;
		}
		if(rating<currentrating){
			ChangeRating(rating);
		}

		//Debug.Log("assigned" + " " + rating);
		//Debug.Log ("max " + totturns + "    cur " + curturns + "     rating " + rating + " " + turnratio);
	}
	static public void InitializeRating(){
		currentrating = 3;
		star1 = GameObject.Find("Star1");
		star2 = GameObject.Find("Star2");
		star3 = GameObject.Find("Star3");
		star1.GetComponent<Image>().color = Color.green;
		star2.GetComponent<Image>().color = Color.green;
		star3.GetComponent<Image>().color = Color.green;
	}
	static public void RestartRating(){
		currentrating = 3;
		star1.GetComponent<Image>().color = Color.green;
		star2.GetComponent<Image>().color = Color.green;
		star3.GetComponent<Image>().color = Color.green;		
	}
	static public void ChangeRating(int newrating){
		if(newrating == 2){
			star3.GetComponent<Image>().color = Color.red;
		}
		if(newrating == 1){
			star2.GetComponent<Image>().color = Color.red;
		}
		if(newrating == 0){
			star1.GetComponent<Image>().color = Color.red;
		}
		currentrating = newrating;
	}

}
