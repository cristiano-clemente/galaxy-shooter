using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool TripleShotActive = false;
    public bool SpeedBoostActive = false;
    public bool ShieldActive = false;
    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;
    public int lives = 3;

    [SerializeField]
    private GameObject _ExplosionPrefab;
    [SerializeField]
    private GameObject _monoShotPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldGameObject;
    [SerializeField]
    private GameObject[] _engines;
    [SerializeField]
    private float _fireRate = 0.25f;
    private float _nextFire = 0.0f;
    [SerializeField]
    private float _speed = 5.0f;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private AudioSource _audioSource;
    private SpawnManager _spawnManager;

    // Use this for initialization
    void Start ()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _uiManager.UpdateLives(lives);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _audioSource = GetComponent<AudioSource>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_gameManager.isCoopMode == false)
        {
            //current pos = new position
            transform.position = new Vector3(0, 0, 0);
        }
    }
	 
	// Update is called once per frame
	void Update ()
    {
        //destroys this ship when game ends (therefore both if isCoopMode == true)
        if (_gameManager.gameOver == true)
        {
            if ((isPlayerOne == true && isPlayerTwo == false) || (isPlayerOne == false && isPlayerTwo == true))
            {
                Destroy(this.gameObject);
            }
        }

        if (isPlayerOne == true)
        {
            MovementPlayerOne();
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                Shoot();
            }
        }

        if (isPlayerTwo == true)
        {
            MovementPlayerTwo();
            if (Input.GetKey(KeyCode.KeypadEnter))
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        if (Time.time > _nextFire)
        {
            _audioSource.Play();

            if (TripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                //spawn laser
                Instantiate(_monoShotPrefab, transform.position, Quaternion.identity);
            }

            _nextFire = Time.time + _fireRate;
        }
    }

    private void MovementPlayerOne()
    {
        float horizontalInput = Input.GetAxis("Player1_Horizontal");
        float verticalInput = Input.GetAxis("Player1_Vertical");

        float _actualSpeed = _speed;
        if(SpeedBoostActive == true)
        {
            _actualSpeed *= 1.5f;
        }

        transform.Translate(Vector3.right * _actualSpeed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * _actualSpeed * verticalInput * Time.deltaTime);

        if (transform.position.y > 0f)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }

    private void MovementPlayerTwo()
    {
        float horizontalInput = Input.GetAxis("Player2_Horizontal");
        float verticalInput = Input.GetAxis("Player2_Vertical");

        float _actualSpeed = _speed;
        if (SpeedBoostActive == true)
        {
            _actualSpeed *= 1.5f;
        }

        transform.Translate(Vector3.right * _actualSpeed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * _actualSpeed * verticalInput * Time.deltaTime);

        if (transform.position.y > 0f)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        if (ShieldActive == true)
        {
            ShieldActive = false;
            _shieldGameObject.SetActive(false);
        }
        else
        {
            lives--;
            _uiManager.UpdateLives(lives);


            //if both off
            //turn one of them on
            if (_engines[0].activeSelf == false && _engines[1].activeSelf == false)
            {
                _engines[Random.Range(0, 2)].SetActive(true);
            }

            //if left on
            //turn right on
            else if (_engines[0].activeSelf == true)
            {
                _engines[1].SetActive(true);
            }

            //if right on
            //turn left on
            else if (_engines[1].activeSelf == true)
            {
                _engines[0].SetActive(true);
            }
        }
        
        if (lives < 1)
        {
            _uiManager.CheckForBestScore();
            Instantiate(_ExplosionPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _spawnManager.StopSpawnRoutines();
            _uiManager.ShowTitleScreen();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotOn()
    {
        TripleShotActive = true;
        StartCoroutine(TripleShotOff());
    }
    public IEnumerator TripleShotOff()
    {
        yield return new WaitForSeconds(5.0f);
        TripleShotActive = false;
    }

    public void SpeedBoostOn()
    {
        SpeedBoostActive = true;
        StartCoroutine(SpeedBoostOff());
    }

    public IEnumerator SpeedBoostOff()
    {
        yield return new WaitForSeconds(5.0f);
        SpeedBoostActive = false;
    }

    public void ShieldOn()
    {
        ShieldActive = true;
        _shieldGameObject.SetActive(true);
    }
}