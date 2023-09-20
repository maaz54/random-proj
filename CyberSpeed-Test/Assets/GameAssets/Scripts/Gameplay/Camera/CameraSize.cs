using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Interface;

namespace Gameplay
{
    public class CameraSize : MonoBehaviour
    {

        /// <summary>
        /// cards in the game
        /// </summary>
        [SerializeField] Transform[] cards;

        private void Start()
        {
            GameplayManager.Instance.OnCardsGenerated += OnCardsGenerate;
        }

        /// <summary>
        /// called when cards are generated in the game.
        /// </summary>
        private void OnCardsGenerate(ICard[,] cards)
        {

            this.cards = cards.Cast<ICard>().Select(card => card.Transform).ToArray();

            Vector3 centerPosition = CalculateCenterPosition();

            Camera.main.transform.position = new Vector3(centerPosition.x - (centerPosition.x / 2), centerPosition.y, Camera.main.transform.position.z);
            Camera.main.orthographicSize = (cards.GetLength(0) > cards.GetLength(1)) ? cards.GetLength(0) : cards.GetLength(1);
        }

        /// <summary>
        /// calculates the center position of the cards
        /// </summary>
        Vector3 CalculateCenterPosition()
        {
            Vector3 sum = Vector3.zero;

            foreach (Transform tile in cards)
            {
                sum += tile.position;
            }

            Vector3 centerPosition = sum / cards.Length;

            return centerPosition;
        }
    }
}
