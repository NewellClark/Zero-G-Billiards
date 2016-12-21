using UnityEngine;
using System.Collections;
using Codonbyte;
using Codonbyte.SpaceBilliards;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text;
using Codonbyte.UI;

namespace Codonbyte.SpaceBilliards.UI
{
	public class NotificationsWall : TimedStringQueue
	{
#pragma warning disable 649
		[SerializeField]
		private Text textField;
#pragma warning restore 649

		void Update()
		{
			var messages = new StringBuilder();
			foreach (string message in Items)
			{
				messages.Append(message + "\n");
			}
			textField.text = messages.ToString();
		}
	} 
}
