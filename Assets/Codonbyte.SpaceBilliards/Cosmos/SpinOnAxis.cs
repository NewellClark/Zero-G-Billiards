using UnityEngine;
using System.Collections;
using System;

namespace Codonbyte.SpaceBilliards.Cosmos
{
    public class SpinOnAxis : MonoBehaviour
    {
        [SerializeField]
        private double _rotationPeriod = 86400;
        [SerializeField]
        private UnitOfTime _periodUnits = UnitOfTime.Second;
        public TimeSpan RotationPeriod
        {
            get
            {
                switch (_periodUnits)
                {
                    case UnitOfTime.Second:
                        return TimeSpan.FromSeconds(_rotationPeriod);
                    case UnitOfTime.Hour:
                        return TimeSpan.FromHours(_rotationPeriod);
                    case UnitOfTime.Day:
                        return TimeSpan.FromDays(_rotationPeriod);
                    default:
                        return new TimeSpan();
                }
            }
        }

        public double CurrentRotationFraction
        {
            get
            {
                DateTime now = DateTime.Now;
                TimeSpan time = now.TimeOfDay + TimeSpan.FromDays(now.DayOfYear);
                double angleFraction = time.TotalSeconds % RotationPeriod.TotalSeconds / RotationPeriod.TotalSeconds;
                return angleFraction;
            }
        }

        [SerializeField]
        private Vector3 axisOfSpin = Vector3.up;

        private Quaternion initialRotation;

        void Start()
        {
            initialRotation = transform.localRotation;

        }
        void Update()
        {
            float angleFraction = (float)CurrentRotationFraction;
            Quaternion spinAngle = Quaternion.AngleAxis(360 * angleFraction, Vector3.up);
            transform.localRotation = initialRotation * spinAngle;
        }
    } 

    public enum UnitOfTime { Second, Hour, Day };
}
