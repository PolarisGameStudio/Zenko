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
	public Vector3 lastposition;
	public bool gototile;
	public bool gotosky;
	public GameObject currenttile;
	public GameObject pasttile;
	public GameObject particle;
	public bool isinboard;
	public bool startedDragging;
	public PieceHolders pieceHolder;
	void Start(){
		particle = GameObject.Find("Main Camera").GetComponent<LevelBuilder>().smoke_particle.gameObject;
		restingpoint = transform.position;
		gototile = false;
		gotosky = false;
		pieceHolder = GameObject.Find("PieceHolders").GetComponent<PieceHolders>();
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
			if(playert != null){			
				if(Vector3.Distance(playert.position, transform.position) > .8){
				myshrinker.SetActive(false);
				myBigger.SetActive(true);
				myBigger.gameObject.GetComponent<Animator>().SetInteger("Phase", 2);
				convertWhenReady = false;

			}
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
		if(startedDragging){
			OnMouseDrag();
		}
		if(Input.GetMouseButtonUp(0) && startedDragging){
			OnMouseUp();
			startedDragging = false;
		}
	}

	 public void OnMouseDown() {
	 	PieceHolders.placedpieces.Remove(this);
		//transform.GetChild(1).gameObject.SetActive(false);
  	  //screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position); // I removed this line to prevent centring 
  	 // _lockedYPosition = screenPoint.y;
	 	toggleColliders();
//	 	Debug.Log(TurnBehaviour.turn);
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
			//Debug.Log (-transform.position.z);
			//Debug.Log(transform.position.x);
			Debug.Log(transform.position.x + "" + transform.position.z);
			if(transform.position.x<LevelBuilder.totaldimension&& -transform.position.z<LevelBuilder.totaldimension){
				mytile = LevelBuilder.tiles[(int)gameObject.transform.position.x, -(int)gameObject.transform.position.z];
				mytile.type = "Ice";
				mytile.isTaken = false;
				Debug.Log(mytile.isSideways);
				if(mytile.isSideways != null){
					mytile.type = mytile.isSideways;
					mytile.isTaken = false;
				}
				Debug.Log(mytile);
				if(myType == "Left" || myType == "Right" ||myType == "Up" ||myType == "Down"){
					Debug.Log("Removing ice");
					removeIcarus(myType, new Vector3((int)gameObject.transform.position.x,(int)gameObject.transform.position.y,(int)gameObject.transform.position.z));
				}					
			}		
			else{
				Debug.Log("out");
				//gotosky = true;

			}
			// positiontogo = PlaneBehavior.planePos;	

			// positiontogo = new Vector3(Mathf.RoundToInt(PlaneBehavior.planePos.x), PlaneBehavior.planePos.y, Mathf.RoundToInt(PlaneBehavior.planePos.z));	
			
			// CheckAvailableTile(positiontogo);


			//if(LevelBuilder.tiles[(int)gameObject.transform.position.x, -(int)gameObject.transform.position].type){

			//}
			GetComponent<BoxCollider>().enabled = false;
		}

		
			//notmoving =	 	false;	
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
			if(lastposition != positiontogo || currenttile == null){
				//Debug.Log("Change");	
				CheckAvailableTile(positiontogo);
			}	
			if(!isinboard)	{

			positiontogo = PlaneBehavior.planePos;	
			transform.GetChild(1).gameObject.SetActive(false);

			}

			lastposition = positiontogo;
//			Debug.Log(positiontogo);


			if(!gotosky){
				transform.position = positiontogo;
				if(positiontogo.x<LevelBuilder.totaldimension && -transform.position.z<LevelBuilder.totaldimension){
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
 void placeNormal(){
 	if(LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].type == "Left" || 
 	LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].type == "Down" ||
 	LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].type == "Up" ||
 	LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].type == "Right"){
 		
 		LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].isSideways = LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].type;
 		
 		}
	
	LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].type = myType;
	LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].isTaken = true;
	LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].tileObj = this.gameObject;	
	
 }

 void CheckAvailableTile(Vector3 position){
 	Collider[] colliders = Physics.OverlapSphere(new Vector3(position.x,0,position.z), .5f);	
	foreach (Collider component in colliders) {
		if(component.tag == "Tile"){
		currenttile = component.gameObject;			
		}
		//Debug.Log(component.gameObject);

//		Debug.Log("current is " + currenttile);
	}
	//Debug.Log(position.x + "" + position.z);
	if(position.x>-1 && position.x<LevelBuilder.totaldimension && position.z<1 && position.z>-LevelBuilder.totaldimension){
		isinboard = true;
		Debug.Log("Doing it");
		if(pasttile != null){
			pasttile.GetComponent<MouseOverer>().Leave();
		}
		if(currenttile!= null && currenttile != pasttile){
			currenttile.GetComponent<MouseOverer>().Enter();
			transform.GetChild(1).gameObject.SetActive(true);		

		}

		pasttile = currenttile;
		particle.SetActive(true);
		particle.transform.position = new Vector3(position.x, particle.transform.position.y, position.z);
		if(myType == "Wall" || myType == "Up" ||myType == "Down" ||myType == "Right" ||myType == "Left" ){
			this.gameObject.GetComponent<Animator>().SetInteger("Phase", 1);

		}

		if(LevelBuilder.tiles[(int)position.x, -(int)position.z].isTaken){
			particle.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
			particle.transform.GetChild(1).GetComponent<Renderer>().enabled = true;
			particle.transform.GetChild(2).GetComponent<Renderer>().enabled = false;


		}
		else{
			particle.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
			particle.transform.GetChild(1).GetComponent<Renderer>().enabled = false;
			particle.transform.GetChild(2).GetComponent<Renderer>().enabled = false;

		}
	}
	else{
		if(myType == "Wall" || myType == "Up" ||myType == "Down" ||myType == "Right" ||myType == "Left" ){
			this.gameObject.GetComponent<Animator>().SetInteger("Phase", 0);

		}
		isinboard = false;
		particle.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
		particle.transform.GetChild(1).GetComponent<Renderer>().enabled = false;
		particle.transform.GetChild(2).GetComponent<Renderer>().enabled = true;
		if(pasttile!= null){
			pasttile.GetComponent<MouseOverer>().Leave();

		}
		currenttile = null;
		pasttile = null;
		particle.transform.position = new Vector3(PlaneBehavior.planePos.x, particle.transform.position.y, PlaneBehavior.planePos.z);

	}
//	Debug.Log(position.x + " " + position.z);

 }
 void OnMouseUp()
 {
 	string pieceholdername;
 	if(particle != null){
 		particle.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
		particle.transform.GetChild(1).GetComponent<Renderer>().enabled = false; 
		particle.transform.GetChild(2).GetComponent<Renderer>().enabled = false; 
 	}

 	toggleColliders();
 	LevelManager.isdragging = false;
 	Cursor.visible = true;
 	Swiping.mydirection = "Null";
 	if(TurnBehaviour.turn == 0)
 	{
		Debug.Log(PlaneBehavior.tilex);
		Debug.Log(PlaneBehavior.tiley);
		GetComponent<BoxCollider>().enabled = true;
		if(currenttile  == null){
		positiontogo = PlaneBehavior.planePos;	

		positiontogo = new Vector3(Mathf.RoundToInt(PlaneBehavior.planePos.x), PlaneBehavior.planePos.y, Mathf.RoundToInt(PlaneBehavior.planePos.z));	
	 	Collider[] colliders = Physics.OverlapSphere(new Vector3(positiontogo.x,0,positiontogo.z), .5f);	
		foreach (Collider component in colliders) {
			Debug.Log(component.gameObject);
			currenttile = component.gameObject;
		}

		}
		if(PlaneBehavior.readyToDrop){//from mouseoverer
			PieceHolders.placedpieces.Add(this);
			//transform.position = new Vector3(PlaneBehavior.tilex, 0, PlaneBehavior.tiley);
			positiontogo = new Vector3(PlaneBehavior.tilex, 0, PlaneBehavior.tiley);
			gototile = true;
			if(myType == "Left" || myType == "Right" ||myType == "Up" ||myType == "Down"){
			placeIcarus(myType, positiontogo);

			}
			else{
				placeNormal();
				/*LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].type = myType;
				LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].isTaken = true;
				LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].tileObj = this.gameObject;*/				
			}
			
			if(myType == "Wall"){
			this.gameObject.GetComponent<Animator>().SetInteger("Phase", 2);
			}
			if(myType == "Left" || myType == "Right" ||myType == "Up" ||myType == "Down"){
			this.gameObject.GetComponent<Animator>().SetInteger("Phase", 2);
			}			

			if (myType == "Seed"){
			LevelBuilder.tiles[(int)transform.position.x, -(int)transform.position.z].seedType = mySeedType;			
			}
			if(myType == "Seed"){
				pieceholdername = mySeedType + myType;
			}
			else{
				pieceholdername = myType;
			}
			pieceHolder.unshadeImage(pieceholdername);
		}
		else{
			if(myType == "Wall" || myType == "Up" ||myType == "Down" ||myType == "Right" ||myType == "Left" ){
				this.gameObject.GetComponent<Animator>().SetInteger("Phase", 0);

			}

			transform.GetChild(1).gameObject.SetActive(true);				
			transform.position = restingpoint;
			if(myType == "Wall"){
				this.gameObject.GetComponent<Animator>().SetInteger("Phase", 0);
			}
			if(myType == "Left" || myType == "Right" ||myType == "Up" ||myType == "Down"){
			this.gameObject.GetComponent<Animator>().SetInteger("Phase", 0);				
			}
			//PieceHolders.placedpieces.Remove(this);
			if(myType == "Seed"){
				string name = mySeedType + myType;
				Debug.Log(name);
				pieceHolder.updateValueUp(name);	
			}
			else{
				pieceHolder.updateValueUp(myType);	
			}
		
			Destroy(this.gameObject);
	

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
		currenttile = null;
		pasttile = null;

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
	public void placeIcarus(string type, Vector3 target){
		if(type == "Left"){
			LevelBuilder.tiles[(int)target.x, -(int)target.z].type = "Wall";
			LevelBuilder.tiles[(int)target.x, -(int)target.z].isTaken = true;
			LevelBuilder.tiles[(int)target.x, -(int)target.z].tileObj = this.gameObject;
			if(LevelBuilder.tiles[(int)target.x-1, -(int)target.z].type == "Ice"){
				LevelBuilder.tiles[(int)target.x-1, -(int)target.z].type = type;		
				//LevelBuilder.tiles[(int)target.x-1, -(int)target.z].isTaken = true;				
			}	
			/*else if(LevelBuilder.tiles[(int)target.x-1, -(int)target.z].type == "Wood"){
				LevelBuilder.tiles[(int)target.x-1, -(int)target.z].type = "Wood" + type;		
				LevelBuilder.tiles[(int)target.x-1, -(int)target.z].isTaken = true;				
			}	
			else if(LevelBuilder.tiles[(int)target.x-1, -(int)target.z].type == "Fragile"){
				LevelBuilder.tiles[(int)target.x-1, -(int)target.z].type = "Fragile" + type;		
				LevelBuilder.tiles[(int)target.x-1, -(int)target.z].isTaken = true;				
			}	*/
			else{
				LevelBuilder.tiles[(int)target.x-1, -(int)target.z].isSideways = "Left";
			}
		

		} 
		if(type =="Right"){
			LevelBuilder.tiles[(int)target.x, -(int)target.z].type = "Wall";
			LevelBuilder.tiles[(int)target.x, -(int)target.z].isTaken = true;
			LevelBuilder.tiles[(int)target.x, -(int)target.z].tileObj = this.gameObject;
			if(LevelBuilder.tiles[(int)target.x+1, -(int)target.z].type == "Ice"){
				LevelBuilder.tiles[(int)target.x+1, -(int)target.z].type = type;		
				//LevelBuilder.tiles[(int)target.x+1, -(int)target.z].isTaken = true;				
			}	
			else{
				LevelBuilder.tiles[(int)target.x+1, -(int)target.z].isSideways = "Right";
			}	

		}
		if(type == "Up" ){
			LevelBuilder.tiles[(int)target.x, -(int)target.z].type = "Wall";
			LevelBuilder.tiles[(int)target.x, -(int)target.z].isTaken = true;
			LevelBuilder.tiles[(int)target.x, -(int)target.z].tileObj = this.gameObject;	
			if(LevelBuilder.tiles[(int)target.x, -(int)target.z-1].type == "Ice"){
				LevelBuilder.tiles[(int)target.x, -(int)target.z-1].type = type;		
				//LevelBuilder.tiles[(int)target.x, -(int)target.z-1].isTaken = true;				
			}	
			else{
				LevelBuilder.tiles[(int)target.x, -(int)target.z-1].isSideways = "Up";
			}
		}
		if(type == "Down"){
			LevelBuilder.tiles[(int)target.x, -(int)target.z].type = "Wall";
			LevelBuilder.tiles[(int)target.x, -(int)target.z].isTaken = true;
			LevelBuilder.tiles[(int)target.x, -(int)target.z].tileObj = this.gameObject;	
			if(LevelBuilder.tiles[(int)target.x, -(int)target.z+1].type == "Ice"){
				LevelBuilder.tiles[(int)target.x, -(int)target.z+1].type = type;		
				//		qavsxvgczLevelBuilder.tiles[(int)target.x, -(int)target.z+1].isTaken = true;				
			}	
			else{
				LevelBuilder.tiles[(int)target.x, -(int)target.z+1].isSideways = "Down";
			}
		}
	}

	public void removeIcarus(string type, Vector3 target){
		Debug.Log(type);
		if(type == "Left"){
			if(LevelBuilder.tiles[(int)target.x-1, -(int)target.z].type == type){
				LevelBuilder.tiles[(int)target.x-1, -(int)target.z].type = "Ice";		
				//LevelBuilder.tiles[(int)target.x-1, -(int)target.z].isTaken = false;	
				LevelBuilder.tiles[(int)target.x-1, -(int)target.z].isSideways = null;					
			}
			else if(LevelBuilder.tiles[(int)target.x-1, -(int)target.z].isSideways == type){
				LevelBuilder.tiles[(int)target.x-1, -(int)target.z].isSideways = null;

			}


		} 
		if(type =="Right"){
			if(LevelBuilder.tiles[(int)target.x+1, -(int)target.z].type == type){
				LevelBuilder.tiles[(int)target.x+1, -(int)target.z].type = "Ice";		
				//LevelBuilder.tiles[(int)target.x+1, -(int)target.z].isTaken = false;	
				LevelBuilder.tiles[(int)target.x+1, -(int)target.z].isSideways = null;					
			}		
			else if(LevelBuilder.tiles[(int)target.x+1, -(int)target.z].isSideways == type){
				LevelBuilder.tiles[(int)target.x+1, -(int)target.z].isSideways = null;

			}
		}
		if(type == "Up" ){	
			if(LevelBuilder.tiles[(int)target.x, -(int)target.z-1].type == type){
				LevelBuilder.tiles[(int)target.x, -(int)target.z-1].type = "Ice";		
				//LevelBuilder.tiles[(int)target.x, -(int)target.z-1].isTaken = false;	
				LevelBuilder.tiles[(int)target.x, -(int)target.z-1].isSideways = null;					
			}	
			else if(LevelBuilder.tiles[(int)target.x, -(int)target.z-1].isSideways == type){
				LevelBuilder.tiles[(int)target.x, -(int)target.z-1].isSideways = null;

			}	
		}
		if(type == "Down"){
			if(LevelBuilder.tiles[(int)target.x, -(int)target.z+1].type == type){
				LevelBuilder.tiles[(int)target.x, -(int)target.z+1].type = "Ice";		
				//LevelBuilder.tiles[(int)target.x, -(int)target.z+1].isTaken = false;	
				LevelBuilder.tiles[(int)target.x, -(int)target.z+1].isSideways = null;					
			}
			else if(LevelBuilder.tiles[(int)target.x, -(int)target.z+1].isSideways == type){
				LevelBuilder.tiles[(int)target.x, -(int)target.z+1].isSideways = null;

			}		
		}
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
				//tiletodropsprite.- = Color.black;

				//tilescript.myTaker = this.gameObject;
				//tilescript.isTaken = true;
				//Debug.Log ("Pew");
				//mytile = tileobject;

			}
		}
	}*/
}