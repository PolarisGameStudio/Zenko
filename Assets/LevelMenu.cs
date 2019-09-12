using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour {
	int levels;
	public GameObject buttonprefab;
	public List<GameObject> levelbuttons = new List<GameObject>();
	public GameObject mc;//maincanvas
	public SceneLoading sl;
	int buttonnum;
	public int currentfirst;
	public static int highestLevelSolved;
	public GameObject highestMarker;
	public static LevelMenu Instance;

	// Use this for initialization
	// void Awake(){
	// 	Instance = this;
	// 	Debug.Log(LevelMenu.Instance);
	// }
	void Start () {
		levels=20;
		sl = mc.GetComponent<SceneLoading>();
		/*for (int i = 1; i < 21; i++){
			createButton(i);

		}
		*/
		//currentfirst = 1;
		//Debug.Log("CURRENTFIRST" + currentfirst);
		//LevelMenu.highestLevelSolved = FindHighestSolved();
		//Debug.Log("CURHIGHEST IS"  + FindHighestSolved());
	}
	void Update(){
		Debug.Log(LevelMenu.highestLevelSolved);	
	}
	// Update is called once per frame
	public static int FindHighestSolved(){
		int curHighest = 0;
		int maxMaps = LevelStorer.leveldic.Count;
		for(int i=1; i<maxMaps+1; i++){
			string mystring = "Level"+i+"Rating";
			//Debug.Log(PlayerPrefs.GetInt(mystring));
			if(PlayerPrefs.GetInt(mystring)>0){
				curHighest = i;
			}	
		}
		Debug.Log(curHighest+1 + " IS CURHIGHEST");
		return curHighest+1;	
	}

	void createButton(int num){
		GameObject curbutton = Instantiate(buttonprefab, new Vector3(0, 0, 0), Quaternion.identity);
		curbutton.transform.parent = this.transform;	
		curbutton.transform.localScale = new Vector3(1,1,1);
		Button btn = curbutton.GetComponent<Button>();
		buttonnum = num;
		btn.onClick.AddListener(delegate{sl.LoadLevel(num);});
		curbutton.transform.GetChild(0).GetComponent<Text>().text = num.ToString();
		/*if(num != 1){
			if(LevelStorer.leveldic[num-1].rating == 0){//previous level has no stars
				curbutton.transform.GetChild(0).gameObject.SetActive(false);	
				curbutton.transform.GetChild(1).gameObject.SetActive(true);			

			}
			else {
				if(LevelStorer.leveldic[num].rating == 1){
					curbutton.transform.GetChild(3).GetChild(1).GetChild(1).gameObject.SetActive(true);
				}
				if(LevelStorer.leveldic[num].rating == 2){
					curbutton.transform.GetChild(3).GetChild(1).GetChild(0).gameObject.SetActive(true);
					curbutton.transform.GetChild(3).GetChild(1).GetChild(2).gameObject.SetActive(true);
				}
				if(LevelStorer.leveldic[num].rating == 3){
					curbutton.transform.GetChild(3).GetChild(1).GetChild(0).gameObject.SetActive(true);
					curbutton.transform.GetChild(3).GetChild(1).GetChild(1).gameObject.SetActive(true);
					curbutton.transform.GetChild(3).GetChild(1).GetChild(2).gameObject.SetActive(true);
				}
			}
		}
		else{*/
				//Debug.Log(LevelStorer.leveldic[num].rating);
		if(LevelStorer.leveldic[num].islocked == true && num != 0){
			curbutton.transform.GetChild(1).gameObject.SetActive(true);	
			curbutton.transform.GetChild(2).gameObject.SetActive(true);	
		}
		if(LevelStorer.leveldic[num].rating == 1){
			curbutton.transform.GetChild(3).GetChild(1).GetChild(1).gameObject.SetActive(true);
		}
		if(LevelStorer.leveldic[num].rating == 2){
			curbutton.transform.GetChild(3).GetChild(1).GetChild(0).gameObject.SetActive(true);
			curbutton.transform.GetChild(3).GetChild(1).GetChild(2).gameObject.SetActive(true);
		}
		if(LevelStorer.leveldic[num].rating == 3){
			curbutton.transform.GetChild(3).GetChild(1).GetChild(0).gameObject.SetActive(true);
			curbutton.transform.GetChild(3).GetChild(1).GetChild(1).gameObject.SetActive(true);
			curbutton.transform.GetChild(3).GetChild(1).GetChild(2).gameObject.SetActive(true);
		}			

		//}


		//curbutton
		levelbuttons.Add(curbutton);
	}



	void addFunction(){
		sl.LoadLevel(buttonnum);
	}

	public void clearMenu(){
		for (int i = 0; i < levelbuttons.Count; i++){
			levelbuttons[i].SetActive(false);
			Destroy(levelbuttons[i]);

		}
		levelbuttons.Clear();
	}
	public void populateMenuDown(){
		if(currentfirst >20){
			clearMenu();
			for (int i = currentfirst-20; i < currentfirst; i++){
				createButton(i);

			}
			currentfirst = currentfirst -20;	
		}
	}
	public void populateMenu(){
		Debug.Log(currentfirst + " is starting first");
		if(currentfirst==0 | currentfirst==null){
			currentfirst = 1;
		}
		for (int i = currentfirst; i < currentfirst+20; i++){
			createButton(i);
		}		
	}

	public void populateMenuUp(){
		if(currentfirst <181){
			clearMenu();
			for (int i = currentfirst+20; i < currentfirst+40; i++){
				createButton(i);

			}
			currentfirst = currentfirst + 20;	
		}
	}
}
