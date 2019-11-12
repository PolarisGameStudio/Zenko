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

	readonly string date = "http://worldclockapi.com/api/json/est/now";

	public string mmyyyy;

	public int dayInMonth;

	public int  mm;

	public int yyyy;


	void Start(){
		Instance = this;
		//StartCoroutine(SimpleGetRequest());
		PopulateDate();

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
		}
		else{

			text = www.downloadHandler.text.Substring(30,10);
			mmyyyy =text.Substring(5,2) + text.Substring(0,4);
			dayInMonth = int.Parse(text.Substring(8,2));
			todayMonthIndex = PotdHolder.monthTranslator[mmyyyy];

			currentMonthIndex = PotdHolder.monthTranslator[mmyyyy];
			mm = int.Parse(text.Substring(5,2));
			yyyy= int.Parse(text.Substring(0,4));
			
			//Test();



			//first in array is first index in month
			//Debug.Log(PotdHolder.monthBank[mmyyyy][0]);
			
			//second in array is amount of days in month
			//Debug.Log(PotdHolder.monthBank[mmyyyy][1]);

		}
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
