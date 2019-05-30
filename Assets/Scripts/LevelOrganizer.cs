using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LevelOrganizer : MonoBehaviour {

	List<int> startersPotd;	
	private string filePath;
	private string result;
	public static string[] levelsPotd;


	// public Start(){
	// 	//(unshufflePotd());
	// }
// 	public IEnumerator unshufflePotd(){ //feeds levelPotd string array from textfile.
// 		startersPotd = new List<int>();
// 		string file = "Ch4_Big.txt";
// 		filePath = System.IO.Path.Combine(Application.streamingAssetsPath, file);
// 		Debug.Log (filePath + "FILEPAPAPATH");
// 		result = " ";
// 		if (filePath.Contains ("://") || filePath.Contains(":///"))  {
// 			UnityWebRequest www = UnityWebRequest.Get (filePath);
// 			yield return www.SendWebRequest ();
// 			result = www.downloadHandler.text;
// 			Debug.Log (result);
// 		} 
// 		else{
// 			result = System.IO.File.ReadAllText (filePath);
// 		}
// 		string text = result;
// 		//string text = System.IO.File.ReadAllText(System.IO.Path.Combine (Application.streamingAssetsPath, "Ch4_Easy.txt"));
// 		string[] lines = Regex.Split(text, "\r?\n");
// 		Debug.Log(lines[0]);
// 		for(int i =0; i<lines.Length;i++){
// 			startersPotd.Add(i);
// //			Debug.Log(lines[i].Length);
// //			Debug.Log(lines[i]);
// 			int mapsize = int.Parse(lines[i].Substring(3,1));		
// 			if(lines[i].Length == 5){
// 				//Debug.Log("size it up for " );
// 				mapsize = int.Parse(lines[i].Substring(3,2));
// 			}			
// 			i = i + mapsize + 2;
// 		}
// 		levelsPotd = (string[])lines.Clone();
// 		if(LevelManager.ispotd){
// 			drawPotd(LevelManager.levelnum);
// 		}
	//}
}