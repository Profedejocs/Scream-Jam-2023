using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Text Health;
    public Text Infection;
    public Text Ammo;

    public static HUDController instance { get; set; }

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else { 
            instance = this;
        }
    }

    public void UpdateDamage(float curHP) {
        Health.text = "Health: " + Mathf.Floor((curHP / 200f) * 100) + "%";
    }

    public void UpdateInfection(float curInfection) { 
        Infection.text = "Infection: " + Mathf.Floor(curInfection) + "%";
    }

    public void UpdateAmmo(float curAmmo) {
        Ammo.text = "Ammo: " + curAmmo;
    }
}
