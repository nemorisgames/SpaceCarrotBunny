using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalonAmigo : MonoBehaviour {

    BalonPj Padre;

	// Use this for initialization
	void Start () {

        Padre = GameObject.Find("balomn").GetComponent<BalonPj>();
	}
	
	// Update is called once per frame
	void Update () {
		
        
	}

    private void OnTriggerEnter(Collider other)
    {
        print("funciono");
        if (other.tag == "Baloncito"){
            Padre.Personaje = gameObject;
            Padre.Lanzado = false;
            

        }
    }
}
