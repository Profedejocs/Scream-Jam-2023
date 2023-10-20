using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int Bullets;

    private GameObject _player;

    void Start()
    {
        _player = GameObject.Find("Character");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == _player.name)
        {
            _player.GetComponent<PlayerShoot>().AddAmmo(Bullets);
            Destroy(this.gameObject);
        }

    }
}
