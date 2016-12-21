using UnityEngine;
using System.Collections;
using Codonbyte.SpaceBilliards;
using System.Collections.Generic;
using System;
using System.Linq;
using Codonbyte.SpaceBilliards.Arena.Balls;
using Codonbyte.SpaceBilliards.Arena.GamePieces;

namespace Codonbyte.SpaceBilliards.GameLogic
{
    public class Player
    {
        public Player(string name)
        {
            Name = name;
            BallAlliance = null;
        }

        public string Name { get; set; }

        /// <summary>
        /// Gets or sets whether the player is solids or stripes. Null if that has not yet been defined.
        /// </summary>
        public BallAlliance? BallAlliance { get; set; }
    }

    public abstract class GameMode
    {
        public GameMode(IEnumerable<BilliardBall> ballsIncludingCueBall, IEnumerable<BilliardPocket> pockets)
        {
            allBallsAtStart = ballsIncludingCueBall.ToList();

            TotalObjectBallsAtStart = ObjectBallsInPlay.Count;

            CueBall = (from ball in AllBallsInPlay
                       where ball.BallType == BallType.Cue
                       select ball).ToArray()[0];

            Pockets = (from pocket in pockets
                       orderby pocket.PocketIndex
                       select pocket).ToList().AsReadOnly();

            IsBreak = true;

            foreach (var pocket in Pockets)
            {
                pocket.OnBallPocketed += (o, e) => { CallOnBallPocketed(e); };
            }
        }

        [Obsolete("Yeah... we're just going to throw some Linq in the goddamn property getter for AllBallsInPlay. Fuck performance.")]
        protected IList<BilliardBall> WriteableAllBallsInPlay { get { return allBallsAtStart; } }

        private List<BilliardBall> allBallsAtStart;
        public IList<BilliardBall> AllBallsAtStart
        {
            get
            {
                return allBallsAtStart.AsReadOnly();
            }
        }
        /// <summary>
        /// Gets a read-only collection of all the balls currently in play.
        /// </summary>
        public IList<BilliardBall> AllBallsInPlay
        {
            get
            {
                var q = from ball in allBallsAtStart
                        where ball.isActiveAndEnabled
                        orderby ball.BallNumber
                        select ball;
                return q.ToList().AsReadOnly();
            }
        }

        public IList<BilliardBall> ObjectBallsInPlay
        {
            get
            {
                return (from ball in AllBallsInPlay
                        where ball.BallType == BallType.Object
                        orderby ball.BallNumber
                        select ball).ToList().AsReadOnly();
            }
        }

        public BilliardBall CueBall { get; private set; }

        public IList<BilliardPocket> Pockets { get; private set; }

        /*
        private int CountAllObjectBallsInPlay()
        {
            int count = 0;
            foreach (var obj in GameObject.FindGameObjectsWithTag("Ball"))
            {
                var ball = obj.GetComponent<BilliardBall>();
                if (ball.BallType == BallType.Object) count++;
            }
            return count;
        }

        private void InitializeBallPocketedEventHandlers()
        {
            foreach (var pocket in GameObject.FindGameObjectsWithTag("Pocket"))
            {
                var comp = pocket.GetComponent<IBallPocketedNotifier>();
                comp.OnBallPocketed += (s, e) => { CallOnBallPocketed(e); };
            }
        }*/

        /// <summary>
        /// Gets a read-only list of all the players in the game.
        /// </summary>
        public abstract IList<Player> Players { get; }

        /// <summary>
        /// Gets the player who's turn it currently is.
        /// </summary>
        public Player CurrentPlayer { get; protected set; }

        public int? CalledPocket { get; set; }

        /// <summary>
        /// Gets a value indicating whether the current turn is the break-shot. 
        /// </summary>
        public bool IsBreak { get; private set; }
        private bool badBreak = false;

        /// <summary>
        /// Called when the turn ends. Call the base form when overriding (raises event).
        /// </summary>
        public virtual void TurnEnded()
        {
            if (OnTurnEnded != null) OnTurnEnded(this, new EventArgs());
            if (!badBreak) IsBreak = false;
            CalledPocket = null;
        }

        /// <summary>
        /// Raised whenever a player's turn ends.
        /// </summary>
        public event EventHandler OnTurnEnded;

        /// <summary>
        /// Gets the total number of object balls in play at the start of the game (including death balls).
        /// </summary>
        protected int TotalObjectBallsAtStart { get; private set; }

        /// <summary>
        /// This must be called every time any ball is pocketed.
        /// </summary>
        /// <param name="e"></param>
        protected abstract void CallOnBallPocketed(BallPocketedEventArgs e);

        /// <summary>
        /// Raised whenever any ball lands in a pocket.
        /// </summary>
        public event EventHandler<BallPocketedEventArgs> OnBallPocketed;
        protected void RaiseBallPocketed(BallPocketedEventArgs e)
        {
            if (OnBallPocketed != null) OnBallPocketed(this, e);
        }

        /// <summary>
        /// Gets a value indicating whether the specified player is allowed to try and sink the death ball. 
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public virtual bool DeathBallShotAllowed(Player player)
        {
            if (player.BallAlliance == null) return false;
            var balls = from ball in ObjectBallsInPlay
                        where !ball.IsDeathBall && ball.BallAlliance == player.BallAlliance
                        select ball;
            return balls.Count() == 0;
        }

        /// <summary>
        /// Raised when the first object ball is pocketed, which determines who will be solids or stripes.
        /// This event is raised after the OnPlayerScored event.
        /// </summary>
        public event EventHandler<BilliardPlayEventArgs> OnAllianceDecided;
        protected void RaiseAllianceDecided(BilliardPlayEventArgs e)
        {
            if (OnAllianceDecided != null) OnAllianceDecided(this, e);
        }

        /// <summary>
        /// Raised when a player pockets one of their own balls.
        /// </summary>
        public event EventHandler<BilliardPlayEventArgs> OnPlayerScored;
        protected void RaisePlayerScored(BilliardPlayEventArgs e) { if (OnPlayerScored != null) OnPlayerScored(this, e); }

        /// <summary>
        /// Raised when a player pockets one of their opponent's balls.
        /// </summary>
        public event EventHandler<BilliardPlayEventArgs> OnPlayerScoredForEnemy;
        protected void RaisePlayerScoredForEnemy(BilliardPlayEventArgs e) { if (OnPlayerScoredForEnemy != null) OnPlayerScoredForEnemy(this, e); }

        /// <summary>
        /// Raised when a player "scratches" (performs some foul play such as pocketing cue-ball). 
        /// The Ball property of the event args will be the cue-ball if pocketing the cue-ball is what
        /// lead to the scratch, otherwise it will be null.
        /// </summary>
        public event EventHandler<BilliardPlayEventArgs> OnPlayerScratch;
        protected void RaisePlayerScratch(BilliardPlayEventArgs e) { if (OnPlayerScratch != null) OnPlayerScratch(this, e); }

        /// <summary>
        /// Raised when the player sinks the dreaded death ball before they pocketed all of their object balls (the equivalent of the 8-ball in regular pool). 
        /// The Ball property of the event args will be equal to the ball that the player pocketed. 
        /// If there were an even number of total object balls at the start of the game (including death balls), then this event
        /// will also be raised if a player pockets their opponent's death ball, even if they have unlocked their own death ball.
        /// </summary>
        public event EventHandler<BilliardPlayEventArgs> OnPlayerIllegallyPocketDeathBall;
        protected void RaisePlayerIllegallyPocketDeathBall(BilliardPlayEventArgs e)
        {
            if (OnPlayerIllegallyPocketDeathBall != null) OnPlayerIllegallyPocketDeathBall(this, e);
        }

        /// <summary>
        /// Raised when the player commits any kind of "scratch" while attempting to pocket the death-ball.
        /// </summary>
        public event EventHandler<BilliardPlayEventArgs> OnPlayerScratchOnDeathBall;
        protected void RaisePlayerScratchOnDeathBall(BilliardPlayEventArgs e)
        {
            if (OnPlayerScratchOnDeathBall != null) OnPlayerScratchOnDeathBall(this, e);
        }

        /// <summary>
        /// Raised when the player wins the game by clearing all of their object balls.
        /// </summary>
        public event EventHandler<BilliardPlayEventArgs> OnPlayerWinByClearingArena;
        protected void RaisePlayerWinByClearingArena(BilliardPlayEventArgs e)
        {
            if (OnPlayerWinByClearingArena != null) OnPlayerWinByClearingArena(this, e);
        }

        public event EventHandler<BilliardPlayEventArgs> OnBadBreak;
        protected void RaiseBadBreak(BilliardPlayEventArgs e)
        {
            if (OnBadBreak != null) OnBadBreak(this, e);
        }

        /// <summary>
        /// Raised when the game ends.
        /// </summary>
        public event EventHandler<GameOverEventArgs> OnGameOver;
        protected void RaiseGameOver(GameOverEventArgs e)
        {
            if (OnGameOver != null) OnGameOver(this, e);
        }
    }

    public class SinglePlayerMode : GameMode
    {
        public SinglePlayerMode(IEnumerable<BilliardBall> ballsIncludingCueBall, IEnumerable<BilliardPocket> pockets) : base(ballsIncludingCueBall, pockets)
        {
            players.Add(new Player("Player 1"));
            CurrentPlayer = players[0];
            CurrentPlayer.BallAlliance = BallAlliance.Neutral;

            InitializeGameOverEventHandlers();
        }

        private void InitializeGameOverEventHandlers()
        {
            OnPlayerScored += CheckForWinCondition;
            OnPlayerScoredForEnemy += CheckForWinCondition;
            OnPlayerIllegallyPocketDeathBall += (s, e) => RaiseGameOver(new GameOverEventArgs(e, this, null));

            //  Scratch on death ball.
            OnPlayerScratch += (s, e) =>
            {
                if (DeathBallShotAllowed(CurrentPlayer))
                {
                    RaisePlayerScratchOnDeathBall(e);
                    RaiseGameOver(new GameOverEventArgs(e, this, null));
                }
            };
        }

        private void CheckForWinCondition(object sender, BilliardPlayEventArgs e)
        {
            /*
            var balls = from ball in (from o in GameObject.FindGameObjectsWithTag("Ball") select o.GetComponent<BilliardBall>())
                        where ball.BallType == BallType.Object
                        select ball;
            */
            var balls = from ball in Component.FindObjectsOfType<BilliardBall>()
                        where ball.BallType == BallType.Object
                        select ball;

            if (balls.Count() == 0) RaiseGameOver(new GameOverEventArgs(e, this, e.Player));
        }

        private List<Player> players = new List<Player>();
        public override IList<Player> Players
        {
            get
            {
                return players.AsReadOnly();
            }
        }

        public override void TurnEnded()
        {
            base.TurnEnded();
        }

        protected override void CallOnBallPocketed(BallPocketedEventArgs e)
        {
            RaiseBallPocketed(e);

            if (e.Ball.BallType == BallType.Cue)
            {
                RaisePlayerScratch(new BilliardPlayEventArgs(CurrentPlayer, e.Ball));
                return;
            }

            //WriteableAllBallsInPlay.Remove(e.Ball);

            if (e.Ball.IsDeathBall && !DeathBallShotAllowed(CurrentPlayer))
            {
                RaisePlayerIllegallyPocketDeathBall(new BilliardPlayEventArgs(CurrentPlayer, e.Ball));
                return;
            }

            if (e.Ball.IsDeathBall && (CalledPocket != e.PocketIndex))
            {
                RaisePlayerIllegallyPocketDeathBall(new BilliardPlayEventArgs(CurrentPlayer, e.Ball));
                return;
            }

            if (e.Ball.BallType == BallType.Object)
            {
                RaisePlayerScored(new BilliardPlayEventArgs(CurrentPlayer, e.Ball));
            }
        }
    }

    public class DoublePlayerMode : GameMode
    {
        public DoublePlayerMode(IEnumerable<BilliardBall> ballsIncludingCueBall, IEnumerable<BilliardPocket> pockets) : base(ballsIncludingCueBall, pockets)
        {
            _players.Add(new Player("player 1"));
            _players.Add(new Player("player 2"));
            CurrentPlayer = _players[0];

            InitializeGameOverEventHandlers();
        }

        private void InitializeGameOverEventHandlers()
        {
            OnPlayerScored += (o, args) =>
            {
                currentPlayerScored = true;
                CheckForWinCondition(o, args);
            };

            OnPlayerScoredForEnemy += CheckForWinCondition;
            OnPlayerIllegallyPocketDeathBall += (o, e) => RaiseGameOver(new GameOverEventArgs(e, this, OtherPlayer));

            //  Lose game if you scratch on 8-ball.
            OnPlayerScratch += (o, e) =>
            {
                currentPlayerScratched = true;
                if (DeathBallShotAllowed(e.Player))
                {
                    RaisePlayerScratchOnDeathBall(e);
                    RaiseGameOver(new GameOverEventArgs(e, this, OtherPlayer));
                }
                
            };
        }

        private void CheckForWinCondition(object sender, BilliardPlayEventArgs e)
        {
            if (ObjectBallsInPlay.Count == 0)
            {
                Player winner = (from player in _players
                                 where player.BallAlliance == e.Ball.BallAlliance
                                 select player).First();
                //GameOverState state = (winner == CurrentPlayer) ? GameOverState.Win : GameOverState.Lose;

                //RaiseGameOver(new GameOverEventArgs(CurrentPlayer, e.Ball, state));
                RaiseGameOver(new GameOverEventArgs(e, this, winner));
            }
        }

        private List<Player> _players = new List<Player>();
        public override IList<Player> Players
        {
            get
            {
                return _players.AsReadOnly();
            }
        }

        /// <summary>
        /// Gets the player who's turn it is not.
        /// </summary>
        private Player OtherPlayer
        {
            get
            {
                Player result = null;
                foreach (var player in _players)
                {
                    if (player != CurrentPlayer)
                    {
                        result = player;
                        break;
                    }
                }
                return result;
            }
        }

        private bool currentPlayerScored = false;
        private bool currentPlayerScratched = false;
        private bool CurrentPlayerGetsToShootAgain()
        {
            return currentPlayerScored && !currentPlayerScratched;
        }

        public override void TurnEnded()
        {
            if (!CurrentPlayerGetsToShootAgain())
            {
                CurrentPlayer = OtherPlayer;
            }
            currentPlayerScored = false;
            currentPlayerScratched = false;

            base.TurnEnded();
        }

        protected override void CallOnBallPocketed(BallPocketedEventArgs e)
        {
            RaiseBallPocketed(e);

            var args = new BilliardPlayEventArgs(CurrentPlayer, e.Ball);

            if (e.Ball.BallType == BallType.Cue)
            {
                RaisePlayerScratch(args);
                return;
            }

            //WriteableAllBallsInPlay.Remove(e.Ball);

            if (e.Ball.IsDeathBall && !DeathBallShotAllowed(CurrentPlayer))
            {
                if (IsBreak)
                {
                    RaiseBadBreak(args);
                    return;
                }
                RaisePlayerIllegallyPocketDeathBall(args);
                return;
            }

            if (e.Ball.IsDeathBall && CalledPocket != e.PocketIndex)
            {
                RaisePlayerIllegallyPocketDeathBall(args);
                return;
            }

            if (CurrentPlayer.BallAlliance == null)
            {
                var alliances = new List<BallAlliance>(new BallAlliance[] { BallAlliance.Solids, BallAlliance.Stripes });
                CurrentPlayer.BallAlliance = e.Ball.BallAlliance;
                alliances.Remove(e.Ball.BallAlliance);
                OtherPlayer.BallAlliance = alliances[0];

                RaisePlayerScored(args);
                RaiseAllianceDecided(args);
            }

            if (e.Ball.BallAlliance == CurrentPlayer.BallAlliance)
            {
                RaisePlayerScored(args);
            }
            else if (e.Ball.IsDeathBall)
            {
                //  Player pockets the death ball legally in a game where both players share one death ball. 
                if (e.Ball.BallAlliance == BallAlliance.Neutral)
                {
                    RaisePlayerScored(args);
                }
                //  Player pockets opponent's death ball in a game where each player has their own death ball.
                else
                {
                    RaisePlayerIllegallyPocketDeathBall(args);
                }
            }
            else
            {
                RaisePlayerScoredForEnemy(args);
            }
        }
    }

    /// <summary>
    /// Argument for an event that involves a particular player.
    /// </summary>
    public class PlayerEventArgs : EventArgs
    {
        public PlayerEventArgs(Player player) : base()
        {
            Player = player;
        }

        /// <summary>
        /// The player involved with the event.
        /// </summary>
        public Player Player { get; private set; }
    }

    /// <summary>
    /// Argument for an event that involves a particular player and a particular ball.
    /// </summary>
    public class BilliardPlayEventArgs : PlayerEventArgs
    {
        public BilliardPlayEventArgs(Player player, BilliardBall ball) : base(player)
        {
            Ball = ball;
        }

        public BilliardBall Ball { get; private set; }
    }

    /*
    public class GameOverEventArgs : BilliardPlayEventArgs
    {
        public GameOverEventArgs(Player player, BilliardBall ball, GameOverState state) : base(player, ball)
        {
            State = state;
        }
        public GameOverEventArgs(BilliardPlayEventArgs e, GameOverState state) : this(e.Player, e.Ball, state) { }

        public GameOverState State { get; private set; }
    }
    public enum GameOverState { Draw, Win, Lose }; 
    */
    public class GameOverEventArgs : BilliardPlayEventArgs
    {
        public GameMode Mode { get; private set; }

        public Player Winner { get; private set; }

        /// <summary>
        /// </summary>
        /// <param name="subject">The player who made the game-ending play. This may or may not be the winning player. 
        /// For example, the subject would be the same as the winning player if the winning player ended the game by successfully clearing their field. 
        /// However, if the game ended via a player pocketing a death ball too early, then subject would be the player who lost themself the game.</param>
        /// <param name="ball">The ball involved in the play. This should be the object ball that ended the game by being pocketed, or the cue ball if
        /// the game ended by a scratch.</param>
        /// <param name="mode"></param>
        /// <param name="winner">The player who won the game, or null if there are no winners (i.e. if it is a single-player game).</param>
        public GameOverEventArgs(Player subject, BilliardBall ball, GameMode mode, Player winner) : base(subject, ball)
        {
            Mode = mode;
            Winner = winner;
        }
        public GameOverEventArgs(BilliardPlayEventArgs e, GameMode mode, Player winner) : this(e.Player, e.Ball, mode, winner) { }
    }
}


/*
/// <summary>
/// Argument for an event where the player makes a move that instantly loses them the game (such as pocketing a death-ball).
/// </summary>
internal class GameLostEventArgs : BilliardPlayEventArgs
{
    public GameLostEventArgs(Player player, BilliardBall ball, string reason) : base(player, ball)
    {
        Reason = reason;
    }

    /// <summary>
    /// Gets a string explaining the reason the specified player lost.
    /// </summary>
    public string Reason { get; private set; }
}*/