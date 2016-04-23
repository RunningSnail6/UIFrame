using UnityEngine;
using System.Collections;

namespace UIFrameWork
{
    public abstract class BaseUI : MonoBehaviour
    {
        #region Cache gameObject & transform
        
        private GameObject _cacheGameObject;

        public GameObject CacheGameOnject
        {
            get
            {
                if (null == _cacheGameObject)
                    _cacheGameObject = this.gameObject;
                return _cacheGameObject;
            }
        }

        private Transform _cacheTransform;
        public Transform CatchTransform
        {
            get
            {
                if (null == _cacheTransform)
                    _cacheTransform = this.transform;
                return _cacheTransform;
            }
        }
        #endregion

        #region EnumObjectState & UI Type
        /// <summary>
        /// The _state
        /// </summary>
        protected EnumObjectState _state = EnumObjectState.None;

        /// <summary>
        /// Occours when state changed
        /// </summary>
        public event StateChangeEvent StateChanged;

        /// <summary>
        /// 取得或设置状态
        /// </summary>
        /// <returns>状态</returns>
        public EnumObjectState State
        {
            protected get
            {
                return this._state;
            }
            set
            {
                EnumObjectState oldState = this._state;
                this._state = value;
                if (null != StateChanged)
                    StateChanged(this, this._state, oldState);
            }
        }

        /// <summary>
        /// 取得使用的界面类型
        /// </summary>
        /// <returns>使用的界面类型</returns>
        public abstract EnumUIType GetUIType();

        #endregion

        void Start()
        {
            OnStart();
        }

        void Awake()
        {
            this.State = EnumObjectState.Initial;
            OnAwake();
        }

        void Update()
        {
            if(this._state == EnumObjectState.Ready)
            {
                OnUpdate(Time.deltaTime);
            }
        }

        public void Release()
        {
            this.State = EnumObjectState.Closing;
            GameObject.Destroy(this.CacheGameOnject);
            OnRelease();
        }

        void OnDestory()
        {
            this.State = EnumObjectState.None;
        }

        protected virtual void OnStart() { }
        protected virtual void OnAwake() 
        {
            this.State = EnumObjectState.Loading;
            //播放音乐
            this.OnPlayOpenUIAudio();
        }
        protected virtual void OnUpdate(float deltalTime) { }
        protected virtual void OnRelease() 
        {
            this.State = EnumObjectState.None;
            //关闭音乐
            this.OnPlayCloseUIAudio();
        }

        /// <summary>
        /// 播放打开界面音乐
        /// </summary>
        protected virtual void OnPlayOpenUIAudio() { }

        /// <summary>
        /// 播放关闭界面音乐
        /// </summary>
        protected virtual void OnPlayCloseUIAudio() { }

        /// <summary>
        /// Sets the UI
        /// </summary>
        /// <param name="uiParams"></param>
        protected virtual void SetUI(params object[] uiParams) 
        {
            this.State = EnumObjectState.Loading;
        } 

        public virtual void SetUIparam(params object[] uiParams)
        {

        }


        /// <summary>
        /// 完成加载数据的逻辑
        /// </summary>
        protected virtual void OnLoadData() 
        { 

        }

        /// <summary>
        /// 在打开的时候设置界面的状态
        /// </summary>
        /// <param name="uiParams"></param>
        public void SetUIWhenOpening(params object[] uiParams)
        {
            SetUI(uiParams);
            StartCoroutine(LoadDataAsyn());
        }
        private IEnumerator LoadDataAsyn()
        {
            yield return new WaitForSeconds(0);
            if(this.State == EnumObjectState.Loading)
            {
                this.OnLoadData();
                this.State = EnumObjectState.Ready;
            }
        }
    }
}