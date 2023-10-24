using MilkShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth = 200;
    private float _health;

    public bool UseIFrames;
    public float iFrameTime = 0.5f;
    private float _iFrameTime;

    public delegate void Death(GameObject died);
    public static event Death OnDeath;

    public ShakePreset DamageShake;

    public bool InIFrame()
    {
        return _iFrameTime > 0f;
    }

    public void TakeDamage(float damage)
    {
        if (_iFrameTime <= 0 || !UseIFrames)
        {
            if (gameObject.name.Equals("Character"))
                GameObject.Find("MainCamera").GetComponent<Shaker>().Shake(DamageShake);

            _health -= damage;

            if (gameObject.name.Equals("Character"))
                HUDController.instance.UpdateDamage(_health);

            if (_health <= 0)
                Die();
            _iFrameTime = iFrameTime;
            if (UseIFrames)
                gameObject.layer = LayerMask.NameToLayer("IFrame");
        }
    }

    public void Die()
    {
        if (OnDeath != null)
            OnDeath(this.gameObject);
    }

    public void Heal(float heal)
    {
        _health += heal;
    }

    // Start is called before the first frame update
    void Start()
    {
        _health = MaxHealth;
    }

    private void Update()
    {
        _iFrameTime -= Time.deltaTime;
        if (_iFrameTime <= 0)
        {
            if (UseIFrames)
                gameObject.layer = LayerMask.NameToLayer("Character");
        }
    }

    public float GetHealth()
    {
        return _health;
    }
}
