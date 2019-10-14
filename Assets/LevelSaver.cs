using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.IO;

public class LevelSaver : MonoBehaviour {
	StreamWriter sl;
	public static List<string> currentmap = new List<string>();
	public static List<string> newmap = new List<string>();
	public static LevelSaver Instance;
	public static string[][]oldMapCoords;
	public static string oldHints;
	public static string newHints;
	public static string[][]newMapCoords;
	public static int totaldimension;
	public static string name;
	//public static string[] currentmap;
	// Use this for initialization
	void Start () {
		//Instance = this;
		string path = Application.dataPath + "/exceptionalmaps.txt";
		sl = File.CreateText(path);		
	}
	
	// Update is called once per frame
	//void Update () {
		
	//}
	public void StoreLevel(){
		foreach(string line in currentmap){
			sl.WriteLine(line);
		}
	}
	/*public static void StoreBiggie(){
		foreach(string line in currentmap){
			sl.WriteLine(line);
		}		
	}*/
	void OnApplicationQuit(){
		sl.Close();
	}
	public void RotateClockWise(){
		oldMapCoords = Jaggy(currentmap);
		newmap = new List<string>();
		Debug.Log(oldMapCoords.Length);
		Debug.Log(oldMapCoords[oldMapCoords.Length-2][0]);
		for(int i=0; i<oldMapCoords.Length-1; i++){
			string newLine = "";
			for(int j = 0; j<oldMapCoords.Length-1;j++){
				//Debug.Log("Position " + i + " "  + j);
				//Debug.Log(oldMapCoords[i][j] + " are the coord");
				if(oldMapCoords[i][j] != " " || oldMapCoords[i][j] != "")
				newLine = newLine + oldMapCoords[totaldimension-1-j][i].ToString() + " ";
			}
			//Debug.Log(newLine);

			newmap.Add(newLine);
		}
		string[] hintArray = Regex.Split (oldHints, " ");
		newHints = "";
		foreach(string hint in hintArray){
			if(hint.StartsWith("T")){
				newHints =newHints + hint;
			}
			else{
				Debug.Log(hint);
				Debug.Log(RotateHintClockwise(hint));
				newHints = newHints + RotateHintClockwise(hint) + " ";

			}
		}
		WriteNew();
	}
	public void MirrorY(){
		oldMapCoords = Jaggy(currentmap);
		newmap = new List<string>();
		Debug.Log(oldMapCoords.Length);
		Debug.Log(oldMapCoords[oldMapCoords.Length-2][0]);
		for(int i=0; i<oldMapCoords.Length-1; i++){
			string newLine = "";
			for(int j = 0; j<oldMapCoords.Length-1;j++){
				//Debug.Log("Position " + i + " "  + j);
				//Debug.Log(oldMapCoords[i][j] + " are the coord");
				if(oldMapCoords[i][j] != " " || oldMapCoords[i][j] != "")
				newLine = newLine + oldMapCoords[totaldimension-1-i][j].ToString() + " ";
			}
			//Debug.Log(newLine);

			newmap.Add(newLine);
		}
		string[] hintArray = Regex.Split (oldHints, " ");
		newHints = "";
		foreach(string hint in hintArray){
			if(hint.StartsWith("T")){
				newHints =newHints + hint;
			}
			else{
				//Debug.Log(hint);
				//Debug.Log(RotateHintClockwise(hint));
				newHints = newHints + MirrorHintY(hint) + " ";

			}
		}
		WriteNew();		
	}
	public void MirrorX(){
		oldMapCoords = Jaggy(currentmap);
		newmap = new List<string>();
		Debug.Log(oldMapCoords.Length);
		Debug.Log(oldMapCoords[oldMapCoords.Length-2][0]);
		for(int i=0; i<oldMapCoords.Length-1; i++){
			string newLine = "";
			for(int j = 0; j<oldMapCoords.Length-1;j++){
				//Debug.Log("Position " + i + " "  + j);
				//Debug.Log(oldMapCoords[i][j] + " are the coord");
				if(oldMapCoords[i][j] != " " || oldMapCoords[i][j] != "")
				newLine = newLine + oldMapCoords[i][totaldimension-1-j].ToString() + " ";
			}
			//Debug.Log(newLine);

			newmap.Add(newLine);
		}
		string[] hintArray = Regex.Split (oldHints, " ");
		newHints = "";
		foreach(string hint in hintArray){
			if(hint.StartsWith("T")){
				newHints =newHints + hint;
			}
			else{
				//Debug.Log(hint);
				//Debug.Log(RotateHintClockwise(hint));
				newHints = newHints + MirrorHintX(hint) + " ";

			}
		}
		WriteNew();		
	}
	public string RotateHintClockwise(string Hint){
		int x = int.Parse(Hint.Substring(1,1));
		int y = int.Parse(Hint.Substring(2,1));
		Debug.Log(x);
		Debug.Log(y);
		int newx = totaldimension-1-y;
		int newy = x;
		Debug.Log(newx);
		Debug.Log(newy);
		string pieceLetter ="";
		switch(Hint.Substring(0,1)){
		case "D":
			pieceLetter = "L";
			break;
		case "d":
			pieceLetter = "l";
			break;
		case "R":
			pieceLetter = "D";
			break;
		case "r":
			pieceLetter = "d";
			break;
		case "U":
			pieceLetter = "R";
			break;
		case "u":
			pieceLetter = "r";
			break;
		case "L":
			pieceLetter = "U";
			break;
		case "l":	
			pieceLetter = "u";
			break;
		case "P":
			pieceLetter = "P";
			break;
		case "p":
			pieceLetter = "p";
			break;
		}

		return pieceLetter + newx.ToString() + newy.ToString();
	}
	public string MirrorHintY(string Hint){
		int x = int.Parse(Hint.Substring(1,1));
		int y = int.Parse(Hint.Substring(2,1));
		Debug.Log(x);
		Debug.Log(y);
		int newx = x;
		int newy = totaldimension-1-y;
		Debug.Log(newx);
		Debug.Log(newy);
		string pieceLetter ="";
		switch(Hint.Substring(0,1)){
		case "D":
			pieceLetter = "U";
			break;
		case "d":
			pieceLetter = "u";
			break;
		case "R":
			pieceLetter = "R";
			break;
		case "r":
			pieceLetter = "r";
			break;
		case "U":
			pieceLetter = "D";
			break;
		case "u":
			pieceLetter = "d";
			break;
		case "L":
			pieceLetter = "L";
			break;
		case "l":	
			pieceLetter = "l";
			break;
		case "P":
			pieceLetter = "P";
			break;
		case "p":
			pieceLetter = "p";
			break;
		}
		return pieceLetter + newx.ToString() + newy.ToString();
	}
	public string MirrorHintX(string Hint){
		int x = int.Parse(Hint.Substring(1,1));
		int y = int.Parse(Hint.Substring(2,1));
		Debug.Log(x);
		Debug.Log(y);
		int newx = totaldimension-1-x;
		int newy = y;
		Debug.Log(newx);
		Debug.Log(newy);
		string pieceLetter="";
		switch(Hint.Substring(0,1)){
		case "D":
			pieceLetter = "D";
			break;
		case "d":
			pieceLetter = "d";
			break;
		case "R":
			pieceLetter = "L";
			break;
		case "r":
			pieceLetter = "l";
			break;
		case "U":
			pieceLetter = "U";
			break;
		case "u":
			pieceLetter = "u";
			break;
		case "L":
			pieceLetter = "R";
			break;
		case "l":	
			pieceLetter = "r";
			break;
		case "P":
			pieceLetter = "P";
			break;
		case "p":
			pieceLetter = "p";
			break;
		}
		return pieceLetter + newx.ToString() + newy.ToString();
	}
	public void WriteNew(){
		name = currentmap[0];
		currentmap = new List<string>();
		Debug.Log("CALLING WRITE NEW");
		Debug.Log(newmap.Count);
		sl.WriteLine(name);
		currentmap.Add(name);
		foreach(string line in newmap){
			Debug.Log(line);
			sl.WriteLine(line);
			currentmap.Add(line);
		}		
		sl.WriteLine(newHints);
		currentmap.Add(newHints);
		sl.WriteLine("");
		currentmap.Add("");
	}
	string[][] Jaggy(List<string> map){
		// newmap = new List<string>();
		// string firstline = map[0];
		// LevelSaver.currentmap.Add(firstline);
		Debug.Log("map size " + map.Count);
		bool donescanning = false;
		int linenumber = 1;
		while(!donescanning){
			if(map[linenumber] == ""){
				donescanning = true;
				totaldimension = linenumber-2;
				Debug.Log(totaldimension);
			}	
			linenumber++;
		}

		string[] lines = new string[totaldimension];

		for (int i=0; i<totaldimension; i++){
			Debug.Log(map[1+i]);
			lines[i] = map[1+i];
		}

		//Debug.Log(lines[lines.Length]);

		string[][] levelBase = new string [totaldimension+1][];
//		Debug.Log(lines);
		for (int i = 0; i<totaldimension; i++){
			string[] stringsOfLine = Regex.Split (lines [i], " ");
			levelBase [i] = stringsOfLine;
		}
		oldHints = map[totaldimension+1];
		Debug.Log(oldHints + " is oldhints");
		return levelBase;
	}

}
