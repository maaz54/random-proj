using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Interface;

namespace Gameplay.SFX
{
    public class SFXPlayer : MonoBehaviour, ISFXPlayer
    {
        /// <summary>
        /// level completion sound effect.
        /// </summary>
        [SerializeField] AudioClip levelCompleteClip;

        /// <summary>
        /// card matching sound effect
        /// </summary>
        [SerializeField] AudioClip cardMatchClip;

        /// <summary>
        ///  card mismatch sound effec
        /// </summary>
        [SerializeField] AudioClip cardMismatchClip;

        /// <summary>
        /// AudioSource component for audio playback
        /// </summary>
        [SerializeField] AudioSource audioSource;

        /// <summary>
        /// Play level complete SFX
        /// </summary>
        public void CompleteSfx()
        {
            PlayClip(levelCompleteClip);
        }

        /// <summary>
        /// Play successful SFX
        /// </summary>
        public void MatchedSfx()
        {
            PlayClip(cardMatchClip);
        }

        /// <summary>
        /// Play unsuccessful SFX.
        /// </summary>
        public void MissedSfx()
        {
            PlayClip(cardMismatchClip);
        }

        /// <summary>
        /// plays a specified audio clip.
        /// </summary>
        private void PlayClip(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }
    }

}