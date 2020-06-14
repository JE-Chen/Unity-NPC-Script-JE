using UnityEngine;
using System.Collections;

public class SteeringForEvade : Steering {

	public GameObject target;
	private Vector3 desiredVelocity;
	private Vehicle m_vehicle;
	private float maxSpeed;


	void Start () {
		m_vehicle = GetComponent<Vehicle>();
		maxSpeed = m_vehicle.maxSpeed;
	}
	

	public override Vector3 Force()
	{
		Vector3 toTarget = target.transform.position - transform.position;
		//向前預測的時間
		float lookaheadTime = toTarget.magnitude / (maxSpeed + target.GetComponent<Vehicle>().velocity.magnitude);
		//計算預期速度
		desiredVelocity = (transform.position - (target.transform.position + target.GetComponent<Vehicle>().velocity * lookaheadTime)).normalized * maxSpeed;
		//傳回操控向量
		return (desiredVelocity - GetComponent<Vehicle>().velocity);
	}
}
