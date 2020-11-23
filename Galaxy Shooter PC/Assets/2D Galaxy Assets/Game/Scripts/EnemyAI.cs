using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private GameObject _enemyExplosionPrefab;
    [SerializeField]

    private AudioClip _clip;
    private UIManager _uiManager;
    private GameManager _gameManager;

    // Use this for initialization
    void Start ()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_gameManager.gameOver == true)
        {
            Destroy(this.gameObject);
        }

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6.52f)
        {
            float randomX = Random.Range(-7.76f, 7.76f);
            transform.position = new Vector3(randomX, 6.44f, 0);
        }      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Laser")
        {
            {
                Destroy(other.transform.parent.gameObject); // destroys laser
                Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
                _uiManager.UpdateScore();
                AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
                Destroy(this.gameObject);
                
            }
        }
    }
}
