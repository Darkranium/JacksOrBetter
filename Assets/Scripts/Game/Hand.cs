using System.Collections.Generic;
using Game.DataSets;

namespace Game
{
    /*
     * Calculations are based on this rule set https://www.pokernews.com/casino/jacks-or-better-video-poker-strategy.htm
     */
    public class Hand
    {
        public HandType HandType { get; private set; }

        private readonly Card[] sortedCards = null;

        public Hand(Card[] _sortedCards)
        {
            sortedCards = _sortedCards;

            if (IsRoyalFlush()) HandType = HandType.RoyalFlush;
            else if (IsStraightFlush()) HandType = HandType.StraightFlush;
            else if (IsFourOfAKind()) HandType = HandType.FourOfAKind;
            else if (IsFullHouse()) HandType = HandType.FullHouse;
            else if (IsFlush()) HandType = HandType.Flush;
            else if (IsStraight()) HandType = HandType.Straight;
            else if (IsThreeOfAKind()) HandType = HandType.ThreeOfAKind;
            else if (IsTwoPair()) HandType = HandType.TwoPair;
            else if (IsJacksOrBetter()) HandType = HandType.JacksOrBetter;
            else HandType = HandType.Other;
        }
        
        private bool IsRoyalFlush()
        {
            int total = 0;
            Suits suitCheck = Suits.Clubs;
            
            for (int i = 0; i < sortedCards.Length; i++)
            {
                int value = (int)sortedCards[i].value + 1;
                
                total += value;

                if (i == 0)
                {
                    suitCheck = sortedCards[i].suit;
                }
                else if (suitCheck != sortedCards[i].suit)
                {
                    return false;
                }
            }

            return total == 60;
        }
        
        private bool IsStraightFlush()
        {
            Suits suitCheck = sortedCards[0].suit;

            for (int i = 0; i < sortedCards.Length; i++)
            {
                if (suitCheck != sortedCards[i].suit)
                {
                    return false;
                }
            }

            for (int i = 0; i < sortedCards.Length; i++)
            {
                int initialValue = (int)sortedCards[0].value + 1;
                int value = (int)sortedCards[i].value + 1;

                if (value == initialValue + i)
                {
                    continue;
                }
                
                return false;
            }
            
            return true;
        }
        
        private bool IsFourOfAKind()
        {
            for (int i = 0; i < 2; i++)
            {
                if (CompareCardValues(i, i + 1) && CompareCardValues(i, i + 2) && CompareCardValues(i, i + 3))
                {
                    return true;
                }    
            }
            
            return false;
        }
        
        private bool IsFullHouse()
        {
            return GetPairCount() == 2 && IsThreeOfAKind();
        }
        
        private bool IsFlush()
        {
            Suits suitCheck = sortedCards[0].suit;

            for (int i = 1; i < 5; i++)
            {
                if (suitCheck != sortedCards[i].suit) return false;
            }
            
            return true;
        }
        
        private bool IsStraight()
        {
            for (int i = 0; i < sortedCards.Length; i++)
            {
                if (sortedCards[i].value == sortedCards[0].value + i)
                {
                    continue;
                }
                
                return false;
            }
            
            return true;
        }
        
        private bool IsThreeOfAKind()
        {
            for (int i = 0; i < 3; i++)
            {
                if (CompareCardValues(i, i + 1) &&
                    CompareCardValues(i, i + 2))
                    return true;
            }
            
            return false;
        }
        
        private bool IsTwoPair()
        {
            return GetPairCount() == 2;
        }
        
        private bool IsJacksOrBetter()
        {
            for (int i = 0; i < sortedCards.Length; i++)
            {
                for (int j = i + 1; j < sortedCards.Length; j++)
                {
                    if (CompareCardValues(i, j) && (int)sortedCards[i].value + 1 > 10)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }

        private bool CompareCardValues(int _index1, int _index2)
        {
            return sortedCards[_index1].value == sortedCards[_index2].value;
        }

        private int GetPairCount()
        {
            List<Card> pairs = new List<Card>();
            
            for (int i = 0; i < sortedCards.Length; i++)
            {
                for (int j = i; j < sortedCards.Length; j++)
                {
                    if (i == j) continue;
                    if (sortedCards[i].value != sortedCards[j].value) continue;

                    if (!pairs.Contains(sortedCards[i]) && !pairs.Contains(sortedCards[j]))
                    {
                        pairs.Add(sortedCards[i]);
                        pairs.Add(sortedCards[j]);
                    }
                    
                    break;
                }
            }

            return pairs.Count / 2;
        }
    }
}