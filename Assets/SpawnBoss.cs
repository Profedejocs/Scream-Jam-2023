using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    [SerializeField] private Transform _spawnpoint;
    private bool _triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_triggered && collision.gameObject.name.Equals("Character")) {
            _triggered = true;
            Instantiate(Resources.Load("Spawnables/Boss"), _spawnpoint.position, Quaternion.identity);
            foreach (var source in collision.gameObject.GetComponents<AudioSource>()) {
                if (source.clip.name.Equals("Mist_Chase")) {
                    source.Play();
                }
            }
        }
    }
}
