using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdMobScript : MonoBehaviour
{
    #region Singleton class: AdMobScript

    public static AdMobScript Instance;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    #endregion

    string App_ID = "ca-app-pub-3940256099942544~3347511713";

    string Interstitial_AD_ID = "ca-app-pub-3940256099942544/1033173712";

    protected InterstitialAd interstitial;

    public static bool isAdClosed = false;

    [Obsolete]
    void Start()
    {
        MobileAds.Initialize(App_ID);
        RequestInterstitial();
    }
    public void RequestInterstitial()
    {
        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(Interstitial_AD_ID);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }
    public void ShowInterstitialAd()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        print("HandleAdClosed event received");
        isAdClosed = true;
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        print("HandleAdLeavingApplication event received");
    }
}
