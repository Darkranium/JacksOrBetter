using Game.DataSets;
using UnityEngine;

namespace Game
{
    public static class Data
    {
        internal static Card[] GetCardsFromFolder()
        {
            Card[] cards = new Card[52];

            Sprite[] graphics = Resources.LoadAll<Sprite>("Art/Cards");
			
            int suitIndex = 0;
            
            for (int i = 0; i < cards.Length; i++)
            {
                if (i != 0 && i % 13 == 0)
                {
                    suitIndex++;
                }
                
                cards[i].suit = (Suits) suitIndex;
                cards[i].value = (Values) Mathf.Repeat(i, 13);
                cards[i].graphic = graphics[i];

                if (i == 0 || i == 13 || i == 26 || i == 39) cards[i].value = Values.Ace;
            }

            return cards;
        }
    }
}