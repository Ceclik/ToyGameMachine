using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Components
{
    public class ToySoundPlayer : MonoBehaviour
    {
        private AudioSource _sound;

        private void Start()
        {
            _sound = GetComponent<AudioSource>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("WinFloor"))
            {
                _sound.Play();
            }
        }
    }
}