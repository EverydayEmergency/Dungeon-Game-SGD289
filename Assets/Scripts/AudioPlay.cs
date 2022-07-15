using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlay : MonoBehaviour
{
    [SerializeField] private AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    public void playAudio()
    {
        audioData.Play(0);
    }
}
