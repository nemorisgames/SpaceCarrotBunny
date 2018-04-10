using UnityEngine;
using System.Collections;

public class CameraHorizontalMovement : MonoBehaviour {
	Camera camera;
    float cameraSpeed = 3f;
    float currentScrollPosition = 0f;
	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
        //Provides the nice zoom-out effect at the beggining of the scene
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, 22f, 0.5f * Time.deltaTime);
        //Camera movement
        currentScrollPosition += Time.deltaTime;
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, cameraSpeed * currentScrollPosition, 1f * Time.deltaTime), transform.position.y, transform.position.z);
    }
}
