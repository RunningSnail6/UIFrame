using UnityEngine;
using System.Collections;
using UIFrameWork;
using UnityEngine.UI;

public class TestTwo : BaseUI
{

    public override EnumUIType GetUIType()
    {
        return EnumUIType.TestTwo;
    }

	// Use this for initialization
	void Start () {
		EventTriggerListener listener = EventTriggerListener.Get(transform.Find("Panel/Button").gameObject);
		listener.SetEventHandle(EnumTouchEventType.OnClick, OnClickBtn);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnClickBtn(GameObject _listener, object _args, params object[] _params)
	{
		UIManager.Instance.OpenUICloseOthers(EnumUIType.TestOne);
	}
}
