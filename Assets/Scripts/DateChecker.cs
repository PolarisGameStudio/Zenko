using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DateChecker : MonoBehaviour
{

	public static DateChecker Instance;

	public static int todayMonthIndex;

	public static int currentMonthIndex;

	//public Text txt;
	public static string todayDate;

	readonly string date = "http://worldclockapi.com/api/json/est/now";

	public string mmyyyy;

	public int dayInMonth;

	public int  mm;

	public int yyyy;

	public int todayIndex;

	public int currentIndex;

	public string firstDate = "2019-11-01";


	void Awake(){
		if(Instance == null){
			PopulateDate();
			Instance = this;
		}
		
		//StartCoroutine(SimpleGetRequest());

		

	}

	public void PopulateDate(){
		//txt.text = "Downloading";
		StartCoroutine(SimpleGetRequest());
	}

	IEnumerator SimpleGetRequest(){
		UnityWebRequest www = UnityWebRequest.Get(date);
		string text;

		yield return www.SendWebRequest();

		if(www.isNetworkError  || www.isHttpError){
			Debug.LogError(www.error);
			text = System.DateTime.Now.ToString();
			mmyyyy = System.DateTime.Now.Month.ToString("d2") + System.DateTime.Now.Year.ToString();
			
			dayInMonth = int.Parse(System.DateTime.Now.Day.ToString());
			currentMonthIndex = PotdHolder.monthTranslator[mmyyyy];
			todayMonthIndex = PotdHolder.monthTranslator[mmyyyy];

			mm = System.DateTime.Now.Month;
			yyyy=System.DateTime.Now.Year;
			string firstDate = "2019-11-01";
			string secondDate = System.DateTime.Now.ToString("yyyy-MM-dd");
			System.DateTime date = System.DateTime.Parse(firstDate);
			System.DateTime now = System.DateTime.Parse(secondDate);
			System.TimeSpan diff = now - date;
			Debug.Log((int)diff.Days);	
			todayIndex = diff.Days;
			currentIndex = diff.Days;
			todayDate = System.DateTime.Now.ToString("yyyy-MM-dd");
		}
		else{

			text = www.downloadHandler.text.Substring(30,10);
			mmyyyy =text.Substring(5,2) + text.Substring(0,4);
			dayInMonth = int.Parse(text.Substring(8,2));
			todayMonthIndex = PotdHolder.monthTranslator[mmyyyy];

			currentMonthIndex = PotdHolder.monthTranslator[mmyyyy];
			mm = int.Parse(text.Substring(5,2));
			yyyy= int.Parse(text.Substring(0,4));
			string firstDate = "2019-11-01";
			string secondDate = System.DateTime.Now.ToString("yyyy-MM-dd");
			System.DateTime date = System.DateTime.Parse(firstDate);
			System.DateTime now = System.DateTime.Parse(secondDate);
			System.TimeSpan diff = now - date;
			//Debug.Log((int)diff.Days);	
			todayIndex = diff.Days;
			currentIndex = diff.Days;	
			todayDate = System.DateTime.Now.ToString("yyyy-MM-dd");


		}
		// #if UNITY_EDITOR
		// todayDate = "2019-01-13";
		// #endif
		PotdUnlocker.Instance.Initiate();
		PotdShortcut.Instance.AssignPotdShortcutAssets(PotdUnlocker.Instance.keysAvailable);

	}

	public int[] ArrayDate(string date){
		int[] newArray = new int[8];
		newArray[0] = int.Parse(date.Substring(0,1));
		newArray[1] = int.Parse(date.Substring(1,1));
		newArray[2] = int.Parse(date.Substring(3,1));
		newArray[3] = int.Parse(date.Substring(4,1));
		newArray[4] = int.Parse(date.Substring(6,1));
		newArray[5] = int.Parse(date.Substring(7,1));
		newArray[6] = int.Parse(date.Substring(8,1));
		newArray[7] = int.Parse(date.Substring(9,1));
		return newArray;
	}

	public void Test(){
			mmyyyy ="012020";
			dayInMonth = 19;
			todayMonthIndex = PotdHolder.monthTranslator[mmyyyy];

			currentMonthIndex = PotdHolder.monthTranslator[mmyyyy];
			Debug.Log(todayMonthIndex + "Is number of month in");
			mm = 1;
			yyyy= 2020;		
	}
	public string GetDate(string text){
		return text.Substring(30, 10);
	}
}
