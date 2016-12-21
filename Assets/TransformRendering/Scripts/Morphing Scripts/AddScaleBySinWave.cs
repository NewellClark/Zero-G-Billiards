using UnityEngine;
using System.Collections;

namespace Codonbyte.Development.TransformRendering
{
    /// <summary>
    /// Sets the object's localScale to its initial value plus a specified value multiplied by a sin function of time.
    /// </summary>
    public class AddScaleBySinWave : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _scaleFactor = Vector3.one;
        public Vector3 ScaleToAdd { get { return _scaleFactor; } set { _scaleFactor = value; } }

        [SerializeField]
        private float _scalePeriodInSeconds = 1;
        public float ScalePeriodInSeconds
        {
            get { return _scalePeriodInSeconds; }
            set { _scalePeriodInSeconds = value; }
        }

        private Vector3 scaleAtStart;
        private float currentTime = 0;

        // Use this for initialization
        void Start()
        {
            scaleAtStart = transform.localScale;
        }

        // Update is called once per frame
        void Update()
        {
            currentTime += Time.deltaTime;
            currentTime %= ScalePeriodInSeconds;

            float theta = 2 * Mathf.PI * currentTime / ScalePeriodInSeconds;
            transform.localScale = scaleAtStart + ScaleToAdd * Mathf.Sin(theta);
        }
    } 
}
