using UnityEngine;
using System.Collections;

public class regeneracionAleatoria : MonoBehaviour {
	Transform camara;
	public Vector2 rangoDiametro;
	float scroll = 0f;
	float ultimaPos = 0f;
	float velocidad = 1f;
	public bool rotacionAleatoria = false;
	// Use this for initialization
	void Start () {
		velocidad = Random.Range (0.2f, 0.6f);
		camara = Camera.main.transform;
		transform.localScale = Vector3.one * Random.Range (rangoDiametro.x, rangoDiametro.y);
		if (rotacionAleatoria)
			transform.Rotate (0f, 0f, Random.Range (0, 360));
	}

	void generar(){
		velocidad = Random.Range (0.2f, 0.6f);
		ultimaPos = camara.position.x;
		transform.position = new Vector2 (camara.transform.position.x + Random.Range (80f, 120f), Random.Range (-10f, 20f)); 
		transform.localScale = Vector3.one * Random.Range (rangoDiametro.x, rangoDiametro.y);
		if (rotacionAleatoria)
			transform.Rotate (0f, 0f, Random.Range (0, 360));
	}
	
	// Update is called once per frame
	void LateUpdate () {
		scroll = camara.position.x - ultimaPos;
		ultimaPos = camara.position.x;
		transform.position = new Vector2 (transform.position.x + scroll * velocidad, transform.position.y);
		if (camara.position.x - transform.position.x > 100f) {
			SendMessage("generar");
		}
	}
}
