using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAds : MonoBehaviour
{

    public static GoogleAds Instance;

    private InterstitialAd interstitial;

    private RewardBasedVideoAd rewardVideo;

    private RewardBasedVideoAd potdVideo;

    public int levelsInSession;
    public int[] levelsToShowAd = new int[] {3,6,11,16,21,26,31,41,51,61,71,81,91,101,111,121,131,141,151,161,171,181,191};

    // Start is called before the first frame update
    void Awake()
    {
        // Debug.Log("INITIALIZING MOBIELADS");
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            //return;
        }
        else{
            Destroy(this.gameObject);
            Debug.Log("Destroyed LevelStorer");
            return;
        }
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
            LevelManager.adFree = true;
        #endif 
        // #if UNITY_ANDROID
        // MobileAds.Initialize("ca-app-pub-3301322474937909~4906291296");
        // #endif
    }
    void Start(){
        levelsInSession = 0;
        
        #if UNITY_ANDROID
        RequestInterstitial();
        this.rewardVideo = RewardBasedVideoAd.Instance;
        this.potdVideo = RewardBasedVideoAd.Instance;
        RequestFirstRewardBasedVideo();
        RequestFirstPotdAd();
        #endif
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

    }
    private void HandleOnAdClosed(object sender, EventArgs args){
        this.interstitial.Destroy();
        RequestInterstitial();
        if(levelsInSession == 6){
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

    private void RequestFirstPotdAd(){
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3301322474937909/3389264645";
        #elif UNITY_IOS
            string adUnitId = "ca-app-pub-3301322474937909/3389264645";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.potdVideo.LoadAd(request, adUnitId);
        
        this.potdVideo.OnAdRewarded += HandleOnPotdAdClosed;        
    }

    public void RequestPotdAd(){
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3301322474937909/3389264645";
        #elif UNITY_IOS
            string adUnitId = "ca-app-pub-3301322474937909/3389264645";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.potdVideo.LoadAd(request, adUnitId);
        
    }

    public void RequestFirstRewardBasedVideo(){
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3301322474937909/3389088666";
        #elif UNITY_IOS
            string adUnitId = "ca-app-pub-3301322474937909/3389088666";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardVideo.LoadAd(request, adUnitId);
        
        this.rewardVideo.OnAdRewarded += HandleOnRewardAdClosed;
    }



    public void RequestRewardBasedVideo(){
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3301322474937909/3389088666";
        #elif UNITY_IOS
            string adUnitId = "ca-app-pub-3301322474937909/3389088666";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardVideo.LoadAd(request, adUnitId);
    }

    private void HandleOnRewardAdClosed(object sender, EventArgs args){
        
    	PieceHolders.Instance.RewardHint();


        //RequestRewardBasedVideo();

      	//Call hint function

    }

    private void HandleOnPotdAdClosed(object sender, EventArgs args){
        
        
        

        //RequestRewardBasedVideo();

        //Call hint function

    }

    public void UserOptToOpenPotd(){
        
    }

    public void UserOptToWatchAd()
    {
        if (rewardVideo.IsLoaded()) {
            rewardVideo.Show();
        }
        else{
            TryAgainScreen();
            RequestRewardBasedVideo();
        }

            //PieceHolders.Instance.RewardHint();
    }

    private void TryAgainScreen(){
        Debug.Log("Try in a few seconds");
        GameObject.Find("TryAgainScreen").transform.GetChild(0).gameObject.SetActive(true);
        //Resources.FindObjectsOfTypeAll("TypeAgainScreen").gameObject.SetActive(true);

    }

}
