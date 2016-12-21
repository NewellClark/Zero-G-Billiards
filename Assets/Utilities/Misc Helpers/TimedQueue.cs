using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Codonbyte
{
    /// <summary>
    /// A queue that automatically removes each item after the specified amount of time.
    /// The expiration time can be different for each item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class TimedQueue<T> : MonoBehaviour, ITimedQueue<T>
    {
        public void AddItem(T item, TimeSpan life)
        {
            StartCoroutine(EnqueItemCoRoutine(item, (float)life.TotalSeconds));
        }

        public void AddItem(T item, float lifeInSeconds)
        {
            AddItem(item, TimeSpan.FromSeconds(lifeInSeconds));
        }

        /// <summary>
        /// Gets a read-only sorted list of all the items currently in the 
        /// </summary>
        public IList<T> Items
        {
            get
            {
                return (from item in lookup
                        orderby item.TimeQueued
                        select item.Item).ToList().AsReadOnly();
            }
        }

        private class QueuedItem
        {
            public T Item { get; private set; }
            public float TimeQueued { get; private set; }

            public QueuedItem(T item, float timeQueued)
            {
                Item = item;
                TimeQueued = timeQueued;
            }
        }

        private HashSet<QueuedItem> lookup = new HashSet<QueuedItem>();

        private IEnumerator<object> EnqueItemCoRoutine(T item, float secondsToLive)
        {
            var queuedItem = new QueuedItem(item, Time.timeSinceLevelLoad);
            lookup.Add(queuedItem);
            yield return new WaitForSeconds(secondsToLive);
            lookup.Remove(queuedItem);
        }
    }
}
