using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenAggroTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _enemyMasks;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Character")) {
            _enemy.GetComponent<EnemyWalkerMovement>().TriggerHiddenAggro(_enemyMasks);
            Destroy(this);
        }
    }
}
