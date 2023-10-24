using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Character")) {
            Debug.Log("ENDINGGGGGGGGGGGGGG");
        } 
    }
}
