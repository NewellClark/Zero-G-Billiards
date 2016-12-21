using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Codonbyte;
using Codonbyte.SpaceBilliards.GameLogic;

namespace Codonbyte.SpaceBilliards.Arena.States
{
    [Serializable]
    public class SetGameModeGameState : GameState
    {
        public override void OnStateEnter(GameState previous)
        {
            if (setGameModeControl == null)
            {
                StartTwoPlayerGame();
                return;
            }
            setGameModeControl.SetActive(true);
        }

        public override void OnStateExit()
        {
            if (setGameModeControl == null) return;
            setGameModeControl.SetActive(false);
        }

        public void StartTwoPlayerGame()
        {
            gameModeHolder.StartTwoPlayerGame();
            StateMachine.Current = nextState;
        }

        public void StartOnePlayerGame()
        {
            gameModeHolder.StartOnePlayerGame();
            StateMachine.Current = nextState;
        }

        [Tooltip("Not using this for now, as single player mode has buggy logic. Going to cut straight to 2-player mode.")]
        [SerializeField]
        private GameObject setGameModeControl;

        [SerializeField]
        [SiblingGameState]
        private GameState nextState;

        
        [SerializeField]
        private RootGameModeHolder gameModeHolder;
        
    }
}
