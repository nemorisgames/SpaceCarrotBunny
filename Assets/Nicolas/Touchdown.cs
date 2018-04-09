using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Touchdown : MonoBehaviour
{
    
    public GameObject Bala;

    public GameObject GameWinText;
    public static GameObject GameWnStatic;
    public GameObject Fondos;
    public static GameObject Fondo;
    public GameObject Scor;
    public static GameObject ScorSta;
    public GameObject Time;
    public static GameObject TimeSta;
    public GameObject Total;
    public static GameObject TotalSta;

    bool Balon = false;
    bool Pantalla = false;
    // Use this for initialization
    void Start(){

        Touchdown.GameWnStatic = GameWinText;
        Touchdown.GameWnStatic.gameObject.SetActive(false);
        Touchdown.Fondo = Fondos;
        Touchdown.Fondo.gameObject.SetActive(false);
        Touchdown.TimeSta = Time;
        Touchdown.TimeSta.gameObject.SetActive(false);
        Touchdown.ScorSta = Scor;
        Touchdown.ScorSta.gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D other){

        if (other.gameObject == Balon){

            SceneManager.LoadScene("Etapas");
            
        }

        print("TouchDown!!");
        Balon = true;
        Pantalla = true;
    }
    public static void Show(){

        Touchdown.Fondo.gameObject.SetActive(true);
        Touchdown.GameWnStatic.gameObject.SetActive(true);
        Touchdown.ScorSta.gameObject.SetActive(true);
        Touchdown.TimeSta.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Balon && Pantalla)
        {

            Touchdown.Show();
        }

    }
}