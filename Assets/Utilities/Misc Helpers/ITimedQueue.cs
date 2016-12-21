using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Codonbyte
{
    public interface ITimedQueue<T>
    {
        void AddItem(T item, float howManySeconds);

        IList<T> Items { get; }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public static class IMessageWallExtensions
    {
        public static void AddItem<T>(this ITimedQueue<T> queue, T item, TimeSpan itemLife)
        {
            queue.AddItem(item, (float)itemLife.TotalSeconds);
        }
    }
}
