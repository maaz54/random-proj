using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool.Interface;
using System.Linq;
using Gameplay.Interface;
using System;

namespace Gameplay
{
    public class Card : MonoBehaviour, ICard, IPoolableObject
    {
        [SerializeField] string cardName;
        public string CardName => cardName;
        public int ObjectID => cardName.GetHashCode();
        public Transform Transform => transform;

        public Action<ICard> TileClicked { get; set; }

        [SerializeField] GameObject frontCard;
        [SerializeField] GameObject backCard;

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void DisableCard()
        {
            frontCard.SetActive(false);
            backCard.SetActive(true);
        }

        public void EnableCard()
        {
            frontCard.SetActive(true);
            backCard.SetActive(false);
        }

        private void OnMouseDown()
        {
            transform.localScale = Vector3.one * 1.1f;
        }

        private void OnMouseUp()
        {
            transform.localScale = Vector3.one;
            TileClicked?.Invoke(this);
        }


    }
}
