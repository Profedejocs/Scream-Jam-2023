using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    public float ContactDamage;
    private GameObject _player;


    public void Start()
    {
        _player = GameObject.Find("Character");
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == _player.name)
        {
            Vector2 direction = (collision.gameObject.transform.position - transform.position).normalized;
            Debug.Log(direction);

            _player.GetComponent<Health>().TakeDamage(ContactDamage);
            _player.GetComponent<PlayerMovement>().Knockback((direction) * 1000f);
            Debug.Log("Attacks");
        }
    }
}
