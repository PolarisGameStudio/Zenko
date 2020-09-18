using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CompleteProject;
public class BuyMenu : MonoBehaviour
{
	public Text priceString;
    public Text title;
    public static BuyMenu Instance;
    // Start is called before the first frame update
    void OnEnable()
    {
        Instance = this;
        AssignPriceMessage();
    }

    //Changes the text to display the message
    void AssignPriceMessage(){
    	float price  = Purchaser.Instance.PriceOfNoAds();
    	priceString.text = "$" + price.ToString();
        Debug.Log(price);
    }

    public void AssignTitle(string newTitle){
        title.text = newTitle;
    }
}
