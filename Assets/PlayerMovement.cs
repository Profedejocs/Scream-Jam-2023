using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float PlayerAccel;
    public float JumpCooldown;
    public float JumpForce;

    private Rigidbody2D _rigidbody;
    private float _jumpCooldown;
    private bool _grounded;
    private Vector2 _velocity;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _velocity = new Vector2();

    }

    void FixedUpdate()
    {
        Vector2 input = GetMovement();
        Vector2 accel = new Vector2();

        accel.x = input.x * PlayerAccel;
        accel.y = 0;
        accel.y = GetJump().y;

        UpdateCounters();

        if (!_grounded)
            _rigidbody.sharedMaterial.friction = 0.0f;
        else
            _rigidbody.sharedMaterial.friction = 0.4f;

        Debug.Log(_rigidbody.sharedMaterial.friction);

        _rigidbody.velocity = Vector2.Lerp(_rigidbody.velocity, (new Vector2(accel.x, _rigidbody.velocity.y)), 15);

        if (accel.y != 0)
        {
            
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, accel.y);
            Debug.Log(_rigidbody.velocity);
        }

        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.gravityScale = 5;
            Debug.Log("Flling");
        }
        else
        {
            _rigidbody.gravityScale = 2.5f;
        }

    }

    void Update()
    {
        UpdateFlags();

    }

    private Vector2 GetMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        return new Vector2(horizontal, vertical);
    }

    private Vector2 GetJump()
    {
        Vector2 jumpVel = new Vector2();

        if (Input.GetButton("Jump") || Input.GetAxisRaw("Vertical") > 0.9f)
        {
            if (_jumpCooldown <= 0 && _grounded)
            {
                _jumpCooldown = JumpCooldown;
                jumpVel.y = JumpForce;
            }
        }

        return jumpVel;
    }

    private void UpdateCounters()
    {
        _jumpCooldown -= Time.deltaTime;
    }

    private void UpdateFlags()
    {
        _grounded = IsGrounded();
        
    }

    private bool IsGrounded()
    {
        int groundLayerMask = 1 << 3;

        Vector2 posLeft = _rigidbody.position;
        posLeft.x -= 0.3f;
        Vector2 posRight = _rigidbody.position;
        posRight.x += 0.3f;

        RaycastHit2D hitLeft = Physics2D.Raycast(posLeft, -Vector2.up, 1.1f, layerMask: groundLayerMask);
        Debug.DrawRay(posLeft, -Vector2.up * 1f, Color.black);

        RaycastHit2D hitCenter = Physics2D.Raycast(_rigidbody.position, -Vector2.up, 1.1f, layerMask: groundLayerMask);
        Debug.DrawRay(posLeft, -Vector2.up * 1f, Color.black);

        RaycastHit2D hitRight = Physics2D.Raycast(posRight, -Vector2.up, 1.1f, layerMask: groundLayerMask);
        Debug.DrawRay(posRight, -Vector2.up * 1f, Color.black);

        return hitLeft || hitCenter || hitRight;
    }
}
