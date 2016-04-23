using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIFrameWork
{ 
    public class UIManager:Singleton<UIManager>
    {
        /// <summary>
        /// 需要打开UI界面时传递的数据类
        /// </summary>
        class UIInfoData
        {
            public EnumUIType UIType { get; private set; }

            public Type ScriptType { get; private set; }
            public string Path { get; private set; }
            public object[] UIparams { get; private set; }
            public UIInfoData(EnumUIType _uiType,string _path,params object[] _uiParams)
            {
                UIType = _uiType;
                Path = _path;
                UIparams = _uiParams;
                this.ScriptType = UIPathDefines.GetUIScriptByType(this.UIType);
            }
        }
        /// <summary>
        /// 保存打开的UI
        /// </summary>
        private Dictionary<EnumUIType, GameObject> dicOpenUIs = null;

        /// <summary>
        /// 保存需要打开的UI
        /// </summary>
        private Stack<UIInfoData> stackOpenUIs = null;

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            dicOpenUIs = new Dictionary<EnumUIType, GameObject>();
            stackOpenUIs = new Stack<UIInfoData>();
        }

        /// <summary>
        /// 获取指定UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_uiType"></param>
        /// <returns></returns>
        public T GetUI<T>(EnumUIType _uiType) where T : BaseUI
        {
            GameObject _retObj = GetUIObject(_uiType);
            //获取指定UI的对象
             if (_retObj != null)
             {
                 return _retObj.GetComponent<T>();
             }
             return null;
        }

        public GameObject GetUIObject(EnumUIType _uiType)
        {
            GameObject _retObj = null;
            //获取指定的UI的对象
            if (!dicOpenUIs.TryGetValue(_uiType, out _retObj))
                throw new Exception("dicOpenUIs TryGetValue Failure ! _uiType: " + _uiType.ToString());
            return _retObj;
        }

        #region Preload UI Prefab By EnumUIType
        /// <summary>
        /// Prefabs the U.
        /// </summary>
        /// <param name="_uiTypes"></param>
        public void PreloadUI(EnumUIType[] _uiTypes)
        {
            for(int i=0;i<_uiTypes.Length;i++)
            {
                PreloadUI(_uiTypes[i]);
            }
        }

        /// <summary>
        /// Preload the U.
        /// </summary>
        /// <param name="_uiType"></param>
        public void PreloadUI(EnumUIType _uiType)
        {
            string path = UIPathDefines.GetPrefabPathByType(_uiType);
            Resources.Load(path);
        }
        #endregion

        #region Open UI By EnumUIType
        /// <summary>
        /// 打开界面
        /// </summary>
        /// <param name="_uiTypes"></param>
        public void OpenUI(EnumUIType[] _uiTypes)
        {
            OpenUI(false, _uiTypes, null);
        }

        public void OpenUI(EnumUIType _uiType,params object[] _uiParams)
        {
            EnumUIType[] _uiTypes = new EnumUIType[1];
            _uiTypes[0] = _uiType;
            OpenUI(false, _uiTypes, _uiParams);
        }

        public void OpenUICloseOthers(EnumUIType[] _uiTypes)
        {
            OpenUI(true, _uiTypes, null);
        }

        public void OpenUICloseOthers(EnumUIType _uiType, params object[] _uiParams)
        {
            EnumUIType[] _uiTypes = new EnumUIType[1];
            _uiTypes[0] = _uiType;
            OpenUI(true, _uiTypes, _uiParams);
        }

        /// <summary>
        /// 打开UI
        /// </summary>
        /// <param name="_isCloseOthers"></param>
        /// <param name="_uiTypes"></param>
        /// <param name="_uiParams"></param>
        public void OpenUI(bool _isCloseOthers, EnumUIType[] _uiTypes, params object[] _uiParams)
        {
            //Close Others UI
            if(_isCloseOthers)
            {
                CloseUIAll();
            }

            //Push _uiTypes in stack
            for(int i=0;i<_uiTypes.Length;i++)
            {
                EnumUIType _uiType = _uiTypes[i];
                if (!dicOpenUIs.ContainsKey(_uiType))
                {
                    string _path = UIPathDefines.GetPrefabPathByType(_uiType);
                    stackOpenUIs.Push(new UIInfoData(_uiType,_path,_uiParams));
                }
            }

            // OPen UI,协同加载UI
            if(stackOpenUIs.Count > 0)
            {
                CoroutineController.Instance.StartCoroutine(AsyncLoadData());
            }
        }

        /// <summary>
        /// Async the Load Data
        /// </summary>
        /// <returns>The load data</returns>
        private IEnumerator<int> AsyncLoadData()
        {
            UIInfoData _uiInfoData = null;
            UnityEngine.Object _prefabObj = null;
            GameObject _uiObject = null;
            if(stackOpenUIs != null && stackOpenUIs.Count > 0)
            {
                do
                {
                    _uiInfoData = stackOpenUIs.Pop();
                    _prefabObj = Resources.Load(_uiInfoData.Path);
                    if (_prefabObj != null)
                    {
                        //此处需要处理，如果是NGUI需要使用AddChild，如果是用UGUI需要添加到桌布下面
                        //_uiObject = NGUITools.AddChild(GameObject.Instance.mainUICamera.gameObject, _prefabObj);
                        _uiObject = MonoBehaviour.Instantiate(_prefabObj) as GameObject;
                        BaseUI _baseUI = _uiObject.GetComponent<BaseUI>();
                        if(_baseUI == null)
                        {
                            _baseUI = _uiObject.AddComponent(_uiInfoData.ScriptType) as BaseUI;
                        }
                        if (_baseUI != null)
                        {
                            _baseUI.SetUIWhenOpening(_uiInfoData.UIparams);
                        }
                        dicOpenUIs.Add(_uiInfoData.UIType, _uiObject);
                    }
                } while (stackOpenUIs.Count > 0);
            }
            yield return 0;
        }
        #endregion

        #region Close UI By EnumUIType
        /// <summary>
        /// 关闭界面
        /// </summary>
        public void CloseUIAll()
        {
            List<EnumUIType> _listKey = new List<EnumUIType>(dicOpenUIs.Keys);
            for (int i = 0; i < _listKey.Count; i++)
            {
                CloseUI(_listKey[i]);
            }
            //CloseUI(_listKey.ToArray());
            dicOpenUIs.Clear();
        }

        public void CloseUI(EnumUIType[] _uiTypes)
        {
            for(int i=0;i<_uiTypes.Length;i++)
            {
                CloseUI(_uiTypes[i]);
            }
        }

        /// <summary>
        /// 关闭UI
        /// </summary>
        /// <param name="_uiType"></param>
        public void CloseUI(EnumUIType _uiType)
        {
            GameObject _uiObj = GetUIObject(_uiType);
            if(null == _uiObj)
            {
                dicOpenUIs.Remove(_uiType);
            }
            else
            {
                BaseUI _baseUI = _uiObj.GetComponent<BaseUI>();
                if(null == _baseUI)
                {
                    GameObject.Destroy(_uiObj);
                    dicOpenUIs.Remove(_uiType);
                }
                else
                {
                    _baseUI.StateChanged += CloseUIHandle;
                    _baseUI.Release();
                }
            }
        }

        /// <summary>
        /// Close the user interface handle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="newState"></param>
        /// <param name="oldState"></param>
        private void CloseUIHandle(object sender, EnumObjectState newState, EnumObjectState oldState)
        {
            if (newState == EnumObjectState.Closing)
            {
                BaseUI _baseUI = sender as BaseUI;
                dicOpenUIs.Remove(_baseUI.GetUIType());
                _baseUI.StateChanged -= CloseUIHandle;
            }
        }
        #endregion

    }
}
