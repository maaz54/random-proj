using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gameplay
{
    public class Score : MonoBehaviour
    {
        /// <summary>
        /// representing the current score.
        /// </summary>
        private int currentScore = 0;

        /// <summary>
        /// triggered when the current score is updated
        /// </summary>
        public Action<int> OnUpdateScore;

        /// <summary>
        /// returns the current score
        /// </summary>
        public int GetScore
        {
            get => currentScore;
        }

        /// <summary>
        /// updating the current score
        /// </summary>
        public void UpdateScore(int score)
        {
            currentScore = score;
            OnUpdateScore?.Invoke(GetScore);
        }

        /// <summary>
        /// current number of turns taken by the player.
        /// </summary>
        private int currentNoOfTurns = 0;

        /// <summary>
        /// triggered when the number of turns is updated
        /// </summary>
        public Action<int> OnUpdateNoOfTurns;

        /// <summary>
        /// returns the current number of turns
        /// </summary>
        public int GetNoOfTurns
        {
            get => currentNoOfTurns;
        }

        /// <summary>
        /// updating the number of turns
        /// </summary>
        public void UpdateNoOfTurns(int NoOfTurns)
        {
            currentNoOfTurns = NoOfTurns;
            OnUpdateNoOfTurns?.Invoke(GetNoOfTurns);
        }
    }
}