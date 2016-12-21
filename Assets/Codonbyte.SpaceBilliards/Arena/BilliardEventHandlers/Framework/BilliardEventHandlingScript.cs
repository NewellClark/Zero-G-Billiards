using UnityEngine;
using System.Collections;
using Codonbyte.SpaceBilliards;
using Codonbyte.SpaceBilliards.GameLogic;

namespace Codonbyte.SpaceBilliards.Arena
{
    [RequireComponent(typeof(GameModeHolder))]
    public abstract class BilliardEventHandlingScript : MonoBehaviour
    {
        private GameModeHolder _holder;
        /// <summary>
        /// Gets the GameModeHolder component on the current GameObject.
        /// </summary>
        protected GameModeHolder GameModeHolder
        {
            get
            {
                if (_holder == null) _holder = GetComponent<GameModeHolder>();
                return _holder;
            }
        }

        /// <summary>
        /// Gets the GameMode object for the current game.
        /// </summary>
        public GameMode Mode
        {
            get { return GameModeHolder.Mode; }
        }

        protected class InitializationData
        {
            public InitializationData() { }
        }

        protected abstract void RegisterBilliardEventHandlers(InitializationData data);

        protected abstract void UnregisterBilliardEventHandlers(InitializationData data);

        private bool registered = false;

        void Awake()
        {
            GameModeHolder.OnGameModeChanged += (s, e) =>
            {
                RegisterBilliardEventHandlers(new InitializationData());
                this.registered = true;
            };
        }

        void OnEnable()
        {
            if (registered) return;
            if (Mode == null) return;
            RegisterBilliardEventHandlers(new InitializationData());
            registered = true;
        }

        void OnDisable()
        {
            if (Mode == null) return;
            UnregisterBilliardEventHandlers(new InitializationData());
            registered = false;
        }
    }
}
