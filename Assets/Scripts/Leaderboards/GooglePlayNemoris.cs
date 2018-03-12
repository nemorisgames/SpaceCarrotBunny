using UnityEngine;
using System.Collections;
#if UNITY_ANDROID
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
//using UnityEngine.SocialPlatforms;
#endif
public class GooglePlayNemoris : MonoBehaviour {
#if UNITY_ANDROID
	bool autenticado = false;
	string mensaje = "";
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		Social.localUser.Authenticate (autenticar);
	}

	void autenticar(bool exito){
		autenticado = exito;
		mensaje = "autenticado " + exito + "\n";
		mensaje += "user " + Social.localUser.userName + "\n";
		mensaje += "id " + Social.localUser.id + "\n";
		mensaje += "underage " + Social.localUser.underage + "\n";
	}

	public void mostrarRanking(){
//		Social.LoadScores ("planets", cargarRanking);
	}
	
	/*void cargarRanking(IScore[] puntuaciones){
		//con los datos siguientes se puede programar una GUI propia para mostrar
		if (puntuaciones.Length > 0) {
			print ("recibidos " + puntuaciones.Length);
			foreach(IScore a in puntuaciones){
				print ("id " + a.userID);
				print ("rank " + a.rank);
				print ("valor " + a.value);
			}
		}
		
		Social.ShowLeaderboardUI ();
	}*/

	void OnGUI(){
		//GUI.Box (new Rect (10f, 200f, 500f, 300f), mensaje);
	}
	// Update is called once per frame
	void Update () {
	
	}
	#endif
}
