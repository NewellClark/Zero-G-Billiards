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
        [SerializeField]
        private Text textField;

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
