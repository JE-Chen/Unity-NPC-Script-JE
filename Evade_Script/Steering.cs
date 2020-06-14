using UnityEngine;
using System.Collections;

public abstract class Steering : MonoBehaviour {
	//表示操控力權數
	public float weight = 1;

	void Start () {
	
	}

	void Update () {
	
	}
	//計算操控力的方法 由衍生類別實現
	public virtual Vector3 Force()
	{
		return new Vector3(0,0,0);
	}
}
