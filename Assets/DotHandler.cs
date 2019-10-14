using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotHandler : MonoBehaviour
{
	public Sprite empty;
	public Sprite taken;
	public static DotHandler thisDH;

	void Awake(){
		thisDH = this;
		//InitializeDots(5);

	}
	void Update(){

	}
    // Start is called before the first frame update
	public static void InitializeDots(int amount){
		for(int i =0; i<21;i++){
			GameObject currentdot = thisDH.transform.GetChild(i).gameObject;
			currentdot.SetActive(false);			
		}
		for (int i = 0; i < amount; i++){
			GameObject currentdot = thisDH.transform.GetChild(i).gameObject;
			RectTransform dotRT = currentdot.GetComponent<RectTransform>();
			float height = ((float)(i+1)/(float)amount)*700f;
			//Debug.Log(dotRT.localPosition);
			//Debug.Log(dotRT.position);
			dotRT.localPosition = new Vector3(dotRT.localPosition.x, (float)height, dotRT.localPosition.z);
			currentdot.GetComponent<Image>().sprite = thisDH.empty;
			currentdot.SetActive(true);
		}
	}
	public static void TakeTurn(int turnnumber){
		if((turnnumber-1)<LevelStorer.efficientturns){
			GameObject currentdot = thisDH.transform.GetChild(turnnumber-1).gameObject;
			currentdot.GetComponent<Image>().sprite = thisDH.taken;			
		}
	}
}
