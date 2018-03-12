using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;

using System.Runtime.InteropServices;
#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif
using UnityEngine.SocialPlatforms;

public class GameCenterNemoris : MonoBehaviour {

	bool autenticado = false;
	string mensaje = "";
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		#if UNITY_ANDROID
		PlayGamesPlatform.Activate ();
		#endif
		Social.localUser.Authenticate (autenticar);

		//Social.ShowLeaderboardUI ();
	}

	void autenticar(bool exito){
		autenticado = exito;
		mensaje = "autenticado " + exito + "\n";
		mensaje += "user " + Social.localUser.userName + "\n";
		mensaje += "id " + Social.localUser.id + "\n";
		mensaje += "underage " + Social.localUser.underage + "\n";
	}

	public void mostrarLogros(){
		Social.LoadAchievements (cargarLogros);
	}

	public void mostrarRanking(){
		Social.LoadScores ("planets", cargarRanking);
	}

	void cargarRanking(IScore[] puntuaciones){
		mensaje = "";
		//con los datos siguientes se puede programar una GUI propia para mostrar
		if (puntuaciones.Length > 0) {
			print ("recibidos " + puntuaciones.Length);
			foreach(IScore a in puntuaciones){
				mensaje += "id " + a.userID + "\n";
				mensaje += "rank " + a.rank + "\n";
				mensaje += "valor " + a.value + "\n";
			}
		}
		
		Social.ShowLeaderboardUI ();
	}

	public void enviarRanking(string id, int puntos){
		#if UNITY_ANDROID
			
		#endif
		#if UNITY_IOS
		if (Social.localUser.authenticated) {
			Social.Active.ReportScore((long)puntos, id, reporteRanking);
		}
		#endif
	}

	void reporteRanking(bool exito){
		mensaje = ("puntaje enviado " + exito);
	}

	void cargarLogros(IAchievement[] logros){
		mensaje = "";
		//con los datos siguientes se puede programar una GUI propia para mostrar
		if (logros.Length > 0) {
			mensaje += "recibidos " + logros.Length + "\n";
			foreach(IAchievement a in logros){
				mensaje += "id " + a.id + "\n";
				mensaje += "completed " + a.completed + "\n";
			}
		}

		Social.ShowAchievementsUI ();
	}

	public void enviarLogro(string id, double porcentaje){
		//ESTO NO FUNCIONA!
		print ("enviar logro " + id);
		if (Social.localUser.authenticated) {
			Social.ReportProgress(id, porcentaje, reporteLogro);
		}
	}

	void reporteLogro(bool exito){
		mensaje = ("logro reportado " + exito);
	}
	void OnGUI(){
		//GUI.Box (new Rect (10f, 200f, 500f, 300f), mensaje);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
