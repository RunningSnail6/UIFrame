using UnityEngine;
using System.Collections;
using UIFrameWork;
using UnityEngine.UI;

public class TestOne : BaseUI
{
	private TestOneModule oneModule;
    private Button btn;
    private Text text;

    public override EnumUIType GetUIType()
    {
        return EnumUIType.TestOne;
    }

    void Start()
    {
		text = this.transform.Find("Panel/Text02").GetComponent<Text>();
        EventTriggerListener listener = EventTriggerListener.Get(transform.Find("Panel/Button").gameObject);
        listener.SetEventHandle(EnumTouchEventType.OnClick, Close, 1, "111");
        listener.SetEventHandle(EnumTouchEventType.OnClick, OnClickBtn);
		oneModule = ModuleManager.Instance.Get<TestOneModule> ();
		text.text = "gold:" + oneModule.Gold;
    }

	protected override void OnAwake()
	{
		MessageCenter.Instance.AddListener ("AutoUpdateGold",UpdateGold);
		base.OnAwake ();
	}

	protected override void OnRelease()
	{
		MessageCenter.Instance.RemoveListener ("AutoUpdateGold",UpdateGold);
		base.OnRelease();
	}

	private void UpdateGold(Message message)
	{
		int gold02 = (int) message["gold"];
		text.text = "gold:" + gold02;
		Debug.Log ("TestOne UpdateGold :" + gold02);
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
