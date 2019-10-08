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
	public GameObject lastgoodtile;
	public bool isinboard;
	public bool startedDragging;
	public PieceHolders pieceHolder;
	public Vector3 piecePosition;
	public GameObject takenTile;
	void Start(){
		particle = GameObject.Find("Main Camera").GetComponent<LevelBuilder>().smoke_particle.gameObject;
		restingpoint = transform.position;
		gototile = false;
		gotosky = false;
		pieceHolder = GameObject.Find("PieceHolders").GetComponent<PieceHolders>();
//		AIBrain.pieces.Add(this.gameObject);
	 	Collider[] colliders = Physics.OverlapSphere(new Vector3(transform.position.x,0,transform.position.z), .5f);	
		foreach (Collider component in colliders) {
			if(component.tag == "Tile"){
			lastgoodtile = component.gameObject;			
			}
			//Debug.Log(component.gameObject);

	//		Debug.Log("current is " + currenttile);
		}
	}

	void Update(){
		//Debug.Log(Input.touchCount);
		if(readyToPop){
			if(Vector3.Distance(LevelManager.playert.position, transform.position) < .7){///FIX THIS
				convertWhenReady = true;
				readyToPop = false;
			}
		}
		if(convertWhenReady){
			if(LevelManager.playert != null){			
				if(Vector3.Distance(LevelManager.playert.position, transform.position) > .8){
				myshrinker.SetActive(false);
				myBigger.SetActive(true);
				myBigger.gameObject.GetComponent<Animator>().SetInteger("Phase", 2);
				SfxHandler.Instance.PlaySeedPop();
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
			if(PieceHolders.hinting){

			}
			else{
				OnMouseDrag();
			}
		}
		if(Input.GetMouseButtonUp(0) && startedDragging){
			startedDragging = false;			
			OnMouseUp();

		}
	}
	public void GoToHint(Vector3 postogo){
		PieceHolders.hinting = true;

		StartCoroutine(HintMove(postogo));
	}
	 public void OnMouseDown() {
	 	Debug.Log(TurnBehaviour.turn + " " + LevelBuilder.resetting + " " + LevelManager.isdragging);
	 	//PieceHolders.placedpieces.Remove(this);
		//transform.GetChild(1).gameObject.SetActive(false);
  	  //screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position); // I removed this line to prevent centring 
  	 // _lockedYPosition = screenPoint.y;
	 	toggleColliders();
//	 	Debug.Log(TurnBehaviour.turn);


		if ((TurnBehaviour.turn == 0 || LevelBuilder.resetting) & !LevelManager.configging) {
			PlaneBehavior.readyToDrop = false;

			//PieceHolders.placedpieces.Remove(this);
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
			Debug.Log(transform.position.x + "" + transform.position.z + "totaldimension" + LevelBuilder.totaldimension);//HERES WHERE THE PROBLEM BE
			//Fix this to avoid untaking wrong tiles when clicking on the 3d object but the planepos somewhere else.
			if(transform.position.x<LevelBuilder.totaldimension && transform.position.x>0 && -transform.position.z<LevelBuilder.totaldimension && transform.position.z<0){
				mytile = LevelBuilder.tiles[(int)gameObject.transform.position.x, -(int)gameObject.transform.position.z];
				mytile.type = "Ice";
				Debug.Log("UNTAKING IT at" + (int)gameObject.transform.position.x + -(int)gameObject.transform.position.z + "Type "+ myType);
				mytile.isTaken = false;
				//Debug.Log(LevelBuilder.tiles[(int)gameObject.transform.position.x, -(int)gameObject.transform.position.z-1].isTaken);
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
  	public IEnumerator HintMove(Vector3 postogo){ 
  		OnMouseDown();
  		//float fadetime = 2;
  		Vector3 initialPosition = transform.position;
  		initialPosition.y = .23f;
  		postogo.y = .23f;
  		Vector3 direction = postogo-initialPosition;
  		Debug.Log(direction);
  		float distance = Vector3.Distance(postogo, initialPosition);
  		float fadetime = .3f*distance;
  		Debug.Log(distance);
  		Debug.Log(postogo + "postogo");
  		PlaneBehavior.RoboRay(initialPosition);
        for(float t = 0.0f; t<fadetime; t+= Time.deltaTime){
        	float normalizedTime = t/fadetime;
            PlaneBehavior.simulatedMouse = new Vector3(initialPosition.x+direction.x*Mathf.Lerp(0,1, normalizedTime),.23f,initialPosition.z+direction.z*Mathf.Lerp(0,1, normalizedTime));
            OnMouseDrag();
            yield return null;
        }
		PlaneBehavior.simulatedMouse = postogo;
        //while(transform.position != postogo){
        OnMouseDrag();
        //}
        OnMouseUp();
        Vector3 position = new Vector2 (postogo.x, -postogo.z);
        Debug.Log(position);
		if(myType == "Wall" || myType == "Seed"){
			pieceHolder.placeNormal(position, this);
		}
		if(myType == "Left" || myType == "Up" || myType == "Down" || myType == "Right"){
			pieceHolder.placeIcarus(position,this);
		}
        startedDragging = false;
        PieceHolders.hinting = false;
//        Vector3 currentposition = new Vector3(Mathf.Lerp(0,1, normalizedTime),Mathf.Lerp(0,1, normalizedTime),Mathf.Lerp(0,1, normalizedTime));
        //OnMouseDrag();
        //turn on colliders of draggers.
        for(int i=0; i<PieceHolders.placedpieces.Count; i++){
        	PieceHolders.placedpieces[i].gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        GoogleAds.Instance.RequestRewardBasedVideo();
        yield break;

	}
	public void DragHint(Vector3 postogo){
		/*if (TurnBehaviour.turn == 0 || LevelBuilder.resetting) {
			Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 15);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;


			positiontogo = PlaneBehavior.planePos;	
			piecePosition = new Vector3(PlaneBehavior.planePos.x, PlaneBehavior.planePos.y, PlaneBehavior.planePos.z);
			positiontogo = new Vector3(Mathf.RoundToInt(PlaneBehavior.planePos.x), PlaneBehavior.planePos.y, Mathf.RoundToInt(PlaneBehavior.planePos.z));	


			if(lastposition != positiontogo || currenttile == null){
				CheckAvailableTile(positiontogo);
			}	


			lastposition = positiontogo;


			if(!gotosky){
				transform.position = piecePosition;
				if(positiontogo.x<LevelBuilder.totaldimension && -transform.position.z<LevelBuilder.totaldimension){
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
		}*/
	}
public void OnMouseDrag()
	{ 
		if ((TurnBehaviour.turn == 0 || LevelBuilder.resetting) & !LevelManager.configging) {
			Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 15);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;
			// curPosition.x = _lockedYPosition;
//			Debug.Log(curScreenPoint);

			positiontogo = PlaneBehavior.planePos;	
			piecePosition = new Vector3(PlaneBehavior.planePos.x, PlaneBehavior.planePos.y, PlaneBehavior.planePos.z);
			positiontogo = new Vector3(Mathf.RoundToInt(PlaneBehavior.planePos.x), PlaneBehavior.planePos.y, Mathf.RoundToInt(PlaneBehavior.planePos.z));	
//			Debug.Log("positiontogo" + positiontogo);
//			Debug.Log("value of test" + LevelBuilder.tiles[2,1].isTaken);
//			Debug.Log("isreadytodrop" + PlaneBehavior.readyToDrop);

			if(lastposition != positiontogo || currenttile == null){
				//Debug.Log("Change");	
				CheckAvailableTile(positiontogo);
			}	
			/*if(!isinboard)	{

			positiontogo = PlaneBehavior.planePos;	
			transform.GetChild(1).gameObject.SetActive(false);

			}*/

			lastposition = positiontogo;
//			Debug.Log(positiontogo);


			if(!gotosky){
				transform.position = piecePosition;
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
 	LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].type == "Right")
 	{

 		LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].isSideways = LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].type;
 		
 	}
	
	LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].type = myType;
	LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].isTaken = true;
	LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].tileObj = this.gameObject;	
	
 }

 void CheckAvailableTile(Vector3 position){
 	Debug.Log("Checkingavail at " + position);
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
//		Debug.Log("Doing it");
		//Debug.Log("value of test" + LevelBuilder.tiles[2,1].isTaken);
		if(pasttile != null){
			pasttile.GetComponent<MouseOverer>().Leave();
		}
		if(currenttile!= null && currenttile != pasttile){
			currenttile.GetComponent<MouseOverer>().Enter();
			transform.GetChild(1).gameObject.SetActive(true);	
			if(PlaneBehavior.readyToDrop){
				lastgoodtile = currenttile;	
			}	
		}

		pasttile = currenttile;
		particle.SetActive(true);
		Debug.Log("lastgoodtile is" + lastgoodtile.transform.position);
		particle.transform.position = new Vector3(lastgoodtile.transform.position.x, particle.transform.position.y, lastgoodtile.transform.position.z);//change this

		if(myType == "Wall" || myType == "Up" ||myType == "Down" ||myType == "Right" ||myType == "Left" ){
			this.gameObject.GetComponent<Animator>().SetInteger("Phase", 1);

		}

		if(LevelBuilder.tiles[(int)position.x, -(int)position.z].isTaken){
			PlaneBehavior.ClosestTile();
			lastgoodtile = PlaneBehavior.highlightedTile;	
			particle.transform.position = new Vector3(lastgoodtile.transform.position.x, particle.transform.position.y, lastgoodtile.transform.position.z);//change this
		
			// particle.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
			// particle.transform.GetChild(1).GetComponent<Renderer>().enabled = false;
			// particle.transform.GetChild(2).GetComponent<Renderer>().enabled = false;
			particle.transform.GetChild(0).gameObject.SetActive(true);
		}
		else{
			
			// particle.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
			// particle.transform.GetChild(1).GetComponent<Renderer>().enabled = false;
			// particle.transform.GetChild(2).GetComponent<Renderer>().enabled = false;
			particle.transform.GetChild(0).gameObject.SetActive(true);
		}
	}
	else{
		if(myType == "Wall" || myType == "Up" ||myType == "Down" ||myType == "Right" ||myType == "Left" ){
			//this.gameObject.GetComponent<Animator>().SetInteger("Phase", 2);

		}
		isinboard = false;
		// particle.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
		// particle.transform.GetChild(1).GetComponent<Renderer>().enabled = false;
		// particle.transform.GetChild(2).GetComponent<Renderer>().enabled = false;
		particle.transform.GetChild(0).gameObject.SetActive(true);;
		if(pasttile!= null){
			pasttile.GetComponent<MouseOverer>().Leave();

		}
		currenttile = null;
		pasttile = null;
		PlaneBehavior.ClosestTile();
		lastgoodtile = PlaneBehavior.highlightedTile;
		// lastgoodtile = ClosestTile(position);
		//ClosestTile(position);
		particle.transform.position = new Vector3(lastgoodtile.transform.position.x, particle.transform.position.y, lastgoodtile.transform.position.z);//change this

		//particle.transform.position = new Vector3(PlaneBehavior.planePos.x, particle.transform.position.y, PlaneBehavior.planePos.z);

	}
//	Debug.Log(position.x + " " + position.z);

 }
 void OnMouseUp()
 {
  	Swiping.mydirection = "Null";
 	string pieceholdername;
 	if(particle != null){
 	// 	particle.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
		// particle.transform.GetChild(1).GetComponent<Renderer>().enabled = false; 
		// particle.transform.GetChild(2).GetComponent<Renderer>().enabled = false; 
		particle.transform.GetChild(0).gameObject.SetActive(false);
 	}

 	toggleColliders();

 	Cursor.visible = true;

 	if(TurnBehaviour.turn == 0 && !LevelManager.configging)
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
			//PieceHolders.placedpieces.Add(this);
			Debug.Log("ADDED " + this + " Back to pieceholders");
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
			LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].seedType = mySeedType;			
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
			Debug.Log("Out but can go to " + lastgoodtile.transform.position);


			//PieceHolders.placedpieces.Add(this);
			//transform.position = new Vector3(PlaneBehavior.tilex, 0, PlaneBehavior.tiley);
			positiontogo = new Vector3(lastgoodtile.transform.position.x, 0, lastgoodtile.transform.position.z);
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
			LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].seedType = mySeedType;			
			}
			if(myType == "Seed"){
				pieceholdername = mySeedType + myType;
			}
			else{
				pieceholdername = myType;
			}
			pieceHolder.unshadeImage(pieceholdername);











			/*if(myType == "Wall" || myType == "Up" ||myType == "Down" ||myType == "Right" ||myType == "Left" ){
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
			}*/
				
			//Destroy(this.gameObject);
	

		}
	}
		//Debug.Log()

		//transform.position.y = PlaneBehavior.tiley;
		Debug.Log("outside?");
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
 		LevelManager.isdragging = false;

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
		Debug.Log("ICARUS IS BEING REMOVED");
		Debug.Log(type);
		if(type == "Left"){
			if(LevelBuilder.tiles[(int)target.x-1, -(int)target.z].type == type){
				LevelBuilder.tiles[(int)target.x-1, -(int)target.z].type = "Ice";		
				//LevelBuilder.tiles[(int)target.x-1, -(int)target.z].isTaken = false;	
				LevelBuilder.tiles[(int)target.x-1, -(int)target.z].isSideways = null;	
				KeepAdjacentIcari(new Vector2(target.x-1, target.z));				
			}
			else if(LevelBuilder.tiles[(int)target.x-1, -(int)target.z].isSideways == type){
				LevelBuilder.tiles[(int)target.x-1, -(int)target.z].isSideways = null;
				KeepAdjacentIcari(new Vector2(target.x-1, target.z));				

			}



		} 
		if(type =="Right"){
			if(LevelBuilder.tiles[(int)target.x+1, -(int)target.z].type == type){
				LevelBuilder.tiles[(int)target.x+1, -(int)target.z].type = "Ice";		
				//LevelBuilder.tiles[(int)target.x+1, -(int)target.z].isTaken = false;	
				LevelBuilder.tiles[(int)target.x+1, -(int)target.z].isSideways = null;	
				KeepAdjacentIcari(new Vector2(target.x+1, target.z));				
				
			}		
			else if(LevelBuilder.tiles[(int)target.x+1, -(int)target.z].isSideways == type){
				LevelBuilder.tiles[(int)target.x+1, -(int)target.z].isSideways = null;
				KeepAdjacentIcari(new Vector2(target.x+1, target.z));				
			}
		}
		if(type == "Up" ){	
			if(LevelBuilder.tiles[(int)target.x, -(int)target.z-1].type == type){
				LevelBuilder.tiles[(int)target.x, -(int)target.z-1].type = "Ice";		
				//LevelBuilder.tiles[(int)target.x, -(int)target.z-1].isTaken = false;	
				LevelBuilder.tiles[(int)target.x, -(int)target.z-1].isSideways = null;	
				KeepAdjacentIcari(new Vector2(target.x, target.z-1));				
			}	
			else if(LevelBuilder.tiles[(int)target.x, -(int)target.z-1].isSideways == type){
				LevelBuilder.tiles[(int)target.x, -(int)target.z-1].isSideways = null;
				KeepAdjacentIcari(new Vector2(target.x, target.z-1));				
			}	
		}
		if(type == "Down"){
			if(LevelBuilder.tiles[(int)target.x, -(int)target.z+1].type == type){
				LevelBuilder.tiles[(int)target.x, -(int)target.z+1].type = "Ice";		
				//LevelBuilder.tiles[(int)target.x, -(int)target.z+1].isTaken = false;	
				LevelBuilder.tiles[(int)target.x, -(int)target.z+1].isSideways = null;	
				KeepAdjacentIcari(new Vector2(target.x, target.z+1));				
			}
			else if(LevelBuilder.tiles[(int)target.x, -(int)target.z+1].isSideways == type){
				LevelBuilder.tiles[(int)target.x, -(int)target.z+1].isSideways = null;
				KeepAdjacentIcari(new Vector2(target.x, target.z+1));				
			}		
		}
	}
	private void KeepAdjacentIcari(Vector2 target){
		//Debug.Log("x " + target.x  + " y "  + target.y);
		//List<Dragger> draggers = PopulateAdjacentIcari(target);


		// if(LevelBuilder.tiles[(int)target.x, -(int)target.y+1].tileObj.tag == "Down")
		// 	LevelBuilder.tiles[(int)target.x, -(int)target.y].type = "Down";
		// if(LevelBuilder.tiles[(int)target.x, -(int)target.y-1].tileObj.tag == "Up")
		// 	LevelBuilder.tiles[(int)target.x, -(int)target.y].type = "Up";
		// if(LevelBuilder.tiles[(int)target.x-1, -(int)target.y].tileObj.tag == "Right")
		// 	LevelBuilder.tiles[(int)target.x, -(int)target.y].type = "Right";
		// if(LevelBuilder.tiles[(int)target.x+1, -(int)target.y].tileObj.tag == "Left")
		// 	LevelBuilder.tiles[(int)target.x, -(int)target.y].type = "Left";
	}
	// private List<Dragger> PopulateAdjacentIcari(Vector2 target){
	// 	// List<Dragger> temp = new List<Dragger>();
	// 	// Vector3 overlapV3 = new vector3 
	// 	// Collider[] colliders = Physics.OverlapSphere(overlapV3, .5f);
	// 	// foreach (Collider component in colliders) {
	// 	// 	if (component.tag == "Right") {

	// 	// 	} 
	// 	// 	if (component.tag == "Left") {
	// 	// 		component.GetComponent<FragileBehaviour>().readytolava = true;
	// 	// 		component.GetComponent<FragileBehaviour>().player = this.gameObject;
	// 	// 	} 
	// 	// 	if (component.tag == "Down") {
	// 	// 		component.GetComponent<FragileBehaviour>().readytolava = true;
	// 	// 		component.GetComponent<FragileBehaviour>().player = this.gameObject;
	// 	// 	} 
	// 	// 	if (component.tag == "Up") {
	// 	// 		component.GetComponent<FragileBehaviour>().readytolava = true;
	// 	// 		component.GetComponent<FragileBehaviour>().player = this.gameObject;
	// 	// 	} 
	// 	// }		
	// }
	public void toggleColliders(){
		for(int i=0; i<LevelManager.piecetiles.Count; i++){
			if(LevelManager.piecetiles[i].gameObject == this.gameObject){

			}
			else{
				LevelManager.piecetiles[i].gameObject.GetComponent<BoxCollider>().enabled = !LevelManager.piecetiles[i].gameObject.GetComponent<BoxCollider>().enabled;
			}
		}
	}
	public void ClosestTile(Vector3 position){
		int currentx = (int)position.x;
		int currenty = (int)-position.z;
		Debug.Log(currentx + "" + currenty);
		
		if(currentx < 0 ){
			currentx= 0;
		}
		if(currenty < 0){
			currenty = 0;
		}
		if(currentx > LevelBuilder.totaldimension-1){
			currentx = LevelBuilder.totaldimension-1;
		}
		if(currenty > LevelBuilder.totaldimension-1){
			currenty = LevelBuilder.totaldimension-1;
		}
		Debug.Log(currentx + "" + currenty);

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