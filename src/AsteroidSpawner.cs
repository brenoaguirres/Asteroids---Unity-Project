using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    private Asteroid _asteroidPrefab;

    [SerializeField]
    private float _trajectoryVariance = 15.0f;
    [SerializeField]
    private float _spawnRate = 2.0f;
    [SerializeField]
    private int _spawnAmount = 1;
    [SerializeField]
    private float _spawnDistance = 15.0f;

    private void Start() 
    {
        InvokeRepeating(nameof(Spawn), _spawnRate, _spawnRate);    
    }

    private void Spawn()
    {
        // spawns a variable amount of asteroids at random position and rotation;
        // sets a random size for asteroid
        // sends asteroid flying towards center

        for (int i = 0; i < _spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * _spawnDistance;
            Vector3 spawnPos = transform.position + spawnDirection;

            float variance = Random.Range(-_trajectoryVariance, _trajectoryVariance);
            Quaternion spawnRot = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(_asteroidPrefab, spawnPos, spawnRot);
            asteroid._size = Random.Range(asteroid._minSize, asteroid._maxSize);

            asteroid.SetTrajectory(spawnRot * -spawnDirection);
        }
    }
}
