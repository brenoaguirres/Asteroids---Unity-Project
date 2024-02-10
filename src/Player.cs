using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public Bullet _bulletPrefab;

    private bool _thrusting;

    private float _turnDirection;
    [SerializeField]
    private float _thrustSpeed = 1.0f;
    [SerializeField]
    private float _turnSpeed = 1.0f;

    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {

    }

    private void Update() 
    {
    
        // Check For Inputs

        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        //torque needs + for left and - for right (inverted)
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _turnDirection = -1.0f;
        }
        else
        {
            _turnDirection = 0.0f;
        }

        // Check input for shooting
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

    }

    private void FixedUpdate() 
    {

        // Movement: Impulse and Rotation

        if (_thrusting)
        {
            _rigidbody.AddForce(this.transform.up * _thrustSpeed);
        }

        if (_turnDirection != 0.0f)
        {
            _rigidbody.AddTorque(_turnDirection * _turnSpeed);
        }
    }

    // instantiates bullets
    // sends bullet forward
    private void Shoot()
    {
        Bullet bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
        bullet.Project(transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Asteroid")
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0.0f;

            gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }
    }
}
