using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed = 80;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Character");
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var direction = _player.transform.position - transform.position;

        _rigidbody2D.velocity = direction * _speed * Time.deltaTime;
    }
}
