using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDictionary : MonoBehaviour
{
	public static TutorialDictionary Instance;
	public static Dictionary<int,string[]> tutDic = new Dictionary<int,string[]>();
	public static bool initialized;

	string[] tut_1 = new string[]{"Hello there!, my name is Zenko, or is it?" , "Do this over there"};
	string[] tut_2 = new string[]{"This is level 2"};
	string[] tut_5 = new string[]{"Level 5 nice", "Hope you're having fun"};
	string[] tut_6 = new string[]{"beastmode"};
	string[] tut_10 = new string[]{"tententen"};

	void Awake(){
		Instance = this;
		if (!initialized)
		PopulateDictionary();
		initialized = true;
	}

	public void PopulateDictionary(){
		tutDic.Add(1,tut_1);
		tutDic.Add(2,tut_2);
		tutDic.Add(5,tut_5);
		tutDic.Add(6,tut_6);
		tutDic.Add(10,tut_10);
		Debug.Log(tutDic[1]);
		if(tutDic.ContainsKey(3))
		Debug.Log(tutDic[3]);
	}

}
