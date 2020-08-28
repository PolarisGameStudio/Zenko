using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

public class MapsHolder : MonoBehaviour
{
    public static MapsHolder Instance;
	public static string[] levelsPotd;
	public static string[] levelsAdventure;
    public static List<int> startersAdventure;
	public static List<int> startersPotd;
    bool adventureLoaded;
    bool potdLoaded;
    public static bool mapsLoaded;
    
    void Awake(){
        Instance = this;

        //LoadChecker();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(initPotd());
        StartCoroutine(initAdventure());
		StartCoroutine(LoadChecker());
    }

    public static void RePotd(){
        Instance.initPotd();
    }

	//feeds levelPotd string array from textfile.
    public IEnumerator initPotd(){ 
		startersPotd = new List<int>();
		string file = "FilteredMaps.txt";
		string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, file);
		string result = " ";
		if (filePath.Contains ("://") || filePath.Contains(":///"))  {
			Debug.Log("contains ://");
			UnityWebRequest www = UnityWebRequest.Get (filePath);
			yield return www.SendWebRequest ();
			result = www.downloadHandler.text;
		} 
		else{
			Debug.Log("Doesnt contain ://");
			result = System.IO.File.ReadAllText (filePath);
		}
		string text = result;
		string[] lines = Regex.Split(text, "\r?\n");
		for(int i =0; i<lines.Length;i++){
			startersPotd.Add(i);
			int mapsize = int.Parse(lines[i].Substring(3,1));		
			if(lines[i].Length == 5){
				mapsize = int.Parse(lines[i].Substring(3,2));
			}			
			i = i + mapsize + 2;
		}
        Debug.Log("Loaded Potd Maps");
		levelsPotd = (string[])lines.Clone();
        potdLoaded = true;
	}

	//turns the map .txt into string array
	public IEnumerator initAdventure(){ //feeds Adventure string array from textfile.
		startersAdventure = new List<int>();
		string file = "AdventureLevels.txt";
		string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, file);
		string result = " ";
		if (filePath.Contains ("://") || filePath.Contains(":///"))  {
			UnityWebRequest www = UnityWebRequest.Get (filePath);
			yield return www.SendWebRequest ();
			result = www.downloadHandler.text;
		} 
		else{
			result = System.IO.File.ReadAllText (filePath);
		}
		string text = result;
		string[] lines = Regex.Split(text, "\r?\n");
		bool nextismap;
		nextismap = true;
		for(int i =0; i<lines.Length;i++){
			if(nextismap){
				startersAdventure.Add(i);
			}
			nextismap = false;
			if(lines[i] == ""){
				nextismap = true;
			}			
		}
        Debug.Log("Loaded Adventure Maps");
		levelsAdventure = (string[])lines.Clone();
        adventureLoaded = true;
	}

    
    IEnumerator LoadChecker(){
        Debug.Log("GONNA CHECK LOAD");
		while(!adventureLoaded && !potdLoaded){
			Debug.Log("Still Checking Maps Loaded");
			yield return null;
		}
        Debug.Log("BOTH LOADED");
        mapsLoaded = true;
    }
}
