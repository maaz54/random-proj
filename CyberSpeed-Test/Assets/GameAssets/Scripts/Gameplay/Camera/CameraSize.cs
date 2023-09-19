using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Interface;

namespace Gameplay
{
    public class CameraSize : MonoBehaviour
    {
        [SerializeField] Transform[] cards;

        private void Start()
        {
            GameplayManager.Instance.OnCardsGenerated += OnCardsGenerate;
        }

        private void OnCardsGenerate(ICard[,] cards)
        {

            this.cards = cards.Cast<ICard>().Select(card => card.Transform).ToArray();

            Vector3 centerPosition = CalculateCenterPosition();

            Camera.main.transform.position = new Vector3(centerPosition.x - (centerPosition.x / 2), centerPosition.y, Camera.main.transform.position.z);
            Camera.main.orthographicSize = (cards.GetLength(0) > cards.GetLength(1)) ? cards.GetLength(0) : cards.GetLength(1);
        }

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
