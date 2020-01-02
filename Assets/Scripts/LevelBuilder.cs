using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LevelBuilder : MonoBehaviour {
	public Material backgroundMaterial;
	public static Material background;
	private string filePath;
	private string result;
//	IceTileHandler myhandler;
	//public Brain brain;

	public GameObject[] tile;
	 List<GameObject> tileBank = new List<GameObject>();
	//public static Transform icetile;

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

	public static string[] levelsPotd;

	public static string[] levelsAdventure;

	public static List<int> startersAdventure;

	public static List<int> startersPotd;

	public Color mygray;// = new Color(173/255f,173/255f,173/255f,1f);

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

	//public static List<Transform> piecetiles = new List<Transform>();

	/*void ShuffleList(){
			System.Random rand = new System.Random();
			int r = tileBank.Count;
			while(r>1){
				r--;
				int n = rand.Next(r+1);
				Debug.Log(n);
				GameObject val = tileBank[n];
				tileBank[n] = tileBank[r];
				tileBank[r] = val;
			}
		}*/
	public void Awake(){
		Instance = this;
		background = backgroundMaterial;
		background.SetColor("Color_A7A46709", new Color(0,0,0,0));
		winboard = mywinboard;
		loseboard = myloseboard;
		hintboard = myhintboard;
	}
	IEnumerator checkAndroid(string file){
		filePath = System.IO.Path.Combine(Application.streamingAssetsPath, file);
		Debug.Log (filePath + "FILEPAPAPATH");
		result = " ";
		if (file.Contains ("://")) {
			UnityWebRequest www = UnityWebRequest.Get (file);
			yield return www.SendWebRequest ();
			result = www.downloadHandler.text;
			Debug.Log (result);

		} else
			result = System.IO.File.ReadAllText (filePath);
		Debug.Log (result);
		Debug.Log (filePath);
	}
	public static void ChangeBackground(string colorVariable, Color newcolor, float fadetime){
		Instance.StartCoroutine(Instance.ChangeColor(colorVariable, newcolor, fadetime));
	}
	public IEnumerator ChangeColor(string colorVariable,Color newcolor, float fadetime){
		Color initColor = backgroundMaterial.GetColor("Color_A7A46709");
		Color currentColor;
		for(float t = 0.0f; t<fadetime; t+= Time.deltaTime){
			float normalizedTime = t/fadetime;
			currentColor = new Color((float)Mathf.Lerp(initColor.r, newcolor.r, normalizedTime),
				(float)Mathf.Lerp(initColor.g, newcolor.g, normalizedTime),
				(float)Mathf.Lerp(initColor.b, newcolor.b, normalizedTime),
				(float)Mathf.Lerp(initColor.a, newcolor.a, normalizedTime));

			backgroundMaterial.SetColor(colorVariable, currentColor);
			yield return null;
		}		
		backgroundMaterial.SetColor(colorVariable, newcolor);	
	}
	public IEnumerator initPotd(){ //feeds levelPotd string array from textfile.
		startersPotd = new List<int>();
		string file = "FilteredMaps.txt";
		filePath = System.IO.Path.Combine(Application.streamingAssetsPath, file);
		Debug.Log (filePath + "FILEPAPAPATHASDASDASDASDASDASDASDASDASD");
		result = " ";
		if (filePath.Contains ("://") || filePath.Contains(":///"))  {
			UnityWebRequest www = UnityWebRequest.Get (filePath);
			yield return www.SendWebRequest ();
			result = www.downloadHandler.text;
			Debug.Log (result);

		} 
		else{
			result = System.IO.File.ReadAllText (filePath);
		}
		string text = result;
		//string text = System.IO.File.ReadAllText(System.IO.Path.Combine (Application.streamingAssetsPath, "Ch4_Easy.txt"));
		string[] lines = Regex.Split(text, "\r?\n");
		Debug.Log(lines[0]);
		for(int i =0; i<lines.Length;i++){
			startersPotd.Add(i);
//			Debug.Log(lines[i].Length);
//			Debug.Log(lines[i]);
			//Debug.Log(i + "is line number");
			int mapsize = int.Parse(lines[i].Substring(3,1));		
			if(lines[i].Length == 5){
				//Debug.Log("size it up for " );
				mapsize = int.Parse(lines[i].Substring(3,2));
			}			
			i = i + mapsize + 2;
		}
		Debug.Log(startersPotd.Count + " number of maps filtered");
		levelsPotd = (string[])lines.Clone();
		if(LevelManager.ispotd){
			Debug.Log(LevelManager.levelnum + " LEVELNUM " );
			drawPotd(PlayerPrefs.GetInt("PoTD"));
		}
	}
	public IEnumerator initAdventure(){ //feeds Adventure string array from textfile.
		startersAdventure = new List<int>();
		string file = "AdventureLevels.txt";
		filePath = System.IO.Path.Combine(Application.streamingAssetsPath, file);
		Debug.Log (filePath + "FILEPAPAPATH");
		result = " ";
		if (filePath.Contains ("://") || filePath.Contains(":///"))  {
			UnityWebRequest www = UnityWebRequest.Get (filePath);
			yield return www.SendWebRequest ();
			result = www.downloadHandler.text;
			Debug.Log (result);

		} 
		else{
			result = System.IO.File.ReadAllText (filePath);
		}
		string text = result;
		//string text = System.IO.File.ReadAllText(System.IO.Path.Combine (Application.streamingAssetsPath, "AdventureLevels.txt"));
		string[] lines = Regex.Split(text, "\r?\n");

		Debug.Log(lines[0]);
		bool nextismap;
		nextismap = true;
		for(int i =0; i<lines.Length;i++){
			if(nextismap){
				startersAdventure.Add(i);
			}
			nextismap = false;
//			Debug.Log(lines[i].Length);
//			Debug.Log(lines[i]);		
			if(lines[i] == ""){
				//Debug.Log("BREAK");
				nextismap = true;
			}			
			//i = i + mapsize + 2;
		}
		levelsAdventure = (string[])lines.Clone();
		if(!LevelManager.ispotd){
			//load potd maps
			drawNormal(LevelManager.levelnum);
		}
	}

	public void drawNormal(int levelnumber){
		Debug.Log(levelnumber);
		TutorialHandler.Instance.TutorialCheck(levelnumber);
		pieceHolder.reset();
		LevelManager.piecetiles = new List<Transform>();
		LevelManager.myhints = new List<Vector2>();
		LevelManager.hintnum = 0;
		LevelManager.hints = new List<Hint>();
		PieceHolders.placedpieces = new List<Dragger>();
		LevelManager.placedPieces = new string[10,10];
		//PopulationManager.readytobrain = false;
		string leveltext = ("Level" + levelnumber.ToString() + ".txt");
		string levelname = ("Level" + levelnumber.ToString ());
//		Debug.Log("Bouttodraw" + levelnumber);
		//StartCoroutine(checkAndroid (leveltext));
		//string filePath = System.IO.Path.Combine (Application.streamingAssetsPath, leveltext);
		Debug.Log(filePath);
		string[][] jagged = readAdventure (levelnumber);
		//mysnowh;
		//check dimension.

		tiles = new Tile [totaldimension, totaldimension];
		if(!iscreated){
			CreateBase();
		}
		CreateOuterBase();
		PlaceBase();
		if(levelnumber < 0){
			Debug.Log(LevelManager.levelnum);
			jagged = readRandomFile(filePath, LevelManager.levelnum);
			//Debug.Log(randomer);
		}
		piecenums = 0;
		Debug.Log(jagged.Length);	
		// create planes based on matrix
		for (int y = 0; y < totaldimension; y++) {
//			Debug.Log(y);
			for (int x = 0; x < jagged [y].Length; x++) {
				double zed = (-y) + (-0.8);
				//Debug.Log(x + " + " + y);ww
//				Debug.Log(jagged[y][x]);
				placeOnWorld(jagged,y,x);
							
			}
		} 
		GoalDirection((int)goaltransform.position.x,-(int)goaltransform.position.z);
		StartDirection((int)starttransform.position.x,-(int)starttransform.position.z);
		for (int y = totaldimension; y < totaldimension+1; y++) {
//			Debug.Log(y);
			for (int x = 0; x < jagged [y].Length; x++) {
				double zed = (-y) + (-0.8);
				//Debug.Log(x + " + " + y);ww
//				Debug.Log(jagged[y][x]);
				placeOnWorld(jagged,y,x);
							
			}
		} 
		Debug.Log(LevelManager.myhints.Count);
		Debug.Log(LevelStorer.efficientturns);
		if(LevelBuilder.totaldimension == 10){
			CameraController.changePosition(1,1);
			CameraController.changeFovAndRot((int)32,52.9f);
			//LightController.setLight(1,1);

		}
		else if (LevelBuilder.totaldimension < 10){
			CameraController.changePosition(0,0);
			CameraController.changeFovAndRot((int)27,52f);
			//LightController.setLight(0,0);

		}
		ProgressBar.InitializeProgressBar(LevelStorer.efficientturns);
		DotHandler.InitializeDots(LevelStorer.efficientturns);
		playertransform.gameObject.GetComponent<PlayerMovement>().canmove = true;
		// foreach (string nline in LevelSaver.currentmap)
		// Debug.Log(nline);
		//PopulationManager.readytobrain = true;
	}
	public void drawPotd(int num){
		pieceHolder.reset();
		string[][] jagged = readPotd(num);
		Debug.Log("TOTAL DIMENSION IS" + totaldimension);
		tiles = new Tile [totaldimension, totaldimension];
//		Debug.Log(tiles[9,9]);
//		Debug.Log(tiles[10,10]);
		if(!iscreated){
			CreateBase ();
		}
		//CreateBase();
		CreateOuterBase();
		PlaceBase();
		LevelManager.piecetiles = new List<Transform>();
		LevelManager.myhints = new List<Vector2>();
		LevelManager.hintnum = 0;
		LevelManager.hints = new List<Hint>();
		PieceHolders.placedpieces = new List<Dragger>();
		LevelManager.placedPieces = new string[10,10];
		piecenums = 0;
		for (int y = 0; y < totaldimension; y++) {
//			Debug.Log(y);
			for (int x = 0; x < jagged [y].Length; x++) {
				double zed = (-y) + (-0.8);
				//Debug.Log(x + " + " + y);ww
//				Debug.Log(jagged[y][x]);
				placeOnWorld(jagged,y,x);
							
			}
		} 
		GoalDirection((int)goaltransform.position.x,-(int)goaltransform.position.z);
		StartDirection((int)starttransform.position.x,-(int)starttransform.position.z);
		for (int y = totaldimension; y < totaldimension+1; y++) {
//			Debug.Log(y);
			for (int x = 0; x < jagged [y].Length; x++) {
				double zed = (-y) + (-0.8);
				//Debug.Log(x + " + " + y);ww
//				Debug.Log(jagged[y][x]);
				placeOnWorld(jagged,y,x);
							
			}
		} 
		if(LevelBuilder.totaldimension == 10){
			CameraController.changePosition(1,1);
			CameraController.changeFovAndRot((int)32,52.9f);
			//LightController.setLight(1,1);

		}
		else if (LevelBuilder.totaldimension < 10){
			CameraController.changePosition(0,0);
			CameraController.changeFovAndRot((int)27,52f);
			//LightController.setLight(0,0);

		}
		ProgressBar.InitializeProgressBar(LevelStorer.efficientturns);
		DotHandler.InitializeDots(LevelStorer.efficientturns);
		playertransform.gameObject.GetComponent<PlayerMovement>().canmove = true;
		#if UNITY_ANDROID
		if(!LevelManager.adFree){
			if (!GoogleAds.Instance.rewardVideo.IsLoaded())
			GoogleAds.Instance.RequestHintAd();
		}
		
		#endif

	}
	string[][] readAdventure(int place){
		place = place-1;
		LevelSaver.currentmap = new List<string>();
		//string text = System.IO.File.ReadAllText(System.IO.Path.Combine (Application.streamingAssetsPath, "AdventureLevels.txt"));
		string firstline = levelsAdventure[startersAdventure[place]];
		LevelSaver.currentmap.Add(firstline);

		Debug.Log("Firstline" + firstline);
		//totaldimension = int.Parse(firstline.Substring(3,1));
		bool donescanning = false;
		int linenumber = 1;
		Debug.Log(levelsAdventure[startersAdventure[place]+linenumber]);
		while(!donescanning){
			if(levelsAdventure[startersAdventure[place]+linenumber] == ""){
				donescanning = true;
				totaldimension = linenumber-2;
				Debug.Log(totaldimension);
			}	
			linenumber++;
		}

		string[] lines = new string[totaldimension+1];

		for (int i=0; i<totaldimension+1; i++){
			lines[i] = levelsAdventure[startersAdventure[place]+1+i];
			Debug.Log(lines[i]);
			LevelSaver.currentmap.Add(lines[i]);
		}

		LevelSaver.currentmap.Add("");
		Debug.Log(lines[lines.Length-1]);

		string[][] levelBase = new string [totaldimension+1][];
//		Debug.Log(lines);
		for (int i = 0; i<totaldimension+1; i++){
			string[] stringsOfLine = Regex.Split (lines [i], " ");
			levelBase [i] = stringsOfLine;
		}
		//Debug.Log(levelBase[5][6]);
		return levelBase;
	}
	string[][] readPotd(int place){
		LevelSaver.currentmap = new List<string>();
		//string text = System.IO.File.ReadAllText(System.IO.Path.Combine (Application.streamingAssetsPath, "Ch4_Easy.txt"));
		string firstline = levelsPotd[startersPotd[place]];
		LevelSaver.currentmap.Add(firstline);
		totaldimension = int.Parse(firstline.Substring(3,1));
		if(firstline.Length == 5){
			totaldimension = int.Parse(firstline.Substring(3,2));
		}
		string[] lines = new string[totaldimension+1];
		for (int i=0; i<totaldimension+1; i++){
			lines[i] = levelsPotd[startersPotd[place]+1+i];
			LevelSaver.currentmap.Add(lines[i]);
		}
		LevelSaver.currentmap.Add("");
		Debug.Log(lines[lines.Length-1]);
		string[][] levelBase = new string [totaldimension+1][];
//		Debug.Log(lines);
		for (int i = 0; i<totaldimension+1; i++){
			string[] stringsOfLine = Regex.Split (lines [i], " ");
			levelBase [i] = stringsOfLine;
		}
		return levelBase;
	}

	string[][] readFile(string file){
		string text = System.IO.File.ReadAllText (file);
		string[] lines = Regex.Split (text, "\n");
		int rows = lines.Length;
		Debug.Log(lines.Length);
		string[][] levelBase = new string[rows][];
		for (int i = 0; i < lines.Length; i++) {
//			Debug.Log(lines[i]);
			string[] stringsOfLine = Regex.Split (lines [i], " ");
			levelBase [i] = stringsOfLine;
		}
		totaldimension = rows-1;
		Debug.Log(levelBase);
		return levelBase;
	}

	string[][] readRandomFile(string file,int pos){
//		Debug.Log(pos);
		string text = System.IO.File.ReadAllText (file);
		string[] lines = Regex.Split (text, "\n");
		int rows = lines.Length;
		Debug.Log(rows);
		string[][] levelBase = new string[rows][];
		int initialpos = 1 + 11*pos;
		Debug.Log(lines[0]);
		for (int i = 0; i < 9; i++) {
//			Debug.Log(lines[i]);
			int currentpos = i+initialpos;
			string[] stringsOfLine = Regex.Split (lines [currentpos], " ");
			levelBase [i] = stringsOfLine;
		}

		//Debug.Log(levelBase[7][0]);
		return levelBase;
	}

	void assignStarters(string file, int pos){
		string text = System.IO.File.ReadAllText(file);
		string[] lines = Regex.Split(text, "\n");
		int rows = lines.Length;

	}
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

	public Transform s_floor_left;
	public Transform s_floor_right;
	public Transform s_floor_up;
	public Transform s_floor_down;
	public Transform s_floor_rock;


	
	public static int levelnum;
	//public bool readytodraw;

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

	float mysnowh;
	int piecenums;
	public GameObject pieceHolderHolder;
	PieceHolders pieceHolder;


	void Start() {
	Swiping.mydirection = "Null";
	pieceHolder = pieceHolderHolder.GetComponent<PieceHolders>();
	//LevelManager.newicarus = true;
	LevelManager.levelselector = this;
	StartCoroutine(initPotd());
	StartCoroutine(initAdventure());
	//readAdventure(3);
	//GameObject.Find("Menu").GetComponent<MenuButton>().ConfigMenu.SetActive(false);

	//LevelManager.levelnum = 1;
	 
	//Debug.Log(LevelManager.levelnum + "isthenum");
	Debug.Log(LevelManager.levelnum); //number that was fed in before loading level scene.
	if (LevelManager.levelnum == null || LevelManager.levelnum == 0) {
		LevelManager.levelnum = 1;
		levelnum =1;
	}
	else{
		levelnum = LevelManager.levelnum;
	}
	

	}

	public void RePotd(){
		StartCoroutine(initPotd());
	}
	public void CreateBase(){
		iscreated = true;
		//int numCopies = totaldimension*totaldimension;
		int numCopies = 100;
		for (int i =0; i<numCopies;i++){
			for(int j=0; j < tile.Length; j++){
				GameObject o = (GameObject) Instantiate(tile[j],new Vector3(-10,-10,0), tile[j].transform.rotation);
				o.SetActive(false);
				tileBank.Add(o);
			}
		}	
	}
	public void PlaceBase(){
		for (int r= 0; r< totaldimension; r++){
			for (int c = 0; c<totaldimension; c++){
				Vector3 tilePos = new Vector3(c,0,r);
				//Debug.Log("TILEBANK SIZE IS" + tileBank.Count);
				for (int n =0; n< tileBank.Count; n++){
					GameObject o = tileBank[n];
					if(!o.activeSelf){
//						Debug.Log(tilePos);
						o.transform.position = new Vector3(tilePos.x,tilePos.y,-tilePos.z);
						o.SetActive(true);
						o.transform.GetChild(0).gameObject.SetActive(true);
						tiles[c,r] = new Tile(o, "Ice", false	);
						//Debug.Log(tiles[c,r].type);
						n = tileBank.Count + 1;
					}
				}
			}
		}
	}
	public void CreateOuterBase(){
		howfar = 5;
		for(int x=1; x<8; x++){
			howfar++;
			for(int y = 0; y<9;y++){
				InstantiateRandomEnvironment(new Vector2(-x,y-(totaldimension/2)));
				InstantiateRandomEnvironment(new Vector2(x+(totaldimension-1),y-(totaldimension/2)));
				//InstantiateRandomEnvironment(new Vector2(-x,-y));
				if(y!=0){
					InstantiateRandomEnvironment(new Vector2(-x,-y-(totaldimension/2)));
					InstantiateRandomEnvironment(new Vector2(x+(totaldimension-1),-y-(totaldimension/2)));					
				}
			}
		}	
		howfar=7;
		for(int x=0;x<totaldimension;x++){
			for(int y=1; y<9; y++){
				InstantiateRandomEnvironment(new Vector2(x,y));
				InstantiateRandomEnvironment(new Vector2(x,-y-totaldimension+1));
				InstantiateSnow(new Vector2(x,-y-(totaldimension-1)));				
			}
		}
	}
	public void InstantiateRandomEnvironment(Vector2 xy){
		Instantiate (environment_ice, new Vector3 (xy.x, 0, xy.y), Quaternion.identity);
		int randomizer = Random.Range(0,10);
		float snowrandom = Random.Range(0f,.04f);
		//if(randomizer == 2){
		Instantiate (floor_snow, new Vector3 (xy.x, -snowrandom, xy.y), Quaternion.identity);

		//}
		//else{
		//Instantiate (floor_snow, new Vector3 (xy.x, -snowrandom, xy.y), Quaternion.identity);

		//}

		if(randomizer<1){
		
			int mynum = Random.Range(0,6);
			Instantiate (floor_rocks[mynum], new Vector3 (xy.x, 0, xy.y), Quaternion.identity);			
		}
		if(randomizer>10-howfar){
		
			int mynum2 = Random.Range(0,4);
			Instantiate (floor_trees[mynum2], new Vector3 (xy.x, 0, xy.y), Quaternion.identity);			
		}
		//if(xy.y!= 0){
			//Instantiate (floor_ice, new Vector3 (-xy.x, 0, -xy.y), Quaternion.identity);
			//mynum = Random.Range(0,6);
			//Instantiate (floor_rocks[mynum], new Vector3 (-xy.x, 0, -xy.y), Quaternion.identity);			
		// //}
		// if(xy.x<8){
		// 	Instantiate (floor_ice, new Vector3 (xy.x, 0, xy.y), Quaternion.identity);
		// 	mynum = Random.Range(0,6);
		// 	Instantiate (floor_rocks[mynum], new Vector3 (xy.x, 0, xy.y), Quaternion.identity);				
		// }
		// if(xy.y ==0){
		// 	Instantiate (floor_ice, new Vector3 (xy.y, 0, xy.x), Quaternion.identity);
		// 	mynum = Random.Range(0,6);
		// 	Instantiate (floor_rocks[mynum], new Vector3 (xy.y, 0, xy.x), Quaternion.identity);					
		// }
	}	
		

	
	public void InstantiateSnow(Vector2 xy){
		Instantiate (environment_ice, new Vector3 (xy.x, 0, xy.y), Quaternion.identity);
		int randomizer = Random.Range(0,10);
		float snowrandom = Random.Range(0f,.06f);
		Debug.Log(snowrandom);	
		//if(randomizer == 2){
		Instantiate (floor_snow, new Vector3 (xy.x, -snowrandom, xy.y), Quaternion.identity);		
	}


	public void PlaceOuterBase(){
		for(int x=1; x<15; x++){
			for(int y = 1; y<15;y++){

			}
		}
	}
	public void StartDirection(int myx, int myy){
		Debug.Log("Directing");
		Debug.Log(myx + " " + myy);
		Debug.Log(tiles.Length);
		Collider[] colliders = Physics.OverlapSphere(new Vector3(myx,0,-(myy+1)), .5f);
		foreach (Collider component in colliders) {
			if (component.tag == "Tile" || component.tag == "Fragile"){
				if(tiles[myx,myy+1].type != "Wall" && tiles[myx,myy+1].type != "Goal"){
					Debug.Log("Down");
					playertransform.eulerAngles = new Vector3(0f,270f,0f);
					starttransform.eulerAngles = new Vector3(0f,270f,0f);
					playerInitialRotation = 270;
					return;
				}
			}
		}
		colliders = Physics.OverlapSphere(new Vector3(myx,0,-(myy-1)), .5f);
		foreach (Collider component in colliders) {
			if (component.tag == "Tile"|| component.tag == "Fragile"){
				if(tiles[myx,myy-1].type != "Wall" && tiles[myx,myy-1].type != "Goal"){
					Debug.Log("Up");
					playertransform.eulerAngles = new Vector3(0f,90f,0f);
					starttransform.eulerAngles = new Vector3(0f,90f,0f);
					playerInitialRotation = 90;
					return;				
				}
			}
		}
		colliders = Physics.OverlapSphere(new Vector3(myx+1,0,-myy), .5f);
		foreach (Collider component in colliders) {
			Debug.Log(component.tag);
			if (component.tag == "Tile"|| component.tag == "Fragile"){
				if(tiles[myx+1,myy].type != "Wall"  && tiles[myx+1,myy].type != "Goal"){
					Debug.Log("Right");
					playertransform.eulerAngles = new Vector3(0f,180f,0f);
					starttransform.eulerAngles = new Vector3(0f,180f,0f);
					playerInitialRotation = 180;
					return;
				}
			}
		}
		colliders = Physics.OverlapSphere(new Vector3(myx-1,0,-myy), .5f);
		foreach (Collider component in colliders) {
			if (component.tag == "Tile"|| component.tag == "Fragile"){
				if(tiles[myx-1,myy].type != "Wall" && tiles[myx-1,myy].type != "Goal"){
					playertransform.eulerAngles = new Vector3(0f,0f,0f);
					starttransform.eulerAngles = new Vector3(0f,0f,0f);
					playerInitialRotation = 0;
					Debug.Log("Left");
					return;
				}
			}
		}
	}
	public void GoalDirection(int myx, int myy){
		//Debug.Log(tiles[0,11].type);
//		if(tiles[0,11].type == null){
//			Debug.Log("RURU");
//		}
		Debug.Log("Directing");
		Debug.Log(myx + " " + myy);
		Collider[] colliders = Physics.OverlapSphere(new Vector3(myx,0,-(myy+1)), .5f);
			foreach (Collider component in colliders) {
				if (component.tag == "Tile"|| component.tag == "Fragile"){
					if(tiles[myx,myy+1].type != "Wall" && tiles[myx,myy+1].type != "Start"){
						Debug.Log("Down");
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
						Debug.Log("Up");
						goaltransform.eulerAngles = new Vector3(0f,0f,0f);
						GoalBehaviour.isvertical = true;	
						return;				}
				}
			}
		colliders = Physics.OverlapSphere(new Vector3(myx+1,0,-myy), .5f);
			foreach (Collider component in colliders) {
				if (component.tag == "Tile"|| component.tag == "Fragile"){
					if(tiles[myx+1,myy].type != "Wall" && tiles[myx+1,myy].type != "Start"){
						Debug.Log("Right");
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
						Debug.Log("Left");
						return;
					}
				}
			}







		// if(myx<5){	
		// 	//Debug.Log(myx + "" + myy);
		// 	Debug.Log(tiles[myx+1,myy].type);
		// 	Debug.Log(myx+1 + " " + myy );
		// 	if(tiles[myx+1,myy].type!="Wall" && tiles[myx+1,myy].type!="Start" && tiles[myx+1,myy].type!=null){
		// 	goaltransform.eulerAngles = new Vector3(0f,90f,0f);
		// 	GoalBehaviour.isvertical = false;
		// 	return;

		// 	Debug.Log("Right");

		// 	}


		// }
		// if(myx>4){
		// 	if(tiles[myx-1,myy].type!="Wall" && tiles[myx-1,myy].type!="Start" && tiles[myx-1,myy].type!=null){
		// 	goaltransform.eulerAngles = new Vector3(0f,270,0f);
		// 	GoalBehaviour.isvertical = false;

		// 	Debug.Log("Left");
		// 	return;
		// 	}
		// }
		// if(myy>4){
		// 	if(tiles[myx,myy-1].type!="Wall" && tiles[myx,myy-1].type!="Start" && tiles[myx,myy-1].type!=null){
		// 	goaltransform.eulerAngles = new Vector3(0f,0f,0f);
		// 	Debug.Log("Up");
		// 	GoalBehaviour.isvertical = true;

		// 	return;	
		// 	}
		// }
		// if(myy<5){
		// 	if(tiles[myx,myy+1].type!="Wall" && tiles[myx,myy+1].type!="Start" && tiles[myx,myy+1].type!=null){
		// 	goaltransform.eulerAngles = new Vector3(0f,180f,0f);
		// 	Debug.Log("Down");
		// 	GoalBehaviour.isvertical = true;


		// 	return;			//rotate transform
		// 	//return;
		// 	}
		// }
	}
	public void MakeLevel(){
		//FindDimension();

	}
	public void DrawNextLevel(int levelnumber){
		LevelManager.piecetiles = new List<Transform>();
		LevelManager.myhints = new List<Vector2>();
		LevelManager.hintnum = 0;
		//PopulationManager.readytobrain = false;
		string leveltext = ("Level" + levelnumber.ToString() + ".txt");
		string levelname = ("Level" + levelnumber.ToString ());
//		Debug.Log("Bouttodraw" + levelnumber);
		StartCoroutine(checkAndroid (leveltext));
		string filePath = System.IO.Path.Combine (Application.streamingAssetsPath, leveltext);
		Debug.Log(filePath);
		string[][] jagged = readFile (filePath);
		//mysnowh;
		if(levelnumber < 0){
			Debug.Log(LevelManager.levelnum);
			jagged = readRandomFile(filePath, LevelManager.levelnum);
			//Debug.Log(randomer);
		}
		piecenums = 0;
		Debug.Log(jagged.Length);	
		// create planes based on matrix
		for (int y = 0; y < 9; y++) {
//			Debug.Log(y);
			for (int x = 0; x < jagged [y].Length; x++) {
				double zed = (-y) + (-0.8);
				//Debug.Log(x + " + " + y);ww
//				Debug.Log(jagged[y][x]);
				placeOnWorld(jagged,y,x);
							
			}
		} 
		GoalDirection((int)goaltransform.position.x,-(int)goaltransform.position.z);
		Debug.Log(LevelManager.myhints.Count);
		//PopulationManager.readytobrain = true;
	}
	public void placeOnWorld(string[][] jagged,int y, int x){
//		Debug.Log(tiles.GetLength(0));
//		Debug.Log("X " +x + "Y "+ y);
//		Debug.Log(jagged[y][x]);
		if(jagged[y][x].Length == 1){
		//					Debug.Log("1 in " + jagged[y][x]);
			switch (jagged [y] [x]) {
			case sstart:

				starttransform = Instantiate (floor_start, new Vector3 (x, 0, -y), Quaternion.identity);
				//GameObject p = player;
				//Instantiate (player, new Vector3 (x, 0, -y), Quaternion.identity);
				mysnowh = Random.Range(0f,0.08f);
				//Instantiate (floor_snow, new Vector3 (x, -mysnowh, -y), Quaternion.identity);


				Debug.Log(x + "+" + y);
				tiles[x,y].tileObj = starttransform.gameObject;
				tiles[x,y].type = "Start";
				tiles[x,y].isTaken = true;
				startpos = new Vector2(x,y);
				if(y == 0 || y==1 || y==2 ){
					//FaceDown();
					//p.transform.rotation.y = 270;
					playertransform = Instantiate (player, new Vector3 (x, 0, -y), Quaternion.Euler(new Vector3(0,270,0)));
					Debug.Log("Down");
					break;
				}
				if(y == totaldimension-1 || y==totaldimension-2 || y==totaldimension-3){
					//FaceUp();
					//p.transform.rotation.y = 90;
					playertransform = Instantiate (player, new Vector3 (x, 0, -y), Quaternion.Euler(new Vector3(0,90,0)));
					Debug.Log("Up");
					break;
				}
				if(x == 0 || x==1 || x==2){
					//FaceRight();
					//p.transform.rotation.y = 180;
					playertransform = Instantiate (player, new Vector3 (x, 0, -y), Quaternion.Euler(new Vector3(0,180,0)));
					Debug.Log("Right");
					break;
				}
				if(x==totaldimension-1 || x==totaldimension-2 || x==totaldimension-3){
					//FaceLeft();
					playertransform = Instantiate (player, new Vector3 (x, 0, -y), Quaternion.identity);
					Debug.Log("Left");
					break;
				}
				break;
			case sgoal:
				goaltransform = Instantiate (floor_goal, new Vector3 (x, 0.162f, -y), Quaternion.identity);
				tiles[x,y].type = "Goal";
				tiles[x,y].isTaken = true;
				tiles[x,y].tileObj = goaltransform.gameObject;
				mysnowh = Random.Range(0f,0.09f);
				//Instantiate (floor_snow, new Vector3 (x, -mysnowh, -y), Quaternion.identity);

				break;
			case sfloor_ice:
			//	Instantiate (floor_ice, new Vector3 (x, -y, 0), Quaternion.identity);
				break;
			case sfloor_wall:
				//Transform instantiator = floor_wall;
			Debug.Log("x is " + x + "and y is " + y);
				int numerator = Random.Range(0,6);
				mysnowh = 0.04f;//Random.Range(0.05f,0.10f);
				int[] validchoices = {0,90,180,270};
				int randomint = validchoices[Random.Range(0,validchoices.Length)];
				Instantiate (floor_snow, new Vector3 (x, -mysnowh, -y),  Quaternion.Euler(new Vector3(0,randomint,0)));
				if(Random.Range(0,10) > 4){
					if(y>2){
						if(tiles[x,y-1].type != "Wood"){
						int mynum2 = Random.Range(0,3);
						Instantiate (floor_trees[mynum2], new Vector3 (x, 0, -y), Quaternion.identity);							
						}
						else{
							Instantiate (floor_rocks[numerator], new Vector3 (x, 0, -y), Quaternion.identity);
						}						
					}
					else{

						int mynum2 = Random.Range(0,3);
						Instantiate (floor_trees[mynum2], new Vector3 (x, 0, -y), Quaternion.identity);	
					}	

					
				}
				else{
				Instantiate (floor_rocks[numerator], new Vector3 (x, 0, -y), Quaternion.identity);

				}
				//Instantiate (floor_wall, new Vector3 (x, 0, -y), Quaternion.identity);
				tiles[x,y].type = "Wall";
				tiles[x,y].isTaken = true;
				/*Collider[] colliderswall = Physics.OverlapSphere(new Vector3(x,0,-y), .5f);
					foreach (Collider component in colliderswall) {
						Debug.Log(component);
						if (component.tag == "Tile") {	
							//Debug.Log(component.gameObject.transform.position);	
							component.gameObject.GetComponent<Renderer>().material.color = mygray;
							//Destroy(component.gameObject);

							break;
						}
					}	*/
				//instantiator = Instantiate (instantiator, new Vector3 (x, -y, 0), Quaternion.identity);
				//instantiator.GetComponent<Wall_Behaviour>().Start();
				break;
			case sfloor_hole:
				Collider[] colliders = Physics.OverlapSphere(new Vector3(x,0,-y), .5f);
					foreach (Collider component in colliders) {
						Debug.Log(component);
						if (component.tag == "Tile") {	
							//Debug.Log(component.gameObject.transform.position);	
							component.gameObject.SetActive(false);

							Debug.Log(component.gameObject.activeSelf);
							Instantiate (floor_hole, new Vector3 (x, 0, -y), Quaternion.identity);
							tiles[x,y].type = "Hole";
							tiles[x,y].isTaken = true;
							//Destroy(component.gameObject);

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
		//					Debug.Log("Assign Rock");
				break;
			case sfloor_wood:
				Collider[] collidersw = Physics.OverlapSphere(new Vector3(x,0,-y), .5f);
				foreach (Collider component in collidersw) {
		//							Debug.Log(component);
					if (component.tag == "Tile") {	
						//Debug.Log(component.gameObject.transform.position);	
						//component.gameObject.SetActive(false);
		//								Debug.Log(component.gameObject.activeSelf);
						Instantiate (floor_wood, new Vector3 (x, 0, -y), Quaternion.identity);							
						tiles[x,y].type = "Wood";
						tiles[x,y].isTaken = true;
						//Destroy(component.gameObject);

						break;
					}
				}		
				//Instantiate (floor_wood, new Vector3 (x, 0, -y), Quaternion.identity);
				//tiles[x,y].type = "Wood";
				//tiles[x,y].isTaken = true;
				//break;
				break; 
			case sfloor_left:
				//Instantiate	(floor_left, new Vector3 ((float)(x+0.8), (float)0.5,(float)(-y)), floor_left.transform.rotation);
				Instantiate (floor_left, new Vector3 (x, 0, -y), Quaternion.Euler(new Vector3(0,270,0)));

				//tiles[x,y].type = "Left";
				//tiles[x,y].isTaken = true;					
				break;
			case sfloor_right:
				Instantiate	(floor_right, new Vector3 (x,0,-y), Quaternion.Euler(new Vector3(0,90,0)));
				//tiles[x,y].type = "Right";
				//tiles[x,y].isTaken = true;
				break;
			case sfloor_up:
				//Instantiate	(floor_up, new Vector3 (x, (float)0.5,(float)zed), floor_up.transform.rotation);
				Instantiate (floor_up, new Vector3 (x, 0, -y), Quaternion.Euler(new Vector3(0,0,0)));

				//tiles[x,y].type = "Up";
				//tiles[x,y].isTaken = true;
				break;
			case sfloor_down:
				Instantiate	(floor_down, new Vector3 (x, 0, -y), Quaternion.Euler(new Vector3(0,180,0))	);
				//tiles[x,y].type = "Down";
				//tiles[x,y].isTaken = true;
				break;
			case sfloor_fragile:
				Collider[] collidersf = Physics.OverlapSphere(new Vector3(x,0,-y), .5f);
				foreach (Collider component in collidersf) {
		//							Debug.Log(component);
					if (component.tag == "Tile") {	
						//Debug.Log(component.gameObject.transform.position);	
						component.gameObject.SetActive(false);
		//								Debug.Log(component.gameObject.activeSelf);
						Instantiate (floor_fragile, new Vector3 (x, 0, -y), Quaternion.identity);							
						tiles[x,y].type = "Fragile";
						tiles[x,y].isTaken = true;
						//Destroy(component.gameObject);

						break;
					}
				}		
				break;
				//Instantiate (floor_fragile, new Vector3 (x, 0, -y), Quaternion.identity);
				//tiles[x,y].type = "Fragile";
				////tiles[x,y].isTaken = true;
				//break;
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
			populateIce();
		//					Debug.Log(jagged[y][x].Substring(0,1));
			int hintx = int.Parse(jagged[y][x].Substring(1,1));
			int hinty = int.Parse(jagged[y][x].Substring(2,1));
			if(jagged[y][x].Substring(0,1) == "T"){
				LevelStorer.efficientturns = int.Parse(jagged[y][x].Substring(1,2));				
			}
			else{
				switch(jagged[y][x].Substring(0,1)){

				case sfloor_left:
					//LevelManager.piecetiles.Add (Instantiate	(floor_left, new Vector3 (2+piecenums, 0, -totaldimension), Quaternion.Euler(new Vector3(0,270,0))));
					//LevelManager.myhints.Add(new Vector2 (hintx,hinty));
					LevelManager.hints.Add(new Hint("Left", hintx,hinty));
					//pieceHolder.AddPiece("Left");
					PlaceCreature("Left");
					break;
				case sfloor_right:
					//LevelManager.piecetiles.Add (Instantiate	(floor_right, new Vector3 (2+piecenums, 0, -totaldimension), Quaternion.Euler(new Vector3(0,90,0))));
					//LevelManager.myhints.Add(new Vector2 (hintx,hinty));
					//pieceHolder.AddPiece("Right");
					LevelManager.hints.Add(new Hint("Right", hintx,hinty));
					PlaceCreature("Right");
					break;
				case sfloor_up:
					//LevelManager.piecetiles.Add (Instantiate	(floor_up, new Vector3 (2+piecenums, 0, -totaldimension), Quaternion.Euler(new Vector3(0,0,0))));
					//LevelManager.myhints.Add(new Vector2 (hintx,hinty));
					//pieceHolder.AddPiece("Up");
					LevelManager.hints.Add(new Hint("Up", hintx,hinty));
					PlaceCreature("Up");
					break;
				case sfloor_down:
					//LevelManager.piecetiles.Add (Instantiate	(floor_down, new Vector3 (2+piecenums, 0, -totaldimension), Quaternion.Euler(new Vector3(0,180,0))));
					//LevelManager.myhints.Add(new Vector2 (hintx,hinty));
					//pieceHolder.AddPiece("Down");
					LevelManager.hints.Add(new Hint("Down", hintx,hinty));
					PlaceCreature("Down");
					break;
				case sfloor_rock:
					//LevelManager.piecetiles.Add (Instantiate (floor_rock, new Vector3 (2+piecenums, 0, -totaldimension), Quaternion.identity));
					//Debug.Log(LevelManager.piecetiles.Count);
					//LevelManager.myhints.Add(new Vector2 (hintx,hinty));
					//pieceHolder.AddPiece("Wall");
					LevelManager.hints.Add(new Hint("Wall", hintx,hinty));
					PlaceCreature("Wall");
					break;

				case ssfloor_left:
					//LevelManager.piecetiles.Add (Instantiate	(s_floor_left, new Vector3 (2+piecenums, 0, -totaldimension), Quaternion.Euler(new Vector3(0,270,0))));
					//LevelManager.myhints.Add(new Vector2 (hintx,hinty));
					//pieceHolder.AddPiece("LeftSeed");
					LevelManager.hints.Add(new Hint("LeftSeed", hintx,hinty));
					PlaceCreature("LeftSeed");
					break;
				case ssfloor_right:
					//pieceHolder.AddPiece("RightSeed");
					LevelManager.hints.Add(new Hint("RightSeed", hintx,hinty));
					PlaceCreature("RightSeed");
					// LevelManager.piecetiles.Add (Instantiate	(s_floor_right, new Vector3 (2+piecenums, 0, -totaldimension), Quaternion.Euler(new Vector3(0,90,0))));
					// LevelManager.myhints.Add(new Vector2 (hintx,hinty));
					break;
				case ssfloor_up:
					//pieceHolder.AddPiece("UpSeed");
					LevelManager.hints.Add(new Hint("UpSeed", hintx,hinty));
					PlaceCreature("UpSeed");
					// LevelManager.piecetiles.Add (Instantiate	(s_floor_up, new Vector3 (2+piecenums, 0, -totaldimension), Quaternion.Euler(new Vector3(0,0,0))));
					// LevelManager.myhints.Add(new Vector2 (hintx,hinty));
					break;
				case ssfloor_down:
				//	pieceHolder.AddPiece("DownSeed");
					LevelManager.hints.Add(new Hint("DownSeed", hintx,hinty));
					PlaceCreature("DownSeed");
					// LevelManager.piecetiles.Add (Instantiate	(s_floor_down, new Vector3 (2+piecenums, 0, -totaldimension),Quaternion.Euler(new Vector3(0,180,0))));
					// LevelManager.myhints.Add(new Vector2 (hintx,hinty));
					break;
				case ssfloor_rock:
				//Debug.Log("Wallseed");
					//LevelManager.piecetiles.Add (Instantiate (s_floor_rock, new Vector3 (2+piecenums, 0, -totaldimension), Quaternion.identity));
					//LevelManager.myhints.Add(new Vector2 (hintx,hinty));
					//pieceHolder.AddPiece("WallSeed");
					LevelManager.hints.Add(new Hint("WallSeed", hintx,hinty));
					PlaceCreature("WallSeed");
					break;
				}
				piecenums++;				
			}
			
		}
		if(jagged[y][x].Length ==2){	
			LevelStorer.efficientturns = int.Parse(jagged[y][x].Substring(1,1));
			Debug.Log("Assigned turns as " + LevelStorer.efficientturns);
		}
	}

	public void DrawRandomFromDataBase(){

	}

	public void DestroyAllExceptCamera(){
//		TurnBehaviour.turn = 0;
		//tileBank.Clear();
		GameObject[] gameobjects = GameObject.FindObjectsOfType <GameObject>();

		foreach (GameObject component in gameobjects)
		{
			if (component.tag != "MainCamera" && component.tag != "Canvas" && component.tag != "Tile") {
				component.SetActive(false);
				Destroy (component);
				//Debug.Log ("destroyed" + component);
			}
			if(component.tag == "Tile"){
				component.SetActive(false);
			}

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
				//Destroy(playertransform.gameObject)/*.SetActive(false)*/;

			}
			// if(starttransform != null){
			// 	Destroy(starttransform.gameObject)/*.SetActive(false)*/;
			// 	SpawnStart(startpos);
			// }
			goaltransform.gameObject.GetComponentInChildren<Animator>().SetInteger("Phase",0);
			GoalBehaviour.restartGoal();
			GoalBehaviour.goaling = false;
			Debug.Log(goaltransform);
			Debug.Log(goaltransform.gameObject.GetComponentInChildren<Animator>().GetInteger("Phase"));
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
			// if(starttransform != null){
			// 	Destroy(starttransform.gameObject)/*.SetActive(false)*/;
			// 	SpawnStart(startpos);
			// }
			DisappearStatue();

	}

	void DisappearStatue(){
		LevelBuilder.starttransform.transform.GetChild(0).gameObject.SetActive(false);
	}

	public void SpawnStart(Vector2 thevector){
		int x = (int)thevector.x;
		int y = (int)thevector.y;
		starttransform = Instantiate (floor_start, new Vector3 (x, 0, -y), Quaternion.identity);
		tiles[x,y].type = "Start";
		tiles[x,y].isTaken = true;		
	}

	public void SpawnPlayer(Vector2 thevector){
		Debug.Log(starttransform);
		Debug.Log(playertransform);
		int x = (int)thevector.x;
		int y = (int)thevector.y;
		playertransform = Instantiate (player, new Vector3 (x, 0, -y), Quaternion.Euler(new Vector3(0,0,0)));
		playertransform.eulerAngles = new Vector3(0f,playerInitialRotation,0f);

		//StartDirection((int)starttransform.position.x,-(int)starttransform.position.z);
	}
	public void populateIce(){
		iceTiles.Clear();
//		Debug.Log(totaldimension);
		for(int i = 0; i<totaldimension; i++){
			for(int j = 0; j<totaldimension; j++){
				if (tiles[i,j].type == "Ice"){
					iceTiles.Add(new Vector2(i,j));
				}
			}
		}
//		Debug.Log(iceTiles[0]);
//		Debug.Log(iceTiles[iceTiles.Count-1]);
	}
	public void PlaceCreature(string creaturetype){

		RemoveSolutions(creaturetype);
		int randomplace = Random.Range(0,iceTiles.Count);
		// if(creaturetype == "Up"){
		// 	randomplace = 6;
		// }
		// if(creaturetype == "RightSeed"){
		// 	randomplace = 3;
		// }

		//int randomplace = Random.Range(0,iceTiles.Count);

		Vector2 tileplace = new Vector2(iceTiles[randomplace].x, iceTiles[randomplace].y);//vector 2 for 2d placement in tiles group
		Vector3 pieceplace = new Vector3(iceTiles[randomplace].x, 0, -iceTiles[randomplace].y);//vector 3 for gameworld position
		Tile currenttile = tiles[(int)tileplace.x,(int)tileplace.y];
		if(creaturetype != "Seed")
			LevelManager.placedPieces[(int)tileplace.x,(int)tileplace.y] = creaturetype;
		string myseedtype = "Not";
		if(creaturetype.Length>5){
			myseedtype = creaturetype.Substring(0,creaturetype.Length-4);
			creaturetype = creaturetype.Substring(creaturetype.Length-4,4);	
			Debug.Log("seedtype is " + myseedtype);
			LevelManager.placedPieces[(int)tileplace.x,(int)tileplace.y] = myseedtype;

		}
		switch(creaturetype){
		case "Wall":
			Transform wallpiece = Instantiate (floor_rock, pieceplace, Quaternion.identity);
			currenttile.isTaken = true;
			currenttile.type = "Wall";
			wallpiece.gameObject.GetComponentInChildren<Animator>().SetInteger("Phase",2);
			PieceHolders.placedpieces.Add(wallpiece.gameObject.GetComponent<Dragger>());
			break;
		case "Left":
			Transform leftpiece = Instantiate (floor_left, pieceplace, Quaternion.Euler(new Vector3(0,270,0)));
			leftpiece.gameObject.GetComponentInChildren<Animator>().SetInteger("Phase",2);
			currenttile.isTaken = true;
			currenttile.type = "Wall";
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
			currenttile.isTaken = true;
			currenttile.type = "Wall";
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
			currenttile.isTaken = true;
			currenttile.type = "Wall";
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
			currenttile.isTaken = true;
			currenttile.type = "Wall";
			currenttile.tileObj = downpiece.gameObject;
			if(LevelBuilder.tiles[(int)tileplace.x, (int)tileplace.y+1].type == "Ice"){
				LevelBuilder.tiles[(int)tileplace.x, (int)tileplace.y+1].type = downpiece.gameObject.GetComponent<Dragger>().myType;		
			}	

			else{
				LevelBuilder.tiles[(int)tileplace.x, (int)tileplace.y+1].isSideways = "Down";
			}
			PieceHolders.placedpieces.Add(downpiece.gameObject.GetComponent<Dragger>());
			break;
		case "Seed":
			Transform seedpiece;
			switch(myseedtype){
			case "Wall":
				seedpiece = Instantiate (s_floor_rock, pieceplace, Quaternion.identity);
				currenttile.tileObj = seedpiece.gameObject;	
				PieceHolders.placedpieces.Add(seedpiece.gameObject.GetComponent<Dragger>());	
			currenttile.isTaken = true;
			currenttile.type = "Seed";
			Debug.Log("Seed type is " + myseedtype);
			currenttile.seedType = myseedtype;
				break;
			case "Left":
				seedpiece = Instantiate (s_floor_left, pieceplace, Quaternion.Euler(new Vector3(0,270,0)));
				currenttile.tileObj = seedpiece.gameObject;	
				PieceHolders.placedpieces.Add(seedpiece.gameObject.GetComponent<Dragger>());
			currenttile.isTaken = true;
			currenttile.type = "Seed";
			Debug.Log("Seed type is " + myseedtype);
			currenttile.seedType = myseedtype;					
				break;
			case "Up":
				seedpiece = Instantiate (s_floor_up, pieceplace, Quaternion.Euler(new Vector3(0,0,0)));
				currenttile.tileObj = seedpiece.gameObject;	
				PieceHolders.placedpieces.Add(seedpiece.gameObject.GetComponent<Dragger>());
			currenttile.isTaken = true;
			currenttile.type = "Seed";
			Debug.Log("Seed type is " + myseedtype);
			currenttile.seedType = myseedtype;
				break;
			case "Right":
				seedpiece = Instantiate (s_floor_right, pieceplace, Quaternion.Euler(new Vector3(0,90,0)));
				currenttile.tileObj = seedpiece.gameObject;	
				PieceHolders.placedpieces.Add(seedpiece.gameObject.GetComponent<Dragger>());
			currenttile.isTaken = true;
			currenttile.type = "Seed";
			Debug.Log("Seed type is " + myseedtype);
			currenttile.seedType = myseedtype;
				break;
			case "Down":
				seedpiece = Instantiate (s_floor_down, pieceplace, Quaternion.Euler(new Vector3(0,180,0)));
				currenttile.tileObj = seedpiece.gameObject;	
				PieceHolders.placedpieces.Add(seedpiece.gameObject.GetComponent<Dragger>());
			currenttile.isTaken = true;
			currenttile.type = "Seed";
			Debug.Log("Seed type is " + myseedtype);
			currenttile.seedType = myseedtype;
				break;
			}
			//seedpiece.gameObject.GetComponentInChildren<Animator>().SetInteger("Phase",2);				
			break;
			//s_floor_left
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