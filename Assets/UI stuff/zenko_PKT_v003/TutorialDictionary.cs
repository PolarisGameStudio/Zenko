using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDictionary : MonoBehaviour
{
	public static TutorialDictionary Instance;
	public static Dictionary<int,string[]> tutDic = new Dictionary<int,string[]>();
	public static bool initialized;

    string[] tut_1 = new string[]{"Hi!, My name is Zenko", "Help me get to that shiny flower by sliding your finger through the screen."};
	string[] tut_2 = new string[]{"That moving piece over there is Pedro", "Place him in the right place so I can use him as a wall", "BEFORE moving me to the flower!"};
	//string[] tut_2 = new string[]{"This place is a mess! I can't get to my goal like this", "Touch the pieces and arrange them on the board", "BEFORE moving me to the flower!"};
	string[] tut_3 = new string[]{"I wouldn't want to end in that hole over there!" };

	string[] tut_6 = new string[]{"Those friendly cube like creatures are called Pedro by the way."};
	string[] tut_12 = new string[]{"That yellow flower just started to grow!","That must mean a new spring perhaps isn't too far away", "Don't put anything over it!, better to preserve it"};
    string[] tut_41 = new string[]{"Those blue birds are called Icarus", "They send a strong air current in front of them", "If I go near I'll change my direction!"};
    string[] tut_50 = new string[]{"If I stop over those fragile tiles, I'll fall!"};
    string[] tut_59 = new string[]{"I think the air is faster than the fragile tiles..."};
    string[] tut_68 = new string[]{"I don't always need the wind to get to the flower!"};
    string[] tut_81 = new string[]{"That pink bag becomes Pedro if I go over it!"};
    string[] tut_121 = new string[]{"That blue bag has an Icarus sleeping inside!", "I'll wake it if I go over it."};
    string[] tut_122 = new string[]{"You can tell by the arrow in the bag, the direction it'll blow when it wakes!"};
	string[] tut_160 = new string[]{"This is the end of our adventure for now", "Be sure to come back soon for more levels and new friends!"};
	void Awake(){
		Instance = this;
		if (!initialized)
		PopulateDictionary();
		initialized = true;
	}

	public void PopulateDictionary(){
		tutDic.Add(1,tut_1);
		tutDic.Add(2,tut_2);
		tutDic.Add(3,tut_3);
		tutDic.Add(6,tut_6);
		tutDic.Add(12,tut_12);
		tutDic.Add(41,tut_41);
		tutDic.Add(50,tut_50);
		tutDic.Add(59,tut_59);
		tutDic.Add(68,tut_68);
		tutDic.Add(81,tut_81);
		tutDic.Add(121,tut_121);
		tutDic.Add(122,tut_122);
		tutDic.Add(160,tut_160);
	}

}
