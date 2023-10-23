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

    private GameObject _rifleObject;
    private GameObject _rifleFirePoint;

    private bool _canShoot;
    private bool _angleBlocked;

    // Start is called before the first frame update
    void Start()
    {
        _rifleAmmo = RifleAmmo;
        _bulletEffect = GameObject.Instantiate(BulletEffect, transform.position, Quaternion.identity);
        _bulletEffectRenderer = _bulletEffect.GetComponent<LineRenderer>();
        _bulletEffectRenderer.enabled = false;
        _audioSource = GetComponent<AudioSource>();

        _rifleObject = transform.GetChild(0).gameObject;
        _rifleFirePoint = _rifleObject.transform.GetChild(0).GetChild(0).gameObject;

        _canShoot = true;
        _angleBlocked = false;
    }

    public void ShowRifle()
    {
        _canShoot = true;
        _rifleObject.SetActive(true);
    }
    public void HideRifle()
    {
        _canShoot = false;
        _rifleObject.SetActive(false);
    }

    private void PointRifle()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2  direction = mousePos - (Vector2)_rifleObject.transform.position;
        float angle;
        float angle_offset;

        if (transform.localScale.x > 0)
        {
            angle = Vector2.SignedAngle(Vector2.left, direction);
            angle_offset = 4;
        }
        else
        {
            angle = Vector2.SignedAngle(Vector2.right, direction);
            angle_offset = -4;
        }

        if (Mathf.Abs(angle) > 40)
        {
            _angleBlocked = true;
            angle = -6 * angle_offset; // Rest pos
        }
        else
            _angleBlocked = false;

        
        Vector3 angles = _rifleObject.transform.eulerAngles;
        angles.z = angle + angle_offset;
        _rifleObject.transform.eulerAngles = angles;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && _canShoot && !_angleBlocked)
        {
            if (_rifleCooldown <= 0 && _rifleAmmo > 0)
            {
                if (!Shoot())
                    return;

                _rifleAmmo--;
                _rifleCooldown = RifleCooldown;
                
                _audioSource.PlayOneShot(Gunshot);
            }
            else if (_rifleAmmo <= 0 && _rifleCooldown <= 0)
            {
                _rifleCooldown = RifleCooldown;
                _audioSource.PlayOneShot(Dryfire);
            }
        }

        PointRifle();
        _rifleCooldown -= Time.deltaTime;
    }

    private bool Shoot()
    {
        int targetMask = GameInfo.GroundLayerMask | GameInfo.TargetableLayerMask;

        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - _rifleFirePoint.transform.position).normalized;

        Debug.Log(direction);

        if (transform.localScale.x < 0) // Facing left
        {
            if (direction.x < 0)
                return false;
        }
        else
        {
            if (direction.x > 0)
                return false;
        }

        RaycastHit2D hit = Physics2D.Raycast(_rifleFirePoint.transform.position, direction, 50, layerMask: targetMask);

        if (hit.rigidbody != null)
        {
            if (((1 << 6) & (1 << hit.rigidbody.gameObject.layer)) != 0)
                hit.rigidbody.gameObject.GetComponent<Health>().TakeDamage(RifleDamage);
        }

        _bulletEffectRenderer.enabled = true;
        _bulletEffectRenderer.SetPosition(0, _rifleFirePoint.transform.position);
        _bulletEffectRenderer.SetPosition(1, _rifleFirePoint.transform.position + (Vector3)(direction * 30f));

        Invoke("HideBulletLine", 0.1f);
        return true;

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
