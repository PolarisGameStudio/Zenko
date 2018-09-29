using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnGraphics : MonoBehaviour {
	static float cwidth;
	static float cheight;
	public GameObject turnorb;
	public static GameObject staticorb;
	static GameObject transformer;
	// Use this for initialization
	void Start () {
		staticorb = turnorb;
		AssignDimensions();
		transformer = this.gameObject;
		//Create5();
		if(LevelStorer.efficientturns != 0){
			SetTurnCounter(LevelStorer.efficientturns);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void AssignDimensions(){
		RectTransform rtransform = gameObject.GetComponent<RectTransform>();
		cwidth = rtransform.rect.width;
		cheight = rtransform.rect.height;		
	}

	static void CreateImage(float x, float y){
		Debug.Log("TURNORB");
		GameObject turnorb1 = Instantiate(staticorb ,new Vector3(0,0,0), Quaternion.identity);
		turnorb1.transform.parent = transformer	.transform;
		turnorb1.transform.localPosition = new Vector3(x*cwidth/4,y*cheight/4,0);
	}
	static void Create2(){
		CreateImage(-1,0);
		CreateImage(1,0);
	}
	static void Create3(){
		CreateImage(-1, -0.5774f);
		CreateImage(1,-0.5774f);
		CreateImage(0, 1.1546f);
	}
	static void Create4(){
		CreateImage(-1, -0.5774f);
		CreateImage(1,-0.5774f);
		CreateImage(0, 1.1546f);		
		CreateImage(0, 0);
		Debug.Log(Vector2.Distance(new Vector2(-1,-.5774f), new Vector2(0, -0)));
		Debug.Log(Vector2.Distance(new Vector2(1,-.5774f),new  Vector2(0, -0)));
		Debug.Log(Vector2.Distance(new Vector2(0,1.1546f), new Vector2(0, -0)));

	}
	static void Create5(){
		//From mathworld.wolfram.com/RegularPentagon.html
		float c1 = 0.30901699437f;
		float c2 = 0.80901699437f;
		float s1 = 0.95105651629f;
		float s2 = 	0.58778525229f;
		Debug.Log(Vector2.Distance(new Vector2(0,1), new Vector2(s1,c1)));

		//CreateImage(0,0);
		CreateImage(0,1);
		CreateImage(s1,c1);
		CreateImage(s2,-c2);
		CreateImage(-s2,-c2);
		CreateImage(-s1,c1);
	}
	static void Create6(){
		//From mathworld.wolfram.com/RegularPentagon.html
		float c1 = 0.30901699437f;
		float c2 = 0.80901699437f;
		float s1 = 0.95105651629f;
		float s2 = 	0.58778525229f;
		Debug.Log(Vector2.Distance(new Vector2(0,1), new Vector2(s1,c1)));

		//CreateImage(0,0);
		CreateImage(0,1);
		CreateImage(s1,c1);
		CreateImage(s2,-c2);
		CreateImage(-s2,-c2);
		CreateImage(-s1,c1);
	}

	public static void SetTurnCounter(int eturns){
		Debug.Log("ETURNS" + eturns);
		switch(eturns){
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
		}
	}
}
