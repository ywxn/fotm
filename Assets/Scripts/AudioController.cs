using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    private AudioSource audioSource;

    public AudioClip intro;
    public AudioClip loop;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.volume = SettingsValues.masterVolume;
        audioSource.volume = 0.4f;
        StartCoroutine(PlayMusicCoroutine(intro, loop));
    }

    private IEnumerator PlayMusicCoroutine(AudioClip intro, AudioClip loop)
    {
        audioSource.loop = false;
        audioSource.clip = intro;
        audioSource.Play();
        yield return new WaitForSeconds(intro.length);
        audioSource.clip = loop;
        audioSource.Play();
        audioSource.loop = true;
    }

    private void StopMusic()
    {
        audioSource.Stop();
    }

}
