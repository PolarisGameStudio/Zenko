using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
	public Color color1;
	public Color color2;
	public static GameObject TurnHolder;
	static Color colorone;
	static Color colortwo;
	static Color progressColor;
	public static float totalHeight;
	public static int turns;
	public Color fillerColor;
	static float stepvalue;
	public static GameObject progressFluid;
	public static GameObject progressFluid2;
	ProgressBar instance;
	public static float width;
	// Use this for initialization
	void Awake () {
		instance = this;
		TurnHolder = this.gameObject;
		colorone = color1;
		colortwo = color2;
		progressColor = fillerColor;
		progressFluid = this.transform.GetChild(1).gameObject;
		progressFluid2 = this.transform.GetChild(2).gameObject;
		totalHeight = 700;
		width = 39.5f;
	}
	
	// Update is called once per frame
	void Update () {
		progressColor = fillerColor;
	}

	public static void InitializeProgressBar(int size){
		progressFluid.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 0);
		progressFluid2.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 0);
		progressFluid2.GetComponent<Image>().color = new Color(1,1,1,1);
		stepvalue = totalHeight/size;
	}
	public void TakeTurn(int number){
		if(number <= LevelStorer.efficientturns){
			StartCoroutine(IncreaseBar(.1f, number));
		}
		else if(number<= LevelStorer.efficientturns*2){	
			StartCoroutine(IncreaseBar2(.1f,number));
		}
		else if(number <= LevelStorer.efficientturns*3){

		}	
	}

	public IEnumerator IncreaseBar(float fadetime, int step){
		float startingHeight = (step-1)*stepvalue;
		if(step == LevelStorer.efficientturns){
			for(float t = 0.0f; t<fadetime; t+= Time.deltaTime){
				float normalizedTime = t/fadetime;
	//			Debug.Log("1");
				progressFluid.GetComponent<RectTransform>().sizeDelta =	new Vector2(width, (stepvalue*(step-1))+(1.1f*stepvalue*Mathf.Lerp(0,1, normalizedTime)));
				yield return null;
			}
			progressFluid.GetComponent<RectTransform>().sizeDelta =	new Vector2(width, (stepvalue*step));			

		}
		else{
			for(float t = 0.0f; t<fadetime; t+= Time.deltaTime){
				float normalizedTime = t/fadetime;
	//			Debug.Log("1");
				progressFluid.GetComponent<RectTransform>().sizeDelta =	new Vector2(width, (stepvalue*(step-1))+(1.1f*stepvalue*Mathf.Lerp(0,1, normalizedTime)));
				yield return null;
			}
			float current = (stepvalue*(step-1))+(1.1f*stepvalue);
			for(float t = 0.0f; t<fadetime; t+= Time.deltaTime){
				float normalizedTime = t/fadetime;
	//			Debug.Log("1");
				progressFluid.GetComponent<RectTransform>().sizeDelta =	new Vector2(width, current-(.1f*stepvalue*Mathf.Lerp(0,1, normalizedTime)));
				yield return null;
			}
			progressFluid.GetComponent<RectTransform>().sizeDelta =	new Vector2(width, (stepvalue*step));			
		}


	}
	public IEnumerator IncreaseBar2 (float fadetime, int step){
		int efturns = LevelStorer.efficientturns;
		float startingHeight = (step-1-efturns)*stepvalue;
		if(step == efturns*2){
			for(float t = 0.0f; t<fadetime; t+= Time.deltaTime){
				float normalizedTime = t/fadetime;
				progressFluid2.GetComponent<RectTransform>().sizeDelta =	new Vector2(width, (stepvalue*(step-1-efturns))+(1.1f*stepvalue*Mathf.Lerp(0,1, normalizedTime)));
				
				yield return null;
			}
			progressFluid2.GetComponent<RectTransform>().sizeDelta =	new Vector2(width, (stepvalue*(step-efturns)));		
			if(efturns != 2){
				instance.StartCoroutine(instance.ModulateColor(.2f, new Color(1,76/255f,76/255f,1)));
			}


		}
		else{
			for(float t = 0.0f; t<fadetime; t+= Time.deltaTime){
				float normalizedTime = t/fadetime;
				progressFluid2.GetComponent<RectTransform>().sizeDelta =	new Vector2(width, (stepvalue*(step-1-efturns))+(1.1f*stepvalue*Mathf.Lerp(0,1, normalizedTime)));
				yield return null;
			}
			float current = (stepvalue*(step-1-efturns))+(1.1f*stepvalue);
			for(float t = 0.0f; t<fadetime; t+= Time.deltaTime){
				float normalizedTime = t/fadetime;
				progressFluid2.GetComponent<RectTransform>().sizeDelta =	new Vector2(width, current-(.1f*stepvalue*Mathf.Lerp(0,1, normalizedTime)));				
				yield return null;
			}
			progressFluid2.GetComponent<RectTransform>().sizeDelta =	new Vector2(width, (stepvalue*(step-efturns)));	
		}

	}

	public IEnumerator ModulateColor(float fadetime, Color color){
		for(float t = 0.0f; t<fadetime; t+= Time.deltaTime){
			float normalizedTime = t/fadetime;
			ProgressBar.progressFluid2.GetComponent<Image>().color = new Color(1, color.g*normalizedTime,
				color.b*normalizedTime,1);
			yield return null;
		}		
	}
}
