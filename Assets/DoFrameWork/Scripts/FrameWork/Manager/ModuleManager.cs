using System;
using System.Collections.Generic;

namespace UIFrameWork
{
	public class ModuleManager:Singleton<ModuleManager>
	{
		private Dictionary<string , BaseModule> dicModules = null;
		public override void Init()
		{
			dicModules = new Dictionary<string, BaseModule> ();
		}

		#region Get Module
		/// <summary>
		/// Get the specified key.
		/// </summary>
		/// <param name="key">Key.</param>
		public BaseModule Get(string key)
		{
			if (dicModules.ContainsKey (key))
				return dicModules [key];
			return null;
		}

		public T Get<T>() where T:BaseModule
		{
			Type t = typeof(T);
			//return Get (t.ToString ()) as T;
			if (dicModules.ContainsKey (t.ToString ()))
				return dicModules [t.ToString ()] as T;
			return null;
		}
		#endregion

		#region Register Module By Module Type
		/// <summary>
		/// Register the specified module.
		/// </summary>
		/// <param name="module">Module.</param>
		public void Register(BaseModule module)
		{
			Type t = module.GetType ();
			Register (t.ToString (), module);
		}

		/// <summary>
		/// Register the specified key and module.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="module">Module.</param>
		public void Register(string key,BaseModule module)
		{
			if (!dicModules.ContainsKey (key))
				dicModules.Add (key, module);
		}
		#endregion

		#region Un Register Module
		/// <summary>
		/// Uns the register.
		/// </summary>
		/// <param name="module">Module.</param>
		public void UnRegister(BaseModule module)
		{
			Type t = module.GetType ();
			UnRegister (t.ToString ());
		}

		/// <summary>
		/// Uns the register.
		/// </summary>
		/// <param name="key">Key.</param>
		public void UnRegister(string key)
		{
			if(dicModules.ContainsKey(key))
			{
				BaseModule module = dicModules[key];
				module.Release();
				dicModules.Remove(key);
				module = null;
			}
		}

		public void UnRegisterAll()
		{
			List<string> _keyList = new List<string> (dicModules.Keys);
			for (int i = 0;i<_keyList.Count; i++)
			{
				UnRegister(_keyList[i]);
			}
			dicModules.Clear ();
		}
		#endregion
	}
}

