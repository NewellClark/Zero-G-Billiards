using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Codonbyte;
using Codonbyte.Development;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Codonbyte.SpaceBilliards.GameLogic;
using Codonbyte.SpaceBilliards.Arena;

namespace Codonbyte.SpaceBilliards.UI
{
	[ExecuteInEditMode]
	public class BasicGameInfoDisplayerPanel : UIBehaviour
	{
#pragma warning disable 649
		[SerializeField]
		private RootGameModeHolder modeHelper;

		[SerializeField]
		private Text modeDisplayer;

		[SerializeField]
		private Text currentPlayerDisplayer;

		[SerializeField]
		private Text currentPlayerBallAllianceDisplayer;
#pragma warning restore 649

		void Update()
		{
			if (modeHelper == null || modeHelper.Mode == null) return;
			if (modeDisplayer != null) modeDisplayer.text = "Game Mode: " + modeHelper.Mode.GetType().Name;
			if (currentPlayerDisplayer != null) currentPlayerDisplayer.text = modeHelper.Mode.CurrentPlayer.Name + "\'s turn.";
			if (currentPlayerBallAllianceDisplayer != null)
			{
				string intro = "Ball type: ";
				string alliance = modeHelper.Mode.CurrentPlayer.BallAlliance == null ? 
					"Undecided" : modeHelper.Mode.CurrentPlayer.BallAlliance.Value.ToString();
				currentPlayerBallAllianceDisplayer.text = intro + alliance;
			}
		}
	} 
}
