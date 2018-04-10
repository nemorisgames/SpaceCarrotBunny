using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {
    
	void Start () {
		transform.position = new Vector2 (transform.position.x + Random.Range (Central.AreaSpawnPlanetas.x, Central.AreaSpawnPlanetas.x + Central.AreaSpawnPlanetas.width), Random.Range (Central.AreaSpawnPlanetas.y, Central.AreaSpawnPlanetas.y + Central.AreaSpawnPlanetas.height)); 
	}
	
	void Update () {

	}
}
