using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Interface;
using UnityEngine;

namespace Gameplay
{
    public class CardsGrid : MonoBehaviour
    {
        /// <summary>
        /// Card objects that represent the different card prefabs available for the grid
        /// </summary>
        [SerializeField] private Card[] cardsPrefabs;

        /// <summary>
        /// generated card objects in a grid structure
        /// </summary>
        private ICard[,] cardsGrid;

        /// <summary>
        /// spacing between cards in the grid.
        /// </summary>
        [SerializeField] private float offset;

        /// <summary>
        /// ACtion triggered when cards are generated.
        /// </summary>
        public Action<ICard[,]> CardsGenerated;

        /// <summary>
        /// generating a grid of cards with a specified size.
        /// </summary>
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


        /// <summary>
        /// destroying all the cards in the grid
        /// </summary>
        public void DestroyCards()
        {
            foreach (var card in cardsGrid)
            {
                GameplayManager.Instance.ObjectPooler.Remove(((Card)card));
            }
        }
    }
}
