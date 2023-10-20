using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    void OnEnable()
    {
        Health.OnDeath += Death;
    }

    void OnDisable()
    {
        Health.OnDeath -= Death;
    }

    void Death(GameObject died)
    {
        if (died.gameObject == gameObject)
            ObjectPoolManager.Return(died);
    }
}
