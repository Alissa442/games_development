using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioClip[] sound;

    // menu button to execute this function in the inspector
    [ContextMenu("Play Sound")]
    public void Play()
    {
        //Debug.Log("Play Sound");
        if (sound == null || sound.Length == 0)
        {
            Debug.LogWarning("No sound assigned to PlaySound script");
            return;
        }

        // pick a random clip from the array
        int index = UnityEngine.Random.Range(0, sound.Length);

        // The time in seconds the sound clip will play
        float len = sound[index].length;
        
        // Create a new GameObject
        GameObject soundObject = new GameObject("Sound");

        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.clip = sound[index];

        audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f); // Randomize pitch
        audioSource.volume = UnityEngine.Random.Range(0.5f, 1f); // Randomize volume

        DestroyIn d = soundObject.AddComponent<DestroyIn>();
        d.DestroyWithDelay(len + 0.3f);

        audioSource.Play();

        //Debug.Log($"Playing sound {sound[index].name} {len}");
    }
}
