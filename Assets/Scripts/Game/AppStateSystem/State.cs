using UnityEngine;

namespace Game.AppStateSystem
{
    public abstract class State : MonoBehaviour
    {
        [HideInInspector]
        public AppStateManager.AppState ChosenAppState = AppStateManager.AppState.Initialize;

        protected abstract void EnterState();
        protected abstract void ExitState();

        private void Start()
        {
            SubscribeToState();
        }

        private void SubscribeToState()
        {
            AppStateManager.Instance.onEnterState += CanEnterState;
            AppStateManager.Instance.onExitState += CanExitState;
        }

        private void CanEnterState()
        {
            if (!IsActive()) return;

            EnterState();
        }

        private void CanExitState()
        {
            if (IsActive())
            {
                ExitState();
            }
        }

        private bool IsActive()
        {
            return AppStateManager.Instance.ActiveAppState == ChosenAppState;
        }
    }
}