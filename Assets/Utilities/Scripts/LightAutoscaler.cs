using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Codonbyte
{
    /// <summary>
    /// DO NOT USE DO NOT USE DO NOT USE DO NOT USE.
    /// </summary>
    [Obsolete("Not even worth it, just trash this", true)]
    public class LightAutoscaler : MonoBehaviour
    {
        private Vector3 oldLossyScale;
        private Dictionary<Light, float> _scaleFactorLookup = new Dictionary<Light, float>();
        private void AddComponentToLookup(Light light)
        {
            _scaleFactorLookup.Add(light, light.intensity / transform.lossyScale.ToArray().Mean());
        }
        private void RecalculateComponentScaleFactor(Light light)
        {
            _scaleFactorLookup[light] = transform.lossyScale.ToArray().Mean();
        }
        private IEnumerable<Light> Lights { get { return _scaleFactorLookup.Keys; } }

        private void RescaleLights()
        {
            if (transform.hasChanged)
            {
                oldLossyScale = transform.lossyScale;
                transform.hasChanged = false;
                foreach (var light in Lights)
                {
                    light.range = _scaleFactorLookup[light] * transform.lossyScale.ToArray().Mean();
                }
            }
            else
            {
                foreach (var light in Lights) RecalculateComponentScaleFactor(light);
            }
        }

        private void RecalculateScaleLookup()
        {
            var toRemove = new List<Light>();
            foreach (var light in lightFinderMethod())
            {
                if (!light)
                {
                    toRemove.Add(light);
                    continue;
                }
                if (_scaleFactorLookup.ContainsKey(light)) continue;
                AddComponentToLookup(light);
            }

            foreach (var light in toRemove) _scaleFactorLookup.Remove(light);
        }

        private Func<IEnumerable<Light>> lightFinderMethod;

        [SerializeField]
        private bool searchInChildren = false;

        void Awake()
        {
            UpdateLightFinderMethod();
            oldLossyScale = transform.lossyScale;
            RecalculateScaleLookup();
        }


        void Update()
        {
            RescaleLights();
        }
#if UNITY_EDITOR
        void OnValidate()
        {
            UpdateLightFinderMethod();
            RecalculateScaleLookup();
            RescaleLights();
        }
#endif

        private void UpdateLightFinderMethod()
        {
            if (searchInChildren)
            {
                lightFinderMethod = GetComponentsInChildren<Light>;
                return;
            }
            lightFinderMethod = GetComponents<Light>;
        }

    }
}
