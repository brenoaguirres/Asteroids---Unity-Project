using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{ 
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    [SerializeField]
    public float _size = 1.0f;
    [SerializeField]
    public float _minSize = 0.5f;
    [SerializeField]
    public float _maxSize = 1.5f;
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private float _speed = 50.0f;
    [SerializeField]
    private float _maxLifeTime = 30.0f;

    private void Awake() 
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();    
    }

    private void Start() 
    {
        //Random Sprite, Rotation, Scale

        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        transform.localScale = Vector3.one * _size;

        _rigidbody.mass = _size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * _speed);

        Destroy(gameObject, _maxLifeTime);
    }

    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            if ((_size * 0.5f) >= _minSize)
            {
                CreateSplit();
                CreateSplit();
            }

        }
        
        FindObjectOfType<GameManager>().AsteroidDestroyed(this);
        Destroy(gameObject);
    }

    private void CreateSplit()
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, transform.rotation);
        half._size = _size * 0.5f;
        half.SetTrajectory(Random.insideUnitCircle.normalized * _speed);
    }
}
