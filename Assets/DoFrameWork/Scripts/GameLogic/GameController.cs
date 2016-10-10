using UnityEngine;
using System.Collections;
using UIFrameWork;
using System;

public class GameController : DDOLSingleton<GameController> {

	// Use this for initialization
	void Start () {

//		UIManager.Instance.OpenUI(EnumUIType.TestOne);
//		GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/TestUIOne"));
//		TestOne to = go.GetComponent<TestOne>();
//		if (null == to)
//			to = go.AddComponent<TestOne>();

		//RegisterAllModules ();\
		ModuleManager.Instance.RegisterAllModules ();
		ScenceManager.Instance.RegisterAllScence ();

////        UIManager.Instance.OpenUI(EnumUIType.TestOne);
//		float time = System.Environment.TickCount;
//		for(int i=1;i<2000;i++)
//		{
//			GameObject go =null;
//			// 1
////			go = Instantiate(Resources.Load<GameObject>("Prefabs/Cube"))as GameObject;
//			// 2
////			go = ResManager.Instance.LoadInstance("Prefabs/Cube") as GameObject;
//			// 3
////			ResManager.Instance.LoadAsyncInstance("Prefabs/Cube",(_obj)=>{
////				go = _obj as GameObject;
////				go.transform.position = UnityEngine.Random.insideUnitSphere * 20;
////			}) ;
//			// 4
////			ResManager.Instance.LoadCoroutineInstance("Prefabs/Cube",(_obj)=>{
////				go = _obj as GameObject;
////				go.transform.position = UnityEngine.Random.insideUnitSphere * 20;
////			}) ;
//			//go.transform.position = UnityEngine.Random.insideUnitSphere * 20;
//		}
//		Debug.Log ("Times:" + (System.Environment.TickCount - time) * 1000);
//        StartCoroutine(AutoUpdateGold());
	}


	//此部分应在GameController中完成


	// Update is called once per frame
	void Update () {

	}

    private IEnumerator AutoUpdateGold()
    {
        int gold = 0;
        while (true)
        {
            gold++;
            yield return new WaitForSeconds(1.0f);
			Message message = new Message(MessageType.Net_MessageTestOne, this);
			message["gold"] = gold;
			MessageCenter.Instance.SendMessage(message);
//            Message message = new Message(MessageType.Net_MessageTestOne, this);
//            message["gold"] = gold;
//            message.Send();
        }
    }
}
