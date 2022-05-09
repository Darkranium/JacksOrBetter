using Game.AppStateSystem;
using Game.AppStateSystem.States;
using Game.DataSets;
using UnityEngine;

namespace Game
{
    /*
     * This class is responsible for initializing the game and UI states by creating a Game object and sets up
     * UI connections dynamically within the RunGame() class. This way we don't have to deal with find each button
     * and assigning a listener. Also keeps game logic internal.
     */
    [RequireComponent(typeof(AppStateManager))]
    [RequireComponent(typeof(AppState_Initialize))]
    [RequireComponent(typeof(AppState_Bet))]
    [RequireComponent(typeof(AppState_Draw))]
    [RequireComponent(typeof(AppState_Restart))]
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Cheat cheat;
        [SerializeField] 
        private int currentMoney = 500;

        private Card[] cards = null;

        private void Awake()
        {
            SetClassStates();
        }

        private void SetClassStates()
        {
            GetComponent<AppState_Initialize>().ChosenAppState = AppStateManager.AppState.Initialize;
            GetComponent<AppState_Bet>().ChosenAppState = AppStateManager.AppState.Bet;
            GetComponent<AppState_Draw>().ChosenAppState = AppStateManager.AppState.Draw;
            GetComponent<AppState_Restart>().ChosenAppState = AppStateManager.AppState.Restart;
        }

        private void Start()
        {
            RunGame();
        }

        // State Order: Initialize => Bet => Draw => Restart
        /*
         * This method retrieves all cards from the project folder, creates a game object
         * which holds current game's data and connects it to the UI state system.
         */
        private void RunGame()
        {
            cards = Data.GetCardsFromFolder();
            Game game = new Game(cards);
            
            AppStateManager.Instance.SetState(AppStateManager.AppState.Initialize);
            
            AppState_Bet betState = AppStateManager.Instance.GetState<AppState_Bet>();
            AppState_Draw drawState = AppStateManager.Instance.GetState<AppState_Draw>();
            AppState_Restart restartState = AppStateManager.Instance.GetState<AppState_Restart>();
            
            // Money and Betting callbacks are setup to update UI within the betting state.
            game.Money.onMoneyUpdate += betState.SetMoneyText;
            game.Money.onBetUpdate += betState.SetBetText;
            game.Money.SetMoney(currentMoney);
            game.Money.SetBet(1);
            currentMoney = game.Money.CurrentMoney;

            // Randomly gets cards from the cards array to give a hand. CheatDeal sets up
            // cheated hands for test cases.
            game.Deal();
            if (cheat.isActive)
            {
                game.CheatDeal(cheat.GetCheatHand(cards));
            }
            
            // Returns whether we had a good hand and what it is or not
            HandType initialHandCalculation = game.Calculate();

            // Here is the logic for setting up all button connections with the UI state system
            SetBetStateUIActions(game, betState, drawState, initialHandCalculation);
            SetDrawStateUIActions(game, drawState, restartState);
            SetRestartStateUIActions(game, restartState);
            SetStateUITransitions(betState, drawState);
        }

        /*
         * After initialization we enter this state to allow player to bet an amount between min and max
         * amount. Then for the bet button we make sure the money gets subtracted from the current money
         * we currently hold.
         */
        private static void SetBetStateUIActions(Game _game, AppState_Bet _betState, AppState_Draw _drawState, HandType _handType)
        {
            _betState.increaseBetButton.onClick.AddListener(_game.Money.IncreaseBetAmount);
            _betState.decreaseBetButton.onClick.AddListener(_game.Money.DecreaseBetAmount);

            _betState.betButton.onClick.AddListener(_game.Money.Bet);
            _betState.betButton.onClick.AddListener(() => _drawState.SetWinPanelContainer((int) _handType, true));
        }
        
        /*
         * This state is responsible for revealing the cards and making them interactable so player can
         * hold next time they switch state. We make sure to clicking on the draw button deals cards which
         * are not held.
         */
        private void SetDrawStateUIActions(Game _game, AppState_Draw _drawState, AppState_Restart _restartState)
        {
            _drawState.ToggleInteractable();
            
            for (int i = 0; i < 5; i++)
            {
                var innerScopeIndex = i;
                _drawState.cardSlotButtons[i].onClick.AddListener(() => { _game.SwapHold(innerScopeIndex); });
                _drawState.cardSlotButtons[i].onClick.AddListener(() => { _drawState.ToggleHold(innerScopeIndex); });
                _drawState.SetSlotGraphic(i, _game.CardsOnTheTable[i].graphic);
            }
            
            _drawState.drawCardsButton.onClick.AddListener(_game.Deal);
            _drawState.drawCardsButton.onClick.AddListener(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    _drawState.SetSlotGraphic(i, _game.CardsOnTheTable[i].graphic);
                }
            });

            _drawState.drawCardsButton.onClick.AddListener(() =>
            {
                var handType = _game.Calculate();
                _game.Money.AddMoney(handType);
                currentMoney = _game.Money.CurrentMoney;
                _drawState.DeactivateWinPanels();
                _drawState.ToggleInteractable();
                _restartState.SetWinPanelContainer((int) handType, true);
            });
        }

        /*
         * Setting up the restart button so game resets and reruns.
         */
        private void SetRestartStateUIActions(Game _game, AppState_Restart _restartState)
        {
            _restartState.resetButton.onClick.AddListener(() =>
            {
                _game.Money.SetBet(1);
                RunGame();
            });
        }

        private static void SetStateUITransitions(AppState_Bet _betState, AppState_Draw _drawState)
        {
            _betState.betButton.onClick.AddListener(() => AppStateManager.Instance.SetState(AppStateManager.AppState.Draw));
            _drawState.drawCardsButton.onClick.AddListener(() => AppStateManager.Instance.SetState(AppStateManager.AppState.Restart));
        }
    }
}