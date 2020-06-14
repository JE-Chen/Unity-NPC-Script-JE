using UnityEngine;
using System.Collections;

//pay attention that this function is quite related with frame rate

public class SteeringForWander : Steering {

	//排迴半徑
	public float wanderRadius;
	//排迴距離
	public float wanderDistance;
	//隨機位移最大值
	public float wanderJitter;
	//是否是平面
	public bool isPlanar;
	//public GameObject targetIndicator;

	private Vector3 desiredVelocity;
	private Vehicle m_vehicle;
	private float maxSpeed;
	private Vector3 circleTarget;
	private Vector3 wanderTarget;


	void Start () {
		m_vehicle = GetComponent<Vehicle>();
		maxSpeed = m_vehicle.maxSpeed;
		isPlanar = m_vehicle.isPlanar;
		//選取圓圈上的點作為初始點
		circleTarget = new Vector3(wanderRadius*0.707f, 0, wanderRadius * 0.707f);
	}
	

	public override Vector3 Force()
	{
		//計算隨機位移
		Vector3 randomDisplacement = new Vector3((Random.value-0.5f)*2*wanderJitter, (Random.value-0.5f)*2*wanderJitter,(Random.value-0.5f)*2*wanderJitter);

		if (isPlanar)
			randomDisplacement.y = 0;
		//將隨機位移加到初始點上 獲得新位置
		circleTarget += randomDisplacement;
		// 新位置可能不在圓周上 投射至圓周上
		circleTarget = wanderRadius * circleTarget.normalized;
		//將之前算出的值轉為世界座標
		wanderTarget = m_vehicle.velocity.normalized * wanderDistance + circleTarget + transform.position;

		//計算預期速度並傳回操控向量
		desiredVelocity = (wanderTarget - transform.position).normalized * maxSpeed;
		return (desiredVelocity - m_vehicle.velocity);
	}
}
