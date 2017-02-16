using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Codonbyte.SpaceBilliards.Framework
{
	public static class EnumerableExtensions
	{
		public static Vector3 Average(this IEnumerable<Vector3> @this)
		{
			return new Vector3(
				@this.Select(x => x.x).Average(),
				@this.Select(x => x.y).Average(),
				@this.Select(x => x.z).Average());
		}

		public static Vector2 Average(this IEnumerable<Vector2> @this)
		{
			return new Vector2(
				@this.Select(x => x.x).Average(),
				@this.Select(x => x.y).Average());
		}
	}
}
