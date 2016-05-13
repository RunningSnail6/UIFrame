using System;
using UnityEngine;

namespace UIFrameWork
{
	static public class MethodExtension
	{
		/// <summary>
		/// Gets the or add component.
		/// </summary>
		/// <returns>The or add component.</returns>
		/// <param name="go">Go.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		static public T GetOrAddComponent<T>(this Component child) where T:Component
		{
			T ret = child.GetComponent<T> ();
			if (null == ret)
				ret = child.gameObject.AddComponent<T> ();
			return ret;
		}
		/// <summary>
		/// Gets the or add component.
		/// </summary>
		/// <returns>The or add component.</returns>
		/// <param name="child">Child.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		static public T GetOrAddComponent<T>(this GameObject child) where T:Component
		{
			T ret = child.GetComponent<T> ();
			if (null == ret)
				ret = child.AddComponent<T> ();
			return ret;
		}
	}
}

