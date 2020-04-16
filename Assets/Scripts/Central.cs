using UnityEngine;
using System.Collections;

public class Central : MonoBehaviour {
	Zanahoria zanahoria;
	public GameObject planetaPrefab;
	public Planeta[] planetas;
	public static Rect AreaSpawnPlanetas = new Rect(16f, -15f, 26f, 30f);
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

	//GameCenterNemoris gameCenterNemoris;

	public GameObject botonRevive;
	public GameObject botonReviveComprado;

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

	public GameObject camaraLogo;
	public TweenAlpha transicion;

	public GameObject marcaBarra;

	//IAPNemoris store;

	//GPGSNemoris gpgsNemoris;
	// Use this for initialization
	void Start () {
		/*GameObject s = GameObject.Find ("IAPNemoris");
		if (s != null)
			store = s.GetComponent<IAPNemoris> ();*/
		Time.timeScale = 1f;
		//botonRevive.SetActive(PlayerPrefs.GetInt ("activateAdsAdBuddiz", 1) == 1);
		//botonReviveComprado.SetActive(PlayerPrefs.GetInt ("activateAdsAdBuddiz", 1) == 0);
		/*GameObject g = GameObject.Find ("GameCenterNemoris");
		if (g != null) {
			gameCenterNemoris = g.GetComponent<GameCenterNemoris>();
		}
		
		GameObject g2 = GameObject.Find ("GPGSNemoris");
		if (g2 != null) {
		//	gpgsNemoris = g2.GetComponent<GPGSNemoris>();
		}*/
		nPlanetas = planetas.Length;
		zanahoria = GameObject.FindWithTag ("Player").GetComponent<Zanahoria> ();

		if (PlayerPrefs.GetInt ("primeraEjecucion", 0) == 1) {
			botonPlay.SetActive (false);
			comenzarJuego ();
		} else {
			audioPrincipal.PlayOneShot(inicioMusica);
		}
	}

	public void comprar(){
		//if (store.compraTerminada)
		//	store.comprar (0);
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
		estado = EstadoJuego.EnJuego;
		//zanahoria.GetComponent<Rigidbody2D> ().isKinematic = false;
		zanahoria.activar ();
		marcaBarra.SetActive(true);
		//tituloUI.PlayReverse();
		foreach(TweenPosition t in juegoUI)
			t.PlayForward();
		//print (PlayerPrefs.GetInt ("primeraEjecucion", 0)+"||"+ PlayerPrefs.GetInt ("maximoPlanetas", 0));
		if (PlayerPrefs.GetInt ("primeraEjecucion", 0) == 0 || PlayerPrefs.GetInt ("maximoPlanetas", 0) <= 1) {
			//transicion.delay = 0.3f;
			instrucciones.SetActive (true);
		} else{
			camaraLogo.SetActive (false);
		}
		PlayerPrefs.SetInt ("primeraEjecucion", 1);
		revivido = false;
	}

	public void verRanking(){
		/*if (gameCenterNemoris != null) {
			gameCenterNemoris.mostrarRanking();
		}*/
		
	/*	if (gpgsNemoris != null) {
			gpgsNemoris.verRecords();
		}*/
	}

	public void revive(){
		//GameObject ads = GameObject.Find ("AppBudizz");
		/*if (PlayerPrefs.GetInt ("activateAdsAdBuddiz", 0) == 1 && ads != null) {
			ads.SendMessage("mostrarAdBuddiz");
		}*/
		foreach(TweenPosition t in juegoUI)
			t.PlayForward();
		foreach(TweenPosition t in resumenUI)
			t.PlayReverse();
		resumenBotonesUI.PlayReverse();
		resumenMedallaSprite.gameObject.SetActive(false);
		botonRevive.SetActive(false);
		botonReviveComprado.SetActive(false);
		revivido = true;
	}

	public void resumenJuego(int planetas, int nConejos){
		/*if (gameCenterNemoris != null) {
			if(planetas > PlayerPrefs.GetInt("maximoPlanetas", 0)){
				#if UNITY_IOS
				gameCenterNemoris.enviarRanking ("planets", planetas);
				#endif
				#if UNITY_ANDROID
				gameCenterNemoris.enviarRanking ("CgkI6uTtj40GEAIQAQ", planetas);
				#endif
			}
			if(PlayerPrefs.GetInt ("nConejos", 0) > 0 && nConejos > 0){
				#if UNITY_IOS
				gameCenterNemoris.enviarRanking ("rescued", PlayerPrefs.GetInt ("nConejos", 0));
				#endif
				#if UNITY_ANDROID
				gameCenterNemoris.enviarRanking ("CgkI6uTtj40GEAIQAg", PlayerPrefs.GetInt ("nConejos", 0));
				#endif

			}
		}*/

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
			resumenMedallaSprite.spriteName = n;
			resumenMedallaSprite.SendMessage ("PlayForward");
			medallaParticulas.Play ();
			audioPrincipal.PlayOneShot(slideSonidoMedalla);
		} else {
			audioPrincipal.PlayOneShot(slideSonidoNormal);
			resumenMedallaSprite.gameObject.SetActive (false);
		}

		/*if (planetas >= 3 && !revivido) {
			//botonRevive.SetActive (PlayerPrefs.GetInt ("activateAdsAdBuddiz", 1) == 1);
			//botonReviveComprado.SetActive (PlayerPrefs.GetInt ("activateAdsAdBuddiz", 1) == 0);
			if(PlayerPrefs.GetInt("instruccionesFinalMostrado", 0) == 0){
				PlayerPrefs.SetInt("instruccionesFinalMostrado", 1);
				instruccionesFinal.SetActive(true);
				//recordsFinal.SetActive(false);
				replayFinal.SetActive(false);
			}
			else{
				instruccionesFinal.SetActive(false);
				//recordsFinal.SetActive(true);
				replayFinal.SetActive(true);
			}
		} else {
			instruccionesFinal.SetActive(false);
			botonRevive.SetActive (false);
			botonReviveComprado.SetActive (false);
			//recordsFinal.SetActive(true);
			replayFinal.SetActive(true);
		}*/
		instruccionesFinal.SetActive(false);
		botonRevive.SetActive (false);
		botonReviveComprado.SetActive (false);
		//recordsFinal.SetActive(true);
		replayFinal.SetActive(true);
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
		/*if (gameCenterNemoris != null) {
			gameCenterNemoris.mostrarLogros ();
		}*/
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
		Central.AreaSpawnPlanetas = new Rect (16f + (15f - 15f * Mathf.Clamp01(nPlanetasEliminados / 10f)), -15f + (15f - 15f * Mathf.Clamp01(nPlanetasEliminados / 10f)), 26f * Mathf.Clamp01(nPlanetasEliminados / 10f), 30f * Mathf.Clamp01(nPlanetasEliminados / 10f));
		nPlanetasEliminados++;
		//GameObject g = (GameObject)Instantiate (planetaPrefab);
		//Planeta p = g.GetComponent<Planeta> ();
		planetaAuxiliar.inicializar (planetas[planetas.Length - 2]);
		planetas[planetas.Length - 1] = planetaAuxiliar;
	}

	public void salir(){
		/*if(HeyzapAds.onBackPressed())
			return;
		else*/
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
