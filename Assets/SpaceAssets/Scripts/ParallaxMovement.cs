using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMovement : MonoBehaviour {
    public enum Direction { LeftToRight, RigthToLeft, DownToUp, UpToDown };
    public Direction direction = Direction.LeftToRight;
    public float minSpeed = 0.2f;
    public float maxSpeed = 0.6f;
    Vector3 speed;
    float scrollValue;
    float lastScrollValue;

    public enum BehaviourOnExit { Destroy, Regenerate };
    public BehaviourOnExit behaviourOnExit = BehaviourOnExit.Regenerate;

    Transform cameraTransform;

    void Start () {
        cameraTransform = Camera.main.transform;
        if(minSpeed > maxSpeed) Debug.LogError("The variable minSpeed cannot be greater than maxSpeed");
        switch (direction)
        {
            case Direction.LeftToRight:
                speed = new Vector3(Random.Range(minSpeed, maxSpeed), 0f, 0f);
                break;
            case Direction.RigthToLeft:
                speed = new Vector3(- Random.Range(minSpeed, maxSpeed), 0f, 0f);
                break;
            case Direction.DownToUp:
                speed = new Vector3(0f, Random.Range(minSpeed, maxSpeed), 0f);
                break;
            case Direction.UpToDown:
                speed = new Vector3(0f, Random.Range(minSpeed, maxSpeed), 0f);
                break;
        }
    }

    void Regenerate()
    {
        switch (direction)
        {
            case Direction.LeftToRight:
                transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1.3f, Random.Range(0f, 1f), 10f));
                break;
            case Direction.RigthToLeft:
                transform.position = Camera.main.ViewportToWorldPoint(new Vector3(-0.3f, Random.Range(0f, 1f), 10f));
                break;
            case Direction.DownToUp:
                transform.position = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 1.3f, 10f));
                break;
            case Direction.UpToDown:
                transform.position = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), -0.3f, 10f));
                break;
        }
    }
	
	void Update () {
        scrollValue = cameraTransform.position.x - lastScrollValue;
        lastScrollValue = cameraTransform.position.x;
        transform.position += speed * scrollValue;

        switch (direction)
        {
            case Direction.LeftToRight:
                if(Camera.main.WorldToViewportPoint(transform.position).x < -0.3f)
                {
                    switch (behaviourOnExit)
                    {
                        case BehaviourOnExit.Destroy:
                            Destroy(gameObject);
                            break;
                        case BehaviourOnExit.Regenerate:
                            Regenerate();
                            break;
                    }
                }
                break;
            case Direction.RigthToLeft:
                if (Camera.main.WorldToViewportPoint(transform.position).x > 1.3f)
                {
                    switch (behaviourOnExit)
                    {
                        case BehaviourOnExit.Destroy:
                            Destroy(gameObject);
                            break;
                        case BehaviourOnExit.Regenerate:
                            Regenerate();
                            break;
                    }
                }
                break;
            case Direction.DownToUp:
                if (Camera.main.WorldToViewportPoint(transform.position).y > 1.3f)
                {
                    switch (behaviourOnExit)
                    {
                        case BehaviourOnExit.Destroy:
                            Destroy(gameObject);
                            break;
                        case BehaviourOnExit.Regenerate:
                            Regenerate();
                            break;
                    }
                }
                break;
            case Direction.UpToDown:
                if (Camera.main.WorldToViewportPoint(transform.position).y < -0.3f)
                {
                    switch (behaviourOnExit)
                    {
                        case BehaviourOnExit.Destroy:
                            Destroy(gameObject);
                            break;
                        case BehaviourOnExit.Regenerate:
                            Regenerate();
                            break;
                    }
                }
                break;
        }
    }
}
