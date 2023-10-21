using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyWalkerMovement : MonoBehaviour
{
    private GameObject _player;

    public float Speed;

    private Rigidbody2D _rigidbody;
    private GameObject _bottomPoint;
    private float _xVelocity;

    private bool _isWalking = false;
    private bool _isIdle = true;
    private float _stateChangeTimer;

    private float _rightBounds;
    private float _leftBounds;
    [SerializeField] private float _pathOffset = 3f;
    [SerializeField] private float _speed = 2f;
    private float _walkDirection = 1f;

    [SerializeField] private float _aggroRange = 2f;
    [SerializeField] private float _deaggroRange = 3f;
    private bool _isAggro = false;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _bottomPoint = transform.Find("BottomPoint").gameObject;
        _xVelocity = Speed;

        _stateChangeTimer = Time.time + 1f;

        _rightBounds = transform.position.x + _pathOffset;
        _leftBounds = transform.position.x - _pathOffset;

        _player = GameObject.Find("Character");
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isAggro && Vector3.Distance(transform.position, _player.transform.position) <= _aggroRange)
        {
            _isAggro = true;
        }
        else if (_isAggro && Vector3.Distance(transform.position, _player.transform.position) >= _deaggroRange) {
            _isAggro = false;
        }

        //If aggro move towards character
        //If not aggro move either back to move range, or move in range

        if (_stateChangeTimer <= Time.time) {
            if (_isWalking)
            {
                _isIdle = true;
                _isWalking = false;
                GetComponent<Animator>().SetBool("IsWalking", false);
                _stateChangeTimer = Time.time + Random.Range(0.0f, 1.5f);
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
            else if (_isIdle)
            {
                _isWalking = true;
                _isIdle = false;
                GetComponent<Animator>().SetBool("IsWalking", true);

                if (transform.position.x >= _rightBounds)
                {
                    _walkDirection = -1f;
                }
                else if (transform.position.x <= _leftBounds)
                {
                    _walkDirection = 1f;
                }
                else
                {
                    _walkDirection = (Random.Range(0, 2) == 0) ? 1 : -1;
                }

                _stateChangeTimer = Time.time + Random.Range(1.0f, 3.0f);
                GetComponent<Rigidbody2D>().velocity = new Vector2(_speed * _walkDirection, 0);
            }
        }


        //FlipDir
        if (transform.localScale.x < 0 && GetComponent<Rigidbody2D>().velocity.x < 0 ||
            transform.localScale.x > 0 && GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    public void SetInWater() {
        GetComponent<Animator>().SetBool("InWater", true);
    }

    public void SetOutOfWater()
    {
        GetComponent<Animator>().SetBool("InWater", false);
    }

}
