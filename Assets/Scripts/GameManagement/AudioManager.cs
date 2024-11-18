using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> _audioClips;

        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySoundFX(int soundIndex)
        {
            _audioSource.PlayOneShot(_audioClips[soundIndex]);
        }
    }
}