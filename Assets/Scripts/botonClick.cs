using UnityEngine;
using System.Collections;

public class botonClick : MonoBehaviour {
	public SpriteRenderer sp;
	bool presionado = false;
	// Use this for initialization
	void Start () {
	
	}

	void OnMouseUp(){
		if (!presionado) {
			Camera.main.gameObject.SendMessage ("botonPlayPresionado");
			presionado = true;
			GetComponent<AudioSource>().Play();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (presionado) {
			transform.localScale *= 1.01f;
			sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a - 0.012f);
			if(sp.color.a <= 0f) Destroy (gameObject);
		}
	}
}
