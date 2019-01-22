using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnGraphics : MonoBehaviour {
	static float cwidth;
	static float cheight;
	public GameObject turnorb;
	public static GameObject staticorb;
	static GameObject transformer;
	public Sprite blankpaw;
	public Sprite greenpaw;
	public Sprite yellowpaw;
	public Sprite redpaw;
	static Sprite nextsprite;
	static List <GameObject> curorbs = new List<GameObject>();
	static List <GameObject> neworbs = new List<GameObject>();
	// Use this for initialization
	void Start () {
		staticorb = turnorb;
		AssignDimensions();
		transformer = this.gameObject;

		//Create5();
		//if(LevelStorer.efficientturns != 0){
		//	SetTurnCounter(LevelStorer.efficientturns);
		//}
		//Create10();

	}
	
	// Update is called once per frame
	void Update () {
		//AssignDimensions();
	//AssignDimensions();
	}
	void AssignDimensions(){
		Debug.Log("ASssigning");
		RectTransform rtransform = gameObject.GetComponent<RectTransform>();
		cwidth = rtransform.rect.width-rtransform.rect.width/6;
		cheight = rtransform.rect.height-rtransform.rect.height/6;		
	}
	public void TakeTurn(int num){
		//nextsprite = greenpaw;

		if(num<=LevelStorer.efficientturns)
		transform.GetChild(num-1).gameObject.GetComponent<Image>().sprite = greenpaw;
		else if(num<=LevelStorer.efficientturns*2){
		transform.GetChild((2*LevelStorer.efficientturns)-num).gameObject.GetComponent<Image>().sprite = yellowpaw;
		}
		else if(num<=LevelStorer.efficientturns*3){
		transform.GetChild(num-(2*LevelStorer.efficientturns)-1).gameObject.GetComponent<Image>().sprite = redpaw;
		}


	}
	//public static void ClearCounters(){
		//foreach (Transform child in GetComponent<Transform>()) {
	    //	GameObject.Destroy(child.gameObject);
	 	//}
	//}

	static void CreateImage(float x, float y){

//		Debug.Log("TURNORB");
		GameObject turnorb1 = Instantiate(staticorb ,new Vector3(0,0,0), Quaternion.identity);
		turnorb1.transform.parent = transformer.transform;
		turnorb1.transform.localPosition = new Vector3(x*cwidth/4,y*cheight/4,0);
		turnorb1.GetComponent<RectTransform>().localScale = new Vector3(.98f,.98f,.98f);
		neworbs.Add(turnorb1);	
	}
	static void Create1(){
		CreateImage(0,0);
	}
	static void Create2(){
		CreateImage(-1,0);
		CreateImage(1,0);
	}
	static void Create3(){
		CreateImage(0,1);
		CreateImage(.866f,-.5f);
		CreateImage(-.866f,-.5f);

	}
	static void Create4(){
		CreateImage(0, 1);
		CreateImage(.866f, -0.5f);
		CreateImage(-.866f,-0.5f);
		CreateImage(0, 0);
		//Debug.Log(Vector2.Distance(new Vector2(-1,-.5774f), new Vector2(0, -0)));
		//Debug.Log(Vector2.Distance(new Vector2(1,-.5774f),new  Vector2(0, -0)));
		//Debug.Log(Vector2.Distance(new Vector2(0,1.1546f), new Vector2(0, -0)));

	}
	static void Create5(){
		//From mathworld.wolfram.com/RegularPentagon.html
		float c1 = 0.30901699437f;
		float c2 = 0.80901699437f;
		float s1 = 0.95105651629f;
		float s2 = 	0.58778525229f;
		// Debug.Log(Vector2.Distance(new Vector2(0,1), new Vector2(s1,c1)));

		//CreateImage(0,0);
		CreateImage(0,1);
		CreateImage(s1,c1);
		CreateImage(s2,-c2);
		CreateImage(-s2,-c2);
		CreateImage(-s1,c1);
	}
	static void Create6(){
		//From mathworld.wolfram.com/RegularPentagon.html
		float c1 = 0.866f;
		float c2 = 0f;
		float s1 = 0.5f;
		float s2 = 	1f;
		//Debug.Log(Vector2.Distance(new Vector2(0,1), new Vector2(s1,c1)));

		//CreateImage(0,0);
		//CreateImage(0,0);
		CreateImage(-s1,c1);
		CreateImage(s1,c1);
		CreateImage(s2,c2);
		CreateImage(s1,-c1);
		CreateImage(-s1,-c1);
		CreateImage(-s2,c2);
	}
	static void Create7(){
		//From mathworld.wolfram.com/RegularPentagon.html
		float c1 = 0.866f;
		float c2 = 0f;
		float s1 = 0.5f;
		float s2 = 	1f;
		//Debug.Log(Vector2.Distance(new Vector2(0,1), new Vector2(s1,c1)));

		//CreateImage(0,0);
		CreateImage(-s1,c1);
		CreateImage(s1,c1);
		CreateImage(s2,c2);
		CreateImage(s1,-c1);
		CreateImage(-s1,-c1);
		CreateImage(-s2,c2);
		CreateImage(0,0);
	}
		static void Create8(){
		//From mathworld.wolfram.com/RegularPentagon.html
		float c1 = 0.866f;
		float c2 = 0f;
		float s1 = 0.5f;
		float s2 = 	1f;
		//Debug.Log(Vector2.Distance(new Vector2(0,1), new Vector2(s1,c1)));

		//CreateImage(0,0);
		CreateImage(0,c1*2);
		CreateImage(s1,c1);
		CreateImage(s2,c2);
		CreateImage(s1,-c1);
		CreateImage(-s1,-c1);
		CreateImage(-s2,c2);
		CreateImage(-s1,c1);
		CreateImage(0,0);
	}
	static void Create9(){
		//From mathworld.wolfram.com/RegularPentagon.html
		float c1 = 0.866f;
		float c2 = 0f;
		float s1 = 0.5f;
		float s2 = 	1f;
		//Debug.Log(Vector2.Distance(new Vector2(0,1), new Vector2(s1,c1)));

		//CreateImage(0,0);
		CreateImage(0,c1*2);
		CreateImage(s1,c1);
		CreateImage(s2,c2);
		CreateImage(s1,-c1);
		CreateImage(0,-c1*2);		
		CreateImage(-s1,-c1);
		CreateImage(-s2,c2);
		CreateImage(-s1,c1);
		CreateImage(0,0);
		
	}	
	static void Create10(){
		//From mathworld.wolfram.com/RegularPentagon.html
		float c1 = 0.866f;
		float c2 = 0f;
		float s1 = 0.5f;
		float s2 = 	1f;
		//Debug.Log(Vector2.Distance(new Vector2(0,1), new Vector2(s1,c1)));


		CreateImage(0,c1*2);
		CreateImage(s1,c1);
		CreateImage(s2,c2);
		CreateImage(s1*3,-c1);
		CreateImage(s1,-c1);		
		CreateImage(-s1,-c1);
		CreateImage(-s1*3,-c1);		
		CreateImage(-s2,c2);
		CreateImage(-s1,c1);
		CreateImage(0,0);
		
	}	

	public static void SetTurnCounter(int eturns){
		curorbs = neworbs;
//		Debug.Log(curorbs.Count);
		if(0<curorbs.Count){
			for(int i=0; i<curorbs.Count; i++){
				Destroy(curorbs[i]);
			}
		}
		curorbs.Clear();
		//Debug.Log(curorbs.Count);
		Debug.Log("Efficient turns = " + eturns);
		switch(eturns){
			case 1:
				Create1();
				break;
			case 2:
				Create2();
				break;
			case 3:
				Create3();
				break;
			case 4:
				Create4();
				break;
			case 5:
				Create5();
				break;
			case 6:
				Create6();
				break;
			case 7:
				Create7();
				break;
			case 8:
				Create8();
				break;
			case 9:
				Create9();
				break;
			case 10:
				Create10();
				break;
		}
	}
}
