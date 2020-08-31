using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dragger : MonoBehaviour {
	private Vector3 screenPoint; 
	private Vector3 offset; 
	public GameObject tentativetile;
	Vector3 myPosition;
	SpriteRenderer tiletodropsprite;
	public bool needtooccupy;
	public GameObject newtile;
	public string myType;
	Tile mytile;
	public string mySeedType;
	public string portalType;
	public GameObject myshrinker;
	public GameObject myBigger;
	public bool readyToPop;
	public Transform playert;
	public bool convertWhenReady;
	public Vector3 positiontogo;
	public Vector3 lastposition;
	public bool gototile;
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
		gototile = false;
		pieceHolder = GameObject.Find("PieceHolders").GetComponent<PieceHolders>();
	 	Collider[] colliders = Physics.OverlapSphere(new Vector3(transform.position.x,0,transform.position.z), .5f);	
		foreach (Collider component in colliders) {
			if(component.tag == "Tile"){
				lastgoodtile = component.gameObject;			
			}
		}
	}

	void Update(){
		//Debug.Log(transform.position);

	}
	void FixedUpdate(){
		if(myType == "Seed"){
			SeedChecking();
		}
		if(gototile){
			MoveUpOrDown();
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

	void SeedChecking(){
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
	}

	void MoveUpOrDown(){
		if(gototile){
			transform.position = Vector3.MoveTowards (transform.position, positiontogo, Time.deltaTime * 8); 
			if(transform.position == positiontogo){
				gototile = false;
			}
		}
	}

	public void GoToHint(Vector3 postogo){
		PieceHolders.hinting = true;
		StartCoroutine(HintMove(postogo));
	}

	public void OnMouseDown() {
	 	toggleColliders();
		PlaneBehavior.Instance.PlaneRay();
		if ((TurnBehaviour.turn == 0 || LevelBuilder.resetting) & !LevelManager.configging) {
			SfxHandler.Instance.PickUp();
			PlaneBehavior.readyToDrop = false;
			if(myType == "Wall" || myType == "Portal"){
				this.gameObject.GetComponent<Animator>().SetInteger("Phase", 1);
			}
			if(myType == "Left" || myType == "Right" ||myType == "Up" ||myType == "Down"){
				this.gameObject.GetComponent<Animator>().SetInteger("Phase", 1);
			}	
	 		LevelManager.isdragging = true;
			Cursor.visible = false;
			Swiping.canswipe = false;
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 15));
			if(positiontogo.x == 0 && positiontogo.y ==0 && positiontogo.z == 0){
				positiontogo = transform.position;
			}
			mytile = LevelBuilder.tiles[Mathf.RoundToInt(positiontogo.x), -Mathf.RoundToInt(positiontogo.z)];
			mytile.type = "Ice";
			mytile.portalType = null;
			LevelManager.placedPieces[Mathf.RoundToInt(positiontogo.x), -Mathf.RoundToInt(positiontogo.z)] = null;
			mytile.isTaken = false;
			if(mytile.isSideways != null){
				mytile.type = mytile.isSideways;
				mytile.isTaken = false;
			}
			if(myType == "Left" || myType == "Right" ||myType == "Up" ||myType == "Down"){
				removeIcarus(myType, new Vector3((int)gameObject.transform.position.x,(int)gameObject.transform.position.y,(int)gameObject.transform.position.z));
			}					
			GetComponent<BoxCollider>().enabled = false;
		}
	}

  	public IEnumerator HintMove(Vector3 postogo){ 
  		Swiping.mydirection = "Null";
  		OnMouseDown();
  		Vector3 initialPosition = transform.position;
  		initialPosition.y = .23f;
  		postogo.y = .23f;
  		Vector3 direction = postogo-initialPosition;
  		float distance = Vector3.Distance(postogo, initialPosition);
  		float fadetime = .3f*distance;
  		PlaneBehavior.RoboRay(initialPosition);
        for(float t = 0.0f; t<fadetime; t+= Time.deltaTime){
        	float normalizedTime = t/fadetime;
            PlaneBehavior.simulatedMouse = new Vector3(initialPosition.x+direction.x*Mathf.Lerp(0,1, normalizedTime),.23f,initialPosition.z+direction.z*Mathf.Lerp(0,1, normalizedTime));
            OnMouseDrag();
            yield return null;
        }
		PlaneBehavior.simulatedMouse = postogo;
        OnMouseDrag();
        yield return new WaitForSeconds(.1f);
        OnMouseUp();
        Vector3 position = new Vector2 (postogo.x, -postogo.z);
		if(myType == "Wall" || myType == "Seed" || myType == "Portal"){
			pieceHolder.placeNormal(position, this);
		}
		if(myType == "Left" || myType == "Up" || myType == "Down" || myType == "Right"){
			pieceHolder.placeIcarus(position,this);
		}
        startedDragging = false;
        PieceHolders.hinting = false;
        for(int i=0; i<PieceHolders.placedpieces.Count; i++){
        	PieceHolders.placedpieces[i].gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        #if UNITY_ANDROID
        GoogleAds.Instance.RequestHintAd();
        #endif
        yield break;
	}

	public void OnMouseDrag()
	{ 
		if ((TurnBehaviour.turn == 0 || LevelBuilder.resetting) & !LevelManager.configging) {
			Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 15);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;
			positiontogo = PlaneBehavior.planePos;	
			piecePosition = new Vector3(PlaneBehavior.planePos.x, PlaneBehavior.planePos.y, PlaneBehavior.planePos.z);
			positiontogo = new Vector3(Mathf.RoundToInt(PlaneBehavior.planePos.x), PlaneBehavior.planePos.y, Mathf.RoundToInt(PlaneBehavior.planePos.z));	
			if(lastposition != positiontogo || currenttile == null){
				CheckAvailableTile(positiontogo);
			}	
			lastposition = positiontogo;
			transform.position = piecePosition;
			myPosition = transform.position;
			if(Input.touchCount>0){
				Touch t = Input.GetTouch(0);
				Swiping.firstPressPos = new Vector2(t.position.x,t.position.y);
			}
			else{
				Swiping.firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			}
		}
	}

	void placeNormal(string t, string sT){
		if(t != "Seed"){
		LevelManager.placedPieces[(int)positiontogo.x,-(int)positiontogo.z] = t;

		}
		else
		LevelManager.placedPieces[(int)positiontogo.x,-(int)positiontogo.z] = sT;


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
		LevelBuilder.tiles[(int)positiontogo.x, -(int)positiontogo.z].portalType = portalType;
	}

	void CheckAvailableTile(Vector3 position){
		Collider[] colliders = Physics.OverlapSphere(new Vector3(position.x,0,position.z), .5f);	
		foreach (Collider component in colliders) {
			if(component.tag == "Tile"){
				currenttile = component.gameObject;			
			}
		}
		if(position.x>-1 && position.x<LevelBuilder.totaldimension && position.z<1 && position.z>-LevelBuilder.totaldimension){
			isinboard = true;
			if(pasttile != null){
				pasttile.GetComponent<MouseOverer>().Leave();
			}
			if(currenttile!= null && currenttile != pasttile){
				currenttile.GetComponent<MouseOverer>().Enter();
				if(PlaneBehavior.readyToDrop){
					lastgoodtile = currenttile;	
					SfxHandler.Instance.Drag();
				}	
			}
			pasttile = currenttile;
			particle.SetActive(true);
			particle.transform.position = new Vector3(lastgoodtile.transform.position.x, particle.transform.position.y, lastgoodtile.transform.position.z);//change this
			if(myType == "Wall" || myType == "Up" ||myType == "Down" ||myType == "Right" ||myType == "Left" || myType == "Portal" ){
				this.gameObject.GetComponent<Animator>().SetInteger("Phase", 1);
			}
			if(LevelBuilder.tiles[(int)position.x, -(int)position.z].isTaken){
				PlaneBehavior.ClosestTile();
				lastgoodtile = PlaneBehavior.highlightedTile;	
				particle.transform.position = new Vector3(lastgoodtile.transform.position.x, particle.transform.position.y, lastgoodtile.transform.position.z);//change this
				particle.transform.GetChild(0).gameObject.SetActive(true);
			}
			else{
				particle.transform.GetChild(0).gameObject.SetActive(true);
			}
		}
		else{
			if(myType == "Wall" || myType == "Up" ||myType == "Down" ||myType == "Right" ||myType == "Left" ){
				//this.gameObject.GetComponent<Animator>().SetInteger("Phase", 2);
			}
			isinboard = false;
			particle.transform.GetChild(0).gameObject.SetActive(true);;
			if(pasttile!= null){
				pasttile.GetComponent<MouseOverer>().Leave();
			}
			currenttile = null;
			pasttile = null;
			PlaneBehavior.ClosestTile();
			lastgoodtile = PlaneBehavior.highlightedTile;
			if(particle.transform.position.x != lastgoodtile.transform.position.x || particle.transform.position.z != lastgoodtile.transform.position.z ){
				SfxHandler.Instance.Drag();
				particle.transform.position = new Vector3(lastgoodtile.transform.position.x, particle.transform.position.y, lastgoodtile.transform.position.z);//change this
			}
		}
	}

	void OnMouseUp()
	{
		Swiping.mydirection = "Null";
		string pieceholdername;
		if(particle != null){
			particle.transform.GetChild(0).gameObject.SetActive(false);
		}
		toggleColliders();
		Cursor.visible = true;
		if(TurnBehaviour.turn == 0 && !LevelManager.configging)
		{
			SfxHandler.Instance.Drop();
			Swiping.canswipe = true;
			LevelManager.isdragging = false;
			GetComponent<BoxCollider>().enabled = true;
			if(currenttile  == null){
				positiontogo = PlaneBehavior.planePos;	

				positiontogo = new Vector3(Mathf.RoundToInt(PlaneBehavior.planePos.x), PlaneBehavior.planePos.y, Mathf.RoundToInt(PlaneBehavior.planePos.z));	
				Collider[] colliders = Physics.OverlapSphere(new Vector3(positiontogo.x,0,positiontogo.z), .5f);	
				foreach (Collider component in colliders) {
					currenttile = component.gameObject;
				}
			}
			if(PlaneBehavior.readyToDrop){//from mouseoverer
				positiontogo = new Vector3(PlaneBehavior.tilex, 0, PlaneBehavior.tiley);
				gototile = true;
				if(myType == "Left" || myType == "Right" ||myType == "Up" ||myType == "Down"){
				placeIcarus(myType, positiontogo);

				}
				else{
					placeNormal(myType, mySeedType);			
				}
				
				if(myType == "Wall" || myType == "Portal"){
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
				positiontogo = new Vector3(lastgoodtile.transform.position.x, 0, lastgoodtile.transform.position.z);
				gototile = true;
				if(myType == "Left" || myType == "Right" ||myType == "Up" ||myType == "Down"){
				placeIcarus(myType, positiontogo);
				}
				else{
					placeNormal(myType, mySeedType);			
				}
				if(myType == "Wall" || myType == "Portal"){
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
		}
		if(Input.touchCount>0){
			Touch t = Input.GetTouch(0);
			Swiping.firstPressPos = new Vector2(t.position.x,t.position.y);

		}
		else{
			Swiping.firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		}
		currenttile = null;
		pasttile = null;
	}
	
	public void placeIcarus(string type, Vector3 target){
		Vector2 smallPos = new Vector2(target.x, -target.z);
		LevelManager.placedPieces[(int)smallPos.x,(int)smallPos.y] = type;
		if(type == "Left"){
			LevelBuilder.tiles[(int)target.x, -(int)target.z].type = "Wall";
			LevelBuilder.tiles[(int)target.x, -(int)target.z].isTaken = true;
			LevelBuilder.tiles[(int)target.x, -(int)target.z].tileObj = this.gameObject;
			if(LevelBuilder.tiles[(int)target.x-1, -(int)target.z].type == "Ice"){
				LevelBuilder.tiles[(int)target.x-1, -(int)target.z].type = type;		
			}	
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
			}	
			else{
				LevelBuilder.tiles[(int)target.x, -(int)target.z+1].isSideways = "Down";
			}
		}
	}

	public void removeIcarus(string type, Vector3 target){
		if(type == "Left"){
			if(LevelBuilder.tiles[(int)target.x-1, -(int)target.z].type == type){
				LevelBuilder.tiles[(int)target.x-1, -(int)target.z].type = "Ice";		
				LevelBuilder.tiles[(int)target.x-1, -(int)target.z].isSideways = null;					
			}
			else if(LevelBuilder.tiles[(int)target.x-1, -(int)target.z].isSideways == type){
				LevelBuilder.tiles[(int)target.x-1, -(int)target.z].isSideways = null;
			}
			KeepAdjacentIcari(new Vector2(target.x-1, target.z));	
		} 
		if(type =="Right"){
			if(LevelBuilder.tiles[(int)target.x+1, -(int)target.z].type == type){
				LevelBuilder.tiles[(int)target.x+1, -(int)target.z].type = "Ice";		
				LevelBuilder.tiles[(int)target.x+1, -(int)target.z].isSideways = null;									
			}		
			else if(LevelBuilder.tiles[(int)target.x+1, -(int)target.z].isSideways == type){
				LevelBuilder.tiles[(int)target.x+1, -(int)target.z].isSideways = null;
			}
			KeepAdjacentIcari(new Vector2(target.x+1, target.z));
		}
		if(type == "Up" ){	
			if(LevelBuilder.tiles[(int)target.x, -(int)target.z-1].type == type){
				LevelBuilder.tiles[(int)target.x, -(int)target.z-1].type = "Ice";		
				LevelBuilder.tiles[(int)target.x, -(int)target.z-1].isSideways = null;	
			}	
			else if(LevelBuilder.tiles[(int)target.x, -(int)target.z-1].isSideways == type){
				LevelBuilder.tiles[(int)target.x, -(int)target.z-1].isSideways = null;
			}	
			KeepAdjacentIcari(new Vector2(target.x, target.z+1));				
		}
		if(type == "Down"){
			if(LevelBuilder.tiles[(int)target.x, -(int)target.z+1].type == type){
				LevelBuilder.tiles[(int)target.x, -(int)target.z+1].type = "Ice";		
				LevelBuilder.tiles[(int)target.x, -(int)target.z+1].isSideways = null;	
			}
			else if(LevelBuilder.tiles[(int)target.x, -(int)target.z+1].isSideways == type){
				LevelBuilder.tiles[(int)target.x, -(int)target.z+1].isSideways = null;				
			}		
			KeepAdjacentIcari(new Vector2(target.x, target.z-1));
		}
	}

	private void KeepAdjacentIcari(Vector2 target){
		for(int i=0; i<PieceHolders.placedpieces.Count; i++){
			Dragger curDragger = PieceHolders.placedpieces[i];
			Vector2 draggerPost = new Vector2(curDragger.transform.position.x, curDragger.transform.position.z);
			string direction = null;
			if(curDragger!=this){
				string curtype = curDragger.myType;
				if(curtype == "Left" && draggerPost.x == target.x+1 &&
					draggerPost.y == target.y){
					direction = "Left";
				}
				if(curtype == "Right" && draggerPost.x == target.x-1 &&
					draggerPost.y == target.y){
					direction = "Right";
				}
				if(curtype == "Up" && draggerPost.x == target.x &&
					draggerPost.y == target.y-1){
					direction = "Up";
				}
				if(curtype == "Down" && draggerPost.x == target.x &&
					draggerPost.y == target.y+1){
					direction = "Down";
				}	
				if(direction != null){
					if(LevelBuilder.tiles[(int)target.x, -(int)target.y].type == "Ice"){
						LevelBuilder.tiles[(int)target.x, -(int)target.y].type = direction;
					}
					else{
						LevelBuilder.tiles[(int)target.x, -(int)target.y].isSideways = direction;
					}					
				}
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

	public void ClosestTile(Vector3 position){
		int currentx = (int)position.x;
		int currenty = (int)-position.z;		
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
	}
}