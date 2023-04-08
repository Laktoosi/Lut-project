using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    AudioClip previousClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.PlayOneShot(RandomClip());
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying) audioSource.PlayOneShot(RandomClip());
    }

    AudioClip RandomClip()
    {
        AudioClip newClip = audioClips[Random.Range(0, audioClips.Length)];
        while(newClip == previousClip)
        {
            newClip = audioClips[Random.Range(0, audioClips.Length)];
        }
        previousClip = newClip;
        return newClip;
    }
}
