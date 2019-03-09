using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LevelBuilder : MonoBehaviour {
	private string filePath;
	private string result;
//	IceTileHandler myhandler;
	//public Brain brain;

	public GameObject[] tile;
	 List<GameObject> tileBank = new List<GameObject>();
	//public static Transform icetile;

	public static int rows = 8;
	public static int cols = 8;
	bool renewBoard = false;
	public static Tile [,] tiles = new Tile[cols,rows];

	static int levelidtoload;

	static Vector2 startpos;

	public static Transform playertransform;

	public static Transform starttransform;

	public static Transform goaltransform;

	public static bool iscreated;

	List<GameObject> outertileBank = new List<GameObject>();

	int howfar;

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

	string[][] readFile(string file){
		string text = System.IO.File.ReadAllText (file);
		string[] lines = Regex.Split (text, "\n");
		int rows = lines.Length;
		string[][] levelBase = new string[rows][];
		for (int i = 0; i < lines.Length; i++) {
//			Debug.Log(lines[i]);
			string[] stringsOfLine = Regex.Split (lines [i], " ");
			levelBase [i] = stringsOfLine;
		}
		Debug.Log(levelBase);
		return levelBase;
	}

	string[][] readRandomFile(string file,int pos){
//		Debug.Log(pos);
		string text = System.IO.File.ReadAllText (file);
		string[] lines = Regex.Split (text, "\n");
		int rows = lines.Length;
		string[][] levelBase = new string[rows][];
		int initialpos = 1 + 11*pos;
		Debug.Log(lines[0]);
		for (int i = 0; i < 9; i++) {
//			Debug.Log(lines[i]);
			int currentpos = i+initialpos;
			string[] stringsOfLine = Regex.Split (lines [currentpos], " ");
			levelBase [i] = stringsOfLine;
		}

		Debug.Log(levelBase[7]);
		return levelBase;
	}

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




	void Start() {
	//LevelManager.newicarus = true;
	LevelManager.levelselector = this;

	//LevelManager.levelnum = 1;
	 
	//Debug.Log(LevelManager.levelnum + "isthenum");
	Debug.Log(LevelManager.levelnum);
	if (LevelManager.levelnum == null || LevelManager.levelnum == 0) {
		LevelManager.levelnum = -1;
		levelnum =-1;
		//LevelManager.levelnum = Random.Range(1,65);
		//Debug.Log(LevelManager.levelnum);

	}
	else{
		levelnum = LevelManager.levelnum;
	}
	//LevelManager.levelnum = 65;
	//levelnum = LevelManager.levelnum;
	//LevelStorer.Lookfor (LevelManager.levelnum);//assigns efficient turn according to dictionary.
	//DrawIce ();
	//DrawNextLevel (levelnum);
	CreateOuterBase();
	CreateBase();
	PlaceBase();
	TurnGraphics.SetTurnCounter(LevelStorer.efficientturns);
	if(LevelManager.levelnum < 0){
		LevelManager.levelnum = Random.Range(0,102);
		Debug.Log(LevelManager.levelnum);
		DrawNextLevel(-11);
	}
	else{
	DrawNextLevel(LevelManager.levelnum);
	}
	TurnGraphics.SetTurnCounter(LevelStorer.efficientturns);
	GameObject.Find("CurrencyHolder").GetComponentInChildren<Text>().text = GameManager.mycurrency.ToString();

	//Debug.Log("MEH");
	//GameObject objectp = GameObject.Find("TheCanvas");
	//IceTileHandler myhandler = objectp.GetComponent<IceTileHandler>();
	//Debug.Log(myhandler);
	//myhandler.GiveIce();


	}
	void Update(){
		if (Input.GetKeyDown(KeyCode.J)){
			//DrawNextLevel(LevelManager.levelnum);
			Debug.Log(tileBank.Count);
		}
	}


		/*if (LevelManager.readytodraw) {
			
			Debug.Log ("OW");
			DestroyAllExceptCamera ();
			DrawIce ();
			DrawNextLevel (levelnum);
			LevelManager.readytodraw = false;
		}*/
	//}
	/*public void DrawIce(){
		string filePath = System.IO.Path.Combine (Application.streamingAssetsPath, "8by8ice.txt");
		//checkAndroid ("8by8ice.txt");
		//Debug.Log (filePath);
		//string leveltext = ("Assets/Resources/8by8ice.txt");
		string[][] jagged = readFile (filePath);

		// create planes based on matrix
		for (int y = 0; y < jagged.Length; y++) {
			for (int x = 0; x < jagged [0].Length; x++) {
				switch (jagged [y] [x]) {
				case sfloor_ice:
					Instantiate (floor_ice, new Vector3 (x, -y, 0), Quaternion.identity);
					break;
				}
			}
		} 
	}*/
	public void CreateBase(){
		iscreated = true;
		int numCopies = rows*cols;
		for (int i =0; i<numCopies;i++){
			for(int j=0; j < tile.Length; j++){
				GameObject o = (GameObject) Instantiate(tile[j],new Vector3(-10,-10,0), tile[j].transform.rotation);
				o.SetActive(false);
				tileBank.Add(o);
			}
		}	
	}
	public void PlaceBase(){
		for (int r= 0; r< rows; r++){
			for (int c = 0; c<cols; c++){
				Vector3 tilePos = new Vector3(c,0,r);
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
		howfar = 0;
		for(int x=1; x<13; x++){
			howfar++;
			for(int y = 0; y<25;y++){
				InstantiateRandomEnvironment(new Vector2(-x,y));
				InstantiateRandomEnvironment(new Vector2(x+7,y));
				if(y!=0){
					InstantiateRandomEnvironment(new Vector2(-x,-y));
					InstantiateRandomEnvironment(new Vector2(x+7,-y));					
				}
			}
		}	
		howfar=3;
		for(int x=0;x<8;x++){
			for(int y=1; y<25; y++){
				InstantiateRandomEnvironment(new Vector2(x,y));
				InstantiateSnow(new Vector2(x,-y-7));				
			}
		}	
	}
	public void InstantiateRandomEnvironment(Vector2 xy){
		Instantiate (environment_ice, new Vector3 (xy.x, 0, xy.y), Quaternion.identity);
		int randomizer = Random.Range(0,10);
		float snowrandom = Random.Range(0f,.1f);
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
		//}
/*		if(xy.x<8){
			Instantiate (floor_ice, new Vector3 (xy.x, 0, xy.y), Quaternion.identity);
			mynum = Random.Range(0,6);
			Instantiate (floor_rocks[mynum], new Vector3 (xy.x, 0, xy.y), Quaternion.identity);				
		}
		if(xy.y ==0){
			Instantiate (floor_ice, new Vector3 (xy.y, 0, xy.x), Quaternion.identity);
			mynum = Random.Range(0,6);
			Instantiate (floor_rocks[mynum], new Vector3 (xy.y, 0, xy.x), Quaternion.identity);					
		}*/
		

	}
	public void InstantiateSnow(Vector2 xy){
		Instantiate (environment_ice, new Vector3 (xy.x, 0, xy.y), Quaternion.identity);
		int randomizer = Random.Range(0,10);
		float snowrandom = Random.Range(0f,.1f);
		//if(randomizer == 2){
		Instantiate (floor_snow, new Vector3 (xy.x, -snowrandom, xy.y), Quaternion.identity);		
	}


	public void PlaceOuterBase(){
		for(int x=1; x<15; x++){
			for(int y = 1; y<15;y++){

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
		if(myx<5){	
			//Debug.Log(myx + "" + myy);
			Debug.Log(tiles[myx+1,myy].type);
			Debug.Log(myx+1 + " " + myy );
			if(tiles[myx+1,myy].type!="Wall" && tiles[myx+1,myy].type!="Start" && tiles[myx+1,myy].type!=null){
			goaltransform.eulerAngles = new Vector3(0f,90f,0f);
			GoalBehaviour.isvertical = false;
			return;

			Debug.Log("Right");

			}


		}
		if(myx>4){
			if(tiles[myx-1,myy].type!="Wall" && tiles[myx-1,myy].type!="Start" && tiles[myx-1,myy].type!=null){
			goaltransform.eulerAngles = new Vector3(0f,270,0f);
			GoalBehaviour.isvertical = false;

			Debug.Log("Left");
			return;
			}
		}
		if(myy>4){
			if(tiles[myx,myy-1].type!="Wall" && tiles[myx,myy-1].type!="Start" && tiles[myx,myy-1].type!=null){
			goaltransform.eulerAngles = new Vector3(0f,0f,0f);
			Debug.Log("Up");
			GoalBehaviour.isvertical = true;

			return;	
			}
		}
		if(myy<5){
			if(tiles[myx,myy+1].type!="Wall" && tiles[myx,myy+1].type!="Start" && tiles[myx,myy+1].type!=null){
			goaltransform.eulerAngles = new Vector3(0f,180f,0f);
			Debug.Log("Down");
			GoalBehaviour.isvertical = true;


			return;			//rotate transform
			//return;
			}
		}
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
		float mysnowh;
		if(levelnumber < 0){
			Debug.Log(LevelManager.levelnum);
			jagged = readRandomFile(filePath, LevelManager.levelnum);
			//Debug.Log(randomer);
		}
		int piecenums = 0;
		Debug.Log(jagged.Length);	
		// create planes based on matrix
		for (int y = 0; y < 9; y++) {
//			Debug.Log(y);
			for (int x = 0; x < jagged [y].Length; x++) {
				double zed = (-y) + (-0.8);
				//Debug.Log(x + " + " + y);ww
//				Debug.Log(jagged[y][x]);
				if(jagged[y][x].Length == 1){
//					Debug.Log("1 in " + jagged[y][x]);
					switch (jagged [y] [x]) {
					case sstart:

						starttransform = Instantiate (floor_start, new Vector3 (x, 0, -y), Quaternion.identity);
						//GameObject p = player;
						//Instantiate (player, new Vector3 (x, 0, -y), Quaternion.identity);
						mysnowh = Random.Range(0f,0.12f);
						//Instantiate (floor_snow, new Vector3 (x, -mysnowh, -y), Quaternion.identity);


						Debug.Log(x + "+" + y);
						tiles[x,y].tileObj = starttransform.gameObject;
						tiles[x,y].type = "Start";
						tiles[x,y].isTaken = true;
						startpos = new Vector2(x,y);
						if(y == 0 || y==1){
							//FaceDown();
							//p.transform.rotation.y = 270;
							playertransform = Instantiate (player, new Vector3 (x, 0, -y), Quaternion.Euler(new Vector3(0,270,0)));
							Debug.Log("Down");
							break;
						}
						if(y == 6 || y==7){
							//FaceUp();
							//p.transform.rotation.y = 90;
							playertransform = Instantiate (player, new Vector3 (x, 0, -y), Quaternion.Euler(new Vector3(0,90,0)));
							Debug.Log("Up");
							break;
						}
						if(x == 0 || x==1){
							//FaceRight();
							//p.transform.rotation.y = 180;
							playertransform = Instantiate (player, new Vector3 (x, 0, -y), Quaternion.Euler(new Vector3(0,180,0)));
							Debug.Log("Right");
							break;
						}
						if(x==6 || x==7){
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
						mysnowh = Random.Range(0f,0.12f);
						//Instantiate (floor_snow, new Vector3 (x, -mysnowh, -y), Quaternion.identity);

						break;
					case sfloor_ice:
					//	Instantiate (floor_ice, new Vector3 (x, -y, 0), Quaternion.identity);
						break;
					case sfloor_wall:
						//Transform instantiator = floor_wall;
						int numerator = Random.Range(0,6);
						mysnowh = Random.Range(0.05f,0.12f);
						Instantiate (floor_snow, new Vector3 (x, -mysnowh, -y), Quaternion.identity);
						Instantiate (floor_rocks[numerator], new Vector3 (x, 0, -y), Quaternion.identity);
						//Instantiate (floor_wall, new Vector3 (x, 0, -y), Quaternion.identity);
						tiles[x,y].type = "Wall";
						tiles[x,y].isTaken = true;

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
//					Debug.Log(jagged[y][x].Substring(0,1));
					int hintx = int.Parse(jagged[y][x].Substring(1,1));
					int hinty = int.Parse(jagged[y][x].Substring(2,1));
					switch(jagged[y][x].Substring(0,1)){

					case sfloor_left:
						LevelManager.piecetiles.Add (Instantiate	(floor_left, new Vector3 (2+piecenums, 0, -8), Quaternion.Euler(new Vector3(0,270,0))));
						LevelManager.myhints.Add(new Vector2 (hintx,hinty));
						break;
					case sfloor_right:
						LevelManager.piecetiles.Add (Instantiate	(floor_right, new Vector3 (2+piecenums, 0, -8), Quaternion.Euler(new Vector3(0,90,0))));
						LevelManager.myhints.Add(new Vector2 (hintx,hinty));
						break;
					case sfloor_up:
						LevelManager.piecetiles.Add (Instantiate	(floor_up, new Vector3 (2+piecenums, 0, -8), Quaternion.Euler(new Vector3(0,0,0))));
						LevelManager.myhints.Add(new Vector2 (hintx,hinty));
						break;
					case sfloor_down:
						LevelManager.piecetiles.Add (Instantiate	(floor_down, new Vector3 (2+piecenums, 0, -8), Quaternion.Euler(new Vector3(0,180,0))));
						LevelManager.myhints.Add(new Vector2 (hintx,hinty));
						break;
					case sfloor_rock:
						LevelManager.piecetiles.Add (Instantiate (floor_rock, new Vector3 (2+piecenums, 0, -8), Quaternion.identity));
						//Debug.Log(LevelManager.piecetiles.Count);
						LevelManager.myhints.Add(new Vector2 (hintx,hinty));
						break;

					case ssfloor_left:
						LevelManager.piecetiles.Add (Instantiate	(s_floor_left, new Vector3 (2+piecenums, 0, -8), Quaternion.Euler(new Vector3(0,270,0))));
						LevelManager.myhints.Add(new Vector2 (hintx,hinty));
						break;
					case ssfloor_right:
						LevelManager.piecetiles.Add (Instantiate	(s_floor_right, new Vector3 (2+piecenums, 0, -8), Quaternion.Euler(new Vector3(0,90,0))));
						LevelManager.myhints.Add(new Vector2 (hintx,hinty));
						break;
					case ssfloor_up:
						LevelManager.piecetiles.Add (Instantiate	(s_floor_up, new Vector3 (2+piecenums, 0, -8), Quaternion.Euler(new Vector3(0,0,0))));
						LevelManager.myhints.Add(new Vector2 (hintx,hinty));
						break;
					case ssfloor_down:
						LevelManager.piecetiles.Add (Instantiate	(s_floor_down, new Vector3 (2+piecenums, 0, -8),Quaternion.Euler(new Vector3(0,180,0))));
						LevelManager.myhints.Add(new Vector2 (hintx,hinty));
						break;
					case ssfloor_rock:
						LevelManager.piecetiles.Add (Instantiate (s_floor_rock, new Vector3 (2+piecenums, 0, -8), Quaternion.identity));
						LevelManager.myhints.Add(new Vector2 (hintx,hinty));
						break;
					}
					piecenums++;
				}
				if(jagged[y][x].Length ==2){	
					LevelStorer.efficientturns = int.Parse(jagged[y][x].Substring(1,1));
				}			
			}
		} 
		GoalDirection((int)goaltransform.position.x,-(int)goaltransform.position.z);
		Debug.Log(LevelManager.myhints.Count);

		//PopulationManager.readytobrain = true;
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
				Destroy (component);
				//Debug.Log ("destroyed" + component);
			}
			if(component.tag == "Tile"){
				component.SetActive(false);
			}

		}
	}
	public void ResetPlayer(){
		Destroy(playertransform.gameObject)/*.SetActive(false)*/;
		Destroy(starttransform.gameObject)/*.SetActive(false)*/;
		SpawnPlayer(startpos);
		goaltransform.gameObject.GetComponentInChildren<Animator>().SetInteger("Phase",0);
		Debug.Log(goaltransform);
		Debug.Log(goaltransform.gameObject.GetComponentInChildren<Animator>().GetInteger("Phase"));



	}

	public void SpawnPlayer(Vector2 thevector){
		Debug.Log(starttransform);
		Debug.Log(playertransform);
		int x = (int)thevector.x;
		int y = (int)thevector.y;
		starttransform = Instantiate (floor_start, new Vector3 (x, 0, -y), Quaternion.identity);
		tiles[x,y].type = "Start";
		tiles[x,y].isTaken = true;

		if(y == 0 || y==1){
			//FaceDown();
			//p.transform.rotation.y = 270;
			playertransform = Instantiate (player, new Vector3 (x, 0, -y), Quaternion.Euler(new Vector3(0,270,0)));
			Debug.Log("Down");
			return;
		}
		if(y == 6 || y==7){
			//FaceUp();
			//p.transform.rotation.y = 90;
			playertransform = Instantiate (player, new Vector3 (x, 0, -y), Quaternion.Euler(new Vector3(0,90,0)));
			Debug.Log("Up");
			return;

		}
		if(x == 0 || x==1){
			//FaceRight();
			//p.transform.rotation.y = 180;
			playertransform = Instantiate (player, new Vector3 (x, 0, -y), Quaternion.Euler(new Vector3(0,180,0)));
			Debug.Log("Right");
			return;

		}
		if(x==6 || x==7){
			//FaceLeft();
			playertransform = Instantiate (player, new Vector3 (x, 0, -y), Quaternion.identity);
			Debug.Log("Left");
			return;

		}
	}
}