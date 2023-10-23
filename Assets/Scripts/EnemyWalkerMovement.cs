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

    [SerializeField] private bool _hasHiddenAggro = false;
    [SerializeField] private List<Vector2> _aggroPathPoints;
    private bool _hiddenAggroTriggered = false;
    private GameObject _hiddenMasks;


    [SerializeField] private float _pathOffset = 3f;
    [SerializeField] private float _speed = 80f;
    private float _walkDirection = 1f;

    [SerializeField] private float _aggroRange = 10f;
    [SerializeField] private float _deaggroRange = 15f;
    [SerializeField] private float _aggroSpeed = 120f;
    private bool _isAggro = false;
    [SerializeField] private bool _isAutoAggro = false;

    private float _curSpeed = 0f;


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
    void FixedUpdate()
    {
        if (!_hasHiddenAggro && !_isAutoAggro && !_isAggro && Vector3.Distance(transform.position, _player.transform.position) <= _aggroRange)
        {
            _isAggro = true;
        }
        else if (!_hasHiddenAggro && !_isAutoAggro && _isAggro && Vector3.Distance(transform.position, _player.transform.position) >= _deaggroRange) {
            _isAggro = false;
        }

        //If aggro move towards character
        //If not aggro move either back to move range, or move in range

        if (!_hasHiddenAggro && !_isAutoAggro && !_isAggro && transform.position.x > _leftBounds && transform.position.x < _rightBounds)
        {
            if (_stateChangeTimer <= Time.time)
            {
                if (_isWalking)
                {
                    _isIdle = true;
                    _isWalking = false;
                    GetComponent<Animator>().SetBool("IsWalking", false);
                    _stateChangeTimer = Time.time + Random.Range(0.0f, 1.5f);
                    _curSpeed = 0;
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
                    _curSpeed = _speed * _walkDirection;
                }
            }
        }
        else if (!_hasHiddenAggro && !_isAutoAggro && !_isAggro && (transform.position.x < _leftBounds || transform.position.x > _rightBounds))
        {
            if (transform.position.x < _leftBounds && (_rigidbody.velocity.x / Mathf.Abs(_rigidbody.velocity.x)) != 1f)
            {
                _curSpeed = _speed;
            }
            else if (transform.position.x > _rightBounds && (_rigidbody.velocity.x / Mathf.Abs(_rigidbody.velocity.x)) != -1f)
            {
                _curSpeed = _speed * -1f;
            }
        }
        else if (!_hasHiddenAggro && !_isAutoAggro && _isAggro)
        {
            Vector3 dir = _player.transform.position - transform.position;
            if ((_rigidbody.velocity.x / Mathf.Abs(_rigidbody.velocity.x)) != (dir.x / Mathf.Abs(dir.x)))
            {
                GetComponent<Animator>().SetBool("IsWalking", true);
                _curSpeed = _aggroSpeed * (dir.x / Mathf.Abs(dir.x));
            }
        }
        else if (_hasHiddenAggro && _hiddenAggroTriggered) {
            if ((Vector2)transform.position == _aggroPathPoints[0]) { 
                _aggroPathPoints.RemoveAt(0);

                if (_aggroPathPoints.Count == 0) {
                    _rigidbody.bodyType = RigidbodyType2D.Dynamic;
                    _hasHiddenAggro = false;
                    _hiddenAggroTriggered = false;
                    if (_isAutoAggro)
                        SetIsAutoAggro();
                    if (_hiddenMasks)
                        Destroy(_hiddenMasks);
                }
            }

            if (_aggroPathPoints.Count > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, _aggroPathPoints[0], 2f * Time.deltaTime);

                //Check facing
                if ((transform.position.x - _aggroPathPoints[0].x < 0 && transform.localScale.x > 0) ||
                    (transform.position.x - _aggroPathPoints[0].x > 0 && transform.localScale.x < 0)) {
                    var scale = transform.localScale;
                    scale.x *= -1;
                    transform.localScale = scale;
                }
            }
        }

        var velocity = _rigidbody.velocity;
        velocity.x = _curSpeed * Time.deltaTime;
        _rigidbody.velocity = velocity;


        //FlipDir
        if ((transform.localScale.x < 0 && GetComponent<Rigidbody2D>().velocity.x < 0) ||
            (transform.localScale.x > 0 && GetComponent<Rigidbody2D>().velocity.x > 0))
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

    public void SetIsAutoAggro() {
        _player = GameObject.Find("Character");
        Vector3 dir = _player.transform.position - transform.position;
        _aggroSpeed = 150f;
        _curSpeed = _aggroSpeed * (dir.x / Mathf.Abs(dir.x));
        _isAutoAggro = true;
        GetComponent<Animator>().SetBool("IsWalking", true);
    }

    public void TriggerHiddenAggro(GameObject enemyMasks) { 
        _hiddenAggroTriggered = true;
        GetComponent<Animator>().SetBool("IsWalking", true);
        _hiddenMasks = enemyMasks;
    }

}
