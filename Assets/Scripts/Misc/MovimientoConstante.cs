using UnityEngine;
using System.Collections;

public class MovimientoConstante : MonoBehaviour {
	public float velocidad = 10f;
	public float aceleracion = 0.01f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		velocidad += aceleracion;
		transform.position = new Vector2 (transform.position.x + transform.right.x * velocidad, transform.position.y + transform.right.y * velocidad);
	}
}
