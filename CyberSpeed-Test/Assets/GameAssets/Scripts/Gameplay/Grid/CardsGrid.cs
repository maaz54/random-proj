using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Interface;
using UnityEngine;

namespace Gameplay
{
    public class CardsGrid : MonoBehaviour
    {
        [SerializeField] private Card[] cardsPrefabs;
        private ICard[,] cardsGrid;
        [SerializeField] private float offset;

        public Action<ICard[,]> CardsGenerated;

        public void GenerateCards(int xLength, int yLength)
        {
            cardsGrid = new ICard[xLength, yLength];
            Vector3 position = new(0, 0, 0);
            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    ICard card;

                    card = GameplayManager.Instance.ObjectPooler.Pool<Card>(cardsPrefabs[UnityEngine.Random.Range(0, cardsPrefabs.Length)]);
                    card.SetPosition(position);

                    cardsGrid[x, y] = card;
                    position.y += offset;
                }
                position.x += offset;
                position.y = 0;
            }
            CardsGenerated?.Invoke(cardsGrid);
        }


        public void DestroyCards()
        {
            foreach (var card in cardsGrid)
            {
                GameplayManager.Instance.ObjectPooler.Remove(((Card)card));
            }
        }
    }
}
