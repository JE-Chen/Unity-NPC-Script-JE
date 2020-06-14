using UnityEngine;
using System.Collections;

public class SteeringForSeek : Steering {
	
	//要尋找的目標物體
	public GameObject target;
	//預期速度
	private Vector3 desiredVelocity;
	//獲得被操控的AI角色，取得各種資訊
	private Vehicle m_vehicle;
	//最大速度
	private float maxSpeed;
	//是否僅在2D平面運動
	private bool isPlanar;
	
	void Start () {
		//獲得被操控的AI角色，取得各種資訊
		m_vehicle = GetComponent<Vehicle>();
		maxSpeed = m_vehicle.maxSpeed;
		isPlanar = m_vehicle.isPlanar;
	}
	
	//計算操控向量
	public override Vector3 Force()
	{
		//計算預期速度
		desiredVelocity = (target.transform.position - transform.position).normalized * maxSpeed;
		if (isPlanar)
			desiredVelocity.y = 0;
		//傳回操控向量
		return (desiredVelocity - m_vehicle.velocity);
	}
}

