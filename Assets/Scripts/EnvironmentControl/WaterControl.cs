using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterControl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Character"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().SetInWater();
        } else if (collision.gameObject.name.Equals("TestEnemy")) {
            collision.gameObject.GetComponent<EnemyWalkerMovement>().SetInWater();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("TestEnemy"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().SetOutOfWater();
        }
        else if (collision.gameObject.name.Equals("TestEnemy"))
        {
            collision.gameObject.GetComponent<EnemyWalkerMovement>().SetOutOfWater();
        }
    }
}
