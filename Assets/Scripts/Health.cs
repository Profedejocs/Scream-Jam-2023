using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth = 200;
    private float _health;

    public delegate void Death(GameObject died);
    public static event Death OnDeath;

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
            Die();
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


    // Update is called once per frame
    void Update()
    {

    }
}
