using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Radar : MonoBehaviour {

	//碰撞體陣列
	private Collider[] colliders;
	//計時器
	private float timer = 0;
	//鄰居列表
	public List<GameObject> neighbors;
	//檢查間隔
	public float checkInterval = 0.3f;
	//鄰居檢測半徑
	public float detectRadius = 10f;
	//檢測哪層的物件
	public LayerMask layersChecked ;


	void Start () {
		//初始化
		neighbors = new List<GameObject>();
	}
	

	void Update () {
		timer += Time.deltaTime;

		//ticked
		//如果上次檢查的時間大於時間間隔 再次檢查
		if (timer > checkInterval)
		{
			//清楚鄰居列表
			neighbors.Clear();
			//尋找檢測半徑的所有碰撞體
			colliders = Physics.OverlapSphere(transform.position, detectRadius);//, layersChecked);
			for (int i=0; i < colliders.Length; i++)
			{
				//取得檢測到的每個碰撞體的Vehicle元件 加入鄰居列表中
				if (colliders[i].GetComponent<Vehicle>())
					neighbors.Add(colliders[i].gameObject);
			}
			//計時歸0
			timer = 0;
		}
	}
}
