using System;
using UnityEngine;

namespace UIFrameWork
{
    #region 全局委托
    public delegate void StateChangeEvent(object sender,EnumObjectState newState,EnumObjectState oldState);
	public delegate void MessageEvent(Message message);
	public delegate void OnTouchEventHandle(GameObject _listener,object _args,params object[] _params);
	#endregion

    #region 全局枚举对象
    public enum EnumObjectState
    {
        None,
        Initial,
        Loading,
        Ready,
        Disabled,
        Closing
    }

    public enum EnumUIType:int
    {
        None = -1,
        TestOne = 0,
        TestTwo = 1,
    }

	public enum EnumTouchEventType
	{
		OnClick,
		OnDoubleClick,
		OnDown,
		OnEnter,
		OnExit,
		OnUp,
		OnSelect, 
		OnUpdateSelect, 
		OnDeSelect,  
		OnDrag, 
		OnDragEnd,
		OnDrop,
		OnScroll,
		OnMove,
	}

    #endregion

    public class UIPathDefines
    {
        /// <summary>
        /// UI预设
        /// </summary>
        public const string UI_PREFAB = "Prefabs/";
        /// <summary>
        /// UI小控件
        /// </summary>
        public const string UI_CONTROLS_PREFAB = "UIPrefab/Control/";
        /// <summary>
        /// UI子页面预设
        /// </summary>
        public const string UI_SUBUI_PREFAB = "UIPrefab/SubUI/";
        /// <summary>
        /// Icon
        /// </summary>
        public const string UI_ICON_PATH = "UI/Icon/";

        /// <summary>
        /// 通过UI类型获取预制路径
        /// </summary>
        /// <param name="_uiType"></param>
        /// <returns>通过UI类型获取预制路径</returns>
        public static string GetPrefabPathByType(EnumUIType _uiType)
        {
            string _path = string.Empty;
            switch(_uiType)
            {
                case EnumUIType.TestOne:
                    _path = UI_PREFAB + "TestUIOne";
                    break;
                case EnumUIType.TestTwo:
                     _path = UI_PREFAB + "TestUITwo";
                    break;
                default:
                    Debug.Log("Not Find EnumUIType! type:" + _uiType.ToString());
                    break;
            }
            return _path;
        }

        /// <summary>
        /// 通过UI类型获取对应的脚本
        /// </summary>
        /// <param name="_uiType"></param>
        /// <returns></returns>
        public static System.Type GetUIScriptByType(EnumUIType _uiType)
        {
            System.Type _scriptType = null;
            switch (_uiType)
            {
                case EnumUIType.TestOne:
                    _scriptType = typeof(TestOne);
                    break;
                case EnumUIType.TestTwo:
                    _scriptType = typeof(TestTwo);
                    break;
                default:
                    Debug.Log("Not Find EnumUIType! type:" + _uiType.ToString());
                    break;
            }
            return _scriptType;
        }
    }

    public class Defines
    {
        public Defines()
        {

        }
    }
}
