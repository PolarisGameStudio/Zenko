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
    void Start()
    {
        Instance = this;
        AssignPriceMessage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Changes the text to display the message
    void AssignPriceMessage(){
    	float price  = Purchaser.Instance.PriceOfNoAds();
    	priceString.text = "$" + price.ToString();
    }

    public void AssignTitle(string newTitle){
        title.text = newTitle;
    }
}
