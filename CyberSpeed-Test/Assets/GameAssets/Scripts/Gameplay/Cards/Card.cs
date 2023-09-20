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
        /// <summary>
        /// name of the card
        /// </summary>
        [SerializeField] string cardName;

        /// <summary>
        /// returns Card name
        /// </summary>
        public string CardName => cardName;

        /// <summary>
        /// Return card name hashcode as a object ID
        /// </summary>
        public int ObjectID => cardName.GetHashCode();

        /// <summary>
        /// return card transform
        /// </summary>
        public Transform Transform => transform;

        /// <summary>
        /// click event for the card
        /// </summary>
        public Action<ICard> TileClicked { get; set; }

        /// <summary>
        /// card front side object 
        /// </summary>
        [SerializeField] GameObject frontCard;

        /// <summary>
        /// card back side object 
        /// </summary>
        [SerializeField] GameObject backCard;

        /// <summary>
        /// flag determines whether the card can be clicked
        /// </summary>
        private bool canClick = false;

        /// <summary>
        /// set target position for the card.
        /// </summary>
        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        /// <summary>
        /// enabling front card
        /// </summary>
        public void EnableCard()
        {
            frontCard.SetActive(true);
            backCard.SetActive(false);
            canClick = false;
        }

        /// <summary>
        /// disabling front card
        /// </summary>
        public void DisableCard()
        {
            frontCard.SetActive(false);
            backCard.SetActive(true);
            canClick = true;
        }


        private void OnMouseDown()
        {
            if (canClick)
                transform.localScale = Vector3.one * 1.1f;
        }

        private void OnMouseUp()
        {
            if (canClick)
            {
                transform.localScale = Vector3.one;
                TileClicked?.Invoke(this);
            }
        }
    }
}
