using UnityEngine;
using System.Collections;

public class PasarEscena : MonoBehaviour {
	public float tiempo;
	public string siguienteEscena;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (tiempo < Time.timeSinceLevelLoad) {
			Application.LoadLevel(siguienteEscena);
		}
	}
}
