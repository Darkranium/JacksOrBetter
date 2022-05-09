using UnityEngine;
using UnityEngine.UI;

namespace Game.AppStateSystem.States
{
    public class AppState_Bet : State
    {
        public Button betButton = null;
        public Button increaseBetButton = null;
        public Button decreaseBetButton = null;

        [SerializeField]
        private Text currentMoneyText = null;
        [SerializeField]
        private Text currentBetText = null;

        // State Functions =========================================================================================================
        protected override void EnterState()
        {
            betButton.gameObject.SetActive(true);
            increaseBetButton.gameObject.SetActive(true);
            decreaseBetButton.gameObject.SetActive(true);
        }

        protected override void ExitState()
        {
            betButton.gameObject.SetActive(false);
            increaseBetButton.gameObject.SetActive(false);
            decreaseBetButton.gameObject.SetActive(false);

            ResetAllListeners();
        }
        // =========================================================================================================================

        public void SetMoneyText(int _money)
        {
            currentMoneyText.text = $"Current Balance: ${_money.ToString()}";
        }
		
        public void SetBetText(int _bet)
        {
            currentBetText.text = $"Current Bet: ${_bet.ToString()}";
        }

        private void ResetAllListeners()
        {
            betButton.onClick.RemoveAllListeners();
            increaseBetButton.onClick.RemoveAllListeners();
            decreaseBetButton.onClick.RemoveAllListeners();
        }
    }
}