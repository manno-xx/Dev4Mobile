using UnityEngine;
using UnityEngine.Advertisements;


public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidGameId = "5872447";
    [SerializeField] private string iosGameId = "5872446";
    [SerializeField] private string interstitialPlacementId = "Interstitial_iOS"; // as defined on the Ad Units page.
    [SerializeField] private bool testMode = true;
    
    private string gameId;

    private bool AdvertisementInitialized = false;
    
    private bool interstitialAdReady = false;

    /// <summary>
    /// Sets the gameID and initializes Advertisement.
    /// </summary>
    void Start()
    {
#if UNITY_ANDROID
        gameId = androidGameId;
#elif UNITY_IOS
        gameId = iosGameId;
#endif
        Advertisement.Initialize(gameId, testMode, this);
    }

    /// <summary>
    /// Callback for when Advertisement has initialized OK
    /// </summary>
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialized.");
        AdvertisementInitialized = true;
    }

    public void LoadInterstitialAd()
    {
        if (!AdvertisementInitialized) return;
        
        Advertisement.Load(interstitialPlacementId, this);
    }
    
    /// <summary>
    /// Oops. No ads, no revenue
    /// </summary>
    /// <param name="error"></param>
    /// <param name="message"></param>
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"Init Failed: {error.ToString()} - {message}");
    }

    /// <summary>
    /// When the ad is loaded. Ready to play
    /// </summary>
    /// <param name="placementId"></param>
    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId == interstitialPlacementId)
        {
            interstitialAdReady = true;
            Debug.Log($"Interstitial ad with id {placementId} is ready.");
        }
    }

    /// <summary>
    /// Oops, something went wrong loadin the ad
    /// </summary>
    /// <param name="placementId"></param>
    /// <param name="error"></param>
    /// <param name="message"></param>
    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Load Failed: {placementId} - {error.ToString()} - {message}");
    }

    /// <summary>
    /// Show the ad
    /// </summary>
    public void ShowInterstitialAd()
    {
        if (interstitialAdReady)
        {
            Debug.Log($"Show Interstitial ad with id {interstitialPlacementId}.");
            Advertisement.Show(interstitialPlacementId, this);
            interstitialAdReady = false; // Reset, will reload after ad is shown
        }
        else
        {
            Debug.Log("Interstitial ad not ready yet.");
        }
    }


    /// <summary>
    /// Ad has been shown, load the next ad
    /// </summary>
    /// <param name="placementId">The placement ID as set in the Ad Units page on the Dashboard</param>
    /// <param name="showCompletionState"></param>
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == interstitialPlacementId && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log($"Interstitial ad with id {placementId} completed, load next.");
            Advertisement.Load(interstitialPlacementId, this); // Preload next ad
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="placementId"></param>
    /// <param name="error"></param>
    /// <param name="message"></param>
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Show Failed: {placementId} - {error.ToString()} - {message}");
    }

    /// <summary>
    /// When a video starts playing
    /// </summary>
    /// <param name="placementId"></param>
    public void OnUnityAdsShowStart(string placementId) { }
    
    /// <summary>
    /// When the player clicks on an advertisement
    /// </summary>
    /// <param name="placementId"></param>
    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log($"Player clicked ad with id {placementId}");
    }
}
