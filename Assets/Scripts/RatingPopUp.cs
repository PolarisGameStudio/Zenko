using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingPopUp : MonoBehaviour {
	public Text myText;
	public static Text text1;
	public static Text text2;
	public static int myrating;
	public Text mySecondText;
	static bool ready;
	public static GameObject starholder;
	public GameObject pubstarholder;

	//int previous rating;

	// Use this for initialization

	void Start(){
		text1=myText;
		text2=mySecondText;
		starholder = pubstarholder;
	}

	public static void GiveRating () {
		
		RatingBehaviour.CalculateRating ();
		myrating = RatingBehaviour.rating;
		string prefname = "Level" + LevelManager.levelnum + "Rating";
		int previousrating = PlayerPrefs.GetInt (prefname);
		//Debug.Log("previousrating" + previousrating);
		if ((myrating > previousrating || myrating == 3) && !LevelManager.ispotd) {
			//Debug.Log("ADDING TO PLAYERPREFS " + prefname + myrating);
			PlayerPrefs.SetInt (prefname, myrating);
			LevelStorer.leveldic[LevelManager.levelnum].rating = myrating;
		}
		if(LevelManager.levelnum > LevelStorer.highestSolved){
			LevelStorer.highestSolved = LevelManager.levelnum;
		}
		ready = true;
		text1.text = "You got " + RatingBehaviour.rating + " Stars";
		text2.text = "You finished level " + LevelManager.levelnum + "!";
		PlayerPrefs.Save();

		if(myrating == 1){
			starholder.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
		}
		else if(myrating == 2){
			starholder.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
			starholder.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);

		}
		else if(myrating == 3){
			starholder.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
			starholder.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
			starholder.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);
		}

		//PlayerPrefs.SetString("SaveFile",LevelManager.levelnum.ToString());
		if(LevelManager.levelnum+1 > LevelMenu.highestLevelSolved && !LevelManager.ispotd){
			//Debug.Log("new highest solved is " + (LevelManager.levelnum+1));
			LevelMenu.highestLevelSolved = LevelManager.levelnum+1;
		}
		if(!LevelManager.ispotd){
			LevelStorer.UnlockLevel (LevelManager.levelnum+1);

		}

		if(LevelManager.ispotd){
			//Debug.Log( " CURRENT INDEX WON IS " + DateChecker.Instance.currentIndex);
			//Debug.Log("CURRENT")
			LevelStorer.potdDic[DateChecker.Instance.currentIndex].rating = myrating;
			LevelStorer.potdDic[DateChecker.Instance.currentIndex].islocked = false;
		}
		PlayServices.instance.SaveLocal();
		#if UNITY_ANDROID
		PlayServices.instance.SaveData();
		#endif
	}
	public static void AddCurrency(int prevr, int newr){
		Debug.Log("Givingc");
		if(prevr == 0){
			GameManager.mycurrency = GameManager.mycurrency + newr;
			PlayerPrefs.SetInt("Currency", GameManager.mycurrency);
			//GameObject.Find("CurrencyHolder").GetComponentInChildren<Text>().text = GameManager.mycurrency.ToString();
		}
		else if(newr == prevr){
			//GameManager.mycurrency = new;

		}
		else if(prevr < newr){
			GameManager.mycurrency = GameManager.mycurrency + (newr - prevr);
			PlayerPrefs.SetInt("Currency", GameManager.mycurrency);	
			//GameObject.Find("CurrencyHolder").GetComponentInChildren<Text>().text = GameManager.mycurrency.ToString();
			//Debug.Log(GameObject.Find("CurrencyHolder").GetComponentInChildren<Text>().text);
		}
	}
	/*void OnEnable(){
		GiveRating ();
		myText.text = "You got " + RatingBehaviour.rating + " Stars";
		mySecondText.text = "You finished level " + LevelManager.levelnum + "!";
	}*/
	// Update is called once per frame
	/*void Update () {
		if (myrating != RatingBehaviour.rating) {
			myText.text = "You got " + RatingBehaviour.rating + " Stars";
			mySecondText.text = "You finished level " + LevelManager.levelnum + "!";
		}
		if (ready) {
			myText.text = "You got " + RatingBehaviour.rating + " Stars";
			mySecondText.text = "You finished level " + LevelManager.levelnum + "!";
			ready = false;
		}
	}*/
}
