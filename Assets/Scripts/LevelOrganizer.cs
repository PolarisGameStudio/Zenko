using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;

public class LevelOrganizer : MonoBehaviour {

	List<int> startersPotd;	
	private string filePath;
	private string result;
	public static string[] levelsPotd;
	public LevelSaver ls;
	StreamWriter sl;
	public void Start(){
		string path = Application.dataPath + "/newbigmaps.txt";
		sl = File.CreateText(path);	
		StartCoroutine(unshufflePotd());
	}
	public IEnumerator unshufflePotd(){ //feeds levelPotd string array from textfile.
		startersPotd = new List<int>();
		string file = "DoubleHitMaps3.txt";
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
		string[] lines = Regex.Split(text, "\r?\n");
		Debug.Log(lines[0]);
		for(int i =0; i<lines.Length;i++){
			startersPotd.Add(i);
			int mapsize = int.Parse(lines[i].Substring(3,1));		
			if(lines[i].Length == 5){
				mapsize = int.Parse(lines[i].Substring(3,2));
			}			
			i = i + mapsize + 2;
		}
		levelsPotd = (string[])lines.Clone();
		//now turn the lines into subdivides
		for (int place=0; place<startersPotd.Count; place++){
			//int place = x;
			LevelSaver.currentmap = new List<string>();	
			string firstline = levelsPotd[startersPotd[place]];
			LevelSaver.currentmap.Add(firstline);
			int totaldimension = int.Parse(firstline.Substring(3,1));
			if(firstline.Length == 5){
				totaldimension = int.Parse(firstline.Substring(3,2));
			}
			lines = new string[totaldimension+1];
			for (int i=0; i<totaldimension+1; i++){
				lines[i] = levelsPotd[startersPotd[place]+1+i];
				LevelSaver.currentmap.Add(lines[i]);
			}
			LevelSaver.currentmap.Add("");
			string[][] levelBase = new string [totaldimension+1][];
			for (int i = 0; i<totaldimension+1; i++){
				string[] stringsOfLine = Regex.Split (lines [i], " ");
				levelBase [i] = stringsOfLine;
			}
			for (int y = 0; y < totaldimension+1; y++) {
//				Debug.Log(jagged[y].Length);
				for (int x = 0; x < levelBase[y].Length; x++) {
					//double zed = (-y) + (-0.8);
					//Debug.Log(jagged[y][x]);
					//placeOnWorld(jagged,y,x);
					// if (jagged[y][x].Length = 3){
					// 	if()
					// }
					if(levelBase[y][x].Length ==3){
						if(levelBase[y][x].Substring(0,1) == "T"){
							int turns = int.Parse(levelBase[y][x].Substring(1,2));	
							if(turns>9){
								Debug.Log("Mapmorethan9");
								foreach(string line in LevelSaver.currentmap){
									sl.WriteLine(line);
								}
							}			
						}							
					}
						
				}
			} 
		}
	}
	void OnApplicationQuit(){
		sl.Close();
	}
}
// 	public void drawPotd(int num){
// 		pieceHolder.reset();
// 		string[][] jagged = readPotd(num);
// 		Debug.Log("TOTAL DIMENSION IS" + totaldimension);
// 		tiles = new Tile [totaldimension, totaldimension];
// //		Debug.Log(tiles[9,9]);
// //		Debug.Log(tiles[10,10]);
// 		if(!iscreated){
// 			CreateBase ();
// 		}
// 		//CreateBase();
// 		CreateOuterBase();
// 		PlaceBase();
// 		LevelManager.piecetiles = new List<Transform>();
// 		LevelManager.myhints = new List<Vector2>();
// 		LevelManager.hintnum = 0;
// 		LevelManager.hints = new List<Hint>();
// 		PieceHolders.placedpieces = new List<Dragger>();
// 		piecenums = 0;
// 		for (int y = 0; y < totaldimension+1; y++) {
// 			Debug.Log(jagged[y].Length);
// 			for (int x = 0; x < jagged[y].Length; x++) {
// 				double zed = (-y) + (-0.8);
// 				//Debug.Log(jagged[y][x]);
// 				placeOnWorld(jagged,y,x);
							
// 			}
// 		} 
// }