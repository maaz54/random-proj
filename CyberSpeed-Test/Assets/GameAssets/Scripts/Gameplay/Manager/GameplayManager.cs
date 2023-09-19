using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using ObjectPool;
using ObjectPool.Interface;
using Gameplay.Interface;
using Gameplay.UI;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gameplay
{
    public class GameplayManager : Singleton<GameplayManager>
    {
        [SerializeField] Vector2 cardsLenght;
        [SerializeField] ObjectPooler objectPooler;
        public IObjectPooler ObjectPooler => objectPooler;
        [SerializeField] CardsGrid cardsGrid;
        public Action<ICard[,]> OnCardsGenerated;
        [SerializeField] MenuUi menuUi;
        ICard[,] currentcardsGrid;
        ICard prevCardSelected = null;

        private void Start()
        {
            cardsGrid.CardsGenerated += CardsGenerated;
            menuUi.OnRestart += OnRestartGame;
            cardsGrid.GenerateCards((int)cardsLenght.x, (int)cardsLenght.y);
        }



        private void CardsGenerated(ICard[,] cards)
        {
            currentcardsGrid = cards;
            OnCardsGenerated?.Invoke(cards);
            _ = StartGame(cards);

            foreach (var card in cards)
            {
                card.TileClicked += OnTileClick;
            }
        }

        private void OnRestartGame()
        {
            foreach (var card in currentcardsGrid)
            {
                card.TileClicked -= OnTileClick;
            }
            cardsGrid.DestroyCards();
            cardsGrid.GenerateCards((int)cardsLenght.x, (int)cardsLenght.y);
        }

        private async Task StartGame(ICard[,] cards)
        {
            foreach (var card in cards)
            {
                card.EnableCard();
            }

            await Task.Delay(TimeSpan.FromSeconds(2));

            foreach (var card in cards)
            {
                card.DisableCard();
            }
        }

        private void OnTileClick(ICard card)
        {
            _ = OnTileClickTask(card);
        }

        private async Task OnTileClickTask(ICard card)
        {
            card.EnableCard();
            if (prevCardSelected == null)
            {
                prevCardSelected = card;
            }
            else
            {
                ICard prevCard = prevCardSelected;
                if (card.CardName.Contains(prevCardSelected.CardName))
                {
                    //score
                }
                prevCardSelected = null;
                await Task.Delay(TimeSpan.FromSeconds(1));
                card.Transform.gameObject.SetActive(false);
                prevCard.Transform.gameObject.SetActive(false);
            }
        }

    }
}
