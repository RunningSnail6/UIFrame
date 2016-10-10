using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

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

		private BaseScene currentScene = new BaseScene ();

		public EnumScenceType LastSceneType{ get; set;}

		public EnumScenceType ChangeSceneType{ get; private set;}

		private EnumUIType sceneOpenUIType = EnumUIType.None;

		private object[] scenceOpenUIParams = null;

		public BaseScene CurrentScence 
		{
			get {return currentScene;}
			set{ currentScene = value;}
		}

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

		#region Change Scence

		public void ChangeScenceDirect(EnumScenceType _sceneType)
		{
			UIManager.Instance.CloseUIAll ();
			if(CurrentScence != null)
			{
				CurrentScence.Release();
				CurrentScence = null;
			}
			LastSceneType = ChangeSceneType;
			ChangeSceneType = _sceneType;
			string sceneName = GetScenceName (_sceneType);
			//change scene
			CoroutineController.Instance.StartCoroutine (AsyncLoadScene (sceneName));
		}

		public void ChangeScenceDirect(EnumScenceType _scenceType,EnumUIType _uiType,params object[] _params)
		{
			sceneOpenUIType = _uiType;
			scenceOpenUIParams = _params;
			if(LastSceneType == _scenceType)
			{
				if(sceneOpenUIType == EnumUIType.None)
				{
					return;
				}
				UIManager.Instance.OpenUI(sceneOpenUIType,scenceOpenUIParams);
				sceneOpenUIType = EnumUIType.None;
			}
			else
			{
				ChangeScenceDirect(_scenceType);
			}
		}

		public IEnumerator<AsyncOperation> AsyncLoadScene(string sceneName)
		{
			AsyncOperation oper = Application.LoadLevelAsync (sceneName);
			yield return oper;
			//message send

			if(sceneOpenUIType != EnumUIType.None)
			{
				UIManager.Instance.OpenUI(sceneOpenUIType,scenceOpenUIParams);
				sceneOpenUIType = EnumUIType.None;
			}
		}
		#endregion

		public void ChangeScence(EnumScenceType _sceneType)
		{
			UIManager.Instance.CloseUIAll ();
			if(CurrentScence != null)
			{
				CurrentScence.Release();
				CurrentScence = null;
			}
			LastSceneType = ChangeSceneType;
			ChangeSceneType = _sceneType;
			string sceneName = GetScenceName (_sceneType);
			//change loding scene
			CoroutineController.Instance.StartCoroutine (AsyncLoadOtherScene ());
		}
		
		public void ChangeScence(EnumScenceType _scenceType,EnumUIType _uiType,params object[] _params)
		{
			sceneOpenUIType = _uiType;
			scenceOpenUIParams = _params;
			if(LastSceneType == _scenceType)
			{
				if(sceneOpenUIType == EnumUIType.None)
				{
					return;
				}
				UIManager.Instance.OpenUI(sceneOpenUIType,scenceOpenUIParams);
				sceneOpenUIType = EnumUIType.None;
			}
			else
			{
				ChangeScence(_scenceType);
			}
		}
		
		private IEnumerator AsyncLoadOtherScene()
		{
			string sceneName = GetScenceName (EnumScenceType.LoadingScene);
			AsyncOperation oper = Application.LoadLevelAsync (sceneName);
			yield return oper;
			//message send
			
			if(sceneOpenUIType != EnumUIType.None)
			{
				UIManager.Instance.OpenUI(sceneOpenUIType,scenceOpenUIParams);
				sceneOpenUIType = EnumUIType.None;
			}
		}
    }
}
