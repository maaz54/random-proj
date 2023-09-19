using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Interface
{
    public interface ISFXPlayer
    {
        void MissedSfx();
        void MatchedSfx();
        void CompleteSfx();
    }
}