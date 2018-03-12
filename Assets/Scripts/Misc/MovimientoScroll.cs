using UnityEngine;
using System.Collections;

public class MovimientoScroll : MonoBehaviour {
	Transform camara;
	float scroll = 0f;
	float ultimaPos = 0f;
	float velocidad = 1f;

	// Use this for initialization
	void Start () {
		velocidad = Random.Range (0.65f, 0.65f);
		camara = Camera.main.transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		scroll = camara.position.x - ultimaPos;
		ultimaPos = camara.position.x;
		transform.position = new Vector2 (transform.position.x + scroll * velocidad, transform.position.y);
	}
}
