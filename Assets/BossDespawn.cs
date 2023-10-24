using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDespawn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Character")) {
            GameObject.Find("Boss(Clone)").GetComponent<BossController>().Despawn();
            Destroy(this);
        }
    }
}
