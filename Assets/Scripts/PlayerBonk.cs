using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBonk : MonoBehaviour
{
    public float BatCooldown;
    public float BatDamage;
    public float BatReach;
    public float BatSweep;

    private float _batCooldown;
    void Start()
    {
        _batCooldown = BatCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        _batCooldown -= Time.deltaTime;
        if (Input.GetButton("Fire2") && _batCooldown < 0)
        {
            _batCooldown = BatCooldown;
            Bonk();
        }
    }

    private void Bonk()
    {
        GetComponent<Animator>().SetTrigger("Bonk");

        bool facingRight = Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x;

        Vector2 topleft = new();
        Vector2 botright = new();

        topleft.y = transform.position.y - BatSweep;
        botright.y = transform.position.y + BatSweep;

        if (facingRight)
        {
            topleft.x = transform.position.x;
            botright.x = transform.position.x + BatReach;
        }
        else
        {
            topleft.x = transform.position.x - BatReach;
            botright.x = transform.position.x;
        }
        Collider2D[] hits = Physics2D.OverlapAreaAll(topleft, botright, GameInfo.TargetableLayerMask);

        foreach (var hit in hits)
        {
            Debug.Log("Bonk");
            hit.gameObject.GetComponent<Health>().TakeDamage(BatDamage);
        }
    }

    private void OnDrawGizmos()
    {        
        Gizmos.DrawWireCube(transform.position + new Vector3(BatReach/2f, 0f, 0f), new Vector3(BatReach, BatSweep*2, 1f));
        Gizmos.DrawWireCube(transform.position - new Vector3(BatReach / 2f, 0f, 0f), new Vector3(BatReach, BatSweep * 2, 1f));
    }
}
