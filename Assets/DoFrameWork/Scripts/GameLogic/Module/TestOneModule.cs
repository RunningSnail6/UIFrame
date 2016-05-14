using System;
using UIFrameWork;

public class TestOneModule :BaseModule
{
	public int Gold{ get; private set;}
	public TestOneModule ()
	{
		this.AutoRegister = true;
	}

	protected override void OnLoad()
	{
		MessageCenter.Instance.AddListener (MessageType.Net_MessageTestOne, UpdateGold);
		base.OnLoad ();
	}

	protected override void OnRelease ()
	{
		MessageCenter.Instance.RemoveListener (MessageType.Net_MessageTestOne, UpdateGold);
		base.OnRelease ();
	}

	private void UpdateGold(Message message)
	{
		int gold02 = (int) message["gold"];
		if(gold02 >= 0)
		{
			Gold = gold02;
			Message message02 = new Message("AutoUpdateGold", this);
			message02["gold"] = gold02;
			message02.Send();
		}
	}
}
