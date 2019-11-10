using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PotdHolder : MonoBehaviour
{

	public static Dictionary<string, int> monthBank = new Dictionary<string,int>();

	//monthsizes starting in Nov2019
	public static int[] monthSizes = new int[] {30, 31, 
		31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31, 
		31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};

	public static PotdHolder Instance;

	void Awake(){
		Instance = this;
	}

	public void PopulateMonthBank(){
		monthBank.Add("112019", 0);
		monthBank.Add("122019", 0);
		monthBank.Add("012019", 0);
		monthBank.Add("November2019", 0);
		monthBank.Add("November2019", 0);
		monthBank.Add("November2019", 0);
		monthBank.Add("November2019", 0);
		monthBank.Add("November2019", 0);
		monthBank.Add("November2019", 0);
		monthBank.Add("November2019", 0);
		monthBank.Add("November2019", 0);
		monthBank.Add("November2019", 0);
		monthBank.Add("November2019", 0);
		monthBank.Add("November2019", 0);



	}

}
