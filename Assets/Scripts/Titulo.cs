using UnityEngine;
using System.Collections;

public class Titulo : MonoBehaviour {
	
	public TweenPosition tituloUI;
	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt("nConejos", 0);
		PlayerPrefs.SetInt("maximoPlanetas", 0);
	}

	public void comenzarJuego(){
		tituloUI.gameObject.SendMessage ("PlayReverse");
		StartCoroutine (cargarEscena ());
	}

	IEnumerator cargarEscena(){
		yield return new WaitForSeconds (1f);
		Application.LoadLevel ("Escena1");
	}

	// Update is called once per frame
	void Update () {
	
	}
}
