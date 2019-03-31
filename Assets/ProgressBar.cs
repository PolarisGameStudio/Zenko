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
	// Use this for initialization
	void Awake () {
		//InitializeProgressBar(10);
		TurnHolder = this.gameObject;
		colorone = color1;
		colortwo = color2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public static void InitializeProgressBar(int size){
		for(int i = 0; i<16;i++){
			GameObject currentChild;
			currentChild = TurnHolder.transform.GetChild(i).gameObject;
			currentChild.SetActive(false);			
		}
		TurnHolder.GetComponent<GridLayoutGroup>().cellSize   =  new Vector2(100f, (float)(700/size)) ;
		for (int i = 0; i < size; i++){
			GameObject currentChild;
			currentChild = TurnHolder.transform.GetChild(i).gameObject;
			currentChild.SetActive(true);
			if(i%2 == 0){
				currentChild.GetComponent<Image>().color = colorone;
			}
			else{
				currentChild.GetComponent<Image>().color = colortwo;
			}
		}
	}
	public static void TakeTurn(int number){
		if(number <= LevelStorer.efficientturns){
		GameObject currentChild = TurnHolder.transform.GetChild(number-1).gameObject;
		currentChild.GetComponent<Image>().color = Color.green;
		}
		else if(number<= LevelStorer.efficientturns*2){
		GameObject currentChild = TurnHolder.transform.GetChild(2*LevelStorer.efficientturns - number).gameObject;
		currentChild.GetComponent<Image>().color = Color.yellow;			
		}
		else if(number <= LevelStorer.efficientturns*3){
		GameObject currentChild = TurnHolder.transform.GetChild(number-1 - 2*LevelStorer.efficientturns).gameObject;
		currentChild.GetComponent<Image>().color = Color.red;			
		}
		
	}
}
