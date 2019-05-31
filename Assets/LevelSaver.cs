using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelSaver : MonoBehaviour {
	StreamWriter sl;
	public static List<string> currentmap = new List<string>();
	//public static string[] currentmap;
	// Use this for initialization
	void Start () {
		string path = Application.dataPath + "/exceptionalmaps.txt";
		sl = File.CreateText(path);		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
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
}
