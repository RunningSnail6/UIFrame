using UnityEngine;
using System.Collections;
using UIFrameWork;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
//        UIManager.Instance.OpenUI(EnumUIType.TestOne);
		float time = System.Environment.TickCount;
		for(int i=1;i<2000;i++)
		{
			GameObject go =null;
			// 1
//			go = Instantiate(Resources.Load<GameObject>("Prefabs/Cube"))as GameObject;
			// 2
//			go = ResManager.Instance.LoadInstance("Prefabs/Cube") as GameObject;
			// 3
//			ResManager.Instance.LoadAsyncInstance("Prefabs/Cube",(_obj)=>{
//				go = _obj as GameObject;
//				go.transform.position = UnityEngine.Random.insideUnitSphere * 20;
//			}) ;
			// 4
//			ResManager.Instance.LoadCoroutineInstance("Prefabs/Cube",(_obj)=>{
//				go = _obj as GameObject;
//				go.transform.position = UnityEngine.Random.insideUnitSphere * 20;
//			}) ;
			//go.transform.position = UnityEngine.Random.insideUnitSphere * 20;
		}
		Debug.Log ("Times:" + (System.Environment.TickCount - time) * 1000);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
