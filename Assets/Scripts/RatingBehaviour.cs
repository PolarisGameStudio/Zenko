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
	public Sprite Lstar;
	public Sprite Lnotstar;
	public Sprite Mstar;
	public Sprite Mnotstar;
	public Sprite Rstar;
	public Sprite Rnotstar;
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
	}

	static public void CalculateRating(){
		totturns = LevelStorer.efficientturns;
		curturns = TurnCounter.turncount;
		turnratio = curturns / totturns;
		if (turnratio <= 1) {
			rating = 3;
		} else if (turnratio <= 1.5) {
			rating = 2;
		} else if (turnratio<2){
			if(totturns>2){
				rating = 1;
			}
		}
		else if(turnratio == 2 && totturns<=2){
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
		star1.GetComponent<Image>().sprite = instance.Lstar;
		star2.GetComponent<Image>().sprite = instance.Mstar;
		star3.GetComponent<Image>().sprite = instance.Rstar;
	}
	static public void RestartRating(){
		currentrating = 3;
		star1.GetComponent<Image>().sprite = instance.Lstar;
		star2.GetComponent<Image>().sprite = instance.Mstar;
		star3.GetComponent<Image>().sprite = instance.Rstar;		
	}
	static public void ChangeRating(int newrating){
		if(newrating == 2){
			star3.GetComponent<Image>().sprite = instance.Rnotstar;
		}
		if(newrating == 1){
			star3.GetComponent<Image>().sprite = instance.Rnotstar;
			star2.GetComponent<Image>().sprite = instance.Mnotstar;
		}
		if(newrating == 0){
			star1.GetComponent<Image>().sprite = instance.Lnotstar;
			star2.GetComponent<Image>().sprite = instance.Mnotstar;
			star3.GetComponent<Image>().sprite = instance.Rnotstar;
			//ProgressBar.progressFluid2.GetComponent<Image>().color = new Color(1,76/255f,76/255f,1);
			if(totturns == 2){
				instance.StartCoroutine(instance.ModulateColor(.2f, new Color(1,76/255f,76/255f,1)));
			
			}
			//Debug.Log(ProgressBar.progressFluid2.GetComponent<Image>().color);

		}
		currentrating = newrating;
	}
	public IEnumerator ModulateColor(float fadetime, Color color){
			for(float t = 0.0f; t<fadetime; t+= Time.deltaTime){
				float normalizedTime = t/fadetime;
	//			Debug.Log("1");
				ProgressBar.progressFluid2.GetComponent<Image>().color = new Color(1, color.g*normalizedTime,
					color.b*normalizedTime,1);
				yield return null;
			}		
	}
}
