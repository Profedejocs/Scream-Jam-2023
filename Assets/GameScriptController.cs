using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScriptController : MonoBehaviour
{
    [SerializeField] private GameObject _bossSpawn;

    public static GameScriptController instance { get; set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else { 
            instance = this;
        }
    }

    public void SpawnBoss() {
        Instantiate(Resources.Load("Spawnables/Boss"), _bossSpawn.transform.position, Quaternion.identity);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
