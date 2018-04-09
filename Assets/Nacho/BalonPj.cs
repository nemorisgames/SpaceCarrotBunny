using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalonPj : MonoBehaviour {

    public GameObject Personaje;
    private Vector3 offset;
    public float Pase = 1000f;
    public float y = 800f;
    Rigidbody Balon;
    public bool Lanzado = false;



    void Start()
    {

        offset = transform.position - Personaje.transform.position;
        Balon = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) )
        {
            Lanzado = true;
            Balon.AddForce(Pase * Time.deltaTime, y, 0);
            
        }
    }


    void LateUpdate()
        {
        if(Lanzado == false) {

            transform.position = Personaje.transform.position + offset;
        }

    }
   
}
