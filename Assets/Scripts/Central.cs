using UnityEngine;
using System.Collections;
using Heyzap;

public class Central : MonoBehaviour {
	Zanahoria zanahoria;
	public GameObject planetaPrefab;
	public Planeta[] planetas;
	public static Rect AreaSpawnPlanetas = new Rect(50f, -20f, 30f, 40f);
	int nPlanetas = 0;
	Planeta planetaAuxiliar;

	//public TweenPosition tituloUI;
	public TweenPosition[] juegoUI;
	public TweenPosition[] resumenUI;
	public TweenScale resumenBotonesUI;
	public TweenAlpha panelSalir;

	public UILabel resumenPlanetasLabel;
	public UILabel resumenRecordLabel;
	public UILabel resumenConejosLabel;
	public UISprite resumenMedallaSprite;
	public ParticleSystem medallaParticulas;

	public enum EstadoJuego{Titulo, EnJuego, Pausa, Terminado};
	public EstadoJuego estado = EstadoJuego.Titulo;

	GameCenterNemoris gameCenterNemoris;

	public GameObject botonRevive;
	public GameObject botonReviveComprado;
	public GameObject botonComprar;

	public GameObject botonPlay;

	public GameObject[] meteoros;
	public GameObject[] superiores;
	public GameObject[] ovnis;
	public GameObject[] inferiores;
	public Transform[] spawnMeteoros;
	public Transform[] spawnSuperiores;
	public Transform[] spawnOvnis;
	public Transform[] spawnInteriores;
	float tiempoSpawnActual = 0f;

	public AudioSource audioPrincipal;
	public AudioClip slideSonidoNormal;
	public AudioClip slideSonidoMedalla;
	public AudioClip inicioMusica;

	public int nPlanetasEliminados = 0;
	public UILabel textoScoreNuevo;
	public UILabel textoConejoNuevo;

	public GameObject instrucciones;

	public GameObject instruccionesFinal;
	public GameObject replayFinal;
	public GameObject recordsFinal;
	bool revivido = false;

	public GameObject botonRestore;
	public GameObject camaraLogo;
	public TweenAlpha transicion;

	public GameObject marcaBarra;

	public UILabel mensajeCompra;

	IAPNemoris store;
	addbuddizNemoris ads;
	HeyDayNemoris adsH;

	//GPGSNemoris gpgsNemoris;
	// Use this for initialization
	void Start () {
		GameObject ga = GameObject.Find ("AppBudizz");
		GameObject gh = GameObject.Find ("HeyDayNemoris");
		if (ga != null) 
			ads = ga.GetComponent<addbuddizNemoris> ();
		if (gh != null)
			adsH = gh.GetComponent<HeyDayNemoris> ();
		GameObject s = GameObject.Find ("IAPNemoris");
		if (s != null)
			store = s.GetComponent<IAPNemoris> ();
		Time.timeScale = 1f;
		GameObject g = GameObject.Find ("GameCenterNemoris");
		if (g != null) {
			gameCenterNemoris = g.GetComponent<GameCenterNemoris>();
		}
		botonRestore.SetActive(PlayerPrefs.GetInt ("activateAdsAdBuddiz", 1) == 1);
		GameObject g2 = GameObject.Find ("GPGSNemoris");
		if (g2 != null) {
		//	gpgsNemoris = g2.GetComponent<GPGSNemoris>();
		}
		nPlanetas = planetas.Length;
		zanahoria = GameObject.FindWithTag ("Player").GetComponent<Zanahoria> ();

		if (PlayerPrefs.GetInt ("primeraEjecucion", 0) >= 1) {
			zanahoria.conejoNormal.color = new Color(1f, 1f, 1f, 1f);
			zanahoria.conejoHD.color = new Color(0f, 0f, 0f, 0f);
			botonPlay.SetActive (false);
			comenzarJuego ();
		} else {
			audioPrincipal.PlayOneShot(inicioMusica);
			
			zanahoria.conejoNormal.color = new Color(0f, 0f, 0f, 0f);
			zanahoria.conejoHD.color = new Color(1f, 1f, 1f, 1f);
		}
	}

	public void restoreTransactions(){
		#if UNITY_IPHONE || UNITY_ANDROID
		//if (store.compraTerminada)
		//	store.restoreTransactions ();
#endif
	}

	public void comprar(){
		print ("comprar");
		#if UNITY_IPHONE || UNITY_ANDROID
		//if (store.compraTerminada)
		//	store.comprar (0);
#endif
	}

	public void compraExitosa(){
		botonRevive.SetActive(PlayerPrefs.GetInt ("activateAdsAdBuddiz", 1) == 1);
		botonReviveComprado.SetActive(PlayerPrefs.GetInt ("activateAdsAdBuddiz", 1) == 0);
		botonComprar.SetActive(PlayerPrefs.GetInt ("activateAdsAdBuddiz", 1) == 1);
		botonRestore.SetActive(false);
		TweenAlpha ta = mensajeCompra.gameObject.GetComponent<TweenAlpha> ();
		ta.ResetToBeginning ();
		ta.PlayForward ();
		mensajeCompra.text = "Thank you for your purchase!";
	}

	public void compraFallida(){
		TweenAlpha ta = mensajeCompra.gameObject.GetComponent<TweenAlpha> ();
		ta.ResetToBeginning ();
		ta.PlayForward ();
		mensajeCompra.text = "Transaction failed";
	}

	public void mostrarNuevoScore(string texto){
		textoScoreNuevo.gameObject.SendMessage ("ResetToBeginning");
		textoScoreNuevo.gameObject.SendMessage ("PlayForward");
		textoScoreNuevo.text = texto;
	}

	public void mostrarNuevoConejo(string texto){
		textoConejoNuevo.gameObject.SendMessage ("ResetToBeginning");
		textoConejoNuevo.gameObject.SendMessage ("PlayForward");
		textoConejoNuevo.text = texto;
	}

	public void botonPlayPresionado(){
		//botonPlay.SetActive (false);
		comenzarJuego ();
	}

	public void comenzarJuego(){
		print (PlayerPrefs.GetInt ("activateAdsAdBuddiz", 1));
		botonRevive.SetActive(PlayerPrefs.GetInt ("activateAdsAdBuddiz", 1) == 1);
		botonReviveComprado.SetActive(PlayerPrefs.GetInt ("activateAdsAdBuddiz", 1) == 0);
		botonComprar.SetActive(PlayerPrefs.GetInt ("activateAdsAdBuddiz", 1) == 1);
		botonRestore.SetActive(false);
		zanahoria.conejoNormal.color = new Color(1f, 1f, 1f, 1f);
		zanahoria.conejoHD.color = new Color(0f, 0f, 0f, 0f);
		estado = EstadoJuego.EnJuego;
		zanahoria.activar ();
		marcaBarra.SetActive(true);
		foreach(TweenPosition t in juegoUI)
			t.PlayForward();
		if (PlayerPrefs.GetInt ("primeraEjecucion", 0) == 0 || PlayerPrefs.GetInt ("maximoPlanetas", 0) <= 1) {
			//transicion.delay = 0.3f;
		} else{
			camaraLogo.SetActive (false);
		}
		PlayerPrefs.SetInt ("primeraEjecucion", PlayerPrefs.GetInt ("primeraEjecucion", 0) + 1);
		//revivido = true;
	}

	public void verRanking(){
		if (gameCenterNemoris != null) {
			gameCenterNemoris.mostrarRanking();
		}
		
	/*	if (gpgsNemoris != null) {
			gpgsNemoris.verRecords();
		}*/
	}

	public void revive(){
		if (PlayerPrefs.GetInt ("activateAdsAdBuddiz", 0) == 1 && ads != null) {
			if (PlayerPrefs.GetInt ("primeraEjecucion", 0) % 3 != 0) {
				print ("interstitial");
				if (AdBuddizBinding.IsReadyToShowAd ())
					ads.mostrarAdBuddiz();
				else {
					adsH.mostrarAd();
				}
			}
		}

		if (PlayerPrefs.GetInt ("activateAdsHeyDay", 0) == 1 && adsH != null) {
			if (PlayerPrefs.GetInt ("primeraEjecucion", 0) % 3 == 0) {
				print ("video");
				if((adsH.videoIncentivado && !HZIncentivizedAd.isAvailable()) && (!adsH.videoIncentivado && !HZVideoAd.isAvailable())){
					if (ads.rewardedVideo && AdBuddizBinding.RewardedVideo.IsReadyToShow()) {
						ads.mostrarVideoAdBuddiz();
					}
					else{
						if (AdBuddizBinding.IsReadyToShowAd ())
							ads.mostrarAdBuddiz();
						else {
							adsH.mostrarAd();
						}
					}
				}
				else
					adsH.mostrarVideo();
			}
		}

		StartCoroutine (esperarAd ());
	}

	IEnumerator esperarAd(){
		foreach(TweenPosition t in juegoUI)
			t.PlayForward();
		foreach(TweenPosition t in resumenUI)
			t.PlayReverse();
		resumenBotonesUI.PlayReverse();
		resumenMedallaSprite.gameObject.SetActive(false);
		botonRevive.SetActive(false);
		botonReviveComprado.SetActive(false);
		yield return new WaitForSeconds (0.1f);
		for (int i = 0; i < 30; i++){
			if(ads == null || adsH == null) break;
			if(!ads.mostrandoAd && !adsH.mostrandoAd && !adsH.mostrandoAdVideo) break;
			yield return new WaitForSeconds (1f);
		}
		zanahoria.revivir ();

		revivido = true;
		StartCoroutine (reviveAnimacion ());
	}

	IEnumerator reviveAnimacion(){
		zanahoria.thankyou.SetActive (true);
		yield return new WaitForSeconds (0.1f);
		for (int i = 0; i < 10; i++) {
			zanahoria.conejoNormal.color = new Color(1f, 1f, 1f, 0f);
			yield return new WaitForSeconds (0.1f);
			zanahoria.conejoNormal.color = new Color(1f, 1f, 1f, 1f);
			yield return new WaitForSeconds (0.1f);
		}
		zanahoria.thankyou.SetActive (false);
	}

	public void resumenJuego(int planetas, int nConejos){
		if (gameCenterNemoris != null) {
			//if(planetas > PlayerPrefs.GetInt("maximoPlanetas", 0)){
				#if UNITY_IOS
				gameCenterNemoris.enviarRanking ("planets", planetas);
				#endif
				#if UNITY_ANDROID
				gameCenterNemoris.enviarRanking ("CgkI6uTtj40GEAIQAQ", planetas);
				#endif
			//}
			//if(PlayerPrefs.GetInt ("nConejos", 0) > 0 && nConejos > 0){
				#if UNITY_IOS
				gameCenterNemoris.enviarRanking ("rescued", PlayerPrefs.GetInt ("nConejos", 0));
				#endif
				#if UNITY_ANDROID
				gameCenterNemoris.enviarRanking ("CgkI6uTtj40GEAIQAg", PlayerPrefs.GetInt ("nConejos", 0));
				#endif

			//}
		}

		/*if (gpgsNemoris != null) {
			if(planetas > PlayerPrefs.GetInt("maximoPlanetas", 0)){
				gpgsNemoris.enviarRecord (0, planetas);
			}
			if(PlayerPrefs.GetInt ("nConejos", 0) > 0 && nConejos > 0)
				gpgsNemoris.enviarRecord (1, PlayerPrefs.GetInt ("nConejos", 0));
		}*/
		
		if(planetas > PlayerPrefs.GetInt("maximoPlanetas", 0)) 
			PlayerPrefs.SetInt("maximoPlanetas", planetas);

		resumenPlanetasLabel.text = "" + planetas;
		resumenRecordLabel.text = "" + PlayerPrefs.GetInt("maximoPlanetas", 0);
		resumenConejosLabel.text = "" + PlayerPrefs.GetInt ("nConejos", 0);
		if (planetas >= 10) {
			resumenMedallaSprite.gameObject.SetActive (true);
			string n = "bronze";
			if (planetas >= 25)
				n = "silver";
			if (planetas >= 50)
				n = "gold";
#if UNITY_IOS
			if(n == "bronze")
				achievements._ReportAchievement("broncemedalist ", 100.0f);
			else {
				if(n == "silver")
					achievements._ReportAchievement("silvermedalist ", 100.0f);
				else
					if(n == "gold")
						achievements._ReportAchievement("goldmedalist ", 100.0f);
			}
#endif
			resumenMedallaSprite.spriteName = n;
			resumenMedallaSprite.SendMessage ("PlayForward");
			medallaParticulas.Play ();
			audioPrincipal.PlayOneShot(slideSonidoMedalla);
		} else {
			audioPrincipal.PlayOneShot(slideSonidoNormal);
			resumenMedallaSprite.gameObject.SetActive (false);
		}

		if (planetas >= 2 && !revivido) {
			print (PlayerPrefs.GetInt ("activateAdsAdBuddiz", 1));
			botonRevive.SetActive (PlayerPrefs.GetInt ("activateAdsAdBuddiz", 1) == 1);
			botonReviveComprado.SetActive (PlayerPrefs.GetInt ("activateAdsAdBuddiz", 1) == 0);
			if(PlayerPrefs.GetInt("instruccionesFinalMostrado", 0) == 0){
				PlayerPrefs.SetInt("instruccionesFinalMostrado", 1);
				instruccionesFinal.SetActive(true);
				recordsFinal.SetActive(false);
				replayFinal.SetActive(false);
			}
			else{
				instruccionesFinal.SetActive(false);
				recordsFinal.SetActive(true);
				replayFinal.SetActive(true);
			}
		} else {
			instruccionesFinal.SetActive(false);
			botonRevive.SetActive (false);
			botonReviveComprado.SetActive (false);
			recordsFinal.SetActive(true);
			replayFinal.SetActive(true);
		}
		foreach(TweenPosition t in juegoUI)
			t.PlayReverse();
		foreach(TweenPosition t in resumenUI)
			t.PlayForward();
		resumenBotonesUI.delay = 2f;
		resumenBotonesUI.PlayForward ();
	}

	public void reiniciar(){
		foreach(TweenPosition t in resumenUI)
			t.PlayReverse();
		resumenBotonesUI.delay = 0f;
		resumenBotonesUI.PlayReverse();
		StartCoroutine ("cargarEscena", Application.loadedLevelName);
	}

	public void verLogros(){
		if (gameCenterNemoris != null) {
			gameCenterNemoris.mostrarLogros ();
		}
	}

	public void compartirFacebook(){
		Application.OpenURL ("https://www.facebook.com/sharer/sharer.php?u=http://nemorisgames.com/come-home-space-carrot-bunny/&t=I%20have%20a%20record%20of%2021%20in%20Come%20Home,%20Space%20Carrot%20Bunny.%20Can%20you%20beat%20me?%20");
	}
	
	public void compartirTwitter(){
		//Application.OpenURL ("https://mobile.twitter.com/home?status=I%20have%20a%20record%20of%2021%20in%20Come%20Home,%20Space%20Carrot%20Bunny.%20Can%20you%20beat%20me?%20http://nemorisgames.com/come-home-space-carrot-bunny/");
		Application.OpenURL ("http://twitter.com/intent/tweet?text=I%20have%20a%20new%20record%20in%20Come%20Home,%20Space%20Carrot%20Bunny.%20Can%20you%20beat%20me?%20http://nemorisgames.com/come-home-space-carrot-bunny/");
	}

	IEnumerator cargarEscena(string nombre){
		transicion.delay = 0.3f;
		transicion.PlayReverse ();
		yield return new WaitForSeconds (1f);
		Application.LoadLevel (nombre);
	}

	public void eliminarPlaneta(Planeta p){
		//Destroy(p.gameObject);
		planetaAuxiliar = p;
		for (int i = 1; i < planetas.Length; i++) {
			planetas[i - 1] = planetas[i];
		}
		agregarPlaneta ();
		zanahoria.actualizarPlanetas (planetas);
	}

	public void agregarPlaneta(){
		//Central.AreaSpawnPlanetas = new Rect (50f + (15f - 15f * Mathf.Clamp01(nPlanetasEliminados / 10f)), -20f + (15f - 15f * Mathf.Clamp01(nPlanetasEliminados / 10f)), 30f * Mathf.Clamp01(nPlanetasEliminados / 10f), 40f * Mathf.Clamp01(nPlanetasEliminados / 10f));
		nPlanetasEliminados++;
		//GameObject g = (GameObject)Instantiate (planetaPrefab);
		//Planeta p = g.GetComponent<Planeta> ();
		planetaAuxiliar.inicializar (planetas[planetas.Length - 2]);
		planetas[planetas.Length - 1] = planetaAuxiliar;
	}

	public void salir(){
		if(HeyzapAds.onBackPressed())
			return;
		else
			Application.Quit ();
	}

	public void reanudar(){
		estado = EstadoJuego.EnJuego;
		Time.timeScale = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (tiempoSpawnActual < Time.time) {
			Transform t;
			GameObject g;
			int tipo = Random.Range(0, 5);
			switch(tipo){
			case 0:
				if(meteoros != null && meteoros.Length > 0){
					t = spawnMeteoros[Random.Range(0, spawnMeteoros.Length)];
					g = (GameObject)Instantiate (meteoros[Random.Range(0, meteoros.Length)], t.position, t.rotation);
				}
				break;
			case 1:
				if(superiores != null && superiores.Length > 0){
					t = spawnSuperiores[Random.Range(0, spawnSuperiores.Length)];
					g = (GameObject)Instantiate (superiores[Random.Range(0, superiores.Length)], t.position, t.rotation);
				}
				break;
			case 2:
				if(ovnis != null && ovnis.Length > 0){
					t = spawnOvnis[Random.Range(0, spawnOvnis.Length)];
					g = (GameObject)Instantiate (ovnis[Random.Range(0, meteoros.Length)], t.position, t.rotation);
				}
				break;
			case 3:
				if(inferiores != null && inferiores.Length > 0){
					t = spawnInteriores[Random.Range(0, spawnInteriores.Length)];
					g = (GameObject)Instantiate (inferiores[Random.Range(0, meteoros.Length)], t.position, t.rotation);
				}
				break;
			}
		}
		if (Input.GetKeyUp (KeyCode.Escape)) {
			switch(estado){
			case EstadoJuego.Titulo:
				estado = EstadoJuego.Pausa;
				panelSalir.PlayForward();
				Time.timeScale = 0f;
				break;
			case EstadoJuego.EnJuego:
				estado = EstadoJuego.Pausa;
				panelSalir.PlayForward();
				Time.timeScale = 0f;
				break;
			case EstadoJuego.Pausa:
				panelSalir.PlayReverse();
				reanudar();
				break;
			case EstadoJuego.Terminado:
				reiniciar();
				break;
			}
		}
	}
}