using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfection : MonoBehaviour
{
    public float MaxInfection;
    private float _infection;

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetInfectionLevel()
    {
        return _infection;
    }

    public void Infect(float quantity)
    {
        _infection += quantity;
        Debug.Log(_infection);
    }

    
}
