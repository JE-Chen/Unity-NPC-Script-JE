using UnityEngine;
using System.Collections;

public class SteeringForCohesion : Steering {

	private Vector3 desiredVelocity;
	private Vehicle m_vehicle;
	private float maxSpeed;


	void Start () {
		m_vehicle = GetComponent<Vehicle>();
		maxSpeed = m_vehicle.maxSpeed;
	}


	public override Vector3 Force()
	{
		//操控向量
		Vector3 steeringForce = new Vector3(0,0,0);
		//AI角色的所有鄰居的平均位置
		Vector3 centerOfMass = new Vector3(0,0,0);
		//鄰居數量
		int neighborCount = 0;
		//檢查所有鄰居
		foreach (GameObject s in GetComponent<Radar>().neighbors)
		{
			if ((s!=null)&&(s != this.gameObject))
			{
				//壘加位置
				centerOfMass += s.transform.position;
				//鄰居數量+1
				neighborCount++;
			}
		}
		
		if (neighborCount > 0)
		{
			//將位置除以鄰居數量 得到平均值
			centerOfMass /= (float)neighborCount;
			//預期速度為平均值與目前位置的差
			desiredVelocity = (centerOfMass - transform.position).normalized * maxSpeed;
			//預期速度減目前速度得出操控向量
			steeringForce = desiredVelocity - m_vehicle.velocity;

		}
		
		return steeringForce;
	}
}
