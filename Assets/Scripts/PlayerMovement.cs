using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float PlayerAccel;
    public float JumpCooldown;
    public float JumpForce;
    public float JumpCharge;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private float _jumpCooldown;
    private bool _grounded;
    private Vector2 _velocity;
    private float _jumpCharge;
    private bool _knockbacked;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
        _velocity = new Vector2();

    }

    void FixedUpdate()
    {
        Vector2 input = GetMovement();
        Vector2 accel = new Vector2();

        accel.x = input.x * PlayerAccel;
        accel.y = 0;
        accel.y = GetJump().y;

        if (input.x == 0) {
            GetComponent<Animator>().SetBool("IsWalking", false);
        } else {
            GetComponent<Animator>().SetBool("IsWalking", true);
        }

        UpdateCounters();

        if (!_grounded || _jumpCooldown > 0)
        {
            _rigidbody.sharedMaterial.friction = 0.0f;
            _collider.sharedMaterial.friction = 0.0f;
            ColliderBugWorkaround();
        }
        else
        {
            _rigidbody.sharedMaterial.friction = 0.4f;
            _collider.sharedMaterial.friction = 0.4f;
            ColliderBugWorkaround();
        }

        if (!_knockbacked)
            _rigidbody.velocity = Vector2.Lerp(_rigidbody.velocity, (new Vector2(accel.x, _rigidbody.velocity.y)), 15);

        if (accel.y != 0)
        {
            if (!_knockbacked)
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, accel.y);
        }

        if (_rigidbody.velocity.y < 0)
        {
            if (!_knockbacked)
                _rigidbody.gravityScale = 5;
        }
        else
        {
            if (!_knockbacked)
                _rigidbody.gravityScale = 2.5f; 
        }

        if (_rigidbody.velocity.x < 0 && transform.localScale.x < 0) {
            Flip();
        } else if (_rigidbody.velocity.x > 0 && transform.localScale.x > 0)
        {
            Flip();
        }

    }

    private void Flip() {
        //Debug.Log("flip");
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void Knockback(Vector2 force)
    {
        _knockbacked = true;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(force, ForceMode2D.Force);

        _rigidbody.sharedMaterial.friction = 0.0f;
        _collider.sharedMaterial.friction = 0.0f;
        _rigidbody.gravityScale = 5f;
        ColliderBugWorkaround();
        Invoke("EndKnockback", 0.5f);
    }

    private void EndKnockback()
    {
        _rigidbody.sharedMaterial.friction = 0.4f;
        _collider.sharedMaterial.friction = 0.4f;
        _rigidbody.gravityScale = 2.5f;
        ColliderBugWorkaround();
        _knockbacked = false;
    }

    void Update()
    {
        UpdateFlags();

        // TEMP JUMP CHARGE ANIMATION
        //Vector3 scale = new(Mathf.Lerp(1.5f, 1.0f, _jumpCharge / JumpCharge), Mathf.Lerp(0.9f, 1.0f, _jumpCharge / JumpCharge), 1.0f);
        //transform.localScale = scale;
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
            if (_jumpCharge <= 0 && _jumpCooldown <= 0 && _grounded)
            {
                _jumpCooldown = JumpCooldown;
                jumpVel.y = JumpForce;
                _jumpCharge = JumpCharge;
            }
        }

        return jumpVel;
    }

    private void UpdateCounters()
    {
        _jumpCooldown -= Time.deltaTime;
        if ((Input.GetButton("Jump") || Input.GetAxisRaw("Vertical") > 0.9f) && _grounded)
        {
            _jumpCharge -= Time.deltaTime;
        }
        else
            _jumpCharge = JumpCharge;
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

    private void ColliderBugWorkaround()
    {
        _collider.enabled = false;
        _collider.enabled = true;
    }

    public void SetInWater() {
        GetComponent<Animator>().SetBool("InWater", true);
    }

    public void SetOutOfWater() {
        GetComponent<Animator>().SetBool("InWater", false);
    }
}
