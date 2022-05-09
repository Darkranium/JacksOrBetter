using System.Collections.Generic;
using System.Linq;
using Game.DataSets;
using UnityEngine;

namespace Game
{
    public class Game
    {
        public Card[] CardsOnTheTable { get; private set; } = null;
        
        public readonly Money Money = null;
        
        private readonly Card[] cards = null;

        public Game(Card[] _cards)
        {
            Money = new Money();
            cards = _cards;
            CardsOnTheTable = new Card[5];
        }

        // Randomly gets five cards from the initial card array.
        public void Deal()
        {
            List<Card> tempCardList = cards.ToList();
            
            for (int i = 0; i < 5; i++)
            {
                if (CardsOnTheTable[i].held) continue;

                int randomCardIndex = Random.Range(0, tempCardList.Count);
                CardsOnTheTable[i] = tempCardList[randomCardIndex];
                tempCardList.RemoveAt(randomCardIndex);
            }
        }

        public void CheatDeal(Card[] _cards)
        {
            for (int i = 0; i < CardsOnTheTable.Length; i++)
            {
                CardsOnTheTable[i] = _cards[i];
            }
        }

        // We sort the cards to make our calculation algorithms easier to decide what hand we have.
        public HandType Calculate()
        {
            Card[] sortedCards =  CardsOnTheTable.ToList().OrderBy(_i => _i.value).ToArray();
            Hand hand = new Hand(sortedCards);

            return hand.HandType;
        }

        public void SwapHold(int _index)
        {
            CardsOnTheTable[_index].held = !CardsOnTheTable[_index].held;
        }
    }
}