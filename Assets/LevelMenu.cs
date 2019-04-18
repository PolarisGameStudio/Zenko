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

	// Use this for initialization
	void Start () {
		levels=20;
		sl = mc.GetComponent<SceneLoading>();
		/*for (int i = 1; i < 21; i++){
			createButton(i);

		}
		currentfirst = 1;*/
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
		for (int i = currentfirst; i < currentfirst+20; i++){
			createButton(i);
		}		
	}

	public void populateMenuUp(){
		if(currentfirst <81){
			clearMenu();
			for (int i = currentfirst+20; i < currentfirst+40; i++){
				createButton(i);

			}
			currentfirst = currentfirst + 20;	
		}
	}
}
