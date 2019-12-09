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
        #if UNITY_ANDROID || UNITY_IOS
        MobileAds.Initialize("ca-app-pub-3301322474937909~4906291296");
        // Debug.Log("INITIALIZED MOBILEADS");
        #endif

        #if UNITY_EDITOR
        	Debug.Log("UNITY EDITOR,UNITY EDITOR,UNITY EDITOR,UNITY EDITOR,UNITY EDITOR,UNITY EDITOR,UNITY EDITOR,UNITY EDITOR");
        #endif

    	#if UNITY_IOS
        MobileAds.Initialize("ca-app-pub-3301322474937909~4906291296");
        #endif

        #if UNITY_ANDROID
        MobileAds.Initialize("ca-app-pub-3301322474937909~4906291296");
        #endif
    }
    void Start(){
        #if UNITY_ANDROID || UNITY_IOS
        RequestInterstitial();
        levelsInSession = 0;
        this.rewardVideo = RewardBasedVideoAd.Instance;
        RequestFirstRewardBasedVideo();
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
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
        this.interstitial.OnAdClosed += HandleOnAdClosed;
    }
    public void ShowInterstitial(){
    	levelsInSession++;
        if(LevelManager.adFree){
 
            return;
        }
        if (this.interstitial.IsLoaded()) {
        	if(IsInList(levelsInSession)){
                this.interstitial.Show();   		
        	}
        }

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

    public void RequestFirstRewardBasedVideo(){
        Debug.Log("REQUESTING");
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3301322474937909/3389088666";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3301322474937909/3389088666";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardVideo.LoadAd(request, adUnitId);
        
        this.rewardVideo.OnAdClosed += HandeOnRewardAdClosed;
    }


    public void RequestRewardBasedVideo(){
        Debug.Log("REQUESTING");
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardVideo.LoadAd(request, adUnitId);
    }

    private void HandeOnRewardAdClosed(object sender, EventArgs args){
        
    	PieceHolders.Instance.RewardHint();


        //RequestRewardBasedVideo();

      	//Call hint function

    }
    public void UserOptToWatchAd()
    {
        #if UNITY_ANDROID || UNITY_IOS
        if (rewardVideo.IsLoaded()) {
            rewardVideo.Show();
        }
        else{
            TryAgainScreen();
            RequestRewardBasedVideo();
        }
        #endif

        #if UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
            PieceHolders.Instance.RewardHint();
        #endif
    }

    private void TryAgainScreen(){
        Debug.Log("Try in a few seconds");
        GameObject.Find("TryAgainScreen").transform.GetChild(0).gameObject.SetActive(true);
        //Resources.FindObjectsOfTypeAll("TypeAgainScreen").gameObject.SetActive(true);

    }

}
