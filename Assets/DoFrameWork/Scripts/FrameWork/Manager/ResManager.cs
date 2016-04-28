using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIFrameWork
{
    public class AssetInfo
    {
        private UnityEngine.Object _Object;
        public Type AssetType { get; set; }
        public string Path { get; set; }
		public int RefCount{ get; set;}
        public bool IsLoad { 
            get
            {
                return null != _Object;
            }
        }

        public UnityEngine.Object AssetObject
        {
            get
            {
                if(null == _Object)
                {
					_ResourcesLoad();
                }
                return _Object;
            }
        }

		public IEnumerator GetGoroutineObject(Action<UnityEngine.Object> _loaded)
		{
			while (true) 
			{
				if(null == _Object)
				{
					yield return null;
					_ResourcesLoad();
					yield return null;
				}else
				{
					if(null != _loaded)
					{
						_loaded(_Object);
					}
				}
				yield break;
			}
		}

		private void _ResourcesLoad()
		{
			try{
				_Object = Resources.Load(Path);
				if(null == _Object)
					Debug.Log("Resources Load Failure Path:" + Path);
			}
			catch(Exception e)
			{
				Debug.Log(e.ToString);
			}
		}

        public IEnumerator GetAsyncObject(Action<UnityEngine.Object> _loaded)
        {
            return GetAsyncObject(_loaded,null);
        }

        public IEnumerator GetAsyncObject(Action<UnityEngine.Object> _loaded,Action<float> _progress)
        {
            //已经加载
            if(null!=_Object)
            {
                _loaded(_Object);
                yield break;
            }
            //没有加载
            ResourceRequest _resRequest = Resources.LoadAsync(Path);
            while(_resRequest.progress < 0.9)
            {
                if (null != _progress)
                    _progress(_resRequest.progress);
                yield return null;
            }
            //
            while(!_resRequest.isDone)
            {
                if (null != _progress)
                    _progress(_resRequest.progress);
                yield return null;
            }

            //?????
            _Object = _resRequest.asset;
            if (null != _loaded)
                _loaded(_Object);

            yield return _resRequest;
        }
    }

    public class ResManager:Singleton<ResManager>
    {
        private Dictionary<string,GameObject> dicAseetInfo = null;
        public override void Init()
        {

        }

        public UnityEngine.Object LoadInstance(string _path)
        {
            UnityEngine.Object _retObj = null;
            UnityEngine.Object _obj = Load(_path);
            if(_obj != null)
            {
                _retObj = MonoBehaviour.Instantiate(_obj);
                if(_retObj != null)
                {
                    return _retObj;
                }
                else
                {
                    Debug.LogError("Error:null Instance _retObj");
                }
            }
            else
            {
                Debug.LogError("Error:null Resource Load Return _obj");
            }
            return null;
        }

        public void LoadInstance(string _path, Action<UnityEngine.Object> _loaded)
        {
            LoadInstance(_path, _loaded, null);
        }

        public void LoadInstance(string _path,Action<UnityEngine.Object> _loaded,Action<float> _progress)
        {
            Load(_path, (_obj) => {
                UnityEngine.Object _retObj = null;
                if (null != _obj)
                {
                    _retObj = MonoBehaviour.Instantiate(_obj);
                    if (null != _retObj)
                    {
                        if (null != _loaded)
                        {
                            _loaded(_retObj);
                        }
                        else
                        {
                            Debug.LogError("Error:null _loaded");
                        }
                    }
                    else
                    {
                        Debug.LogError("Error:null Instance _retObj");
                    }
                }
                else
                {
                    Debug.LogError("Error:null Resource Load Return _obj");
                }
            }, _progress);
        }

        public UnityEngine.Object Load(string _path)
        {
           if(string.IsNullOrEmpty(_path))
           {
               Debug.LogError("Error:Null _path name.");
           }

           UnityEngine.Object _retObj = null;
           return _retObj;
        }

        public void Load(string _path,Action<UnityEngine.Object> _loaded)
        {
            Load(_path,_loaded,null);
        }


        public void Load(string _path,Action<UnityEngine.Object> _loaded,Action<float> _progress)
        {
            if(string.IsNullOrEmpty(_path))
            {
                Debug.LogError("Error:Null _path name.");
                if (null != _loaded)
                    _loaded(null);
            }

			//load Res..
			AssetInfo _assetInfo = null;
			if(!dicAseetInfo.TryGetValue(_path,out _assetInfo))
			{
				_assetInfo = new AssetInfo();
				_assetInfo.Path = _path;
				dicAseetInfo.Add(_path,_assetInfo);
			}
			_assetInfo.RefCount++;
			CoroutineController.Instance.StartCoroutine (_assetInfo.GetAsyncObject (_loaded,_progress));
        }
    }
}
