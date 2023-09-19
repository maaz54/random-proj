using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Interface;

namespace Gameplay.SFX
{
    public class SFXPlayer : MonoBehaviour, ISFXPlayer
    {
        [SerializeField] AudioClip levelCompleteClip;
        [SerializeField] AudioClip cardMatchClip;
        [SerializeField] AudioClip cardMismatchClip;
        [SerializeField] AudioSource audioSource;

        public void CompleteSfx()
        {
            PlayClip(levelCompleteClip);
        }

        public void MatchedSfx()
        {
            PlayClip(cardMatchClip);
        }

        public void MissedSfx()
        {
            PlayClip(cardMismatchClip);
        }

        private void PlayClip(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }
    }

}