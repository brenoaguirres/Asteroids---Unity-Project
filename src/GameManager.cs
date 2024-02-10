using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public Player player;
    [SerializeField]
    public ParticleSystem explosion;

    [SerializeField]
    public float respawnTime = 3.0f;
    [SerializeField]
    private int lives = 3;
    [SerializeField]
    private float invincibilityTime = 3.0f;
    [SerializeField]
    private int score = 0;

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        explosion.transform.position = asteroid.transform.position;
        explosion.Play();

        if (asteroid._size < 0.75f)
        {
            score += 100;
        }
        else if (asteroid._size < 1.2f)
        {
            score += 50;
        }
        else
        {
            score += 25;
        }
    }
    
    public void PlayerDied()
    {
        explosion.transform.position = player.transform.position;
        explosion.Play();

        lives--;

        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), respawnTime);   
        }

    }

    private void Respawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), 3.0f);
    }

    private void TurnOnCollisions()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        lives = 3;
        score = 0;

        Invoke(nameof(Respawn), respawnTime);
    }
}
