using System;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

/// <summary>
/// Demonstrates how to use the Google Mobile Ads MobileAds Instance.
/// 演示如何使用Google Mobile Ads MobileAds实例。
/// </summary>
[AddComponentMenu("GoogleMobileAds/Samples/GoogleMobileAdsController")]
public class GoogleMobileAdsController : MonoBehaviour
{
    private static bool _isInitialized;

    /// <summary>
    /// Initializes the MobileAds SDK
    /// 初始化MobileAds SDK
    /// </summary>
    private void Start()
    {
        // Demonstrates how to configure Google Mobile Ads.
        //演示如何配置谷歌移动广告
        // Google Mobile Ads needs to be run only once and before loading any ads.
        //谷歌移动广告只需要在加载任何广告之前运行一次。
        if (_isInitialized)
        {
            return;
        }

        // On Android, Unity is paused when displaying interstitial or rewarded video.
        //在Android上，当显示间隙或奖励视频时，Unity会暂停。
        // This setting makes iOS behave consistently with Android.
        //此设置使iOS的行为与Android一致。
        MobileAds.SetiOSAppPauseOnBackground(true);

        // When true all events raised by GoogleMobileAds will be raised
        //当为true时，GoogleMobileAads引发的所有事件都将被引发
        // on the Unity main thread. The default value is false.
        //在Unity主线上。默认值为false。
        // https://developers.google.com/admob/unity/quick-start#raise_ad_events_on_the_unity_main_thread
        //在Unity主线程上引发广告事件
        MobileAds.RaiseAdEventsOnUnityMainThread = true;

        // Set your test devices.
        // https://developers.google.com/admob/unity/test-ads
        List<string> deviceIds = new List<string>()
            {
                AdRequest.TestDeviceSimulator,
                // Add your test device IDs (replace with your own device IDs).
                #if UNITY_IPHONE
                "ca-app-pub-3940256099942544/2934735716";
                #elif UNITY_ANDROID
                "ca-app-pub-1438783342069644~4881382274"
                #endif
            };

        // Configure your RequestConfiguration with Child Directed Treatment
        //使用儿童指导治疗配置您的请求配置以及测试设备Id。
        // and the Test Device Ids.
        RequestConfiguration requestConfiguration = new RequestConfiguration
        {
            TestDeviceIds = deviceIds
        };
        MobileAds.SetRequestConfiguration(requestConfiguration);

        // Initialize the Google Mobile Ads SDK.
        //Debug.Log("Google Mobile Ads Initializing.");
        Debug.Log("谷歌移动广告正在初始化。");

        MobileAds.Initialize((InitializationStatus initstatus) =>
        {
            if (initstatus == null)
            {
                //Debug.LogError("Google Mobile Ads initialization failed.");
                Debug.LogError("谷歌移动广告初始化失败");
                return;
            }

            // If you use mediation, you can check the status of each adapter.
            //如果使用中介，则可以检查每个适配器的状态。
            var adapterStatusMap = initstatus.getAdapterStatusMap();
            if (adapterStatusMap != null)
            {
                foreach (var item in adapterStatusMap)
                {
                    Debug.Log(string.Format($"Adapter {item.Key} is {item.Value.InitializationState}"));
                }
            }

            //Debug.Log("Google Mobile Ads initialization complete.");
            Debug.Log("谷歌移动广告初始化完成。");
            _isInitialized = true;
        });
    }

    /// <summary>
    /// Opens the AdInspector.
    /// </summary>
    public void OpenAdInspector()
    {
        //Debug.Log("Opening ad Inspector.");
        Debug.Log("打开广告检测");
        MobileAds.OpenAdInspector((AdInspectorError error) =>
        {
            // If the operation failed, an error is returned.
            //如果操作失败，则返回错误。
            if (error != null)
            {
                //Debug.Log("Ad Inspector failed to open with error: " + error);
                Debug.Log("广告检查器无法打开，出现错误:" + error);
                return;
            }

            //Debug.Log("Ad Inspector opened successfully.");
            Debug.Log("广告检查器已成功打开。");
        });
    }
}