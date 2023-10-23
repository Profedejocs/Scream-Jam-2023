using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeMonitor : MonoBehaviour
{
    private Health _health;
    // Start is called before the first frame update
    void Start()
    {
        _health = GetComponent<Health>();
        Health.OnDeath += Death;
    }

    void Death(GameObject died)
    {
        if (died.gameObject == gameObject)
        {
            Debug.Log("Player has died!");
            Time.timeScale = 0.0f; // TEMPORARY - Makes clear player is dead
        }
    }
}
