using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIFrameWork
{
    public class ScenceManager:Singleton<ScenceManager>
    {
		#region ScenceInfoData class
		public class ScenceInfoData
		{
			public Type ScenceType { get; private set; }
			public string ScenceName { get; private set; }
			public object[] Params { get; private set; }
			public ScenceInfoData(string _scenceName,Type _scenceType,params object[] _params)
			{
				this.ScenceType = _scenceType;
				this.ScenceName = _scenceName;
				this.Params = _params;
			}
		}
		#endregion

		private Dictionary<EnumScenceType,ScenceInfoData> dicScenceInfos = null;

		public override void Init()
		{
			dicScenceInfos = new Dictionary<EnumScenceType, ScenceInfoData> ();
		}

		public void RegisterAllScence()
		{
			RegisterScence (EnumScenceType.StartGame, "StartGameScence", null, null);
			RegisterScence (EnumScenceType.LoginScence, "LoginScence", typeof(BaseScene), null);
			RegisterScence (EnumScenceType.MainScence, "MainScence", null, null);
			RegisterScence (EnumScenceType.CopyScence, "CopyScence", null, null);
		}

		public void RegisterScence(EnumScenceType _scenceID,string _scenceName,Type _scenceType, params object[] _params)
		{
			if(_scenceType == null || _scenceType.BaseType != typeof(BaseScene))
			{
					throw new Exception("Register scence type must not null and extends BaseScence");
			}
			if(dicScenceInfos.ContainsKey(_scenceID))
			{
				ScenceInfoData info = new ScenceInfoData(_scenceName,_scenceType,_params);
				dicScenceInfos.Add(_scenceID,info);
			}
		}

		public void UnRegisterScence(EnumScenceType _scenceID)
		{
			if(dicScenceInfos.ContainsKey(_scenceID))
			{
				dicScenceInfos.Remove(_scenceID);
			}
		}

		public bool IsRegisterScence(EnumScenceType _scenceID)
		{
			return dicScenceInfos.ContainsKey(_scenceID);
		}

		public ScenceInfoData GetScenceInfo(EnumScenceType _scenceID)
		{
			if(dicScenceInfos.ContainsKey(_scenceID))
			{
				return dicScenceInfos[_scenceID];
			}
			Debug.LogError ("This Scence is not register! ID:" + _scenceID.ToString ());
			return null;
		}

		public string GetScenceName(EnumScenceType _scenceID)
		{
			if(dicScenceInfos.ContainsKey(_scenceID))
			{
				return dicScenceInfos[_scenceID].ScenceName;
			}
			Debug.LogError ("This Scence is not register! ID:" + _scenceID.ToString ());
			return null;
		}

		public void ClearScence()
		{
			dicScenceInfos.Clear ();
		}
    }
}
