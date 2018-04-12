using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSize : MonoBehaviour {
    [Range(1.0f, 10.0f)]
    public float multiplierMax = 3f;
    float initialScale;

    void Start () {
        //Initial scale(chooses the bigger scale comapring the x and y axis)
        initialScale = Mathf.Max(transform.localScale.x, transform.localScale.y);
        Generate();
    }

    public void Generate()
    {
        //Choose a random multiplied scale from the initial scale(assuming a regular scale) and the multiplierMax variable
        transform.localScale = Vector3.one * initialScale * Random.Range(1f, multiplierMax);
    }
    
    void Update () {
		
	}
}
