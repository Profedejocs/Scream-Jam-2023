using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkerMovement : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D _rigidbody;
    private GameObject _bottomPoint;
    private float _xVelocity;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _bottomPoint = transform.Find("BottomPoint").gameObject;
        _xVelocity = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = _rigidbody.position;

        newPos.x += _xVelocity * Time.deltaTime;

        Vector2 groundPos = Physics2D.Raycast(_rigidbody.position, Vector2.down, 50, layerMask: GameInfo.GroundLayerMask).point;
        newPos.y = groundPos.y + _rigidbody.position.y - _bottomPoint.transform.position.y;

        _rigidbody.MovePosition(newPos);
    }
}
