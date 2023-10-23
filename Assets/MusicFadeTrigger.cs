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
            _isTriggered = true;
            var sources = collision.gameObject.GetComponents<AudioSource>();

            foreach (var source in sources)
            {
                if (source.clip.name.Equals("Scream Jam SFX Master ambience_forest_night"))
                {
                    _charSource1 = source;
                }
                else if (source.clip.name.Equals("Monster Ambience, Distant Destruction, Huge Groans")) { 
                    _charSource2 = source;
                }
            }

            StartCoroutine(FadeMusic());
        }
    }

    private IEnumerator FadeMusic() {
        float currentTime = 0;
        float duration = 3f;
        float start1 = _charSource1.volume;
        float start2 = _charSource2.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            _charSource1.volume = Mathf.Lerp(start1, 0, currentTime / duration);
            _charSource2.volume = Mathf.Lerp(start2, 0, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
