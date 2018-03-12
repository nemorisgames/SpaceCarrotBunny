using UnityEngine;
using System.Collections;

public class ControlCamara : MonoBehaviour {
	Transform zanahoria;
	Central central;
	Camera camara;

	// Use this for initialization
	void Start () {
		zanahoria = GameObject.FindWithTag ("Player").transform;
		central = gameObject.GetComponent<Central> ();
		camara = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		switch (central.estado) {
		/*case Central.EstadoJuego.Titulo:
			transform.position = new Vector3 (Mathf.Lerp(transform.position.x, zanahoria.position.x, 1f * Time.deltaTime), Mathf.Lerp(transform.position.y, zanahoria.position.y, 1f * Time.deltaTime), transform.position.z);
			camara.orthographicSize = Mathf.Lerp(camara.orthographicSize, 10f, 1f * Time.deltaTime);
			break;*/
		case Central.EstadoJuego.EnJuego:
			transform.position = new Vector3 (Mathf.Lerp(transform.position.x, zanahoria.position.x + 15f, 1f * Time.deltaTime), Mathf.Lerp(transform.position.y, 0f, 1f * Time.deltaTime), transform.position.z);
			camara.orthographicSize = Mathf.Lerp(camara.orthographicSize, 22f, 0.5f * Time.deltaTime);
			break;
		}
	}
}
