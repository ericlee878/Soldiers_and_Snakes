using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip clip;
    public AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        audioSrc.clip = clip;
        audioSrc.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }

    //public void PlaySound(string clip)
    //{
    //    switch (clip)
    //    {
    //        case "scoreSound":
    //            audioSrc.PlayOneShot(scoreSound);
    //            break;
    //    }
    //}
}
