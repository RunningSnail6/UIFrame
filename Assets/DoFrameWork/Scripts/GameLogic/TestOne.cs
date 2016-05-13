using UnityEngine;
using System.Collections;
using UIFrameWork;
using UnityEngine.UI;

public class TestOne : BaseUI
{
    private Button btn;
    private Text text;

    public override EnumUIType GetUIType()
    {
        return EnumUIType.TestOne;
    }

    void Start()
    {
        EventTriggerListener listener = EventTriggerListener.Get(transform.Find("Panel/Button").gameObject);
        listener.SetEventHandle(EnumTouchEventType.OnClick, Close, 1, "111");
        listener.SetEventHandle(EnumTouchEventType.OnClick, OnClickBtn);
    }

    private void Close(GameObject _listener,object _args,params object[] _params)
    {
        int i = (int)_params[0];
        string s = (string)_params[1];
        Debug.Log(i);
        Debug.Log(s);
       // UIManager.Instance.OpenUICloseOthers(EnumUIType.TestTwo);
     }

    private void OnClickBtn(GameObject _listener, object _args, params object[] _params)
    {
        UIManager.Instance.OpenUICloseOthers(EnumUIType.TestTwo);
    }
}
