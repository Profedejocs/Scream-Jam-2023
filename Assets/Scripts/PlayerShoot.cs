using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public int RifleAmmo;
    public float RifleCooldown;
    public float RifleDamage;

    private float _rifleCooldown;
    private int _rifleAmmo;

    public GameObject BulletEffect;
    private GameObject _bulletEffect;
    private LineRenderer _bulletEffectRenderer;

    public AudioClip Gunshot;
    public AudioClip Dryfire;
    
    private AudioSource _audioSource;


    // Start is called before the first frame update
    void Start()
    {
        _rifleAmmo = RifleAmmo;
        _bulletEffect = GameObject.Instantiate(BulletEffect, transform.position, Quaternion.identity);
        _bulletEffectRenderer = _bulletEffect.GetComponent<LineRenderer>();
        _bulletEffectRenderer.enabled = false;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (_rifleCooldown <= 0 && _rifleAmmo > 0)
            {
                _rifleAmmo--;
                _rifleCooldown = RifleCooldown;
                Shoot();
                _audioSource.PlayOneShot(Gunshot);
            }
            else if (_rifleAmmo <= 0 && _rifleCooldown <= 0)
            {
                _rifleCooldown = RifleCooldown;
                _audioSource.PlayOneShot(Dryfire);
            }
        }

        _rifleCooldown -= Time.deltaTime;
    }

    private void Shoot()
    {
        int targetMask = GameInfo.GroundLayerMask | GameInfo.TargetableLayerMask;

        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 50, layerMask: targetMask);

        if (hit.rigidbody != null)
        {
            if (((1 << 6) & (1 << hit.rigidbody.gameObject.layer)) != 0)
                hit.rigidbody.gameObject.GetComponent<Health>().TakeDamage(RifleDamage);
        }

        _bulletEffectRenderer.enabled = true;
        _bulletEffectRenderer.SetPosition(0, transform.position);
        _bulletEffectRenderer.SetPosition(1, transform.position + (Vector3)(direction * 30f));

        Invoke("HideBulletLine", 0.1f);

    }

    public void AddAmmo(int amount)
    {
        _rifleAmmo += amount;
    }

    private void HideBulletLine()
    {
        _bulletEffectRenderer.enabled = false;
    }
}
