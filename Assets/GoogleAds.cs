using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAds : MonoBehaviour
{

    public static GoogleAds Instance;
    private InterstitialAd interstitial;

    // Start is called before the first frame update
    void Awake()
    {
        // Debug.Log("INITIALIZING MOBIELADS");
        Instance = this;
        MobileAds.Initialize("ca-app-pub-3301322474937909~4906291296");
        // Debug.Log("INITIALIZED MOBILEADS");

    }
    void Start(){
        RequestInterstitial();
    }
    private void RequestInterstitial()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/4411468910";
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
        if (this.interstitial.IsLoaded()) {
            this.interstitial.Show();
        }
    }
    private void HandleOnAdClosed(object sender, EventArgs args){
        this.interstitial.Destroy();
        RequestInterstitial();
    }

}
