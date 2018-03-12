using UnityEngine;
using System.Collections;

public class CargaInicial : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("primeraEjecucion", 0);
		PlayerPrefs.SetInt("instruccionesFinalMostrado", 0);
		//PlayerPrefs.SetInt ("nConejos", 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
