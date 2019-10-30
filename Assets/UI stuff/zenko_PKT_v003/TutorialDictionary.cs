using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDictionary : MonoBehaviour
{
	public static TutorialDictionary Instance;
	public static Dictionary<int,string[]> tutDic = new Dictionary<int,string[]>();
	public static bool initialized;
	public static Dictionary<int,string[]> randomTutDic = new Dictionary<int,string[]>();

    string[] tut_1 = new string[]{"Hi!, My name is Zenko", "Help me get to that shiny flower by sliding your finger through the screen."};
	string[] tut_2 = new string[]{"That moving piece over there is Pedro", "Drag him into the right place so I can use him as a wall", "BEFORE moving me to the flower!"};
	//string[] tut_2 = new string[]{"This place is a mess! I can't get to my goal like this", "Touch the pieces and arrange them on the board", "BEFORE moving me to the flower!"};
	string[] tut_3 = new string[]{"I wouldn't want to end in that hole over there!" };
	string[] tut_6 = new string[]{"Make sure both Pedros are on the right place for me to get to the flower"};
	string[] tut_9 = new string[]{"You will only get 3 stars if you do the least amount of moves possible.", "Those dots next to the bar on the right represent the number of best moves"};
	string[] tut_12 = new string[]{"That yellow flower just started to grow!","That must mean a new spring perhaps isn't that far away", "Don't put anything over it!, better to preserve it"};
    string[] tut_41 = new string[]{"Those birds are called Icarus", "They send a strong air current in the tile right in front of them", "If I go near It'll change my direction!"};
    string[] tut_50 = new string[]{"That's a fragile tile", "It'll fall quickly, but don't worry, I can outrun them"};
	string[] tut_52 = new string[]{"Make sure not to linger on one of those fragile tiles or I'll fall", "I can go through them, but not stop on them"};				
    string[] tut_55 = new string[]{"I'm pretty sure Icarus can blow air faster than that fragile tile can fall"};
    string[] tut_68 = new string[]{"I don't always need the wind to get to the flower!"};
    string[] tut_81 = new string[]{"That bag with the circle becomes Pedro once I go over it!"};
    string[] tut_121 = new string[]{"That bag with the arrow has an Icarus sleeping inside!", "I'll wake it once I go over it."};
    string[] tut_122 = new string[]{"You can tell by the arrow in the bag, the direction it'll blow when it wakes!"};
	string[] tut_160 = new string[]{"This is the end of our adventure for now", "Be sure to come back soon for more levels and new friends!"};
	
    //Choose one of the dialogs to happen after 30s of innactivity

    string[] random1 = new string[] {"Focus! You can do it!"};
    string[] random2 = new string[] {"If you have trouble arranging the pieces, press that button with a bulb over there!"};
    string[] random3 = new string[] {"If you're stuck, Restart!"};
    //Choose one of the dialogs to happen after 5 consecutive deaths
    string[] random4 = new string[] {"You can restart the level and ask for a hint once I'm back in my starting position"};
    string[] random5 = new string[] {"We almost made it! Keep trying!"};
    string[] random6 = new string[] {"This time we'll make it, I'm sure!"};


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
		tutDic.Add(9,tut_9);
		tutDic.Add(12,tut_12);
		tutDic.Add(41,tut_41);
		tutDic.Add(50,tut_50);
		tutDic.Add(52,tut_52);
		tutDic.Add(55,tut_55);
		tutDic.Add(68,tut_68);
		tutDic.Add(81,tut_81);
		tutDic.Add(121,tut_121);
		tutDic.Add(122,tut_122);
		tutDic.Add(160,tut_160);
	}

}
