using UnityEngine;
using UnityEngine.UI;

namespace Game.AppStateSystem.States
{
    public class AppState_Initialize : State
    {
        [SerializeField]
        private Button[] cardSlotButtons = null;
        
        // State Functions =========================================================================================================
        protected override void EnterState()
        {
            foreach (var slots in cardSlotButtons)
            {
                slots.gameObject.SetActive(false);
            }
            
            AppStateManager.Instance.SetState(AppStateManager.AppState.Bet);
        }

        protected override void ExitState()
        {

        }
        // =========================================================================================================================
    }
}
