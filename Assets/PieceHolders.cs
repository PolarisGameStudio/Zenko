using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Holder{
	public string name;
	public int maxpieces;
	public int currentpieces;
	public GameObject mygameobject;
	public bool active;

	public Holder(string newname, int newmaxpieces, int newcurrentpieces,
		GameObject newgameobject, bool isactive){
		name = newname;
		maxpieces = newmaxpieces;
		currentpieces = newcurrentpieces;
		mygameobject = newgameobject;
		active = isactive;

	}

}
public class PieceHolders : MonoBehaviour {
	public List<Holder> holders = new List<Holder>();
	public int holdersNumber;
	public GameObject pedroHolder;
	public GameObject leftHolder;
	public GameObject rightHolder;
	public GameObject upHolder;
	public GameObject downHolder;
	public GameObject wallSeedHolder;
	public GameObject leftSeedHolder;
	public GameObject rightSeedHolder;
	public GameObject upSeedHolder;
	public GameObject downSeedHolder;
	public int pedroNumber;
	public int leftNumber;
	public int rightNumber;
	public int upNumber;
	public int downNumber;
	public int wallSeedNumber;
	public int leftSeedNumber;
	public int rightSeedNumber;
	public int upSeedNumber;
	public int downSeedNumber;
	public static List<Dragger> placedpieces = new List<Dragger>();
	// Use this for initialization
	void Awake () {
		initValues();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void initSize(){
		RectTransform rt = GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(holdersNumber*150, 150);
	}
	public void reset(){
		pedroHolder.SetActive(false);
		leftHolder.SetActive(false);
		rightHolder.SetActive(false);
		upHolder.SetActive(false);
		downHolder.SetActive(false);
		wallSeedHolder.SetActive(false);
		initValues();
		holders = new List<Holder>();
	}
	public void initValues(){
	pedroNumber = 0;
	leftNumber = 0;
	rightNumber = 0;
	upNumber = 0;
	downNumber = 0;
	holdersNumber = 0;
	wallSeedNumber = 0;
	pedroHolder.GetComponent<Image>().color = Color.white;
	leftHolder.GetComponent<Image>().color = Color.white;
	upHolder.GetComponent<Image>().color = Color.white;
	rightHolder.GetComponent<Image>().color = Color.white;
	downHolder.GetComponent<Image>().color = Color.white;
	wallSeedHolder.GetComponent<Image>().color = Color.white;
	}
	public void InitHolder(string type){
		if(type == "Pedro" || type ==  "Wall"){
			holders.Add(new Holder(type,1,1,pedroHolder,true));
		}
		if(type == "Left"){
			holders.Add(new Holder(type,1,1,leftHolder,true));
		}
		if(type == "Right"){
			holders.Add(new Holder(type,1,1,rightHolder,true));
		}
		if(type == "Up"){
			holders.Add(new Holder(type,1,1,upHolder,true));
		}
		if(type == "Down"){
			holders.Add(new Holder(type,1,1,downHolder,true));
		}
		if(type == "WallSeed"){
			holders.Add(new Holder(type,1,1,wallSeedHolder,true));
		}
	}
	public void AddtoHolder(string type){
		//Debug.Log("Test");
		Holder currentholder= holders.Find(myItem => myItem.name == type);
		currentholder.maxpieces++;
		currentholder.currentpieces++;
//		Debug.Log(currentholder.name);

	}
	public void SubstractToHolder(string type){
		//Debug.Log("Test");
		Holder currentholder= holders.Find(myItem => myItem.name == type);
		currentholder.currentpieces--;
		//currentholder.
//		Debug.Log(currentholder.name);

	}
	public void AddPiece(string type){
		if(type == "Pedro" || type == "Wall"){
			if(pedroHolder.active){
				pedroNumber++;
				AddtoHolder(type);
			}
			else{
				InitHolder(type);
				pedroHolder.SetActive(true);
				holdersNumber++;
				pedroNumber++;
				initSize();
			}			
		}
		if(type == "Up"){
			if(upHolder.active){
				upNumber++;
				AddtoHolder(type);
			}
			else{
				InitHolder(type);
				upHolder.SetActive(true);
				holdersNumber++;
				upNumber++;
				initSize();
			}			
		}
		if(type == "Down"){
			if(downHolder.active){
				downNumber++;
				AddtoHolder(type);
			}
			else{
				InitHolder(type);
				downHolder.SetActive(true);
				holdersNumber++;
				downNumber++;
				initSize();
			}			
		}
		if(type == "Left"){
			if(leftHolder.active){
				leftNumber++;
				AddtoHolder(type);
			}
			else{
				InitHolder(type);
				leftHolder.SetActive(true);
				holdersNumber++;
				leftNumber++;
				initSize();
			}			
		}
		if(type == "Right"){
			if(rightHolder.active){
				rightNumber++;
				AddtoHolder(type);
			}
			else{
				InitHolder(type);
				rightHolder.SetActive(true);
				holdersNumber++;
				rightNumber++;
				initSize();
			}			
		}
		if(type == "WallSeed"){
			if(wallSeedHolder.active){
				wallSeedNumber++;
				AddtoHolder(type);
			}
			else{
				InitHolder(type);
				wallSeedHolder.SetActive(true);
				holdersNumber++;
				wallSeedNumber++;
				initSize();
			}			
		}
		updateText(type);
	}
	public void updateValueUp(string type){
		if(type == "Pedro" || type == "Wall"){
			pedroNumber ++;
			pedroHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Wall x" + pedroNumber;
			pedroHolder.GetComponent<Image>().color = Color.white;

		}
		if(type == "Up"){
			upNumber++;
			upHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Up x" + upNumber;
			upHolder.GetComponent<Image>().color = Color.white;
		}
		if(type == "Down"){
			downNumber++;
			downHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Down x" + downNumber;
			downHolder.GetComponent<Image>().color = Color.white;
		}
		if(type == "Left"){
			leftNumber++;
			leftHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Left x" + leftNumber;
			leftHolder.GetComponent<Image>().color = Color.white;
		}
		if(type == "Right"){
			rightNumber++;
			rightHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Right x" + rightNumber;
			rightHolder.GetComponent<Image>().color = Color.white;
		}		
		if(type == "WallSeed"){
			wallSeedNumber++;
			wallSeedHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "WallSeed x" + wallSeedNumber;
			wallSeedHolder.GetComponent<Image>().color = Color.white;
		}	
	}
	public void unshadeImage(string type){
		if(type == "Pedro" || type == "Wall"){
			Color tempColor = pedroHolder.GetComponent<Image>().color;
			tempColor.a = 1f;
			pedroHolder.GetComponent<Image>().color = tempColor;
		}
		if(type == "Up"){
			Color tempColor = upHolder.GetComponent<Image>().color;
			tempColor.a = 1f;
			upHolder.GetComponent<Image>().color = tempColor;
		}
		if(type == "Down"){
			Color tempColor = downHolder.GetComponent<Image>().color;
			tempColor.a = 1f;
			downHolder.GetComponent<Image>().color = tempColor;
		}
		if(type == "Left"){
			Color tempColor = leftHolder.GetComponent<Image>().color;
			tempColor.a = 1f;
			leftHolder.GetComponent<Image>().color = tempColor;
		}
		if(type == "Right"){
			Color tempColor = rightHolder.GetComponent<Image>().color;
			tempColor.a = 1f;
			rightHolder.GetComponent<Image>().color = tempColor;
		}	
		if(type == "WallSeed"){
			Color tempColor = rightHolder.GetComponent<Image>().color;
			tempColor.a = 1f;
			wallSeedHolder.GetComponent<Image>().color = tempColor;
		}					
	}
	public bool isAvailable(string type){
		if(type == "Pedro" || type == "Wall"){
			if(pedroNumber!=0){
				return true;
			}
			else{
				return false;
			}
		}
		if(type == "Left"){
			if(leftNumber!=0){
				return true;
			}
			else{
				return false;
			}
		}
		if(type == "Right"){
			if(rightNumber!=0){
				return true;
			}
			else{
				return false;
			}
		}
		if(type == "Down"){
			if(downNumber!=0){
				return true;
			}
			else{
				return false;
			}
		}
		if(type == "Up"){
			if(upNumber!=0){
				return true;
			}
			else{
				return false;
			}
		}
		if(type == "WallSeed"){
			if(wallSeedNumber!=0){
				return true;
			}
			else{
				return false;
			}
		}
		else return false;
	}
	public void updateValueDown(string type){
		if(type == "Pedro" || type == "Wall"){
			pedroNumber --;
			pedroHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Pedro x" + pedroNumber;
			if(pedroNumber == 0){
				pedroHolder.GetComponent<Image>().color = Color.gray;
			}

		}
		if(type == "Up"){
			upNumber--;
			upHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Up x" + upNumber;
			if(upNumber == 0){
				upHolder.GetComponent<Image>().color = Color.gray;
			}
		}
		if(type == "Down"){
			downNumber--;
			downHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Down x" + downNumber;
			if(downNumber == 0){
				downHolder.GetComponent<Image>().color = Color.gray;
			}
		}
		if(type == "Left"){
			leftNumber--;
			leftHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Left x" + leftNumber;
			if(leftNumber == 0){
				leftHolder.GetComponent<Image>().color = Color.gray;
			}
		}
		if(type == "Right"){
			rightNumber--;
			rightHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Right x" + rightNumber;
			if(rightNumber == 0){
				rightHolder.GetComponent<Image>().color = Color.gray;
			}
		}	
		if(type == "WallSeed"){
			wallSeedNumber--;
			wallSeedHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "WallSeed x" + wallSeedNumber;
			if(wallSeedNumber == 0){
				wallSeedHolder.GetComponent<Image>().color = Color.gray;
			}
		}		
	}	
	public void updateText(string type){
		if(type == "Pedro" || type == "Wall"){
			pedroHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Wall x" + pedroNumber;
		}
		if(type == "Up"){
			upHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Up x" + upNumber;
		}
		if(type == "Down"){
			downHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Down x" + downNumber;
		}
		if(type == "Left"){
			leftHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Left x" + leftNumber;
		}
		if(type == "Right"){
			rightHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Right x" + rightNumber;
		}
		if(type == "WallSeed"){
			wallSeedHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "WallSeed x" + rightNumber;
		}
	}
	public void Hint(){//right now works for one stored solution.
		Debug.Log(placedpieces.Count);
		Vector2 postogo;
		GameObject piece;
		if(TurnBehaviour.turn == 0){
			if(placedpieces.Count!=0){ //if any pieces are on board.
				for(int i = 0; i < placedpieces.Count; i++){//first pass, to confirm the ones in right place
					//Debug.Log(placedpieces[i].myType + "" + placedpieces[i].transform.position.x + "" + -placedpieces[i].transform.position.z);
					if(PartofSolution(i)){//if placed in a correct solution, this is to find if any piece is in the right place.
					//filter out solutions without this piece
						Debug.Log("Piece in the right place");
						placedpieces[i].transform.GetChild(1).GetComponent<Renderer>().material.color = Color.green;
					}

				}
				for(int i = 0; i < placedpieces.Count; i++){//second pass, to correct the first one found in wrong place.
					if(!PartofSolution(i)){
						removePiece(new Vector2(placedpieces[i].transform.position.x, -placedpieces[i].transform.position.z), placedpieces[i].myType);
						//LookforPosition(placedpieces[i].myType);
						 postogo = LookforPosition(placedpieces[i].myType);
						if(postogo != new Vector2(0,0)){
							PlaceHint(placedpieces[i], postogo);
							return;

						}
					}
				}
				for(int i=0; i < holders.Count; i++){//if none on board can be moved to a good one (because a wrong is there), look through holders
					if(holders[i].currentpieces > 0){
						postogo = LookforPosition(holders[i].name);
						//Debug.Log("Instantiate a "+ holders[i].name);
						if(postogo!= new Vector2(0,0)){
							 piece = holders[i].mygameobject.GetComponent<UIMonster>().Spawnit();
							PlaceHint(piece.GetComponent<Dragger>(), postogo);
							updateValueDown(holders[i].name);
							return;
						}
					}
				}
			}
			else{//if no pieces placed, ergo all in respective holders.
				for(int i=0; i < holders.Count; i++){
					if(holders[i].currentpieces > 0){
						postogo = LookforPosition(holders[i].name);
						//Debug.Log("Instantiate a "+ holders[i].name);
						if(postogo!= new Vector2(0,0)){
							piece = holders[i].mygameobject.GetComponent<UIMonster>().Spawnit();
							PlaceHint(piece.GetComponent<Dragger>(), postogo);
							updateValueDown(holders[i].name);
							return;
						}

					}
				}
			}			
		}
		
	}
	public void PlaceHint(Dragger dragger, Vector2 position){
		dragger.transform.GetChild(1).gameObject.SetActive(true);

		dragger.transform.position = new Vector3(position.x,0,-position.y);
		if(dragger.myType == "Wall"){
			placeNormal(position, dragger);
		}
		if(dragger.myType == "Left" || dragger.myType == "Up" || dragger.myType == "Down" || dragger.myType == "Right"){
			placeIcarus(position,dragger);
		}
		placedpieces.Add(dragger);
	}
	public bool PartofSolution(int position){
		for (int i = 0; i <LevelManager.hints.Count; i++){
			if(placedpieces[position].myType == LevelManager.hints[i].type && 
				placedpieces[position].transform.position.x == LevelManager.hints[i].x &&
				-placedpieces[position].transform.position.z == LevelManager.hints[i].y){

				return true;

			}
		}
		return false;
	}
	public int CountUnplaced(){
		int amount = 0;
		for (int i = 0; i<holders.Count; i++){
			amount = amount+holders[i].currentpieces;
		}
		return amount;
	}
	public void CountPlaced(){
		int amount = 0;
		for (int i=0; i<holders.Count; i++){

		}
	}
	public Vector2 LookforPosition(string type){
		for(int i =0; i<LevelManager.hints.Count; i++){
			if(type == LevelManager.hints[i].type){
				if(!Occupied(LevelManager.hints[i])){
					return new Vector2(LevelManager.hints[i].x, LevelManager.hints[i].y);
				}
			}
		}
		return new Vector2(0,0);
	}
	public bool Occupied(Hint hint){
		return LevelBuilder.tiles[hint.x,hint.y].isTaken;
		//return false;
	}

 	public void placeNormal(Vector2 positiontogo, Dragger td){
	 	if(LevelBuilder.tiles[(int)positiontogo.x, (int)positiontogo.y].type == "Left" || 
	 	LevelBuilder.tiles[(int)positiontogo.x, (int)positiontogo.y].type == "Down" ||
	 	LevelBuilder.tiles[(int)positiontogo.x, (int)positiontogo.y].type == "Up" ||
	 	LevelBuilder.tiles[(int)positiontogo.x, (int)positiontogo.y].type == "Right"){
	 		
	 		LevelBuilder.tiles[(int)positiontogo.x, (int)positiontogo.y].isSideways = LevelBuilder.tiles[(int)positiontogo.x, (int)positiontogo.y].type;
 		
 		}
//		Debug.Log(td.myType);
		LevelBuilder.tiles[(int)positiontogo.x, (int)positiontogo.y].type = td.myType;
		LevelBuilder.tiles[(int)positiontogo.x, (int)positiontogo.y].isTaken = true;
		LevelBuilder.tiles[(int)positiontogo.x, (int)positiontogo.y].tileObj = td.gameObject;	
		
	}
	public void placeIcarus(Vector2 target, Dragger td){
		if(td.myType == "Left"){
			LevelBuilder.tiles[(int)target.x, (int)target.y].type = "Wall";
			LevelBuilder.tiles[(int)target.x, (int)target.y].isTaken = true;
			LevelBuilder.tiles[(int)target.x, (int)target.y].tileObj = td.gameObject;
			if(LevelBuilder.tiles[(int)target.x-1, (int)target.y].type == "Ice"){
				LevelBuilder.tiles[(int)target.x-1, (int)target.y].type = td.myType;		
			}	

			else{
				LevelBuilder.tiles[(int)target.x-1, (int)target.y].isSideways = "Left";
			}
		

		} 
		if(td.myType =="Right"){
			LevelBuilder.tiles[(int)target.x, (int)target.y].type = "Wall";
			LevelBuilder.tiles[(int)target.x, (int)target.y].isTaken = true;
			LevelBuilder.tiles[(int)target.x, (int)target.y].tileObj = td.gameObject;
			if(LevelBuilder.tiles[(int)target.x+1, (int)target.y].type == "Ice"){
				LevelBuilder.tiles[(int)target.x+1, (int)target.y].type = td.myType;		
			}	
			else{
				LevelBuilder.tiles[(int)target.x+1, (int)target.y].isSideways = "Right";
			}	

		}
		if(td.myType == "Up" ){
			LevelBuilder.tiles[(int)target.x, (int)target.y].type = "Wall";
			LevelBuilder.tiles[(int)target.x, (int)target.y].isTaken = true;
			LevelBuilder.tiles[(int)target.x, (int)target.y].tileObj = td.gameObject;	
			if(LevelBuilder.tiles[(int)target.x, (int)target.y-1].type == "Ice"){
				LevelBuilder.tiles[(int)target.x, (int)target.y-1].type = td.myType;		
			}	
			else{
				LevelBuilder.tiles[(int)target.x, (int)target.y-1].isSideways = "Up";
			}
		}
		if(td.myType == "Down"){
			LevelBuilder.tiles[(int)target.x, (int)target.y].type = "Wall";
			LevelBuilder.tiles[(int)target.x, (int)target.y].isTaken = true;
			LevelBuilder.tiles[(int)target.x, (int)target.y].tileObj = td.gameObject;	
			if(LevelBuilder.tiles[(int)target.x, (int)target.y+1].type == "Ice"){
				LevelBuilder.tiles[(int)target.x, (int)target.y+1].type = td.myType;		
			}	
			else{
				LevelBuilder.tiles[(int)target.x, (int)target.y+1].isSideways = "Down";
			}
		}
	}
	public void removePiece(Vector2 target, string type){
		if(type == "Wall"){
			LevelBuilder.tiles[(int)target.x, (int)target.y].type = "Ice";
			LevelBuilder.tiles[(int)target.x, (int)target.y].isTaken = false;	
			if(LevelBuilder.tiles[(int)target.x, (int)target.y].isSideways != null){
				LevelBuilder.tiles[(int)target.x, (int)target.y].type = LevelBuilder.tiles[(int)target.x, (int)target.y].isSideways;
			}			
		}


		if(type == "Left" || type == "Right" || type == "Down" || type == "Up" )	{
			removeIcarus(type, target);
		}
		if(type == "Seed"){
			LevelBuilder.tiles[(int)target.x, (int)target.y].type = "Ice";
			LevelBuilder.tiles[(int)target.x, (int)target.y].isTaken = false;	
			if(LevelBuilder.tiles[(int)target.x, (int)target.y].isSideways != null){
				LevelBuilder.tiles[(int)target.x, (int)target.y].type = LevelBuilder.tiles[(int)target.x, (int)target.y].isSideways;
			}				
		}
	}
	public void removeIcarus(string type, Vector3 target){
		Debug.Log(type);
		LevelBuilder.tiles[(int)target.x, (int)target.y].type = "Ice";
		LevelBuilder.tiles[(int)target.x, (int)target.y].isTaken = false;	
		if(LevelBuilder.tiles[(int)target.x, (int)target.y].isSideways != null){
				LevelBuilder.tiles[(int)target.x, (int)target.y].type = LevelBuilder.tiles[(int)target.x, -(int)target.z].isSideways;
		}	

		if(type == "Left"){
			if(LevelBuilder.tiles[(int)target.x-1, (int)target.y].type == type){
				LevelBuilder.tiles[(int)target.x-1, (int)target.y].type = "Ice";		
				LevelBuilder.tiles[(int)target.x-1, (int)target.y].isTaken = false;	
				LevelBuilder.tiles[(int)target.x-1, (int)target.y].isSideways = null;					
			}
			else if(LevelBuilder.tiles[(int)target.x-1, (int)target.y].isSideways == type){
				LevelBuilder.tiles[(int)target.x-1, (int)target.y].isSideways = null;

			}


		} 
		if(type =="Right"){
			if(LevelBuilder.tiles[(int)target.x+1, (int)target.y].type == type){
				LevelBuilder.tiles[(int)target.x+1, (int)target.y].type = "Ice";		
				LevelBuilder.tiles[(int)target.x+1, (int)target.y].isTaken = false;	
				LevelBuilder.tiles[(int)target.x+1, (int)target.y].isSideways = null;					
			}		
			else if(LevelBuilder.tiles[(int)target.x+1, (int)target.z].isSideways == type){
				LevelBuilder.tiles[(int)target.x+1, (int)target.z].isSideways = null;

			}
		}
		if(type == "Up" ){	
			if(LevelBuilder.tiles[(int)target.x, (int)target.y+1].type == type){
				LevelBuilder.tiles[(int)target.x, (int)target.y+1].type = "Ice";		
				LevelBuilder.tiles[(int)target.x, (int)target.y+1].isTaken = false;	
				LevelBuilder.tiles[(int)target.x, (int)target.y+1].isSideways = null;					
			}	
			else if(LevelBuilder.tiles[(int)target.x, (int)target.y+1].isSideways == type){
				LevelBuilder.tiles[(int)target.x, (int)target.y+1].isSideways = null;

			}	
		}
		if(type == "Down"){
			if(LevelBuilder.tiles[(int)target.x, (int)target.y-1].type == type){
				LevelBuilder.tiles[(int)target.x, (int)target.y-1].type = "Ice";		
				LevelBuilder.tiles[(int)target.x, (int)target.y-1].isTaken = false;	
				LevelBuilder.tiles[(int)target.x, (int)target.y-1].isSideways = null;					
			}
			else if(LevelBuilder.tiles[(int)target.x, (int)target.y-1].isSideways == type){
				LevelBuilder.tiles[(int)target.x, (int)target.y-1].isSideways = null;

			}		
		}
	}
}
