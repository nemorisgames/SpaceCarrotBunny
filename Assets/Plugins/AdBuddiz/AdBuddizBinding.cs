using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
#if UNITY_EDITOR 
using UnityEditor;
#endif

public class AdBuddizBinding {

	public enum ABLogLevel { Info, Error, Silent };

	#if UNITY_IOS
	[DllImport ("__Internal")] private static extern void _setLogLevel(string level);
	[DllImport ("__Internal")] private static extern void _setPublisherKey(string publisherKey);
	[DllImport ("__Internal")] private static extern void _setTestModeActive();
	[DllImport ("__Internal")] private static extern void _cacheAds();
	[DllImport ("__Internal")] private static extern bool _isReadyToShowAd();
	[DllImport ("__Internal")] private static extern bool _isReadyToShowAdWithPlacement(string placementId);
	[DllImport ("__Internal")] private static extern void _showAd();
	[DllImport ("__Internal")] private static extern void _showAdWithPlacement(string placementId);
	[DllImport ("__Internal")] private static extern void _logNative(string text);
	#endif

	#if UNITY_ANDROID
	private static AndroidJavaObject adBuddizPlugin;
	private static AndroidJavaObject adBuddizRewardedVideoPlugin;
	#endif

	static AdBuddizBinding()
	{
		#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			adBuddizPlugin = new AndroidJavaObject ("com.purplebrain.adbuddiz.sdk.AdBuddizUnityBinding");
			adBuddizRewardedVideoPlugin = new AndroidJavaObject ("com.purplebrain.adbuddiz.sdk.AdBuddizRewardedVideoUnityBinding");
		}
		#endif

		#if UNITY_ANDROID || UNITY_IOS
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
			GameObject o = GameObject.Find ("AdBuddizManager");
			if (o == null) {
				new GameObject ("AdBuddizManager").AddComponent<AdBuddizManager>();
			}
			o = GameObject.Find ("AdBuddizRewardedVideoManager");
			if (o == null) {
				new GameObject ("AdBuddizRewardedVideoManager").AddComponent<AdBuddizRewardedVideoManager>();
			}
		}
		#endif
	}
	
	public static void SetLogLevel(ABLogLevel level)
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
			#if UNITY_ANDROID
			adBuddizPlugin.Call ("setLogLevel", level.ToString());
			#endif

			#if UNITY_IOS
			_setLogLevel (level.ToString());
			#endif
		}
	}
	
	public static void SetAndroidPublisherKey(string publisherKey)
	{
		if (Application.platform == RuntimePlatform.Android) {
			#if UNITY_ANDROID
			adBuddizPlugin.Call ("setPublisherKey", publisherKey);
			#endif
		}
	}

	public static void SetIOSPublisherKey(string publisherKey)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			#if UNITY_IOS
			_setPublisherKey (publisherKey);
			#endif
		}
	}

	public static void SetTestModeActive()
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
			#if UNITY_ANDROID
			adBuddizPlugin.Call ("setTestModeActive");
			#endif
		
			#if UNITY_IOS
			_setTestModeActive ();
			#endif
		}
	}

	public static void CacheAds()
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
			#if UNITY_ANDROID
			adBuddizPlugin.Call ("cacheAds");
			#endif

			#if UNITY_IOS
			_cacheAds ();
			#endif
		}
	}
	
	public static bool IsReadyToShowAd() 
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
			#if UNITY_ANDROID
			return adBuddizPlugin.Call<bool> ("isReadyToShowAd");
			#endif

			#if UNITY_IOS
			return _isReadyToShowAd ();
			#endif
		}

		return false;
	}
	
	public static bool IsReadyToShowAd(string placementId) 
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
			#if UNITY_ANDROID
			return adBuddizPlugin.Call<bool> ("isReadyToShowAd", placementId);
			#endif
		
			#if UNITY_IOS
			return _isReadyToShowAdWithPlacement (placementId);
			#endif
		}

		return false;
	}

	public static void ShowAd()
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
			#if UNITY_ANDROID
			adBuddizPlugin.Call ("showAd");
			#endif

			#if UNITY_IOS
			_showAd ();
			#endif
		} else {
			#if UNITY_EDITOR 
			EditorUtility.DisplayDialog ("Error", "ShowAd() only works on real device!", "OK");
			#endif
		}
	}
	
	public static void ShowAd(string placementId)
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
			#if UNITY_ANDROID
			adBuddizPlugin.Call ("showAd", placementId);
			#endif

			#if UNITY_IOS
			_showAdWithPlacement (placementId);
			#endif
		}
	}
	
	public static void LogNative(string text)
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
			#if UNITY_ANDROID
			adBuddizPlugin.Call ("logNative", text);
			#endif
			
			#if UNITY_IOS
			_logNative(text);
			#endif
		}
	}

	public static void ToastNative(string text)
	{
		if (Application.platform == RuntimePlatform.Android) {
			#if UNITY_ANDROID
			adBuddizPlugin.Call ("toastNative", text);
			#endif
		}
	}

	public class RewardedVideo {

		public static void Fetch()
		{
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
				#if UNITY_ANDROID
				adBuddizRewardedVideoPlugin.Call ("fetch");
				#endif
				
				#if UNITY_IOS
				ThrowUnimplementedError (true);
				#endif
			}
		}

		public static bool IsReadyToShow() 
		{
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
				#if UNITY_ANDROID
				return adBuddizRewardedVideoPlugin.Call<bool> ("isReadyToShow");
				#endif
				
				#if UNITY_IOS
				ThrowUnimplementedError (false);
				return false;
				#endif
			}
			
			return false;
		}

		public static void Show()
		{
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
				#if UNITY_ANDROID
				adBuddizRewardedVideoPlugin.Call("show");
				#endif
				
				#if UNITY_IOS
				ThrowUnimplementedError (true);
				#endif
			} else {
				#if UNITY_EDITOR 
				EditorUtility.DisplayDialog ("Error", "Show() only works on real device!", "OK");
				#endif
			}
		}

		public static void SetUserId(String userId)
		{
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
				#if UNITY_ANDROID
				adBuddizRewardedVideoPlugin.Call("setUserId", userId);
				#endif
			}
		}

		public static void ThrowUnimplementedError(bool notifyDelegate) {
			LogNative ("Can't fetch/show Ad: UNIMPLEMENTED. This functionnality is not available for the underlying platform.");

			if (notifyDelegate) {
				GameObject o = GameObject.Find ("AdBuddizRewardedVideoManager");
				if (o != null) {
					o.BroadcastMessage ("OnDidFail", "UNIMPLEMENTED");
				}
			}
		}
	}

}