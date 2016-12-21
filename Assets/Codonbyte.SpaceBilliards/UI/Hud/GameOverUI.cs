using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Codonbyte;
using Codonbyte.SpaceBilliards;
using Codonbyte.SpaceBilliards.Arena;
using Codonbyte.SpaceBilliards.Arena.States;
using Codonbyte.SpaceBilliards.GameLogic;


namespace Codonbyte.SpaceBilliards.UI
{
    public class GameOverUI : UIBehaviour, GameOverGameState.IGameOverScreen
    {
        private GameOverEventArgs _gameOverInfo;
        public GameOverEventArgs GameInfo
        {
            get { return _gameOverInfo; }
            set
            {
                _gameOverInfo = value;
                UpdateEverything();
            }
        }

        private string _gameOverReason = string.Empty;
        public string GameOverReason
        {
            get { return _gameOverReason; }
            set
            {
                _gameOverReason = value;
                UpdateEverything();
            }
        }

        private void UpdateEverything()
        {
            if (GameInfo == null) return;
            winnerHeading.text = GameInfo.Winner != null ? "Winner: " + GameInfo.Winner.Name : string.Empty;
            details.text = GameOverReason;
        }

        [SerializeField]
        private Text winnerHeading;

        [SerializeField]
        private Text details;
    }
}
