using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderClap : MonoBehaviour {

    public AudioClip clip;
    AudioSource audio;
    bool canFlicker = true;


    private void Update()
    {
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        if (canFlicker)
        {
            canFlicker = false;
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(clip);
            GetComponent<Light>().enabled = true;
            yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));
            GetComponent<Light>().enabled = false;
            yield return new WaitForSeconds(Random.Range(0.1f, 5));
            canFlicker = true;
        }
    }
}
