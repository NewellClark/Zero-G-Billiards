using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Codonbyte.UI
{
    /// <summary>
    /// Forces the current object to maintain a specific border within its parent object.
    /// </summary>
    public class BorderedClientRegion : UIBehaviour
    {

        [SerializeField]
        [Tooltip("Normalized border to maintain around the the outside of the current object.")]
        private Vector2 _border = Vector2.zero;
        /// <summary>
        /// Gets or sets the border to maintain between itself and its parent.
        /// </summary>
        public Vector2 Border
        {
            get { return _border; }
            set
            {
                _border = value;
                UpdateEverything();
            }
        }

        public new RectTransform transform
        {
            get { return (RectTransform)base.transform; }
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            UpdateEverything();
        }
#endif

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            UpdateEverything();
        }

        private void UpdateEverything()
        {
            transform.anchorMin = Border;
            transform.anchorMax = Vector2.one - Border;
            transform.offsetMax = transform.offsetMin = Vector2.zero;
        }
    } 
}
