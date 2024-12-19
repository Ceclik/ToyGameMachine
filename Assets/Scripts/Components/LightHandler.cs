using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Components
{
    public class LightHandler : MonoBehaviour
    {
        [SerializeField] private AudioResource secondAudioSound;
        [SerializeField] private Light winLight;
        [SerializeField] private Light backLight;
        [SerializeField] private float lightTurnOnDelay;
        [SerializeField] private float secondSoundTurnOnDelay;

        private AudioSource _sound;

        private void Start()
        {
            _sound = GetComponent<AudioSource>();
            StartCoroutine(TurnOnLight());
        }

        private IEnumerator TurnOnLight()
        {
            yield return new WaitForSeconds(lightTurnOnDelay);
            GetComponent<Light>().enabled = true;
            winLight.enabled = true;
            backLight.enabled = true;
            _sound.Play();
            StartCoroutine(WorkingSoundPlayer());
        }

        private IEnumerator WorkingSoundPlayer()
        {
            yield return new WaitForSeconds(secondSoundTurnOnDelay);
            _sound.resource = secondAudioSound;
            _sound.loop = true;
            _sound.volume = 0.05f;
            _sound.Play();
        }
    }
}