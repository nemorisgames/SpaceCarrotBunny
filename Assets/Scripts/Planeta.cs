using UnityEngine;
using System.Collections;

public class Planeta : MonoBehaviour {
	int id = 0;
	public float gravedad = 10f;
	public float distanciaEfecto = 15f;
	public Vector2 rangoDiametro;
	public Vector2 rangoEfecto;
	public Transform[] objetos;
	Transform camara;
	Transform conejo;
	Central central;
	Transform areaGravedad;

	public TextMesh texto;
	public TweenScale textoEscala;
	public TweenPosition textoPos;
	public ParticleSystem particulasNormal;
	public ParticleSystem particulasBonus;
	public ParticleSystem particulas;

	public bool tieneConejo = false;
	public bool aterrizado = false;

	public Transform objetoRotacion;
	float velocidadRotacion = -35f;

	public float rangoMovimientoX = 0.1f;
	public float rangoMovimientoY = 0.1f;

	public TrailRenderer trail;

	// Use this for initialization
	void Start () {
		camara = Camera.main.transform;
		conejo = transform.FindChild ("puntoRotacion/pivoteConejo/placeholderConejo/conejo");
		central = camara.GetComponent<Central> ();
		areaGravedad = transform.FindChild ("gravedad");
		distanciaEfecto = Random.Range (rangoEfecto.x, rangoEfecto.y);
		int indiceAux = 0;
		switch (gameObject.name) {
		case "planeta1": indiceAux = 0; break;
		case "planeta2": indiceAux = 1; break;
		case "planeta3": indiceAux = 2; break;
		case "planeta4": indiceAux = 3; break;
		case "planeta5": indiceAux = 4; break;
		case "planeta6": indiceAux = 5; break;
		case "planeta7": indiceAux = 6; break;
		}
		transform.localScale = Vector3.one * Random.Range (Mathf.Clamp(rangoDiametro.x + 1f - Mathf.Clamp(indiceAux / 20f, 0f, 1f), 0f, rangoDiametro.y), Mathf.Clamp(rangoDiametro.y * ( 1f - 0.4f * Mathf.Clamp(indiceAux / 20f, 0f, 1f)), rangoDiametro.x, 100f));
		foreach(Transform t in objetos)
			t.localScale = Vector3.one * 0.5f / transform.localScale.x;
		areaGravedad.localScale = Vector3.one * 0.666f * distanciaEfecto / transform.localScale.x;
		texto.transform.localScale = Vector3.one * 0.5f / transform.localScale.x;
		if (gameObject.name == "planeta5" || gameObject.name == "planeta6") { 
			conejo.parent.localScale = Vector3.one * 1.2f / transform.localScale.x;
			tieneConejo = Random.Range (0, 100) <= 40;
			conejo.gameObject.SetActive (tieneConejo);
		}
		velocidadRotacion = (Random.Range(0f,1f)>0.5f?1f:-1f) * 35f + Random.Range (-10f, 10f);
	}

	public void quitarConejo(){
		particulas.Play ();
		conejo.gameObject.SetActive (false);
	}

	public void aterrizando(){
		//texto.text = "+1";
		if (aterrizado)
			return;
		central.mostrarNuevoScore ("+1");
		textoEscala.PlayForward();
		textoPos.PlayForward();
		particulasNormal.Play ();
		aterrizado = true;
	}

	public void bonus(int multi){
		//texto.text = "x" + multi;
		central.mostrarNuevoScore ("x" + multi);
		textoEscala.PlayForward();
		textoPos.PlayForward();
		particulasBonus.Play ();
	}

	public void inicializar(Planeta p){
		trail.Reset(this);
		aterrizado = false;
		if (camara == null) {
			camara = Camera.main.transform;
			central = camara.GetComponent<Central> ();
		}
		transform.position = new Vector2 (p.transform.position.x + Random.Range (Central.AreaSpawnPlanetas.x, Central.AreaSpawnPlanetas.x + Central.AreaSpawnPlanetas.width), Random.Range (Central.AreaSpawnPlanetas.y, Central.AreaSpawnPlanetas.y + Central.AreaSpawnPlanetas.height)); 
		transform.localScale = Vector3.one * Random.Range (Mathf.Clamp(rangoDiametro.x + 1f - Mathf.Clamp(central.nPlanetasEliminados / 20f, 0f, 1f), 0f, rangoDiametro.y), Mathf.Clamp(rangoDiametro.y * ( 1f - 0.4f * Mathf.Clamp(central.nPlanetasEliminados / 20f, 0f, 1f)), rangoDiametro.x, 100f));
		distanciaEfecto = Random.Range (rangoEfecto.x, rangoEfecto.y);
		foreach(Transform t in objetos)
			t.localScale = Vector3.one * 0.5f / transform.localScale.x;
		conejo.parent.localScale = Vector3.one * 1.2f / transform.localScale.x;
		areaGravedad.localScale = Vector3.one * 0.666f * distanciaEfecto / transform.localScale.x;
		tieneConejo = Random.Range (0, 100) <= 25;
		conejo.gameObject.SetActive(tieneConejo);
		texto.transform.localScale = Vector3.one * 0.5f / transform.localScale.x;
		velocidadRotacion = (Random.Range(0f,1f)>0.5f?1f:-1f) * 35f + Random.Range (-10f, 10f);

		float rangoMovimiento = Mathf.Clamp(central.nPlanetasEliminados * 0.05f, 0f, 0.2f);
		rangoMovimientoX = (Random.Range(0, 100) <  Mathf.Clamp(central.nPlanetasEliminados * 2f, 0f, 100f))?(Mathf.Sign(Random.Range(-1, 1)) * rangoMovimiento):0f;
		if ((Random.Range (0, 100) < Mathf.Clamp (central.nPlanetasEliminados * 5f, 0f, 100f))) {
			if(rangoMovimientoX != 0f){
				rangoMovimientoY = Mathf.Sign(Random.Range(-1, 1)) * rangoMovimientoX;
			}
			else{
				rangoMovimientoY = (Mathf.Sign(Random.Range(-1, 1)) * rangoMovimiento);
			}
		}

		texto.text = "";
		textoEscala.ResetToBeginning();
		textoPos.ResetToBeginning ();
	}
	
	// Update is called once per frame
	void Update () {
		objetoRotacion.Rotate (0f, 0f, velocidadRotacion * Time.deltaTime);
		if (camara.position.x - transform.position.x > 130f) {

			central.eliminarPlaneta(this);
		}
		transform.position += new Vector3(rangoMovimientoX * Mathf.Cos(Time.time%360), rangoMovimientoY * Mathf.Sin(Time.time%360), 0f);
	}
}
