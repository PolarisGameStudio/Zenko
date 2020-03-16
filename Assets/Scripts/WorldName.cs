using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WorldName : MonoBehaviour
{
	public static WorldName Instance;
	public Sprite[] world;
    public Sprite[] worldSpanish;
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
    public Sprite[] slash;
	public List<Sprite[]> numbers = new List<Sprite[]>();
	public GameObject[] AdvHolder;

    public GameObject[] PotdHolder;


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
        //Debug.Log("ASSIGNEDLEVELNAME");
    	AssignAdventureTag(worldnum, level);

    }
    public void AssignAdventureTag(int worldnum, int level){
        RectTransform rt = AdvHolder[0].GetComponent<RectTransform>();
        if(worldnum >4)
        worldnum = 4;
        if(LanguageHandler.IsEnglish()){
            AdvHolder[0].GetComponent<Image>().sprite = world[worldnum-1];
            rt.sizeDelta = new Vector2((float)254.5, rt.sizeDelta.y);
            rt.localPosition = new Vector3((float)-64.2,0,0);
        }
    	
        else
        {
            AdvHolder[0].GetComponent<Image>().sprite = worldSpanish[worldnum-1];
            rt.sizeDelta = new Vector2((float)322, rt.sizeDelta.y);
            rt.localPosition = new Vector3((float)-91,0,0);
        }
        
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
    public void AssignPotdDate(int index){
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(true);
        string firstDate = "2019-11-01";
        System.DateTime date = System.DateTime.Parse(firstDate);
        
        System.DateTime levelDate = date.AddDays(index);

        string dateString = levelDate.ToString("ddMMyyyy");
        Debug.Log(dateString + "IS THE STRING");
        PotdHolder[0].GetComponent<Image>().sprite = numbers[int.Parse(dateString.Substring(0,1))][0];
        PotdHolder[1].GetComponent<Image>().sprite = numbers[int.Parse(dateString.Substring(1,1))][0];
        PotdHolder[2].GetComponent<Image>().sprite = numbers[int.Parse(dateString.Substring(2,1))][0];
        PotdHolder[3].GetComponent<Image>().sprite = numbers[int.Parse(dateString.Substring(3,1))][0];
        PotdHolder[4].GetComponent<Image>().sprite = numbers[int.Parse(dateString.Substring(4,1))][0];
        PotdHolder[5].GetComponent<Image>().sprite = numbers[int.Parse(dateString.Substring(5,1))][0];
        PotdHolder[6].GetComponent<Image>().sprite = numbers[int.Parse(dateString.Substring(6,1))][0];
        PotdHolder[7].GetComponent<Image>().sprite = numbers[int.Parse(dateString.Substring(7,1))][0];
    }
}
