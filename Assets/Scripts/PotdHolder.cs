using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PotdHolder : MonoBehaviour
{

	public static List<int[]> monthBank = new List<int[]>();

	public static Dictionary<string, int> monthTranslator = new Dictionary<string,int>();

	public List<Sprite> monthSprites = new List<Sprite>();

	public List<Sprite> yearSprites = new List<Sprite>();
	//monthsizes starting in Nov2019

	public List<Sprite> monthSpritesSpanish = new List<Sprite>();

	public List<Sprite> yearSpritesSpanish = new List<Sprite>();

	public static int[] monthSizes = new int[] {30, 31, 
		31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31, 
		31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};

	public static PotdHolder Instance;

	public void Awake(){
		Instance = this;
		if(monthBank.Count == 0)
		PopulateMonthBank();
	}
	
	public void PopulateMonthBank(){

		monthBank.Add(new int[]{0,30});
		monthBank.Add(new int[]{30,31});
		monthBank.Add(new int[]{61,31});
		monthBank.Add(new int[]{92,29});
		monthBank.Add(new int[]{121,31});
		monthBank.Add(new int[]{152,30});
		monthBank.Add(new int[]{182,31});
		monthBank.Add(new int[]{213,30});
		monthBank.Add(new int[]{243,31});
		monthBank.Add(new int[]{274,31});
		monthBank.Add(new int[]{305,30});
		monthBank.Add(new int[]{335,31});
		monthBank.Add(new int[]{366,30});
		monthBank.Add(new int[]{396,31});
		monthBank.Add(new int[]{427,31});
		monthBank.Add(new int[]{458,28});
		monthBank.Add(new int[]{486,31});
		monthBank.Add(new int[]{517,30});
		monthBank.Add(new int[]{547,31});
		monthBank.Add(new int[]{578,30});
		monthBank.Add(new int[]{608,31});
		monthBank.Add(new int[]{639,31});
		monthBank.Add(new int[]{670,30});
		monthBank.Add(new int[]{700,31});
		monthBank.Add(new int[]{731,30});
		monthBank.Add(new int[]{761,31});

		monthTranslator.Add("112019", 0);
		monthTranslator.Add("122019", 1);
		monthTranslator.Add("012020", 2);
		monthTranslator.Add("022020", 3);
		monthTranslator.Add("032020", 4);
		monthTranslator.Add("042020", 5);
		monthTranslator.Add("052020", 6);
		monthTranslator.Add("062020", 7);
		monthTranslator.Add("072020", 8);
		monthTranslator.Add("082020", 9);
		monthTranslator.Add("092020", 10);
		monthTranslator.Add("102020", 11);
		monthTranslator.Add("112020", 12);
		monthTranslator.Add("122020", 13);
		monthTranslator.Add("012021", 14);
		monthTranslator.Add("022021", 15);
		monthTranslator.Add("032021", 16);
		monthTranslator.Add("042021", 17);
		monthTranslator.Add("052021", 18);
		monthTranslator.Add("062021", 19);
		monthTranslator.Add("072021", 20);
		monthTranslator.Add("082021", 21);
		monthTranslator.Add("092021", 22);
		monthTranslator.Add("102021", 23);
		monthTranslator.Add("112021", 24);
		monthTranslator.Add("122021", 25);
	}

}
