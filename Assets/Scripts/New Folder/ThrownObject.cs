using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CLASE1{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ThrownObject : MonoBehaviour
    {
        private Rigidbody2D rigidBody;
        private Collider2D coll2D;

        public Collider2D Collider { get { return coll2D; } }

        void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();

            coll2D = GetComponent<Collider2D>();
            if (coll2D == null)
                Debug.LogError(gameObject.name);
        }

        private Vector2 velocityToRg = Vector2.zero;
        public void ThrowObj(Vector3 velocity)
        {
            Debug.Log("BalonLanzado");

            velocityToRg = velocity;
            rigidBody.velocity = velocityToRg;
            rigidBody.isKinematic = false;
        }

        private Vector3 velocity = Vector3.zero;
        public void Reset(Vector3 pos)
        {
            rigidBody.velocity = velocity;
            transform.position = pos;
        }
        
}
}