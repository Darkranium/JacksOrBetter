using System;
using UnityEngine;

namespace Game.AppStateSystem
{
    public class AppStateManager : MonoBehaviour
    {
        public static AppStateManager Instance = null;

        public enum AppState
        {
            Initialize,
            Bet,
            Draw,
            Restart
        }

        public AppState ActiveAppState = AppState.Initialize;

        public OnEnterState onEnterState;
        public OnExitState onExitState;

        public delegate void OnEnterState();
        public delegate void OnExitState();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            SetFirstState();
        }

        private void SetFirstState()
        {
            EnterState();
        }

        public void SetState(AppState _newState)
        {
            ExitState();
            ActiveAppState = _newState;
            EnterState();
        }

        public void SetState(string _newStateName)
        {
            ExitState();
            ActiveAppState = (AppState) Enum.Parse(typeof(AppState), _newStateName);
            EnterState();
        }
        
        public T GetState<T>()
        {
            Delegate[] states = onEnterState.GetInvocationList();

            foreach (Delegate state in states)
            {
                if (state.Target is T target)
                {
                    return target;
                }
            }

            return (T)states[0].Target;
        }

        private void EnterState()
        {
            onEnterState?.Invoke();
        }

        private void ExitState()
        {
            onExitState?.Invoke();
        }
    }
}
