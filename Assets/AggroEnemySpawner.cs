using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroEnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Character")) {
            var newEnemy = Instantiate(Resources.Load("Spawnables/TestEnemy"), _spawnPoint.position, Quaternion.identity) as GameObject;
            newEnemy.GetComponent<EnemyWalkerMovement>().SetIsAutoAggro();
            Destroy(this);
        }
    }
}
