using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsTimer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreditsTimerMethod());
    }

    private IEnumerator CreditsTimerMethod() {
        yield return new WaitForSeconds(15f);

        SceneManager.LoadScene(0);
    }
}
