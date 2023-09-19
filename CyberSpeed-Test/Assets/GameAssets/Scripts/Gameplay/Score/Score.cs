using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gameplay
{
    public class Score : MonoBehaviour
    {
        private int currentScore = 0;
        public Action<int> OnUpdateScore;

        public int GetScore
        {
            get => currentScore;
        }

        public void UpdateScore(int score)
        {
            currentScore = score;
            OnUpdateScore?.Invoke(GetScore);
        }

        private int currentNoOfTurns = 0;
        public Action<int> OnUpdateNoOfTurns;

        public int GetNoOfTurns
        {
            get => currentNoOfTurns;
        }

        public void UpdateNoOfTurns(int NoOfTurns)
        {
            currentNoOfTurns = NoOfTurns;
            OnUpdateNoOfTurns?.Invoke(GetNoOfTurns);
        }
    }
}