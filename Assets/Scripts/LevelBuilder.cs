using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LevelBuilder : MonoBehaviour {
	Vector2 xy;
	public static Material background;
	public GameObject[] tile;
	Transform[] tileBank = new Transform[100];
	public static int rows = 10;
	public static int cols = 10;
	public static int totaldimension = 10;
	bool renewBoard = false;
	public static Tile [,] tiles = new Tile[10,10];
	public static List<Vector2> iceTiles = new List<Vector2>();
	static int levelidtoload;
	static Vector2 startpos;
	public static Transform playertransform;
	public static Transform starttransform;
	public static Transform goaltransform;
	public static bool iscreated;
	public static string[] levelsHorizontal;
	public static int[] startersHorizonal;
	List<GameObject> outertileBank = new List<GameObject>();
	int howfar;
	public Color mygray;
	public static int playerInitialRotation;
	public static bool resetting;
	public GameObject mywinboard;
	public static GameObject winboard;
	public GameObject myloseboard;
	public static GameObject loseboard;	
	public GameObject myhintboard;
	public static GameObject hintboard;	
	public static GameObject settingsBoard;
	public static LevelBuilder Instance;
	public static List<GameObject> Portals = new List<GameObject>();
	public GameObject loadingGO;
	public Texture portalTexture2;
	int wallIndex;
	int holeIndex;
	int flowerIndex;
	int snowIndex;
	int wallOnlyIndex;
	int fragileIndex;
	int snowEnvIndex;
	int iceEnvIndex;
	int environmentRockIndex;
	int environmentFlowerIndex;
	int environmentTreeIndex;
	Vector3 positionOutsidePlayer;
	public Transform smoke_particle;
	public Transform player;
	public Transform floor_ice;
	public Transform floor_wall;
	public Transform floor_snow;
	public Transform environment_ice;
	public List<Transform> floor_rocks;
	public List<Transform> floor_trees;
	public Transform floor_rock;
	public Transform floor_start;
	public Transform floor_goal;
	public Transform floor_hole;
	public Transform floor_wood;
	public Transform floor_left;
	public Transform floor_right;
	public Transform floor_up;
	public Transform floor_down;
	public Transform floor_fragile;
	public Transform floor_quicksand;
	public Transform floor_boss;
	public Transform floor_portal;
	public Transform s_floor_left;
	public Transform s_floor_right;
	public Transform s_floor_up;
	public Transform s_floor_down;
	public Transform s_floor_rock;
	public static int levelnum;
	public const string sfloor_ice = "0";
	public const string sfloor_wall = "1";
	public const string sfloor_rock = "P";
	public const string sstart = "S";
	public const string sgoal = "G";
	public const string sfloor_hole = "H";
	public const string sfloor_wood = "#";
	public const string sfloor_left = "L";
	public const string sfloor_right = "R";
	public const string sfloor_up = "U";
	public const string sfloor_down = "D";
	public const string sfloor_fragile = "F";
	public const string sfloor_quicksand = "Q";
	public const string sfloor_boss = "B";
	public const string ssfloor_left = "l";
	public const string ssfloor_right = "r";
	public const string ssfloor_up = "u";
	public const string ssfloor_down = "d";
	public const string ssfloor_rock = "p";
	public const string sfloor_portalleft = "PL";
	public const string sfloor_portalright = "PR";
	public const string sfloor_portalup = "PU";
	public const string sfloor_portaldown = "PD";
	float mysnowh;
	int piecenums;
	public GameObject pieceHolderHolder;
	PieceHolders pieceHolder;

	public void Awake(){
		Instance = this;
		AssignBoards();
	}

	void Start() {
		InitializeTileIndex();
		Swiping.mydirection = "Null";
		pieceHolder = pieceHolderHolder.GetComponent<PieceHolders>();
		LevelManager.levelselector = this;
		if (LevelManager.levelnum == null || LevelManager.levelnum == 0) {
			LevelManager.levelnum = 1;
			levelnum =1;
		}
		else{
			levelnum = LevelManager.levelnum;
		}
		DrawFirstMap();
	}

	//Assigns references to UI boards
	void AssignBoards(){
		winboard = mywinboard;
		loseboard = myloseboard;
		hintboard = myhintboard;
	}

	//These are references to the index in list for using assets from TileKeeper
	void InitializeTileIndex(){
		wallIndex = 0;
		holeIndex = 0;
		flowerIndex = 0;
		snowIndex = 0;
		wallOnlyIndex = 0;
		fragileIndex = 0;
		snowEnvIndex = 0;
		iceEnvIndex = 0;
		environmentRockIndex = 0;
		environmentFlowerIndex = 0;
		environmentTreeIndex = 0;
	}

	void DrawFirstMap(){
		if(LevelManager.ispotd){
			drawPotd(PlayerPrefs.GetInt("PoTD"));
		}
		else{
			drawNormal(LevelManager.levelnum);
		}
	}

	public void drawNormal(int levelnumber){
		InitializeTileIndex();
		TutorialHandler.Instance.TutorialCheck(levelnumber);
		pieceHolder.reset();
		InitializeHintsAndPieces();
		string leveltext = ("Level" + levelnumber.ToString() + ".txt");
		string levelname = ("Level" + levelnumber.ToString ());
		string[][] jagged = readAdventure (levelnumber);
		tiles = new Tile [totaldimension, totaldimension];
		if(!iscreated){
			CreateBase();
		}
		PlaceBase();
		piecenums = 0;
		for (int y = 0; y < totaldimension; y++) {
			for (int x = 0; x < jagged [y].Length; x++) {
				double zed = (-y) + (-0.8);
				placeOnWorld(jagged,y,x);			
			}
		} 
		GoalDirection((int)goaltransform.position.x,-(int)goaltransform.position.z);
		StartDirection((int)starttransform.position.x,-(int)starttransform.position.z);
		CreateOuterBase();
		for (int y = totaldimension; y < totaldimension+1; y++) {
			for (int x = 0; x < jagged [y].Length; x++) {
				double zed = (-y) + (-0.8);
				placeOnWorld(jagged,y,x);		
			}
		} 
		if(LevelBuilder.totaldimension == 10){
			CameraController.changePosition(1,1);
			CameraController.changeFovAndRot((int)32,52.9f);
		}
		else if (LevelBuilder.totaldimension < 10){
			CameraController.changePosition(0,0);
			CameraController.changeFovAndRot((int)27,52f);
		}
		ProgressBar.InitializeProgressBar(LevelStorer.efficientturns);
		DotHandler.InitializeDots(LevelStorer.efficientturns);
		playertransform.gameObject.GetComponent<PlayerMovement>().canmove = true;
		StartCoroutine(CloseLoading());
	}

	public void drawPotd(int num){
		InitializeTileIndex();
		pieceHolder.reset();
		string[][] jagged = readPotd(num);
		tiles = new Tile [totaldimension, totaldimension];
		if(!iscreated){
			CreateBase ();
		}		
		PlaceBase();
		InitializeHintsAndPieces();
		piecenums = 0;
		for (int y = 0; y < totaldimension; y++) {
			for (int x = 0; x < jagged [y].Length; x++) {
				double zed = (-y) + (-0.8);
				placeOnWorld(jagged,y,x);
			}
		} 
		GoalDirection((int)goaltransform.position.x,-(int)goaltransform.position.z);
		StartDirection((int)starttransform.position.x,-(int)starttransform.position.z);
		for (int y = totaldimension; y < totaldimension+1; y++) {
			for (int x = 0; x < jagged [y].Length; x++) {
				double zed = (-y) + (-0.8);
				placeOnWorld(jagged,y,x);			
			}
		} 
		CreateOuterBase();
		if(LevelBuilder.totaldimension == 10){
			CameraController.changePosition(1,1);
			CameraController.changeFovAndRot((int)32,52.9f);
		}
		else if (LevelBuilder.totaldimension < 10){
			CameraController.changePosition(0,0);
			CameraController.changeFovAndRot((int)27,52f);
		}
		ProgressBar.InitializeProgressBar(LevelStorer.efficientturns);
		DotHandler.InitializeDots(LevelStorer.efficientturns);
		playertransform.gameObject.GetComponent<PlayerMovement>().canmove = true;
		StartCoroutine(CloseLoading());
	}

	void InitializeHintsAndPieces(){
		LevelManager.piecetiles = new List<Transform>();
		LevelManager.myhints = new List<Vector2>();
		LevelManager.hintnum = 0;
		LevelManager.hints = new List<Hint>();
		PieceHolders.placedpieces = new List<Dragger>();
		LevelManager.placedPieces = new string[10,10];
		Portals = new List<GameObject>();
	}

	IEnumerator CloseLoading(){
		//yield return new WaitForSeconds(.1);
		loadingGO.SetActive(false);
		yield return null;
	}
	
	string[][] readAdventure(int place){
		if(place>0)
		place = place-1;
		LevelSaver.currentmap = new List<string>();
		string firstline = MapsHolder.levelsAdventure[MapsHolder.startersAdventure[place]];
		LevelSaver.currentmap.Add(firstline);
		bool donescanning = false;
		int linenumber = 1;
		while(!donescanning){
			if(MapsHolder.levelsAdventure[MapsHolder.startersAdventure[place]+linenumber] == ""){
				donescanning = true;
				totaldimension = linenumber-2;
			}	
			linenumber++;
		}
		string[] lines = new string[totaldimension+1];
		for (int i=0; i<totaldimension+1; i++){
			lines[i] = MapsHolder.levelsAdventure[MapsHolder.startersAdventure[place]+1+i];
			LevelSaver.currentmap.Add(lines[i]);
		}
		LevelSaver.currentmap.Add("");
		string[][] levelBase = new string [totaldimension+1][];
		for (int i = 0; i<totaldimension+1; i++){
			string[] stringsOfLine = Regex.Split (lines [i], " ");
			levelBase [i] = stringsOfLine;
		}
		return levelBase;
	}

	string[][] readPotd(int place){
		LevelSaver.currentmap = new List<string>();
		string firstline = MapsHolder.levelsPotd[MapsHolder.startersPotd[place]];
		LevelSaver.currentmap.Add(firstline);
		totaldimension = int.Parse(firstline.Substring(3,1));
		if(firstline.Length == 5){
			totaldimension = int.Parse(firstline.Substring(3,2));
		}
		string[] lines = new string[totaldimension+1];
		for (int i=0; i<totaldimension+1; i++){
			lines[i] = MapsHolder.levelsPotd[MapsHolder.startersPotd[place]+1+i];
			LevelSaver.currentmap.Add(lines[i]);
		}
		LevelSaver.currentmap.Add("");
		string[][] levelBase = new string [totaldimension+1][];
		for (int i = 0; i<totaldimension+1; i++){
			string[] stringsOfLine = Regex.Split (lines [i], " ");
			levelBase [i] = stringsOfLine;
		}
		return levelBase;
	}

	public void CreateBase(){
		iscreated = true;
		tileBank = TileKeeper.Instance.iceBank;
	}

	public void PlaceBase(){
		for (int r= 0; r< totaldimension; r++){
			for (int c = 0; c<totaldimension; c++){
				Vector3 tilePos = new Vector3(c,0,r);
				for (int n =0; n< tileBank.Length; n++){
					GameObject o = tileBank[n].gameObject;
					if(!o.activeSelf){
						o.transform.position = new Vector3(tilePos.x,tilePos.y,-tilePos.z);
						o.SetActive(true);
						o.transform.GetChild(0).gameObject.SetActive(true);
						tiles[c,r] = new Tile(o, "Ice", false	);
						n = tileBank.Length + 1;
					}
				}
			}
		}
	}

	public void CreateOuterBase(){
		howfar = 4;
		for(int x=1; x<8; x++){
			howfar++;
			for(int y = 0; y<9;y++){
				PlaceRandomEnvironment(new Vector2(-x,y-(totaldimension/2)));
				PlaceRandomEnvironment(new Vector2(x+(totaldimension-1),y-(totaldimension/2)));
				if(y!=0){
					PlaceRandomEnvironment(new Vector2(-x,-y-(totaldimension/2)));
					PlaceRandomEnvironment(new Vector2(x+(totaldimension-1),-y-(totaldimension/2)));					
				}
			}
		}	
		howfar=6;
		for(int x=0;x<totaldimension;x++){
			for(int y=1; y<9; y++){
				PlaceRandomEnvironment(new Vector2(x,y));
				PlaceRandomEnvironment(new Vector2(x,-y-totaldimension+1));
				InstantiateSnow(new Vector2(x,-y-(totaldimension-1)));				
			}
		}
	}

	public void PlaceRandomEnvironment(Vector2 newxy){
		xy = newxy;
		PlaceEnvironment(EnvironmentKeeper.Instance.iceBank[iceEnvIndex]);
		iceEnvIndex++;
		int randomizer = Random.Range(0,10);
		float snowrandom = Random.Range(0f,.04f);
		PlaceSnow();
		snowEnvIndex++;
		if(xy.x == positionOutsidePlayer.x && xy.y == positionOutsidePlayer.y){	
			if (randomizer<4){
				PlaceEnvironment(EnvironmentKeeper.Instance.treeBank[environmentTreeIndex]);
				environmentTreeIndex++;				
			}
			else{
				PlaceEnvironment(EnvironmentKeeper.Instance.rockBank[environmentRockIndex]);
				environmentRockIndex++;	
			}
			return;
		}
		if(randomizer<1){
			PlaceEnvironment(EnvironmentKeeper.Instance.rockBank[environmentRockIndex]);
			environmentRockIndex++;		
		}
		if (howfar>8)
		howfar = 8;
		if(randomizer>10-howfar){
			int newrandomizer = Random.Range(0,5);
			if(newrandomizer <1){
				PlaceEnvironment(EnvironmentKeeper.Instance.flowerBank[environmentFlowerIndex]);
				environmentFlowerIndex++;
			}
			else{
				PlaceEnvironment(EnvironmentKeeper.Instance.treeBank[environmentTreeIndex]);
				environmentTreeIndex++;
			}			
		}
	}	
	void PlaceEnvironment(Transform environmentTransform){
		environmentTransform.position = new Vector3(xy.x, 0, xy.y);
		environmentTransform.rotation = Quaternion.identity;
		environmentTransform.gameObject.SetActive(true);
	}
	void PlaceSnow(){
		float snowrandom = Random.Range(0f,.06f);
		EnvironmentKeeper.Instance.snowBank[snowEnvIndex].position = new Vector3 (xy.x, -snowrandom, xy.y);
		EnvironmentKeeper.Instance.snowBank[snowEnvIndex].rotation = Quaternion.identity;
		EnvironmentKeeper.Instance.snowBank[snowEnvIndex].gameObject.SetActive(true);
	}


	public void InstantiateSnow(Vector2 newxy){
		xy = newxy;
		PlaceEnvironment(EnvironmentKeeper.Instance.iceBank[iceEnvIndex]);
		iceEnvIndex++;
		
		PlaceSnow();
		snowEnvIndex++;
	}

	//could improve
	public void StartDirection(int myx, int myy){
		Collider[] colliders = Physics.OverlapSphere(new Vector3(myx,0,-(myy+1)), .5f);
		foreach (Collider component in colliders) {
			if (component.tag == "Tile" || component.tag == "Fragile"){
				if(tiles[myx,myy+1].type != "Wall" && tiles[myx,myy+1].type != "Goal"){
					SetStartRotation(270);
					positionOutsidePlayer = new Vector2(myx,-(myy-1));
					return;
				}
			}
		}
		colliders = Physics.OverlapSphere(new Vector3(myx,0,-(myy-1)), .5f);
		foreach (Collider component in colliders) {
			if (component.tag == "Tile"|| component.tag == "Fragile"){
				if(tiles[myx,myy-1].type != "Wall" && tiles[myx,myy-1].type != "Goal"){
					SetStartRotation(90);
					positionOutsidePlayer = new Vector2(myx,-(myy+1));
					return;				
				}
			}
		}
		colliders = Physics.OverlapSphere(new Vector3(myx+1,0,-myy), .5f);
		foreach (Collider component in colliders) {
			if (component.tag == "Tile"|| component.tag == "Fragile"){
				if(tiles[myx+1,myy].type != "Wall"  && tiles[myx+1,myy].type != "Goal"){
					SetStartRotation(180);
					positionOutsidePlayer = new Vector2(myx-1,-myy);
					return;
				}
			}
		}
		colliders = Physics.OverlapSphere(new Vector3(myx-1,0,-myy), .5f);
		foreach (Collider component in colliders) {
			if (component.tag == "Tile"|| component.tag == "Fragile"){
				if(tiles[myx-1,myy].type != "Wall" && tiles[myx-1,myy].type != "Goal"){
					SetStartRotation(0);
					positionOutsidePlayer = new Vector2(myx+1,-myy);
					return;
				}
			}
		}
	}
	
	void SetStartRotation(float rotation){
		playertransform.eulerAngles = new Vector3(0f,rotation,0f);
		starttransform.eulerAngles = new Vector3(0f,rotation,0f);
		playerInitialRotation = (int)rotation;
	}
	
	//could improve
	public void GoalDirection(int myx, int myy){
		Collider[] colliders = Physics.OverlapSphere(new Vector3(myx,0,-(myy+1)), .5f);
		foreach (Collider component in colliders) {
			if (component.tag == "Tile"|| component.tag == "Fragile"){
				if(tiles[myx,myy+1].type != "Wall" && tiles[myx,myy+1].type != "Start"){
					goaltransform.eulerAngles = new Vector3(0f,180f,0f);
					GoalBehaviour.isvertical = true;
					return;
				}
			}
		}
		colliders = Physics.OverlapSphere(new Vector3(myx,0,-(myy-1)), .5f);
		foreach (Collider component in colliders) {
			if (component.tag == "Tile"|| component.tag == "Fragile"){
				if(tiles[myx,myy-1].type != "Wall" && tiles[myx,myy-1].type != "Start"){
					goaltransform.eulerAngles = new Vector3(0f,0f,0f);
					GoalBehaviour.isvertical = true;	
					return;				}
			}
		}
		colliders = Physics.OverlapSphere(new Vector3(myx+1,0,-myy), .5f);
		foreach (Collider component in colliders) {
			if (component.tag == "Tile"|| component.tag == "Fragile"){
				if(tiles[myx+1,myy].type != "Wall" && tiles[myx+1,myy].type != "Start"){
					goaltransform.eulerAngles = new Vector3(0f,90f,0f);
					GoalBehaviour.isvertical = false;
					return;
				}
			}
		}
		colliders = Physics.OverlapSphere(new Vector3(myx-1,0,-myy), .5f);
		foreach (Collider component in colliders) {
			if (component.tag == "Tile"|| component.tag == "Fragile"){
				if(tiles[myx-1,myy].type != "Wall" && tiles[myx-1,myy].type != "Start"){
					goaltransform.eulerAngles = new Vector3(0f,270,0f);
					GoalBehaviour.isvertical = false;
					return;
				}
			}
		}
	}

	//Places according to value in string
	public void placeOnWorld(string[][] jagged,int y, int x){
		if(jagged[y][x].Length == 1){
			switch (jagged [y] [x]) {
			case sstart:
				starttransform = TileKeeper.Instance.zenkoStatue;
				starttransform.position = new Vector3(x,0,-y);
				mysnowh = Random.Range(0f,0.08f);
				tiles[x,y].tileObj = starttransform.gameObject;
				tiles[x,y].type = "Start";
				tiles[x,y].isTaken = true;
				startpos = new Vector2(x,y);
				if(y == 0 || y==1 || y==2 ){
					playertransform = Instantiate (player, new Vector3 (x, 0, -y), Quaternion.Euler(new Vector3(0,270,0)));
					break;
				}
				if(y == totaldimension-1 || y==totaldimension-2 || y==totaldimension-3){
					playertransform = Instantiate (player, new Vector3 (x, 0, -y), Quaternion.Euler(new Vector3(0,90,0)));
					break;
				}
				if(x == 0 || x==1 || x==2){
					playertransform = Instantiate (player, new Vector3 (x, 0, -y), Quaternion.Euler(new Vector3(0,180,0)));
					break;
				}
				if(x==totaldimension-1 || x==totaldimension-2 || x==totaldimension-3){
					playertransform = Instantiate (player, new Vector3 (x, 0, -y), Quaternion.identity);
					break;
				}
				break;
			case sgoal:
				goaltransform = TileKeeper.Instance.goal;
				goaltransform.position = new Vector3 (x,0.162f,-y);
				GoalBehaviour.goaling = false;
				goaltransform.gameObject.SetActive(true);
				tiles[x,y].type = "Goal";
				tiles[x,y].isTaken = true;
				tiles[x,y].tileObj = goaltransform.gameObject;
				mysnowh = Random.Range(0f,0.09f);
				break;
			case sfloor_ice:
				break;
			case sfloor_wall:
				int numerator = Random.Range(0,6);
				mysnowh = 0.04f;//Random.Range(0.05f,0.10f);
				int[] validchoices = {0,90,180,270};
				int randomint = validchoices[Random.Range(0,validchoices.Length)];

				TileKeeper.Instance.snowBank[snowIndex].position = new Vector3 (x, -mysnowh, -y);
				TileKeeper.Instance.snowBank[snowIndex].rotation = Quaternion.Euler(new Vector3(0,randomint,0));
				snowIndex++;
				if(y>1){
					if(tiles[x,y-1].type != "Wood"){
					TileKeeper.Instance.wallBank[wallIndex].position = new Vector3 (x, 0, -y);
					TileKeeper.Instance.wallBank[wallIndex].rotation = Quaternion.Euler(new Vector3(0,randomint,0));
					wallIndex++;
					}
					else{
						TileKeeper.Instance.wallOnly[wallOnlyIndex].position = new Vector3 (x, 0, -y);
						TileKeeper.Instance.wallOnly[wallOnlyIndex].rotation = Quaternion.Euler(new Vector3(0,randomint,0));
						wallOnlyIndex++;
					}						
				}
				else{
					TileKeeper.Instance.wallBank[wallIndex].position = new Vector3 (x, 0, -y);
					TileKeeper.Instance.wallBank[wallIndex].rotation = Quaternion.Euler(new Vector3(0,randomint,0));
					wallIndex++;
				}	
				tiles[x,y].type = "Wall";
				tiles[x,y].isTaken = true;
				break;
			case sfloor_hole:
				Collider[] colliders = Physics.OverlapSphere(new Vector3(x,0,-y), .5f);
				foreach (Collider component in colliders) {
					if (component.tag == "Tile") {	
						component.gameObject.SetActive(false);
						TileKeeper.Instance.holeBank[holeIndex].position = new Vector3(x,0,-y);
						TileKeeper.Instance.holeBank[holeIndex].gameObject.SetActive(true);
						holeIndex++;
						tiles[x,y].type = "Hole";
						tiles[x,y].isTaken = true;
						break;
					}
				}		
				break;
				Instantiate (floor_hole, new Vector3 (x, 0, -y), Quaternion.identity);
				tiles[x,y].type = "Hole";
				tiles[x,y].isTaken = true;
				break;
			case sfloor_rock:
				Instantiate (floor_rock, new Vector3 (x, 0, -y), Quaternion.identity);
				break;
			case sfloor_wood:
				Collider[] collidersw = Physics.OverlapSphere(new Vector3(x,0,-y), .5f);
				foreach (Collider component in collidersw) {
					if (component.tag == "Tile") {	
						TileKeeper.Instance.flowerBank[flowerIndex].position = new Vector3(x,0,-y);
						flowerIndex++;
						tiles[x,y].type = "Wood";
						tiles[x,y].isTaken = true;
						break;
					}
				}		
				break; 
			case sfloor_left:
				Instantiate (floor_left, new Vector3 (x, 0, -y), Quaternion.Euler(new Vector3(0,270,0)));
				break;
			case sfloor_right:
				Instantiate	(floor_right, new Vector3 (x,0,-y), Quaternion.Euler(new Vector3(0,90,0)));
				break;
			case sfloor_up:
				Instantiate (floor_up, new Vector3 (x, 0, -y), Quaternion.Euler(new Vector3(0,0,0)));
				break;
			case sfloor_down:
				Instantiate	(floor_down, new Vector3 (x, 0, -y), Quaternion.Euler(new Vector3(0,180,0))	);
				break;
			case sfloor_fragile:
				Collider[] collidersf = Physics.OverlapSphere(new Vector3(x,0,-y), .5f);
				foreach (Collider component in collidersf) {
					if (component.tag == "Tile") {	
						component.gameObject.SetActive(false);
						TileKeeper.Instance.fragileBank[fragileIndex].position = new Vector3(x,0,-y);
						TileKeeper.Instance.fragileBank[fragileIndex].gameObject.SetActive(true);
						fragileIndex++;
						tiles[x,y].type = "Fragile";
						tiles[x,y].isTaken = true;
						break;
					}
				}		
				break;
			case sfloor_quicksand:
				Instantiate (floor_quicksand, new Vector3 (x, 0, -y), Quaternion.identity);
				tiles[x,y].type = "Quicksand";
				tiles[x,y].isTaken = true;					
				break;
			case sfloor_boss:
				Instantiate (floor_boss, new Vector3(x, 0, -y), Quaternion.identity);
				break;
			case ssfloor_left:
				Instantiate	(s_floor_left, new Vector3 (x, 0, -y), s_floor_left.transform.rotation);
				break;
			case ssfloor_right:
				Instantiate	(s_floor_right, new Vector3 (x, 0, -y), s_floor_right.transform.rotation);
				break;
			case ssfloor_up:
				Instantiate	(s_floor_up, new Vector3 (x, 0, -y), s_floor_up.transform.rotation);
				break;
			case ssfloor_down:
				Instantiate	(s_floor_down, new Vector3 (x, 0, -y), s_floor_down.transform.rotation);
				break;
			case ssfloor_rock:
				Instantiate (s_floor_rock, new Vector3 (x, 0, -y), Quaternion.identity);
				break;
			}
		}
		if(jagged[y][x].Length ==3){
			int hintx = int.Parse(jagged[y][x].Substring(1,1));
			int hinty = int.Parse(jagged[y][x].Substring(2,1));
			if(jagged[y][x].Substring(0,1) == "T"){
				LevelStorer.efficientturns = int.Parse(jagged[y][x].Substring(1,2));				
			}
			else{
				populateIce();
				switch(jagged[y][x].Substring(0,1)){
				case sfloor_left:
					LevelManager.hints.Add(new Hint("Left", hintx,hinty));
					PlaceCreature("Left");
					break;
				case sfloor_right:
					LevelManager.hints.Add(new Hint("Right", hintx,hinty));
					PlaceCreature("Right");
					break;
				case sfloor_up:
					LevelManager.hints.Add(new Hint("Up", hintx,hinty));
					PlaceCreature("Up");
					break;
				case sfloor_down:
					LevelManager.hints.Add(new Hint("Down", hintx,hinty));
					PlaceCreature("Down");
					break;
				case sfloor_rock:
					LevelManager.hints.Add(new Hint("Wall", hintx,hinty));
					PlaceCreature("Wall");
					break;
				case ssfloor_left:
					LevelManager.hints.Add(new Hint("LeftSeed", hintx,hinty));
					PlaceCreature("LeftSeed");
					break;
				case ssfloor_right:
					LevelManager.hints.Add(new Hint("RightSeed", hintx,hinty));
					PlaceCreature("RightSeed");
					break;
				case ssfloor_up:
					LevelManager.hints.Add(new Hint("UpSeed", hintx,hinty));
					PlaceCreature("UpSeed");
					break;
				case ssfloor_down:
					LevelManager.hints.Add(new Hint("DownSeed", hintx,hinty));
					PlaceCreature("DownSeed");
					break;
				case ssfloor_rock:
					LevelManager.hints.Add(new Hint("WallSeed", hintx,hinty));
					PlaceCreature("WallSeed");
					break;
				}
				piecenums++;				
			}
		}
		if(jagged[y][x].Length ==4){
			populateIce();
			int hintx = int.Parse(jagged[y][x].Substring(2,1));
			int hinty = int.Parse(jagged[y][x].Substring(3,1));
			switch(jagged[y][x].Substring(0,2)){
			case sfloor_portalleft:
				LevelManager.hints.Add(new Hint("PortalLeft", hintx,hinty));
				PlaceCreature("PortalLeft");
				break;
			case sfloor_portalright:
				LevelManager.hints.Add(new Hint("PortalRight", hintx,hinty));
				PlaceCreature("PortalRight");
				break;
			case sfloor_portalup:
				LevelManager.hints.Add(new Hint("PortalUp", hintx,hinty));
				PlaceCreature("PortalUp");
				break;
			case sfloor_portaldown:
				LevelManager.hints.Add(new Hint("PortalDown", hintx,hinty));
				PlaceCreature("PortalDown");
				break;
			}
			piecenums++;				
		}
		if(jagged[y][x].Length ==2){	
			LevelStorer.efficientturns = int.Parse(jagged[y][x].Substring(1,1));
		}
	}

	public void DestroyAllExceptCamera(){
		playertransform.gameObject.SetActive(false);
		Destroy(playertransform.gameObject);
		foreach (Dragger piece in PieceHolders.placedpieces){
			Destroy(piece.gameObject);
		}		
	}

	public void ResetPlayer(){
		if(!resetting){
			playertransform.gameObject.GetComponent<PlayerMovement>().LevelLostBoard.SetActive(false);
			playertransform.gameObject.GetComponent<PlayerMovement>().levelWonBoard.SetActive(false);
			GoalBehaviour.active = false;
			resetting = true;
			if(playertransform != null){
				StartCoroutine(DestroyPlayer(.4f, playertransform));
			}
			goaltransform.gameObject.GetComponentInChildren<Animator>().SetInteger("Phase",0);
			GoalBehaviour.restartGoal();
			GoalBehaviour.goaling = false;
			foreach (Dragger piece in PieceHolders.placedpieces){
				piece.gameObject.GetComponent<BoxCollider>().enabled = true;
			}		
		}
	}

	public IEnumerator DestroyPlayer(float fadetime, Transform ptransform){
		float lowValue = -1f;
		float highValue = 1f;
		ptransform.gameObject.GetComponent<PlayerMovement>().enabled = false;
		for(float t = 0.0f; t<fadetime; t+= Time.deltaTime){
			float normalizedTime = t/fadetime;
			ptransform.gameObject.GetComponent<PlayerAnimation>().dissolveMat.SetFloat("Vector1_B5CA3B27", Mathf.Lerp(lowValue,highValue,normalizedTime));
			yield return null;
		}
		ptransform.gameObject.GetComponent<PlayerAnimation>().dissolveMat.SetFloat("Vector1_B5CA3B27", highValue);
		Destroy(playertransform.gameObject);
		SpawnPlayer(startpos);
		resetting = false;
		GoalBehaviour.active = true;
		DisappearStatue();
	}

	void DisappearStatue(){
		LevelBuilder.starttransform.transform.GetChild(0).gameObject.SetActive(false);
	}

	public void SpawnPlayer(Vector2 thevector){
		int x = (int)thevector.x;
		int y = (int)thevector.y;
		playertransform = Instantiate (player, new Vector3 (x, 0, -y), Quaternion.Euler(new Vector3(0,0,0)));
		playertransform.eulerAngles = new Vector3(0f,playerInitialRotation,0f);
	}

	public void populateIce(){
		iceTiles.Clear();
		for(int i = 0; i<totaldimension; i++){
			for(int j = 0; j<totaldimension; j++){
				if (tiles[i,j].type == "Ice"){
					iceTiles.Add(new Vector2(i,j));
				}
			}
		}
	}

	//Placed the creature piece in a random tile
	public void PlaceCreature(string creaturetype){
		RemoveSolutions(creaturetype);
		int randomplace = Random.Range(0,iceTiles.Count);
		Vector2 tileplace = new Vector2(iceTiles[randomplace].x, iceTiles[randomplace].y);//vector 2 for 2d placement in tiles group
		Vector3 pieceplace = new Vector3(iceTiles[randomplace].x, 0, -iceTiles[randomplace].y);//vector 3 for gameworld position
		Tile currenttile = tiles[(int)tileplace.x,(int)tileplace.y];
		if(creaturetype != "Seed")
			LevelManager.placedPieces[(int)tileplace.x,(int)tileplace.y] = creaturetype;
		string myseedtype = "Not";
		if(creaturetype.Length>5){
			if(creaturetype.Substring(creaturetype.Length-4,4) == "Seed"){
				myseedtype = creaturetype.Substring(0,creaturetype.Length-4);
				creaturetype = creaturetype.Substring(creaturetype.Length-4,4);	
				LevelManager.placedPieces[(int)tileplace.x,(int)tileplace.y] = myseedtype;	
			}
			else{
				LevelManager.placedPieces[(int)tileplace.x,(int)tileplace.y] = creaturetype;
			}
		}
		currenttile.isTaken = true;
		currenttile.type = "Wall";
		switch(creaturetype){
		case "Wall":
			Transform wallpiece = Instantiate (floor_rock, pieceplace, Quaternion.identity);
			wallpiece.gameObject.GetComponentInChildren<Animator>().SetInteger("Phase",2);
			PieceHolders.placedpieces.Add(wallpiece.gameObject.GetComponent<Dragger>());
			break;
		case "Left":
			Transform leftpiece = Instantiate (floor_left, pieceplace, Quaternion.Euler(new Vector3(0,270,0)));
			leftpiece.gameObject.GetComponentInChildren<Animator>().SetInteger("Phase",2);
			currenttile.tileObj = leftpiece.gameObject;
			if(LevelBuilder.tiles[(int)tileplace.x-1, (int)tileplace.y].type == "Ice"){
				LevelBuilder.tiles[(int)tileplace.x-1, (int)tileplace.y].type = leftpiece.gameObject.GetComponent<Dragger>().myType;		
			}	

			else{
				LevelBuilder.tiles[(int)tileplace.x-1, (int)tileplace.y].isSideways = "Left";
			}
			PieceHolders.placedpieces.Add(leftpiece.gameObject.GetComponent<Dragger>());
			break;
		case "Up":
			Transform uppiece = Instantiate (floor_up, pieceplace, Quaternion.Euler(new Vector3(0,0,0)));
			uppiece.gameObject.GetComponentInChildren<Animator>().SetInteger("Phase",2);
			currenttile.tileObj = uppiece.gameObject;
			if(LevelBuilder.tiles[(int)tileplace.x, (int)tileplace.y-1].type == "Ice"){
				LevelBuilder.tiles[(int)tileplace.x, (int)tileplace.y-1].type = uppiece.gameObject.GetComponent<Dragger>().myType;		
			}	

			else{
				LevelBuilder.tiles[(int)tileplace.x, (int)tileplace.y-1].isSideways = "Up";
			}
			PieceHolders.placedpieces.Add(uppiece.gameObject.GetComponent<Dragger>());
			break;
		case "Right":
			Transform rightpiece = Instantiate (floor_right, pieceplace, Quaternion.Euler(new Vector3(0,90,0)));
			rightpiece.gameObject.GetComponentInChildren<Animator>().SetInteger("Phase",2);
			currenttile.tileObj = rightpiece.gameObject;
			if(LevelBuilder.tiles[(int)tileplace.x+1, (int)tileplace.y].type == "Ice"){
				LevelBuilder.tiles[(int)tileplace.x+1, (int)tileplace.y].type = rightpiece.gameObject.GetComponent<Dragger>().myType;		
			}	
			else{
				LevelBuilder.tiles[(int)tileplace.x+1, (int)tileplace.y].isSideways = "Right";
			}	
			PieceHolders.placedpieces.Add(rightpiece.gameObject.GetComponent<Dragger>());
			break;
		case "Down":
			Transform downpiece = Instantiate (floor_down, pieceplace, Quaternion.Euler(new Vector3(0,180,0)));
			downpiece.gameObject.GetComponentInChildren<Animator>().SetInteger("Phase",2);				
			currenttile.tileObj = downpiece.gameObject;
			if(LevelBuilder.tiles[(int)tileplace.x, (int)tileplace.y+1].type == "Ice"){
				LevelBuilder.tiles[(int)tileplace.x, (int)tileplace.y+1].type = downpiece.gameObject.GetComponent<Dragger>().myType;		
			}	
			else{
				LevelBuilder.tiles[(int)tileplace.x, (int)tileplace.y+1].isSideways = "Down";
			}
			PieceHolders.placedpieces.Add(downpiece.gameObject.GetComponent<Dragger>());
			break;
		case "PortalLeft":
			Transform portalleftpiece = Instantiate (floor_portal, pieceplace, Quaternion.Euler(new Vector3(0,270,0)));
			currenttile.type = "Portal";
			currenttile.tileObj = portalleftpiece.gameObject;
			currenttile.portalType = "Left";
			portalleftpiece.gameObject.GetComponent<Dragger>().portalType = "Left";
			PieceHolders.placedpieces.Add(portalleftpiece.gameObject.GetComponent<Dragger>());
			Portals.Add(portalleftpiece.gameObject);
			break;
		case "PortalUp":
			Transform portaluppiece = Instantiate (floor_portal, pieceplace, Quaternion.Euler(new Vector3(0,0,0)));
			currenttile.type = "Portal";
			currenttile.portalType = "Up";
			currenttile.tileObj = portaluppiece.gameObject;
			portaluppiece.gameObject.GetComponent<Dragger>().portalType = "Up";
			PieceHolders.placedpieces.Add(portaluppiece.gameObject.GetComponent<Dragger>());
			Portals.Add(portaluppiece.gameObject);
			break;
		case "PortalRight":
			Transform portalrightpiece = Instantiate (floor_portal, pieceplace, Quaternion.Euler(new Vector3(0,90,0)));
			currenttile.type = "Portal";
			currenttile.tileObj = portalrightpiece.gameObject;
			currenttile.portalType = "Right";
			portalrightpiece.gameObject.GetComponent<Dragger>().portalType = "Right";
			PieceHolders.placedpieces.Add(portalrightpiece.gameObject.GetComponent<Dragger>());
			Portals.Add(portalrightpiece.gameObject);
			break;
		case "PortalDown":
			Transform portaldownpiece = Instantiate (floor_portal, pieceplace, Quaternion.Euler(new Vector3(0,180,0)));
			currenttile.type = "Portal";
			currenttile.portalType = "Down";
			currenttile.tileObj = portaldownpiece.gameObject;
			portaldownpiece.gameObject.GetComponent<Dragger>().portalType = "Down";
			PieceHolders.placedpieces.Add(portaldownpiece.gameObject.GetComponent<Dragger>());
			Portals.Add(portaldownpiece.gameObject);
			break;
		case "Seed":
			Transform seedpiece;
			currenttile.type = "Seed";
			currenttile.seedType = myseedtype;
			switch(myseedtype){
			case "Wall":
				seedpiece = Instantiate (s_floor_rock, pieceplace, Quaternion.identity);
				break;
			case "Left":
				seedpiece = Instantiate (s_floor_left, pieceplace, Quaternion.Euler(new Vector3(0,270,0)));				
				break;
			case "Up":
				seedpiece = Instantiate (s_floor_up, pieceplace, Quaternion.Euler(new Vector3(0,0,0)));
				break;
			case "Right":
				seedpiece = Instantiate (s_floor_right, pieceplace, Quaternion.Euler(new Vector3(0,90,0)));
				break;
			case "Down":
				seedpiece = Instantiate (s_floor_down, pieceplace, Quaternion.Euler(new Vector3(0,180,0)));
				break;
			default:
				seedpiece = Instantiate (s_floor_rock, pieceplace, Quaternion.identity);
				break;
			}
			PieceHolders.placedpieces.Add(seedpiece.gameObject.GetComponent<Dragger>());	
			currenttile.tileObj = seedpiece.gameObject;	
			break;
		}
	}

	public void RemoveSolutions(string creaturetype){
		for (int i = 0; i<LevelManager.hints.Count; i++){
			if(LevelManager.hints[i].type == creaturetype){
				iceTiles.Remove(new Vector2(LevelManager.hints[i].x, LevelManager.hints[i].y));
			}
		}
	}
}