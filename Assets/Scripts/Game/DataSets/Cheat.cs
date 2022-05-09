using System;

namespace Game.DataSets
{
    [System.Serializable]
    public struct Cheat
    {
        public bool isActive;
        public HandType handType;

        public Card[] GetCheatHand(Card[] _cards)
        {
            Card[] cards = new Card[5];

            switch (handType)
            {
                case HandType.RoyalFlush:
                    cards[0] = _cards[0];
                    cards[1] = _cards[9];
                    cards[2] = _cards[10];
                    cards[3] = _cards[11];
                    cards[4] = _cards[12];
                    break;
                case HandType.StraightFlush:
                    cards[0] = _cards[3];
                    cards[1] = _cards[4];
                    cards[2] = _cards[5];
                    cards[3] = _cards[6];
                    cards[4] = _cards[7];
                    break;
                case HandType.FourOfAKind:
                    cards[0] = _cards[0];
                    cards[1] = _cards[13];
                    cards[2] = _cards[26];
                    cards[3] = _cards[39];
                    cards[4] = _cards[7];
                    break;
                case HandType.FullHouse:
                    cards[0] = _cards[0];
                    cards[1] = _cards[13];
                    cards[2] = _cards[15];
                    cards[3] = _cards[28];
                    cards[4] = _cards[41];
                    break;
                case HandType.Flush:
                    cards[0] = _cards[0];
                    cards[1] = _cards[2];
                    cards[2] = _cards[4];
                    cards[3] = _cards[6];
                    cards[4] = _cards[9];
                    break;
                case HandType.Straight:
                    cards[0] = _cards[5];
                    cards[1] = _cards[6];
                    cards[2] = _cards[20];
                    cards[3] = _cards[34];
                    cards[4] = _cards[48];
                    break;
                case HandType.ThreeOfAKind:
                    cards[0] = _cards[5];
                    cards[1] = _cards[18];
                    cards[2] = _cards[31];
                    cards[3] = _cards[34];
                    cards[4] = _cards[1];
                    break;
                case HandType.TwoPair:
                    cards[0] = _cards[5];
                    cards[1] = _cards[18];
                    cards[2] = _cards[15];
                    cards[3] = _cards[28];
                    cards[4] = _cards[1];
                    break;
                case HandType.JacksOrBetter:
                    cards[0] = _cards[10];
                    cards[1] = _cards[23];
                    cards[2] = _cards[34];
                    cards[3] = _cards[17];
                    cards[4] = _cards[0];
                    break;
                case HandType.Other:
                    break;
            }
            
            
            return cards;
        }
    }
}