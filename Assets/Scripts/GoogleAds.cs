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
        if(Instance == null){
            Instance = this;
            levelsToShowAd = new int[] {7,16,51,101,161,191};
            DontDestroyOnLoad(this.gameObject);
            //return;
        }
        else{
            Destroy(this.gameObject);
            return;
        }

        AddInitCounter();

        #if UNITY_ANDROID || UNITY_IOS
            MobileAds.Initialize("ca-app-pub-3301322474937909~4906291296");
        #endif
    }

    void AddInitCounter(){
        if(!PlayerPrefs.HasKey("InitNum")){
            PlayerPrefs.SetInt("InitNum", 0);
        }
        else{
            PlayerPrefs.SetInt("InitNum", PlayerPrefs.GetInt("InitNum") + 1);
        }
    }

    void Start(){
        levelsInSession = 0;
        #if UNITY_ANDROID || UNITY_IOS
        RequestInterstitial();
        RequestHintAd();
        RequestPotdAd();
        #endif
    }

    public void RequestHintAd(){
        #if UNITY_ANDROID || UNITY_IOS
            string adUnitId = "ca-app-pub-3301322474937909/3389088666";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        this.rewardVideo = new RewardedAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardVideo.OnUserEarnedReward += HandleOnRewardAdClosed;
        this.rewardVideo.OnAdClosed += HintClosedEarly;
        this.rewardVideo.LoadAd(request);
    }

    public void RequestPotdAd(){
        #if UNITY_ANDROID || UNITY_IOS
            string adUnitId = "ca-app-pub-3301322474937909/3389264645";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        this.potdVideo = new RewardedAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
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

        this.interstitial = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
        this.interstitial.OnAdClosed += HandleOnAdClosed;
    }

    public void ShowInterstitial(){
    	levelsInSession++;
        if(PlayerPrefs.GetInt("InitNum") == 4 || PlayerPrefs.GetInt("InitNum") == 11 || PlayerPrefs.GetInt("InitNum") == 20 || PlayerPrefs.GetInt("InitNum") == 20){
            PlayerPrefs.SetInt("InitNum", PlayerPrefs.GetInt("InitNum") + 1);
        }
        #if UNITY_ANDROID || UNITY_IOS
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
        if(levelsInSession == 16 || levelsInSession == 15){
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

    private void HandleOnRewardAdClosed(object sender, EventArgs args){
        
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
        if(!isPotd)
        PieceHolders.Instance.RewardHint();
        else{
            Swiping.mydirection = "Null";
            LevelMenu.UnlockPotdLevel(potdNum);
        }        
    }

    public void UserOptToOpenPotd(){
    	isPotd = true;
        potdNum = LevelMenu.levelToUnlock;
        #if UNITY_ANDROID || UNITY_IOS
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
            RequestHintAd(); 
        }
    }

    private void TryAgainScreen(){
        if (!Social.localUser.authenticated){
            GameObject.Find("TryAgainScreen").transform.GetChild(1).gameObject.SetActive(true);
        }
        else{
            GameObject.Find("TryAgainScreen").transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
