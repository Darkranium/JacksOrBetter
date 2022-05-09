using UnityEngine;
using UnityEngine.UI;

namespace Game.AppStateSystem.States
{
    public class AppState_Draw : State
    {
        public Button drawCardsButton = null; // State Change Button

        public Button[] cardSlotButtons = null;
        public GameObject[] holdTextContainers = null;
        
        public GameObject[] winPanelContainer = null;
        
        // State Functions =========================================================================================================
        protected override void EnterState()
        {
            drawCardsButton.gameObject.SetActive(true);

            foreach (var slots in cardSlotButtons)
            {
                slots.gameObject.SetActive(true);
            }
        }

        protected override void ExitState()
        {
            drawCardsButton.gameObject.SetActive(false);
            
            foreach (var holdText in holdTextContainers)
            {
                holdText.gameObject.SetActive(false);
            }

            ResetAllListeners();
        }
        // =========================================================================================================================
        
        private void ResetAllListeners()
        {
            drawCardsButton.onClick.RemoveAllListeners();

            foreach (Button slot in cardSlotButtons)
            {
                slot.onClick.RemoveAllListeners();
            }
        }

        public void ToggleHold(int _index)
        {
            holdTextContainers[_index].gameObject.SetActive(!holdTextContainers[_index].activeInHierarchy);
        }

        public void ToggleInteractable()
        {
            for (int i = 0; i < 5; i++)
            {
                cardSlotButtons[i].interactable = !cardSlotButtons[i].interactable;
            }
        }

        public void SetSlotGraphic(int _index, Sprite _graphic)
        {
            cardSlotButtons[_index].image.sprite = _graphic;
        }

        public void SetWinPanelContainer(int _index, bool _bool)
        {
            if (_index == 9) return;
            
            winPanelContainer[_index].gameObject.SetActive(_bool);
        }

        public void DeactivateWinPanels()
        {
            foreach (var winPanel in winPanelContainer)
            {
                winPanel.gameObject.SetActive(false);
            }
        }
    }
}