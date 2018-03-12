using UnityEngine;
using System.Collections;

public class addbuddizNemoris : MonoBehaviour {
	//public string[] escenasInterstitial;
	public bool activateAds = true;
	public int conteoMuestra = 5;
	public int incremento = 2;
	int conteo = 0;
	string mensaje = "";
	public bool rewardedVideo = true;
	public bool mostrarLog = false;
	public bool mostrandoAd = false;
	// Use this for initialization
	void Start() { 

		//PlayerPrefs.DeleteKey ("activateAdsAdBuddiz");

		AdBuddizBinding.SetAndroidPublisherKey("6f7abe61-e7a0-4aea-bcd9-3769ed23c9c8");
		AdBuddizBinding.SetIOSPublisherKey("b9859a3b-8cd7-4adb-800a-f3cfe80db8b7");
		if(rewardedVideo)
			AdBuddizBinding.RewardedVideo.Fetch();
		AdBuddizBinding.CacheAds();
		if (PlayerPrefs.HasKey ("activateAdsAdBuddiz")) {
			activateAds = (PlayerPrefs.GetInt ("activateAdsAdBuddiz", 1) == 1);
		} else {
			if (activateAds)
				PlayerPrefs.SetInt ("activateAdsAdBuddiz", 1);
			else
				PlayerPrefs.SetInt ("activateAdsAdBuddiz", 0);
		}
		if (activateAds) {
			DontDestroyOnLoad (gameObject);
			mensaje += "sdk cargado\n";
		}
	}

	void OnEnable() { // register as a listener
		AdBuddizManager.didFailToShowAd += DidFailToShowAd;
		AdBuddizManager.didCacheAd += DidCacheAd;
		AdBuddizManager.didShowAd += DidShowAd;
		AdBuddizManager.didClick += DidClick;
		AdBuddizManager.didHideAd += DidHideAd;
		AdBuddizRewardedVideoManager.didComplete += DidComplete;
		AdBuddizRewardedVideoManager.didFetch += DidFetch; 
		AdBuddizRewardedVideoManager.didFail += DidFail;   
		AdBuddizRewardedVideoManager.didNotComplete += DidNotComplete; 
	}
	
	void OnDisable() { // unregister as a listener
		AdBuddizManager.didFailToShowAd -= DidFailToShowAd;
		AdBuddizManager.didCacheAd -= DidCacheAd;
		AdBuddizManager.didShowAd -= DidShowAd;
		AdBuddizManager.didClick -= DidClick;
		AdBuddizManager.didHideAd -= DidHideAd;
		AdBuddizRewardedVideoManager.didComplete -= DidComplete;
		AdBuddizRewardedVideoManager.didFetch -= DidFetch; 
		AdBuddizRewardedVideoManager.didFail -= DidFail;
		AdBuddizRewardedVideoManager.didNotComplete -= DidNotComplete;
	}
	
	// do whatever you like inside these methods
	void DidFailToShowAd(string adBuddizError) {mostrandoAd = false; mensaje += "error show ad " + adBuddizError + " \n";}
	void DidCacheAd() {mensaje += "ad cache\n";}
	void DidShowAd() {mensaje += "show ad\n";}
	void DidClick() {mostrandoAd = false; mensaje += "click\n";}
	void DidHideAd() {mostrandoAd = false; mensaje += "hide\n";}
	void DidComplete() {mostrandoAd = false; mensaje += "rewarded completo\n";}
	void DidFetch(){ mensaje += "rewarded fetched\n";}// a video is ready to be displayed
	void DidFail(string adBuddizRewardedVideo){ mensaje += "rewarded fetch fail\n";} // SDK was unable to fetch or show a video
	void DidNotComplete(){ mensaje += "rewarded not completo\n";} // an error happened during video playback

	void OnGUI(){
		if(mostrarLog) GUI.Box (new Rect (400f, 0f, 400f, 600f), mensaje);
		
		if(mostrarLog) GUI.Box (new Rect (400f, 0f, 150f, 50f), "" + mostrandoAd);
	}

	void OnLevelWasLoaded(int idEscena){
		mostrandoAd = false;

		if (conteoMuestra <= 0)
			return;

		conteo++;
		if (conteo > conteoMuestra) {
			mostrarAdBuddiz();
			conteo = 0;
			conteoMuestra += incremento;
		}
	}

	public void mostrarVideoAdBuddiz(){
		if (activateAds) {
			mostrandoAd = true;
			AdBuddizBinding.RewardedVideo.Show();
		}
	}

	public void mostrarAdBuddiz(){
		if (activateAds) {
			mostrandoAd = true;
			AdBuddizBinding.ShowAd ();
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

}
