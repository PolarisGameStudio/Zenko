using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WorldName : MonoBehaviour
{
	public static WorldName Instance;
	public Sprite[] world;
	public Sprite[] dash;
	public Sprite[] num0;
	public Sprite[] num1;
	public Sprite[] num2;
	public Sprite[] num3;
	public Sprite[] num4;
	public Sprite[] num5;
	public Sprite[] num6;
	public Sprite[] num7;
	public Sprite[] num8;
	public Sprite[] num9;
	public List<Sprite[]> numbers = new List<Sprite[]>();
	public GameObject[] AdvHolder;
    // Start is called before the first frame update
    void Awake()
    {
    	Instance = this;
        numbers.Add(num0);
        numbers.Add(num1);
        numbers.Add(num2);
        numbers.Add(num3);
        numbers.Add(num4);
        numbers.Add(num5);
        numbers.Add(num6);
        numbers.Add(num7);
        numbers.Add(num8);
        numbers.Add(num9);

    }

    // Update is called once per frame

    public void AssignLevelName(int worldnum, int level){
    	AssignAdventureTag(worldnum, level);
    }
    public void AssignAdventureTag(int worldnum, int level){
    	AdvHolder[0].GetComponent<Image>().sprite = world[worldnum-1];
    	AdvHolder[1].GetComponent<Image>().sprite = numbers[worldnum][worldnum-1];
    	AdvHolder[2].GetComponent<Image>().sprite = dash[worldnum-1];

    	Vector3 digits = AssignLevelTag(level);
    	AdvHolder[3].GetComponent<Image>().sprite = numbers[(int)digits.x][worldnum-1];
    	if(digits.y != -1){
    	AdvHolder[4].GetComponent<Image>().sprite = numbers[(int)digits.y][worldnum-1];
    	AdvHolder[4].GetComponent<Image>().color = new Color(1,1,1,1);

    	}
    	else{
    	AdvHolder[4].GetComponent<Image>().color = new Color(1,1,1,0);
    	}
    	if(digits.z != -1){
    	AdvHolder[5].GetComponent<Image>().sprite = numbers[(int)digits.z][worldnum-1];
    	AdvHolder[5].GetComponent<Image>().color = new Color(1,1,1,1);

    	}
    	else{
    	AdvHolder[5].GetComponent<Image>().color = new Color(1,1,1,0);

    	}
    }
    public Vector3 AssignLevelTag(int level){
    	if(level<10){
    		return new Vector3(level, -1, -1);
    	}    
    	else if (level<100){
    		return new Vector3((int)(level/10),level%10,-1);
    	}	
    	else{
    		return new Vector3((int)(level/100), (level -(int)(level/100)*100)/10, level%10);
    	}
    }
}
