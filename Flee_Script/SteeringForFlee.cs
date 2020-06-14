using UnityEngine;
using System.Collections;

public class SteeringForFlee : Steering {

	//目標
	public GameObject target;
	//恐懼範圍
	public float fearDistance = 20;

	//預期速度
	private Vector3 desiredVelocity;
	//查詢資訊用
	private Vehicle m_vehicle;
	//最大速度
	private float maxSpeed;


	void Start () {
		//取得AI角色的資訊用
		m_vehicle = GetComponent<Vehicle>();
		//取得最大速度
		maxSpeed = m_vehicle.maxSpeed;
	}


	public override Vector3 Force()
	{
		//if the target is in the fear range, then flee, else ignore it by return 0;
		//取得位置向量
		Vector3 tmpPos = new Vector3(transform.position.x, 0, transform.position.z);
		Vector3 tmpTargetPos = new Vector3(target.transform.position.x, 0, target.transform.position.z);

		//如果在恐懼範圍外 不移動
		if (Vector3.Distance(tmpPos, tmpTargetPos) > fearDistance)
			desiredVelocity = Vector3.zero;
		//如果在恐懼範圍內 往相反方向移動
		desiredVelocity = (transform.position - target.transform.position).normalized * maxSpeed;
		return (desiredVelocity - m_vehicle.velocity);
	}
}
