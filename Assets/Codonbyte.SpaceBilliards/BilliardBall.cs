using UnityEngine;
using System.Collections;
using Codonbyte.SpaceBilliards.GameLogic;

namespace Codonbyte.SpaceBilliards
{
    public enum BallType { Object, Cue };
    public enum BallAlliance { Neutral, Solids, Stripes };

    /// <summary>
    /// Contains the properties that describe a billiard ball, such as its number, whether it is 
    /// an object ball or a cue ball, whether it is a death-ball, whether it is solids or stripes, etc. 
    /// </summary>
    public class BilliardBall : MonoBehaviour
    {
        [SerializeField]
        private BallType _ballType;
        /// <summary>
        /// Gets or sets a value indicating whether the current ball is an object ball or a cue ball. 
        /// </summary>
        public BallType BallType
        {
            get { return _ballType; }
            set
            {
                var temp = _ballType;
                _ballType = value;
                if (temp != value) UpdateProperties();
            }
        }
        [SerializeField]
        private int _ballNumber;
        /// <summary>
        /// Gets or sets the number of the ball.
        /// </summary>
        public int BallNumber
        {
            get { return _ballNumber; }
            set
            {
                var temp = _ballNumber;
                _ballNumber = value;
                if (temp != value) UpdateProperties();
            }
        }

        [SerializeField]
        private BallAlliance _ballAlliance;
        /// <summary>
        /// Gets or sets the team that the ball belongs to.
        /// </summary>
        public BallAlliance BallAlliance
        {
            get { return _ballAlliance; }
            set
            {
                var temp = _ballAlliance;
                _ballAlliance = value;
                if (temp != value) UpdateProperties();
            }
        }

        [SerializeField]
        private bool _isDeathBall;
        /// <summary>
        /// Gets or sets a value indicating whether the current ball is a "death ball". In regular, real-life pool, the 8-ball would
        /// be a death ball. A player loses if they pocket a death ball before clearing all of their object balls. 
        /// </summary>
        public bool IsDeathBall
        {
            get { return _isDeathBall; }
            set
            {
                var temp = _isDeathBall;
                _isDeathBall = value;
                if (temp != value) UpdateProperties();
            }
        }

        /// <summary>
        /// Raised when one of the main stats (i.e. the public properties of this behavior) changes.
        /// </summary>
        public event System.EventHandler OnBallDataChanged;

        private void UpdateProperties()
        {
            if (OnBallDataChanged != null) OnBallDataChanged(this, new System.EventArgs());
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
