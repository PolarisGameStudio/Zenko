using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Holder{
	public string name;
	public int maxpieces;
	public int currentpieces;
	public GameObject mygameobject;
	public bool active;
	public List <Dragger> placedpieces;
}
public class PieceHolders : MonoBehaviour {
	public int holdersNumber;
	public GameObject pedroHolder;
	public GameObject leftHolder;
	public GameObject rightHolder;
	public GameObject upHolder;
	public GameObject downHolder;
	public int pedroNumber;
	public int leftNumber;
	public int rightNumber;
	public int upNumber;
	public int downNumber;
	// Use this for initialization
	void Awake () {
		initValues();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void initSize(){
		RectTransform rt = GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(holdersNumber*150, 150);
	}
	public void reset(){
		pedroHolder.SetActive(false);
		leftHolder.SetActive(false);
		rightHolder.SetActive(false);
		upHolder.SetActive(false);
		downHolder.SetActive(false);
		initValues();
	}
	public void initValues(){
	pedroNumber = 0;
	leftNumber = 0;
	rightNumber = 0;
	upNumber = 0;
	downNumber = 0;
	holdersNumber = 0;
	pedroHolder.GetComponent<Image>().color = Color.white;
	leftHolder.GetComponent<Image>().color = Color.white;
	upHolder.GetComponent<Image>().color = Color.white;
	rightHolder.GetComponent<Image>().color = Color.white;
	downHolder.GetComponent<Image>().color = Color.white;
	}
	public void AddPiece(string type){
		if(type == "Pedro" || type == "Wall"){
			if(pedroHolder.active){
				pedroNumber++;
			}
			else{
				pedroHolder.SetActive(true);
				holdersNumber++;
				pedroNumber++;
				initSize();
			}			
		}
		if(type == "Up"){
			if(upHolder.active){
				upNumber++;
			}
			else{
				upHolder.SetActive(true);
				holdersNumber++;
				upNumber++;
				initSize();
			}			
		}
		if(type == "Down"){
			if(downHolder.active){
				downNumber++;
			}
			else{
				downHolder.SetActive(true);
				holdersNumber++;
				downNumber++;
				initSize();
			}			
		}
		if(type == "Left"){
			if(leftHolder.active){
				leftNumber++;
			}
			else{
				leftHolder.SetActive(true);
				holdersNumber++;
				leftNumber++;
				initSize();
			}			
		}
		if(type == "Right"){
			if(rightHolder.active){
				rightNumber++;
			}
			else{
				rightHolder.SetActive(true);
				holdersNumber++;
				rightNumber++;
				initSize();
			}			
		}
		updateText(type);
	}
	public void updateValueUp(string type){
		if(type == "Pedro" || type == "Wall"){
			pedroNumber ++;
			pedroHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Pedro x" + pedroNumber;
			pedroHolder.GetComponent<Image>().color = Color.white;

		}
		if(type == "Up"){
			upNumber++;
			upHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Up x" + upNumber;
			upHolder.GetComponent<Image>().color = Color.white;
		}
		if(type == "Down"){
			downNumber++;
			downHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Down x" + pedroNumber;
			downHolder.GetComponent<Image>().color = Color.white;
		}
		if(type == "Left"){
			leftNumber++;
			leftHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Left x" + pedroNumber;
			leftHolder.GetComponent<Image>().color = Color.white;
		}
		if(type == "Right"){
			rightNumber++;
			rightHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Right x" + pedroNumber;
			rightHolder.GetComponent<Image>().color = Color.white;
		}		
	}
	public void unshadeImage(string type){
		if(type == "Pedro" || type == "Wall"){
			Color tempColor = pedroHolder.GetComponent<Image>().color;
			tempColor.a = 1f;
			pedroHolder.GetComponent<Image>().color = tempColor;
		}
		if(type == "Up"){
			Color tempColor = upHolder.GetComponent<Image>().color;
			tempColor.a = 1f;
			upHolder.GetComponent<Image>().color = tempColor;
		}
		if(type == "Down"){
			Color tempColor = downHolder.GetComponent<Image>().color;
			tempColor.a = 1f;
			downHolder.GetComponent<Image>().color = tempColor;
		}
		if(type == "Left"){
			Color tempColor = leftHolder.GetComponent<Image>().color;
			tempColor.a = 1f;
			leftHolder.GetComponent<Image>().color = tempColor;
		}
		if(type == "Right"){
			Color tempColor = rightHolder.GetComponent<Image>().color;
			tempColor.a = 1f;
			rightHolder.GetComponent<Image>().color = tempColor;
		}				
	}
	public bool isAvailable(string type){
		if(type == "Pedro" || type == "Wall"){
			if(pedroNumber!=0){
				return true;
			}
			else{
				return false;
			}
		}
		if(type == "Left"){
			if(leftNumber!=0){
				return true;
			}
			else{
				return false;
			}
		}
		if(type == "Right"){
			if(rightNumber!=0){
				return true;
			}
			else{
				return false;
			}
		}
		if(type == "Down"){
			if(downNumber!=0){
				return true;
			}
			else{
				return false;
			}
		}
		if(type == "Up"){
			if(upNumber!=0){
				return true;
			}
			else{
				return false;
			}
		}
		else return false;
	}
	public void updateValueDown(string type){
		if(type == "Pedro" || type == "Wall"){
			pedroNumber --;
			pedroHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Pedro x" + pedroNumber;
			if(pedroNumber == 0){
				pedroHolder.GetComponent<Image>().color = Color.gray;
			}

		}
		if(type == "Up"){
			upNumber--;
			upHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Up x" + upNumber;
			if(upNumber == 0){
				upHolder.GetComponent<Image>().color = Color.gray;
			}
		}
		if(type == "Down"){
			downNumber--;
			downHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Down x" + pedroNumber;
			if(downNumber == 0){
				downHolder.GetComponent<Image>().color = Color.gray;
			}
		}
		if(type == "Left"){
			leftNumber--;
			leftHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Left x" + pedroNumber;
			if(leftNumber == 0){
				leftHolder.GetComponent<Image>().color = Color.gray;
			}
		}
		if(type == "Right"){
			rightNumber--;
			rightHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Right x" + pedroNumber;
			if(rightNumber == 0){
				rightHolder.GetComponent<Image>().color = Color.gray;
			}
		}		
	}	
	public void updateText(string type){
		if(type == "Pedro" || type == "Wall"){
			pedroHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Pedro x" + pedroNumber;
		}
		if(type == "Up"){
			upHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Up x" + upNumber;
		}
		if(type == "Down"){
			downHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Down x" + downNumber;
		}
		if(type == "Left"){
			leftHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Left x" + leftNumber;
		}
		if(type == "Right"){
			rightHolder.transform.GetChild(0).transform.GetComponent<Text>().text = "Right x" + rightNumber;
		}
	}
}
