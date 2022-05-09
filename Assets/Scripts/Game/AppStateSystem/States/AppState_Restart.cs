using UnityEngine;
using UnityEngine.UI;

namespace Game.AppStateSystem.States
{
    public class AppState_Restart : State
    {
        public Button resetButton = null;

        public GameObject[] winPanelContainer = null;

        // State Functions =========================================================================================================
        protected override void EnterState()
        {
            resetButton.gameObject.SetActive(true);
        }

        protected override void ExitState()
        {
            resetButton.gameObject.SetActive(false);

            ResetAllListeners();
            
            foreach (var winPanel in winPanelContainer)
            {
                winPanel.gameObject.SetActive(false);
            }
        }

        // =========================================================================================================================
        
        private void ResetAllListeners()
        {
            resetButton.onClick.RemoveAllListeners();
        }
        
        public void SetWinPanelContainer(int _index, bool _bool)
        {
            if (_index == 9) return;
            
            winPanelContainer[_index].gameObject.SetActive(_bool);
        }
    }
}