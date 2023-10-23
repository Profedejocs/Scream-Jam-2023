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

    public AudioClip Whoosh;
    public AudioClip BonkSound;

    private AudioSource _audioSource;

    private PlayerShoot _shootScript;

    void Start()
    {
        _batCooldown = BatCooldown;
        _audioSource = GetComponent<AudioSource>();
        _shootScript = GetComponent<PlayerShoot>();
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

    private void ShowRifle()
    {
        _shootScript.ShowRifle();
    }


    private void Bonk()
    {
        _shootScript.HideRifle();
        Invoke("ShowRifle", 0.85f);
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

        

        if (hits.Length > 0)
        {
            _audioSource.PlayOneShot(BonkSound);
        }
        else
        {
            _audioSource.PlayOneShot(Whoosh);
        }

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
