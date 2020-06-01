using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAds : MonoBehaviour
{

    public static GoogleAds Instance;

    private InterstitialAd interstitial;

    public RewardedAd rewardVideo;

    public RewardedAd potdVideo;

    public int levelsInSession;
    public int[] levelsToShowAd;
    public int potdNum;
    public bool isPotd;

    // Start is called before the first frame update
    void Awake()
    {
        // Debug.Log("INITIALIZING MOBIELADS");
        if(Instance == null){
            Instance = this;
            levelsToShowAd = new int[] {7,16,51,101,161,191};
            DontDestroyOnLoad(this.gameObject);
            //return;
        }
        else{
            Destroy(this.gameObject);
            Debug.Log("Destroyed LevelStorer");
            return;
        }

        AddInitCounter();

        #if UNITY_ANDROID
            MobileAds.Initialize("ca-app-pub-3301322474937909~4906291296");
        // Debug.Log("INITIALIZED MOBILEADS");
        #endif

        #if UNITY_EDITOR
        	Debug.Log("UNITY EDITOR,UNITY EDITOR,UNITY EDITOR,UNITY EDITOR,UNITY EDITOR,UNITY EDITOR,UNITY EDITOR,UNITY EDITOR");
        #endif

    	#if UNITY_IOS
        //MobileAds.Initialize("ca-app-pub-3301322474937909~4906291296");
        #endif

        #if UNITY_STANDALONE
            
            //LevelManager.adFree = true;
        #endif 
        // #if UNITY_ANDROID
        // MobileAds.Initialize("ca-app-pub-3301322474937909~4906291296");
        // #endif
    }
    void AddInitCounter(){
        if(!PlayerPrefs.HasKey("InitNum")){
            PlayerPrefs.SetInt("InitNum", 0);
        }
        else{
            PlayerPrefs.SetInt("InitNum", PlayerPrefs.GetInt("InitNum") + 1);
        }
        Debug.Log("COUNTER AT " + PlayerPrefs.GetInt("InitNum"));
    }
    void Start(){
        levelsInSession = 0;
        
        #if UNITY_ANDROID
        RequestInterstitial();
        RequestHintAd();
        RequestPotdAd();
        //this.potdVideo = RewardBasedVideoAd.Instance;
        //RequestFirstRewardBasedVideo();
        //RequestFirstPotdAd();
        #endif
    }
    void Update(){
    	//Debug.Log(levelsToShowAd[0] + " " + levelsToShowAd[1] + " " + levelsToShowAd[2] + " " + levelsToShowAd[3] + " " + levelsToShowAd[4]);
    }
    public void RequestHintAd(){
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3301322474937909/3389088666";
        #elif UNITY_IOS
            string adUnitId = "ca-app-pub-3301322474937909/3389088666";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create an empty ad request.
        // Load the rewarded video ad with the request.
        this.rewardVideo = new RewardedAd(adUnitId);

        AdRequest request = new AdRequest.Builder().Build();
        
        this.rewardVideo.OnUserEarnedReward += HandleOnRewardAdClosed;

        this.rewardVideo.OnAdClosed += HintClosedEarly;

        this.rewardVideo.LoadAd(request);
    }

    public void RequestPotdAd(){
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3301322474937909/3389264645";
        #elif UNITY_IOS
            string adUnitId = "ca-app-pub-3301322474937909/3389264645";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        this.potdVideo = new RewardedAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        //AddTestDevice("7B4A528D487015EA780FDA9E0F1541EB").
        // Load the rewarded video ad with the request.

        this.potdVideo.OnUserEarnedReward += HandleOnRewardAdClosed;

        this.potdVideo.OnAdClosed += PotdClosedEarly;

        this.potdVideo.LoadAd(request);
        
    }

    private void HintClosedEarly(object sender, EventArgs args){
        RequestHintAd();
    }

    private void PotdClosedEarly(object sender, EventArgs args){
        RequestPotdAd();
    }

    private void RequestInterstitial()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3301322474937909/8967775483";
        #elif UNITY_IOS
            string adUnitId = "ca-app-pub-3301322474937909/8967775483";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        //.AddTestDevice("7B4A528D487015EA780FDA9E0F1541EB").
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
        
        this.interstitial.OnAdClosed += HandleOnAdClosed;
    }
    public void ShowInterstitial(){
    	levelsInSession++;
        if(PlayerPrefs.GetInt("InitNum") == 4 || PlayerPrefs.GetInt("InitNum") == 11 || PlayerPrefs.GetInt("InitNum") == 20 || PlayerPrefs.GetInt("InitNum") == 20){
            //Debug Log, Do you wish to rate this app? Not now, Yes
            PlayerPrefs.SetInt("Initnum", PlayerPrefs.GetInt("Initnum") + 1);
            //Application.OpenURL ("market://details?id=" + Application.identifier);
            
        }
        #if UNITY_ANDROID
        if(LevelManager.adFree){
 
            return;
        }
        if (this.interstitial.IsLoaded()) {
        	if(IsInList(levelsInSession)){
                this.interstitial.Show();   		
        	}
        }
        #endif
        // if(IsInList(levelsInSession)){
        // 	Debug.Log("ISINLIST");
        // }


    }
    private void HandleOnAdClosed(object sender, EventArgs args){
        this.interstitial.Destroy();
        RequestInterstitial();
        if(levelsInSession == 16 || levelsInSession == 15){
            //DISABLEADSMENU
            SceneLoading.Instance.buyMenu.SetActive(true);
            LevelManager.isdragging = true;
        }    
    }
    private bool IsInList(int num){
    	foreach(int x in levelsToShowAd){
    		if (x == num){
    			return true;
    		}
    	}
    	return false;
    }	

    // private void RequestFirstPotdAd(){
    //     #if UNITY_ANDROID
    //         string adUnitId = "ca-app-pub-3301322474937909/3389264645";
    //     #elif UNITY_IOS
    //         string adUnitId = "ca-app-pub-3301322474937909/3389264645";
    //     #else
    //         string adUnitId = "unexpected_platform";
    //     #endif

    //     // Create an empty ad request.
    //     AdRequest request = new AdRequest.Builder().Build();
    //     // Load the rewarded video ad with the request.
    //     this.potdVideo.LoadAd(request, adUnitId);
        
    //     this.potdVideo.OnAdRewarded += HandleOnPotdAdClosed;        
    // }



    // public void RequestFirstRewardBasedVideo(){
    //     #if UNITY_ANDROID
    //         string adUnitId = "ca-app-pub-3301322474937909/3389088666";
    //     #elif UNITY_IOS
    //         string adUnitId = "ca-app-pub-3301322474937909/3389088666";
    //     #else
    //         string adUnitId = "unexpected_platform";
    //     #endif

    //     // Create an empty ad request.
    //     AdRequest request = new AdRequest.Builder().Build();
    //     // Load the rewarded video ad with the request.
    //     this.rewardVideo.LoadAd(request, adUnitId);
        
    //     this.rewardVideo.OnAdRewarded += HandleOnRewardAdClosed;
    // }



    // public void RequestRewardBasedVideo(){
    //     #if UNITY_ANDROID
    //         string adUnitId = "ca-app-pub-3301322474937909/3389088666";
    //     #elif UNITY_IOS
    //         string adUnitId = "ca-app-pub-3301322474937909/3389088666";
    //     #else
    //         string adUnitId = "unexpected_platform";
    //     #endif

    //     // Create an empty ad request.
    //     AdRequest request = new AdRequest.Builder().Build();
    //     //.AddTestDevice("7B4A528D487015EA780FDA9E0F1541EB")
    //     // Load the rewarded video ad with the request.
    //     this.rewardVideo.LoadAd(request, adUnitId);
    // }

    private void HandleOnRewardAdClosed(object sender, EventArgs args){
        
        Debug.Log("CLOSED HINT");
        if(!isPotd)
    	PieceHolders.Instance.RewardHint();
    	else{
            Swiping.mydirection = "Null";
            LevelMenu.UnlockPotdLevel(potdNum);
        }

        //RequestRewardBasedVideo();

      	//Call hint function

    }

    private void HandleOnPotdAdClosed(object sender, EventArgs args){
        //RequestPotdAd();
       //LevelMenu.UnlockPotdLevel(potdNum);
        Debug.Log("CLOSED POTD");
        if(!isPotd)
        PieceHolders.Instance.RewardHint();
        else{
            Swiping.mydirection = "Null";
            LevelMenu.UnlockPotdLevel(potdNum);
        }        
        

        //RequestRewardBasedVideo();

        //Call hint function

    }

    public void UserOptToOpenPotd(){
        Debug.Log("OPT POTD");
    	isPotd = true;
        potdNum = LevelMenu.levelToUnlock;
        #if UNITY_ANDROID
        if (potdVideo.IsLoaded()) {
            potdVideo.Show();
        }
        else{
            //TryAgainScreen();
            RequestPotdAd();
            Swiping.mydirection = "Null";
            LevelMenu.UnlockPotdLevel(potdNum);
        }
        #endif

        #if UNITY_EDITOR
        Swiping.mydirection = "Null";
        LevelMenu.UnlockPotdLevel(potdNum);
        #endif
    }

    

    public void UserOptToWatchAd()
    {
    	isPotd = false;

        if (rewardVideo.IsLoaded()) {
            rewardVideo.Show();
        }
        else{

            PieceHolders.Instance.RewardHint();
            //TryAgainScreen();
            RequestHintAd(); 


        }


            //PieceHolders.Instance.RewardHint();
    }

    private void TryAgainScreen(){
        if (!Social.localUser.authenticated){
            GameObject.Find("TryAgainScreen").transform.GetChild(1).gameObject.SetActive(true);
        
        }
        else{
            GameObject.Find("TryAgainScreen").transform.GetChild(0).gameObject.SetActive(true);
        
        }
        //Resources.FindObjectsOfTypeAll("TypeAgainScreen").gameObject.SetActive(true);

    }
}
