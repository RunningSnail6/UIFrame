using UnityEngine;
using System.Collections;
using UIFrameWork;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UIManager.Instance.OpenUI(EnumUIType.TestOne);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
