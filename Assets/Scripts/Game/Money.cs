using Game.DataSets;

namespace Game
{
    /*
     * Money class only deals with the money and keeps it detached from the actual game code base.
     */
    public class Money
    {
        private int currentBet = 1;

        public int CurrentMoney { get; private set; } = 500;

        public OnBetUpdate onBetUpdate;
        public OnMoneyUpdate onMoneyUpdate;

        public delegate void OnBetUpdate(int _bet);
        public delegate void OnMoneyUpdate(int _money);

        private const int MaxBet = 5;
        private const int MinBet = 1;

        private const int RoyalFlushEarning = 250;
        private const int StraightFlushEarning = 50;
        private const int FourOfAKindEarning = 25;
        private const int FullHouseEarning = 9;
        private const int FlushEarning = 5;
        private const int StraightEarning = 4;
        private const int ThreeOfAKindEarning = 3;
        private const int TwoPairEarning = 2;
        private const int JacksOrBetterEarning = 1;

        public void SetMoney(int _money)
        {
            CurrentMoney = CurrentMoney - currentBet < 0 ? 0 : _money;
            onMoneyUpdate?.Invoke(CurrentMoney);
        }
        
        public void AddMoney(HandType _handType)
        {
            switch (_handType)
            {
                case HandType.RoyalFlush:
                    CurrentMoney += RoyalFlushEarning;
                    break;
                case HandType.StraightFlush:
                    CurrentMoney += StraightFlushEarning;
                    break;
                case HandType.FourOfAKind:
                    CurrentMoney += FourOfAKindEarning;
                    break;
                case HandType.FullHouse:
                    CurrentMoney += FullHouseEarning;
                    break;
                case HandType.Flush:
                    CurrentMoney += FlushEarning;
                    break;
                case HandType.Straight:
                    CurrentMoney += StraightEarning;
                    break;
                case HandType.ThreeOfAKind:
                    CurrentMoney += ThreeOfAKindEarning;
                    break;
                case HandType.TwoPair:
                    CurrentMoney += TwoPairEarning;
                    break;
                case HandType.JacksOrBetter:
                    CurrentMoney += JacksOrBetterEarning;
                    break;
                case HandType.Other:
                    CurrentMoney += 0;
                    break;
            }
            
            onMoneyUpdate?.Invoke(CurrentMoney);
        }

        public void SetBet(int _bet)
        {
            currentBet = _bet;
            onBetUpdate?.Invoke(currentBet);
        }

        public void IncreaseBetAmount()
        {
            if (currentBet < MaxBet)
            {
                SetBet(currentBet + 1);
            }
        }

        public void DecreaseBetAmount()
        {
            if (currentBet > MinBet)
            {
                SetBet(currentBet - 1);
            }
        }

        public void Bet()
        {
            SetMoney(CurrentMoney - currentBet);
        }
    }
}