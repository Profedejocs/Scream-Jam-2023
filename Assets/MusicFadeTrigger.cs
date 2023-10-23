using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFadeTrigger : MonoBehaviour
{
    private bool _isTriggered = false;
    private AudioSource _charSource1;
    private AudioSource _charSource2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isTriggered && collision.gameObject.name.Equals("Character")) { 
            
        }
    }
}
