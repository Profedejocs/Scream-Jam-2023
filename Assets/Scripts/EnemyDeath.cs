using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public GameObject Infection;

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
        {
            ObjectPoolManager.Return(died);
            Quaternion rotation = Quaternion.Euler(-90f, 0f, 0f);

            Instantiate(Infection, transform.position, rotation);
        }
    }
}
