using System;

namespace S22.Sasl {
	internal static class Extensions {
		/// <summary>
		/// Adds a couple of useful extensions to reference types.
		/// </summary>
		/// <summary>
		/// Throws an ArgumentNullException if the given data item is null.
		/// </summary>
		/// <param name="data">The item to check for nullity.</param>
		/// <param name="name">The name to use when throwing an
		/// exception, if necessary</param>
		public static void ThrowIfNull<T>(this T data, string name)
			where T : class {
			if (data == null)
				throw new ArgumentNullException(name);
		}

		/// <summary>
		/// Throws an ArgumentNullException if the given data item is null.
		/// </summary>
		/// <param name="data">The item to check for nullity.</param>
		public static void ThrowIfNull<T>(this T data)
			where T : class {
			if (data == null)
				throw new ArgumentNullException();
		}

		/// <summary>
		/// Throws an ArgumentException if the given string is null or
		/// empty.
		/// </summary>
		/// <param name="data">The string to check for nullity and
		/// emptiness.</param>
		public static void ThrowIfNullOrEmpty(this string data) {
			if (String.IsNullOrEmpty(data))
				throw new ArgumentException();
		}

		/// <summary>
		/// Throws an ArgumentException if the given string is null or
		/// empty.
		/// </summary>
		/// <param name="data">The string to check for nullity and
		/// emptiness.</param>
		/// <param name="name">The name to use when throwing an
		/// exception, if necessary</param>
		public static void ThrowIfNullOrEmpty(this string data, string name) {
			if (String.IsNullOrEmpty(data))
				throw new ArgumentException("The " + name +
					" parameter must not be null or empty");
		}
	}
}
