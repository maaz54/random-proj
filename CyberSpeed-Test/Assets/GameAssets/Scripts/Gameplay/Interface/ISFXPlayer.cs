using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Interface
{
    public interface ISFXPlayer
    {
        /// <summary>
        /// Play unsuccessful SFX.
        /// </summary>
        void MissedSfx();

        /// <summary>
        /// Play successful SFX
        /// </summary>
        void MatchedSfx();

        /// <summary>
        /// Play level complete SFX
        /// </summary>
        void CompleteSfx();
    }
}