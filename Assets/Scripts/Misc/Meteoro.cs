using UnityEngine;
using System.Collections;

public class Meteoro : MonoBehaviour {
	bool activado = false;
	Transform camara;
	float velocidad = 1f;
	public Vector2 velocidadRango = new Vector2(0.2f, 0.5f);
	Vector3 posicionInicial;
	AudioSource audio;
	public float tiempoEsperaMaximo = 30f;
	// Use this for initialization
	IEnumerator Start () {
		audio = gameObject.GetComponent<AudioSource> ();
		camara = Camera.main.transform;
		posicionInicial = transform.position - camara.position;
		yield return new WaitForSeconds (Random.Range(2f, tiempoEsperaMaximo * 2f / 3f));
		activar ();
	}

	IEnumerator activarMeteoro(){
		//print ("activando");
		yield return new WaitForSeconds (Random.Range(2f, tiempoEsperaMaximo));
		transform.position = posicionInicial + camara.position;
		//print ("activando");
		activar ();
	}

	public void activar(){
		//print ("activado");
		audio.Play ();
		activado = true;
		velocidad = Random.Range (velocidadRango.x, velocidadRango.y);
		audio.pitch = 1f + (velocidad - velocidadRango.x) / (velocidadRango.y - velocidadRango.x);
	}
	
	// Update is called once per frame
	void Update () {
		audio.volume = ((40f - Mathf.Clamp(Vector3.Distance (transform.position, camara.position), 0f, 40f)) / 40f) * 0.2f;
		if (!activado)
			return;
		transform.position = new Vector2 (transform.position.x + transform.right.x * velocidad, transform.position.y + transform.right.y * velocidad);
		if (Vector3.Distance (transform.position, camara.position) > 100f) {
			activado = false;
			audio.Stop();
			StartCoroutine (activarMeteoro ());
		}
		/*if (Vector3.Distance (transform.position, camara.position) < 30f && !audio.isPlaying) {
			audio.Play();
		}*/
	}
}
