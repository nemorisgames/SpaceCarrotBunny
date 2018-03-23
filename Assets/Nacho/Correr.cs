using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Correr : MonoBehaviour {

    public float speed = 0f;
    Rigidbody RG;
    public Transform PJA;


	// Use this for initialization
	void Start () {

        RG = GetComponent<Rigidbody>();

	}

    // Update is called once per frame
    void Update()
    {

        RG.velocity = new Vector3(speed * Time.deltaTime, RG.velocity.y, RG.velocity.z);

       

    }
}
