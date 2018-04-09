using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CLASE1
{
   
    public class Amigo : MonoBehaviour
    {
        public GameObject Balon;
        public PullCtrl Manejo;

        // Use this for initialization
        void Start()
        {
            Manejo = GameObject.Find("Jugador").GetComponent<PullCtrl>();
            Manejo.enabled = false;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            print("Ahora!!");
            Manejo.GetComponent<PullCtrl>().enabled = true;
            Balon = GameObject.Find("Baloncito");
        }
        // Update is called once per frame
        void Update()
        {

        }
    }


}
