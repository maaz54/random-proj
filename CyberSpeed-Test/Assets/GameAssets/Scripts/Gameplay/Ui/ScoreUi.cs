using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Gameplay.UI
{
    public class ScoreUi : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] TextMeshProUGUI noOfTurnsText;

        public void UpdateScoreText(int score)
        {
            scoreText.text = score.ToString();
        }

        public void UpdateNoOfTurnsText(int noOfTurns)
        {
            noOfTurnsText.text = noOfTurns.ToString();
        }
    }
}
