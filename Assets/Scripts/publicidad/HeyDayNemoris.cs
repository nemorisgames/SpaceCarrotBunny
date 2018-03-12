using UnityEngine;
using System.Collections;
using Heyzap;

public class HeyDayNemoris : MonoBehaviour {
	
	public bool activateAds = true;
	public int conteoMuestra = 5;
	public int incremento = 2;
	int conteo = 0;
	string mensaje = "";
	public bool mostrarLog = false;
	public bool videoIncentivado = false;
	public bool mostrandoAd = false;
	public bool mostrandoAdVideo = false;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("activateAdsHeyDay")) {
			activateAds = (PlayerPrefs.GetInt ("activateAdsHeyDay", 1) == 1);
		} else {
			if (activateAds)
				PlayerPrefs.SetInt ("activateAdsHeyDay", 1);
			else
				PlayerPrefs.SetInt ("activateAdsHeyDay", 0);
		}
		if (activateAds) {
			DontDestroyOnLoad (gameObject);
			HeyzapAds.start ("268f0f16490c582afaac18d73891d2df", HeyzapAds.FLAG_NO_OPTIONS);
			//HeyzapAds.showMediationTestSuite ();

			if(videoIncentivado) HZIncentivizedAd.fetch();
			else HZVideoAd.fetch();
			HZVideoAd.AdDisplayListener listener = delegate(string adState, string adTag) {
				if (adState.Equals ("show")) {
				mensaje += "show video" + adTag + "\n";
				}
				if (adState.Equals ("hide")) {
					// Do something after the ad hides itself
					mostrandoAdVideo = false;
					mensaje += "hide video" + adTag + "\n";
				}
				if (adState.Equals ("click")) {
					// Do something when an ad is clicked on
					mostrandoAdVideo = false;
					mensaje += "click video" + adTag + "\n";
				}
				if (adState.Equals ("failed")) {
					// Do something when an ad fails to show
					mostrandoAdVideo = false;
					mensaje += "failed video" + adTag + "\n";
				}
				if (adState.Equals ("available")) {
					// Do something when an ad has successfully been fetched
					mensaje += "avail video" + adTag + "\n";
				}
				if (adState.Equals ("fetch_failed")) {
					// Do something when an ad did not fetch
					mensaje += "fetch failed video" + adTag + "\n";
				}
				if (adState.Equals ("audio_starting")) {
					// The ad being shown will use audio. Mute any background music
				}
				if (adState.Equals ("audio_finished")) {
					// The ad being shown has finished using audio.
					// You can resume any background music.
				}
			};

			HZInterstitialAd.AdDisplayListener listener2 = delegate(string adState, string adTag) {
				if (adState.Equals ("show")) {
					mensaje += "show " + adTag + "\n";
				}
				if (adState.Equals ("hide")) {
					// Do something after the ad hides itself
					mostrandoAd = false;
					mensaje += "hide " + adTag + "\n";
				}
				if (adState.Equals ("click")) {
					// Do something when an ad is clicked on
					mostrandoAd = false;
					mensaje += "click " + adTag + "\n";
				}
				if (adState.Equals ("failed")) {
					// Do something when an ad fails to show
					mostrandoAd = false;
					mensaje += "failed " + adTag + "\n";
				}
				if (adState.Equals ("available")) {
					// Do something when an ad has successfully been fetched
					mensaje += "avail " + adTag + "\n";
				}
				if (adState.Equals ("fetch_failed")) {
					// Do something when an ad did not fetch
					mensaje += "fetch failed " + adTag + "\n";
				}
				if (adState.Equals ("audio_starting")) {
					// The ad being shown will use audio. Mute any background music
				}
				if (adState.Equals ("audio_finished")) {
					// The ad being shown has finished using audio.
					// You can resume any background music.
				}
			};

			HZVideoAd.setDisplayListener (listener);
			HZInterstitialAd.setDisplayListener (listener2);
			HZInterstitialAd.fetch();

			mensaje += "sdk cargado\n";
		}
	}

	void OnGUI(){
		if(mostrarLog) GUI.Box (new Rect (0f, 0f, 400f, 600f), mensaje);
		
		if(mostrarLog) GUI.Box (new Rect (0f, 0f, 150f, 50f), mostrandoAd + " " + mostrandoAdVideo);
	}

	void OnLevelWasLoaded(int idEscena){
		mostrandoAd = false;
		mostrandoAdVideo = false;
		if (conteoMuestra <= 0)
			return;
		
		conteo++;
		if (conteo > conteoMuestra) {
			mostrarAd();
			conteo = 0;
			conteoMuestra += incremento;
		}
	}
	
	public void mostrarAd(){
		if (activateAds) {
			if(HZInterstitialAd.isAvailable()){
				mostrandoAd = true;
				mensaje += ("mostrando interstitial\n");
				HZInterstitialAd.show ();
			}
		}
	}

	public void mostrarVideo(){
		if (activateAds) {
			mensaje += ("intentando video :" + HZVideoAd.isAvailable() + "\n");
			mensaje += ("intentando video incentivado:" + HZIncentivizedAd.isAvailable() + "\n");
			if(videoIncentivado){
				if (HZIncentivizedAd.isAvailable ()) {
					mensaje += ("mostrando video\n");
					mostrandoAdVideo = true;
					HZIncentivizedAd.show();
				}
				else mostrarAd();

			}
			else{
				if(HZVideoAd.isAvailable()){
					mensaje += ("mostrando video\n");
					mostrandoAdVideo = true;
					HZVideoAd.show();
				}
				else mostrarAd();
			}
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
