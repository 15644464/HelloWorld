using System;
using UnityEngine;
using GoogleMobileAds.Api;

/// <summary>
/// Demonstrates how to use Google Mobile Ads banner views.
/// 演示如何使用横幅广告。
/// </summary>
[AddComponentMenu("GoogleMobileAds/Samples/BannerViewController")]
public class BannerViewController : MonoBehaviour
{
    /// <summary>
    /// UI element activated when an ad is ready to show.
    /// </summary>
    public GameObject AdLoadedStatus;

    private BannerView _bannerView;

    /// <summary>
    /// Creates a 320x50 banner at top of the screen.
    /// </summary>
    public void CreateBannerView()
    {
        //Debug.Log("Creating banner view.");
        Debug.Log("正在创建横幅视图.");

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyAd();
        }

        // Create a 320x50 banner at top of the screen.
        _bannerView = new BannerView(AppOpenAdController._adUnitId,AdSize.Banner, AdPosition.Top);

        // Listen to events the banner may raise.
        ListenToAdEvents();

        //Debug.Log("Banner view created.");
        Debug.Log("横幅视图已创建.");
    }

    /// <summary>
    /// Creates the banner view and loads a banner ad.
    /// </summary>
    public void LoadAd()
    {
        // Create an instance of a banner view first.
        if (_bannerView == null)
        {
            CreateBannerView();
        }

        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        // Send the request to load the ad.
        Debug.Log("加载横幅广告.");
        _bannerView.LoadAd(adRequest);
    }

    /// <summary>
    /// Shows the ad.
    /// </summary>
    public void ShowAd()
    {
        if (_bannerView != null)
        {
            Debug.Log("显示横幅广告");
            _bannerView.Show();
        }
    }

    /// <summary>
    /// Hides the ad.
    /// </summary>
    public void HideAd()
    {
        if (_bannerView != null)
        {
            Debug.Log("隐藏广告。");
            _bannerView.Hide();
        }
    }

    /// <summary>
    /// Destroys the ad.
    /// When you are finished with a BannerView, make sure to call
    /// the Destroy() method before dropping your reference to it.
    /// </summary>
    public void DestroyAd()
    {
        if (_bannerView != null)
        {
            Debug.Log("删除横幅广告");
            _bannerView.Destroy();
            _bannerView = null;
        }

        // Inform the UI that the ad is not ready.
        AdLoadedStatus?.SetActive(false);
    }

    /// <summary>
    /// Logs the ResponseInfo.
    /// 记录响应信息
    /// </summary>
    public void LogResponseInfo()
    {
        if (_bannerView != null)
        {
            var responseInfo = _bannerView.GetResponseInfo();
            if (responseInfo != null)
            {
                UnityEngine.Debug.Log(responseInfo);
            }
        }
    }

    /// <summary>
    /// Listen to events the banner may raise.
    /// 监听横幅可能升起的事件。
    /// </summary>
    private void ListenToAdEvents()
    {
        // Raised when an ad is loaded into the banner view.
        _bannerView.OnBannerAdLoaded += () =>
        {
            //Debug.Log("Banner view loaded an ad with resposnse : "
            Debug.Log("横幅视图加载了带有响应的广告: "
                + _bannerView.GetResponseInfo());

            // Inform the UI that the ad is ready.
            AdLoadedStatus?.SetActive(true);
        };
        // Raised when an ad fails to load into the banner view.
        _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            //Debug.LogError("Banner view failed to load an ad with error : " + error);
            Debug.LogError("横幅视图加载广告失败，出现错误 : " + error);
        };
        // Raised when the ad is estimated to have earned money.
        _bannerView.OnAdPaid += (AdValue adValue) =>
        {
            //Debug.Log(String.Format($"Banner view paid {adValue.Value} {adValue.CurrencyCode}"));
            Debug.Log(String.Format($"横幅视图已付费 {adValue.Value} {adValue.CurrencyCode}"));
        };
        // Raised when an impression is recorded for an ad.
        _bannerView.OnAdImpressionRecorded += () =>
        {
            //Debug.Log("Banner view recorded an impression.");
            Debug.Log("横幅视图缓存了一个视频");
        };
        // Raised when a click is recorded for an ad.
        _bannerView.OnAdClicked += () =>
        {
            Debug.Log("已单击横幅视图.");
        };
        // Raised when an ad opened full screen content.
        _bannerView.OnAdFullScreenContentOpened += () =>
        {
            //Debug.Log("Banner view full screen content opened.");
            Debug.Log("横幅视图全屏内容打开.");
        };
        // Raised when the ad closed full screen content.
        _bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("横幅视图全屏内容已关闭.");
            //Debug.Log("Banner view full screen content closed.");
        };
    }
}
