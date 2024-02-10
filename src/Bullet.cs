using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private float _speed = 500.0f;
    [SerializeField]
    private float _maxLifeTime = 10.0f;

    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody2D>();    
    }

    // Sends bullet flying foward
    // Destroy bullet after given seconds
    public void Project(Vector2 direction)
    {
        _rigidbody.AddForce(direction * _speed);

        Destroy(gameObject, _maxLifeTime);
    }

    public void OnCollisionEnter2D(Collision2D collider)
    {
        Destroy(gameObject);
    }
    
}
