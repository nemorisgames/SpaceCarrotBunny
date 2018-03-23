using UnityEngine;
using System.Collections;

public class Zanahoria : MonoBehaviour
{
    public Planeta[] planetas;
    Rigidbody2D rigidbody;
    public float fuerzaCarga = 0f;
    public float fuerzaCargaMaximo = 20f;
    public Transform punto;
    Central central;

    public Animator animator;

    int contadorPlanetas = 0;
    int contadorBonus = 0;

    public UILabel distanciaLabel;
    public UILabel conejosLabel;
    int nConejos = 0;

    public ParticleSystem cargando;
    public ParticleSystem explosion;
    public ParticleSystem humo;

    public AudioSource cargandoAudio;
    public AudioSource motorAudio;
    public AudioSource conejosAudio;
    public AudioClip explosionSonido;
    public AudioClip aterrizajeSonido;
    public AudioClip rescateSonido;
    public AudioClip[] conejosGritos;
    float tiempoConejoGrito;

    public enum EstadoZanahoria { desactivado, enPlaneta, preparando, enDisparo, enHoyoNegro };
    public EstadoZanahoria estado = EstadoZanahoria.enPlaneta;
    Planeta ultimoPlaneta;

    //GameCenterNemoris gameCenterNemoris;

    public UISprite barraCarga;

    int altoSugerido;
    // Use this for initialization
    void Start()
    {
        /*GameObject g = GameObject.Find ("GameCenterNemoris");
		if (g != null) {
			gameCenterNemoris = g.GetComponent<GameCenterNemoris>();
		}*/
        nConejos = PlayerPrefs.GetInt("nConejos", 0);
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;
        central = Camera.main.GetComponent<Central>();
        conejosLabel.text = "" + nConejos;

        barraCarga.transform.parent.gameObject.SendMessage("PlayReverse");
        altoSugerido = barraCarga.root.transform.GetComponent<UIRoot>().activeHeight;
    }

    public void activar()
    {
        estado = Zanahoria.EstadoZanahoria.enPlaneta;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (estado == EstadoZanahoria.enDisparo)
        {
            humo.Stop();
            Planeta p = coll.gameObject.GetComponent<Planeta>();
            ultimoPlaneta = p;
            if (!p.aterrizado)
            {
                contadorPlanetas++;
            }
            if (p.tieneConejo)
            {
                central.mostrarNuevoConejo("+1");
                nConejos++;
                PlayerPrefs.SetInt("nConejos", nConejos);
                /*if (gameCenterNemoris != null) {
#if UNITY_IOS
					if(nConejos == 1) gameCenterNemoris.enviarLogro("firstrabbit", 100.0);
#endif
#if UNITY_ANDROID
					if(nConejos == 1) gameCenterNemoris.enviarLogro("CgkI6uTtj40GEAIQAw", 100.0);
#endif
				}*/

                conejosLabel.text = "" + nConejos;
                p.quitarConejo();
                conejosAudio.PlayOneShot(rescateSonido);
            }
            else
                if (!p.aterrizado) conejosAudio.PlayOneShot(aterrizajeSonido);
            if (!p.aterrizado)
                p.aterrizando();
            rigidbody.velocity = new Vector2(0f, 0f);
            rigidbody.angularVelocity = 0f;
            rigidbody.isKinematic = true;
            punto = coll.transform.Find("puntoFijo");
            estado = EstadoZanahoria.enPlaneta;
            fuerzaCarga = 0f;

            barraCarga.transform.parent.gameObject.SendMessage("PlayReverse");

            for (int i = 0; i < planetas.Length; i++)
            {
                if (planetas[i].transform.position.x < transform.position.x && !planetas[i].aterrizado)
                {
                    print("bonus");
                    contadorBonus++;
                    contadorPlanetas += 2 * contadorBonus;
                    planetas[i].aterrizando();
                    planetas[i].bonus(2 * contadorBonus);
                }
            }
            distanciaLabel.text = "" + contadorPlanetas;
            motorAudio.Stop();
            animator.SetBool("enVuelo", false);

            central.marcaBarra.SetActive(false);
        }
    }

    public void actualizarPlanetas(Planeta[] p)
    {
        planetas = null;
        planetas = new Planeta[p.Length];
        for (int i = 0; i < planetas.Length; i++)
        {
            planetas[i] = p[i];
        }
    }

    public void revivir()
    {
        estado = EstadoZanahoria.enPlaneta;
        animator.SetBool("enVuelo", false);
        fuerzaCarga = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (central.estado == Central.EstadoJuego.Pausa && estado != EstadoZanahoria.enDisparo)
            return;
        switch (estado)
        {
            case Zanahoria.EstadoZanahoria.enPlaneta:
                transform.position = punto.position;
                transform.rotation = punto.rotation;
                if (Input.GetButtonDown("Fire1"))
                {
                    estado = EstadoZanahoria.preparando;
                    cargando.Play();
                    cargandoAudio.Play();
                    animator.SetBool("cargando", true);
                    barraCarga.transform.parent.localPosition = new Vector3(Camera.main.WorldToScreenPoint(transform.position).x + 50f, Camera.main.WorldToScreenPoint(transform.position).y + 75f, 0f);
                    barraCarga.transform.parent.gameObject.SendMessage("PlayForward");
                    barraCarga.fillAmount = 0f;
                }
                break;
            case EstadoZanahoria.preparando:
                transform.position = punto.position;
                transform.rotation = punto.rotation;
                if (Input.GetButton("Fire1"))
                {
                    fuerzaCarga += 1000f * Time.deltaTime;
                    if (fuerzaCarga > fuerzaCargaMaximo)
                    {
                        fuerzaCarga = fuerzaCargaMaximo;
                        disparar();
                    }
                    barraCarga.fillAmount = (fuerzaCarga / fuerzaCargaMaximo);

                    //print (Input.mousePosition.x + " " + Input.mousePosition.y);
                    //barraCarga.transform.parent.localPosition = new Vector3(Camera.main.WorldToScreenPoint(transform.position).x + 0f, Camera.main.WorldToScreenPoint(transform.position).y + 0f, 0f) * altoSugerido / Screen.height;//Input.mousePosition.x + 50f, Input.mousePosition.y + 75f, 0f);
                }
                if (Input.GetButtonUp("Fire1"))
                {
                    disparar();
                }
                break;
            case EstadoZanahoria.enDisparo:
                print(rigidbody.velocity.magnitude);
                if (transform.eulerAngles.z < 0f || transform.eulerAngles.z > 180f)
                {
                    motorAudio.pitch = 0.3f;
                    humo.Stop();
                }
                else
                    motorAudio.pitch = rigidbody.velocity.magnitude / 40f;

                if (tiempoConejoGrito < Time.time)
                {
                    conejoGritar();
                }

                Vector3 fuerzaGravedad = Vector3.zero;
                foreach (Planeta p in planetas)
                {
                    float distancia = Vector3.Distance(p.transform.position, transform.position);
                    if (distancia <= p.distanciaEfecto && !p.aterrizado)
                        fuerzaGravedad += ((p.transform.position - transform.position) / distancia) * p.gravedad;
                }
                rigidbody.AddForce(new Vector2(fuerzaGravedad.x, fuerzaGravedad.y) * Time.deltaTime);

                transform.right = rigidbody.velocity.normalized;

                if (transform.position.y < -40f)
                {
                    barraCarga.transform.parent.gameObject.SendMessage("PlayReverse");
                    estado = EstadoZanahoria.enHoyoNegro;
                    rigidbody.isKinematic = true;
                    central.resumenJuego(contadorPlanetas, nConejos);
                    motorAudio.Stop();
                }

                break;
        }
        //barraCarga.transform.parent.localPosition = new Vector3(Camera.main.WorldToScreenPoint(transform.position).x + 0f, Camera.main.WorldToScreenPoint(transform.position).y + 0f, 0f) * altoSugerido / Screen.height;//Input.mousePosition.x + 50f, Input.mousePosition.y + 75f, 0f);
    }

    void disparar()
    {
        if (fuerzaCarga > 500f)
        {
            print(new Vector2(transform.right.x, transform.right.y) * fuerzaCarga);
            rigidbody.isKinematic = false;
            rigidbody.AddForce(new Vector2(transform.right.x, transform.right.y) * fuerzaCarga);
            estado = EstadoZanahoria.enDisparo;
            explosion.Play();
            conejoGritar();
            humo.Play();
            animator.SetBool("enVuelo", true);
            motorAudio.Play();
            conejosAudio.PlayOneShot(explosionSonido);
        }
        else
        {
            barraCarga.transform.parent.gameObject.SendMessage("PlayReverse");
            fuerzaCarga = 0f;
            estado = EstadoZanahoria.enPlaneta;
        }

        animator.SetBool("cargando", false);
        cargandoAudio.Stop();
        cargando.Stop();
    }

    void conejoGritar()
    {
        conejosAudio.PlayOneShot(conejosGritos[Random.Range(0, conejosGritos.Length)]);
        tiempoConejoGrito = Time.time + Random.Range(2.5f, 5f);
    }
}

