using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionSource : MonoBehaviour
{
    public float InfectionRadius;
    public float InfectionRate;

    private static GameObject _player;

    void Start()
    {
        if (_player == null)
            _player = GameObject.Find("Character");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) < InfectionRadius)
        {
            _player.GetComponent<PlayerInfection>().Infect(InfectionRate * Time.deltaTime);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, InfectionRadius);
    }
}
