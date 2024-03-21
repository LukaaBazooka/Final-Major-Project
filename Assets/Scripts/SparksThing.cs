using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SparksThing : MonoBehaviour
{
    public ParticleSystem particleSystem; // Drag and drop your Particle System here
    public AudioSource audioSource; // Drag and drop your Audio Source here
    public AudioClip[] soundClips; // Array of sound clips to play

    private float minInterval = 3f;
    private float maxInterval = 7f;
    private float timer = 0f;

    void  FixedUpdate()
    {
        // Update timer
        timer += Time.deltaTime;

        // Check if it's time to emit particle and play sound
        if (timer >= Random.Range(minInterval, maxInterval))
        {
            // Reset timer
            timer = 0f;

            // Emit particle
            if (particleSystem != null)
            {
                particleSystem.Emit(Random.Range(5,20));
            }

            // Play random sound
            if (audioSource != null && soundClips.Length > 0)
            {
                AudioClip clipToPlay = soundClips[Random.Range(0, soundClips.Length)];
                audioSource.PlayOneShot(clipToPlay);
            }
        }
    }
}
