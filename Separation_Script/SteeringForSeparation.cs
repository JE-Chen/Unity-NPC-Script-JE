using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Radar))]
public class SteeringForSeparation : Steering {
	//可接受的距離
	public float comfortDistance = 1;
	//距離過近的懲罰因數
	public float multiplierInsideComfortDistance = 2;

	void Start () {
	
	}

	public override Vector3 Force()
	{
		Vector3 steeringForce = new Vector3(0,0,0);
		//檢查每個鄰居
		foreach (GameObject s in GetComponent<Radar>().neighbors)
		{	
			//如果不是當前AI角色
			if ((s!=null)&&(s != this.gameObject))
			{
				//計算距離
				Vector3 toNeighbor = transform.position - s.transform.position;
				//計算操控力 排斥力 大小與距離成反比
				float length = toNeighbor.magnitude;
				steeringForce += toNeighbor.normalized / length;
				//距離大於可接受距離十 排斥力要乘以額外因數
				if (length < comfortDistance)
					steeringForce *= multiplierInsideComfortDistance;
			}
		}

		return steeringForce;
	}
}
