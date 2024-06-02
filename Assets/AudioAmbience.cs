using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAmbience : MonoBehaviour
{
    public AudioClip[] audioClips; // Array to hold the audio clips
    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Ensure that there are exactly 6 audio clips assigned
        if (audioClips.Length != 5)
        {
            Debug.LogError("Please assign exactly 6 audio clips.");
            return;
        }

        // Start the coroutine to play audio after a random interval
        StartCoroutine(PlayRandomAudio());
    }

    IEnumerator PlayRandomAudio()
    {
        while (true)
        {
            // Wait for a random time between 1 and 2 minutes
            float randomInterval = Random.Range(60f, 120f);
            yield return new WaitForSeconds(randomInterval);
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0.2f, 4f);

            Debug.Log("Playing clip");

            // Pick a random audio clip from the array
            int randomIndex = Random.Range(0, audioClips.Length);
            AudioClip randomClip = audioClips[randomIndex];

            // Play the selected audio clip
            audioSource.clip = randomClip;
            audioSource.Play();

            // Wait until the audio clip has finished playing
            yield return new WaitForSeconds(randomClip.length);
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, 4f);

        }
    }
}
