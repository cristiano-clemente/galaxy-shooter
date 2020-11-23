using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID; //0=3shot 1=speedboost 2=shield
    [SerializeField]
    private AudioClip _clip;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (_gameManager.gameOver == true)
        {
            Destroy(this.gameObject);
        }

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6.52f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (powerupID == 0)
            {
                player.TripleShotOn();
            }
            else if (powerupID == 1)
            {
                player.SpeedBoostOn();
            }
            else if (powerupID == 2)
            {
                player.ShieldOn();
            }
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
    }
}