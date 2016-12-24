using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Codonbyte.SpaceBilliards.UI
{
	public class MainMenuController : MonoBehaviour
	{
		[SerializeField]
		private string _gameScene;
		public string GameScene
		{
			get { return _gameScene; }
			set
			{
				_gameScene = value;
			}
		}

		public void GoToNewGame()
		{
			//Application.LoadLevel(GameScene);
			SceneManager.LoadScene(GameScene);
		}
	} 
}
