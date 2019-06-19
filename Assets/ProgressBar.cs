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
	ProgressBar instance;
	// Use this for initialization
	void Awake () {
		instance = this;
		//InitializeProgressBar(10);
		TurnHolder = this.gameObject;
		colorone = color1;
		colortwo = color2;
		progressColor = fillerColor;
		progressFluid = this.transform.GetChild(1).gameObject;
		totalHeight = 700;
	}
	
	// Update is called once per frame
	void Update () {
		progressColor = fillerColor;
	}
	// public static void InitializeProgressBar(int size){
	// 	for(int i = 0; i<16;i++){
	// 		GameObject currentChild;
	// 		currentChild = TurnHolder.transform.GetChild(i).gameObject;
	// 		currentChild.SetActive(false);			
	// 	}
	// 	TurnHolder.GetComponent<GridLayoutGroup>().cellSize   =  new Vector2(100f, (float)(700/size)) ;
	// 	for (int i = 0; i < size; i++){
	// 		GameObject currentChild;
	// 		currentChild = TurnHolder.transform.GetChild(i).gameObject;
	// 		currentChild.SetActive(true);
	// 		if(i%2 == 0){
	// 			currentChild.GetComponent<Image>().color = colorone;
	// 		}
	// 		else{
	// 			currentChild.GetComponent<Image>().color = colortwo;
	// 		}
	// 	}
	// }
	public static void InitializeProgressBar(int size){
		progressFluid.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 0);
		stepvalue = totalHeight/size;
	}
	public void TakeTurn(int number){
		if(number <= LevelStorer.efficientturns){
			//progressFluid.GetComponent<RectTransform>().sizeDelta = new Vector2(50, stepvalue*number);
			StartCoroutine(IncreaseBar(.2f, number));
		}
		else if(number<= LevelStorer.efficientturns*2){	

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
				progressFluid.GetComponent<RectTransform>().sizeDelta =	new Vector2(50, (stepvalue*(step-1))+(1.1f*stepvalue*Mathf.Lerp(0,1, normalizedTime)));
				yield return null;
			}
			progressFluid.GetComponent<RectTransform>().sizeDelta =	new Vector2(50, (stepvalue*step));			

		}
		else{
			for(float t = 0.0f; t<fadetime; t+= Time.deltaTime){
				float normalizedTime = t/fadetime;
	//			Debug.Log("1");
				progressFluid.GetComponent<RectTransform>().sizeDelta =	new Vector2(50, (stepvalue*(step-1))+(1.1f*stepvalue*Mathf.Lerp(0,1, normalizedTime)));
				yield return null;
			}
			float current = (stepvalue*(step-1))+(1.1f*stepvalue);
			for(float t = 0.0f; t<fadetime; t+= Time.deltaTime){
				float normalizedTime = t/fadetime;
	//			Debug.Log("1");
				progressFluid.GetComponent<RectTransform>().sizeDelta =	new Vector2(50, current-(.1f*stepvalue*Mathf.Lerp(0,1, normalizedTime)));
				yield return null;
			}
			progressFluid.GetComponent<RectTransform>().sizeDelta =	new Vector2(50, (stepvalue*step));			
		}


	}
	// public static void TakeTurn(int number){
	// 	if(number <= LevelStorer.efficientturns){
	// 	GameObject currentChild = TurnHolder.transform.GetChild(number-1).gameObject;
	// 	currentChild.GetComponent<Image>().color = Color.green;
	// 	}
	// 	else if(number<= LevelStorer.efficientturns*2){
	// 	GameObject currentChild = TurnHolder.transform.GetChild(2*LevelStorer.efficientturns - number).gameObject;
	// 	currentChild.GetComponent<Image>().color = Color.yellow;			
	// 	}
	// 	else if(number <= LevelStorer.efficientturns*3){
	// 	GameObject currentChild = TurnHolder.transform.GetChild(number-1 - 2*LevelStorer.efficientturns).gameObject;
	// 	currentChild.GetComponent<Image>().color = Color.red;			
	// 	}	
	// }
}
