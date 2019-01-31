using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dragger : MonoBehaviour {
	private Vector3 screenPoint; 
	private Vector3 offset; 
	public GameObject tentativetile;
	Vector3 myPosition;
	SpriteRenderer tiletodropsprite;
//	TileHandler tilescript;
	public bool needtooccupy;
	public GameObject newtile;
	private Vector3 restingpoint;
	public string myType;
	Tile mytile;
	public string mySeedType;
	public GameObject myshrinker;
	public GameObject myBigger;
	public bool readyToPop;
	public Transform playert;
	public bool convertWhenReady;
	public Vector3 positiontogo;
	public bool gototile;
	public bool gotosky;

	void Start(){
		restingpoint = transform.position;
		gototile = false;
		gotosky = false;
//		AIBrain.pieces.Add(this.gameObject);

	}

	void Update(){
		if(readyToPop){
			Debug.Log(playert.position);
			if(Vector3.Distance(playert.position, transform.position) < .7){
				convertWhenReady = true;
				readyToPop = false;
			}
		}
		if(convertWhenReady){
			if(Vector3.Distance(playert.position, transform.position) > .8){
				myshrinker.SetActive(false);
				myBigger.SetActive(true);
				myBigger.gameObject.GetComponent<Animator>().SetInteger("Phase", 2);
				convertWhenReady = false;

			}
		}
		if(gototile){
			transform.position = Vector3.MoveTowards (transform.position, positiontogo, Time.deltaTime * 8); 
			if(transform.position == positiontogo){
				gototile = false;
			}
		}
		if(gotosky){
			transform.position = Vector3.MoveTowards (transform.position, positiontogo, Time.deltaTime * 10); 
			if(transform.position == positiontogo){
				gotosky = false;
			}
		}
	}

	 void OnMouseDown() {
    //screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position); // I removed this line to prevent centring 
   // _lockedYPosition = screenPoint.y;
	 	toggleColliders();
		if (TurnBehaviour.turn == 0) {
			if(myType == "Wall"){
				this.gameObject.GetComponent<Animator>().SetInteger("Phase", 1);
			}
			if(myType == "Left" || myType == "Right" ||myType == "Up" ||myType == "Down"){
				this.gameObject.GetComponent<Animator>().SetInteger("Phase", 1);
			}	
	 		LevelManager.isdragging = true;
			Cursor.visible = false;
			Swiping.canswipe = false;
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 15));
			//Cursor.visible = false;
			Debug.Log (-transform.position.z);
			Debug.Log(transform.position.x);
			if(transform.position.x<LevelBuilder.cols&& -transform.position.z<LevelBuilder.rows){
				mytile = LevelBuilder.tiles[(int)gameObject.transform.position.x, -(int)gameObject.transform.position.z];
				mytile.type = null;
				mytile.isTaken = false;
				Debug.Log(mytile);
			}		
			else{
				Debug.Log("out");
				//gotosky = true;

			}


			//if(LevelBuilder.tiles[(int)gameObject.transform.position.x, -(int)gameObject.transform.position].type){

			//}
			GetComponent<BoxCollider>().enabled = false;
		}
		//notmoving = false;
 }
 
 void OnMouseDrag()
	{ 
		if (TurnBehaviour.turn == 0) {
			Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 15);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;
			// curPosition.x = _lockedYPosition;
//			Debug.Log(curScreenPoint);
			positiontogo = PlaneBehavior.planePos;	
			positiontogo = new Vector3(Mathf.RoundToInt(PlaneBehavior.planePos.x), PlaneBehavior.planePos.y, Mathf.RoundToInt(PlaneBehavior.planePos.z));	

			if(!gotosky){
				transform.position = positiontogo;
				if(positiontogo.x<LevelBuilder.cols && -transform.position.z<LevelBuilder.rows){
					//transform.position = new Vector3(mytile);
				}
			}

			myPosition = transform.position;
			if(Input.touchCount>0){
				Touch t = Input.GetTouch(0);
				Swiping.firstPressPos = new Vector2(t.position.x,t.position.y);

			}
			else{
				Swiping.firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			}

//			FindHoveredTile ();
			//Touch t = Input.GetTouch(0);
				//save began touch 2d point
			//Swiping.firstPressPos = new Vector2(t.position.x,t.position.y);
		}

	}
 
 void OnMouseUp()
 {
 	toggleColliders();
 	LevelManager.isdragging = false;
 	Cursor.visible = true;
 	Swiping.mydirection = "Null";

 	if(TurnBehaviour.turn == 0)
 	{
		Debug.Log(PlaneBehavior.tilex);
		Debug.Log(PlaneBehavior.tiley);
		GetComponent<BoxCollider>().enabled = true;
		if(PlaneBehavior.readyToDrop){
			//transform.position = new Vector3(PlaneBehavior.tilex, 0, PlaneBehavior.tiley);
			positiontogo = new Vector3(PlaneBehavior.tilex, 0, PlaneBehavior.tiley);
			gototile = true;
			LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].type = myType;
			LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].isTaken = true;
			LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].tileObj = this.gameObject;
			if(myType == "Wall"){
			this.gameObject.GetComponent<Animator>().SetInteger("Phase", 2);
			}
			if(myType == "Left" || myType == "Right" ||myType == "Up" ||myType == "Down"){
			this.gameObject.GetComponent<Animator>().SetInteger("Phase", 2);
			}			

			if (myType == "Seed"){
			LevelBuilder.tiles[(int)transform.position.x, -(int)transform.position.z].seedType = mySeedType;			
			}
		}
		else{
			transform.position = restingpoint;
			if(myType == "Wall"){
				this.gameObject.GetComponent<Animator>().SetInteger("Phase", 0);
			}
			if(myType == "Left" || myType == "Right" ||myType == "Up" ||myType == "Down"){
			this.gameObject.GetComponent<Animator>().SetInteger("Phase", 0);
			}	
		}
	}
		//Debug.Log()

		//transform.position.y = PlaneBehavior.tiley;
		if(Input.touchCount>0){
			Touch t = Input.GetTouch(0);
			Swiping.firstPressPos = new Vector2(t.position.x,t.position.y);

		}
		else{
			Swiping.firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		}
		//save began touch 2d point
		//Swiping.firstPressPos = new Vector2(t.position.x,t.position.y);
		Swiping.canswipe = true;

		/*if (TurnBehaviour.turn == 0) {
   			 Cursor.visible = true;
   			 }
		if (tilescript.myTaker == null) {
			needtooccupy = true;
			transform.position = tentativetile.transform.position + new Vector3 (0, 0, -.01f);
			newtile = tentativetile;
		} 
		else {
			transform.position = restingpoint;
		}*/
		//float z = -.01f;
		//transform.position.z = z;
		//Touch t = Input.GetTouch(0);

		//Swiping.firstPressPos = new Vector2(t.position.x,t.position.y);

		//Swiping.canswipe = true;
	}
	public void unPop(){

	} 

	public void toggleColliders(){
		for(int i=0; i<LevelManager.piecetiles.Count; i++){
			if(LevelManager.piecetiles[i].gameObject == this.gameObject){

			}
			else{
				LevelManager.piecetiles[i].gameObject.GetComponent<BoxCollider>().enabled = !LevelManager.piecetiles[i].gameObject.GetComponent<BoxCollider>().enabled;
			}
		}
	}
	/*void FindHoveredTile(){
		Collider2D[] colliders = Physics2D.OverlapCircleAll(myPosition, .1f); ///Presuming the object you are testing also has a collider 0 otherwise{
		foreach(Collider2D component in colliders){
			if (component.tag == "Ground") {
				tentativetile = component.gameObject;
				tilescript = tentativetile.GetComponent<TileHandler> ();
				//newtile = tentativetile;
				//tiletodropsprite = tentativetile.GetComponent<SpriteRenderer> ();
				//tiletodropsprite.color = Color.black;

				//tilescript.myTaker = this.gameObject;
				//tilescript.isTaken = true;
				//Debug.Log ("Pew");
				//mytile = tileobject;

			}
		}
	}*/
}