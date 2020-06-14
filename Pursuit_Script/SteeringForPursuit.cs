using UnityEngine;
using System.Collections;

public class SteeringForPursuit : Steering {

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
		//計算追逐者的正向與逃避者正向之間的夾角
		float relativeDirection = Vector3.Dot(transform.forward, target.transform.forward);
		//如果夾角大於0 且追逐者面對著逃避者 那麼像逃避者位置移動
		if ((Vector3.Dot(toTarget, transform.forward) > 0) && (relativeDirection < -0.95f))
		{
			//計算預期速度
			desiredVelocity = (target.transform.position - transform.position).normalized * maxSpeed;
			//傳回操控向量
			return (desiredVelocity - m_vehicle.velocity);
		}
		//計算預期時間 正比於追逐者與逃避者距離 反比於追逐者與逃避者速度和
		float lookaheadTime = toTarget.magnitude / (maxSpeed + target.GetComponent<Vehicle>().velocity.magnitude);

		desiredVelocity = (target.transform.position + target.GetComponent<Vehicle>().velocity * lookaheadTime - transform.position).normalized * maxSpeed;
		return (desiredVelocity - m_vehicle.velocity);

	}
}
