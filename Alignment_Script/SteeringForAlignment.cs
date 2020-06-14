using UnityEngine;
using System.Collections;

public class SteeringForAlignment : Steering {

	void Start () {
	
	}	

	public override Vector3 Force()
	{
		//目前的平均朝向
		Vector3 averageDirection = new Vector3(0,0,0);
		//鄰居數目
		int neighborCount = 0;

		//檢查目前AI角色的所有鄰居
		foreach (GameObject s in GetComponent<Radar>().neighbors)
		{
			//如果非目前AI角色
			if ((s!=null)&&(s != this.gameObject))
			{
				//將朝向向量加到averageDirection中
				averageDirection += s.transform.forward;
				//鄰居數目+1
				neighborCount++;
			}
		}
		//鄰居數量大於0
		if (neighborCount > 0)
		{
			//壘加獲得的朝向除與鄰居個數 求出平均朝向
			averageDirection /= (float)neighborCount;
			//平均朝向減去目前朝向 得出操控力
			averageDirection -= transform.forward;
		}

		//print(gameObject.name + averageDirection);
		return averageDirection;
	}
}
