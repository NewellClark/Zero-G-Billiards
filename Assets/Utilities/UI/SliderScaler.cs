using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace Codonbyte.UI
{
    /// <summary>
    /// Allows a slider to be resized to fill its containing object without the features of the slider getting 
    /// distorted (i.e. the knob will still maintain the proper aspect ratio when the slider is scaled). 
    /// </summary>
    [ExecuteInEditMode]
    public class SliderScaler : UIBehaviour
    {
        [SerializeField]
        [Tooltip("The slider to scale. The slider's parent will be set to the object that this script is on.")]
        private Slider _slider;

        private const float sliderDefaultHeight = 20;

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            UpdateEverything();
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            UpdateEverything();
        }
#endif

        private void UpdateEverything()
        {
            if (_slider == null) return;
            var sliderRect = (RectTransform)_slider.transform;
            sliderRect.SetParent(transform, false);

            float scaleFactor = transform.rect.height / sliderDefaultHeight;
            sliderRect.localScale = Vector3.one * scaleFactor;
            Vector2 anchorOffset = Vector2.one / (2 * scaleFactor);
            Vector2 center = Vector2.one / 2;
            sliderRect.anchorMin = center - anchorOffset;
            sliderRect.anchorMax = center + anchorOffset;
            sliderRect.offsetMin = sliderRect.offsetMax = Vector2.zero;
        }

        public new RectTransform transform
        {
            get { return (RectTransform)base.transform; }
        }
    } 
}
