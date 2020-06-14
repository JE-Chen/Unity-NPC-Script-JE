using UnityEngine;
using System.Collections;

public class SteeringFollowPath : Steering 
{
	//節點陣列
	public GameObject[] waypoints = new GameObject[4];
	//目標點
	private Transform target;
	//目前路點
	private int currentNode;
	//與路點的距離小於這值時當成已經到達
	private float arriveDistance;
	private float sqrArriveDistance;
	//路點的數量
	private int numberOfNodes;
	//操控力
	private Vector3 force;
	//預期速度
	private Vector3 desiredVelocity;
	private Vehicle m_vehicle;
	private float maxSpeed;
	private bool isPlanar;
	//當與目標小於此距離時減速
	public float slowDownDistance;

	void Start () 
	{
		//儲存陣列中的路點個數
		numberOfNodes = waypoints.Length;

		m_vehicle = GetComponent<Vehicle>();
		maxSpeed = m_vehicle.maxSpeed;
		isPlanar = m_vehicle.isPlanar;
		//設定目前為第1個路點
		currentNode = 0;
		//設定目前路點為目標點
		target = waypoints[currentNode].transform;
		arriveDistance = 1.0f;
		sqrArriveDistance = arriveDistance * arriveDistance;
	}

	public override Vector3 Force()
	{
		force = new Vector3(0,0,0);
		Vector3 dist = target.position - transform.position;
		if (isPlanar)
			dist.y = 0;		
		//如果已經是最後一個路點
		if (currentNode == numberOfNodes - 1)
		{
			//如果目前路點的距離大於減速距離
			if (dist.magnitude > slowDownDistance)
			{
				//求出預期速度
				desiredVelocity = dist.normalized * maxSpeed;
				force = desiredVelocity - m_vehicle.velocity;
			}
			else
			{
				//計算操控向量
				desiredVelocity = dist - m_vehicle.velocity;
				force = desiredVelocity - m_vehicle.velocity;
			}
		}
		else
		{
			//如果目前路點的距離平方小於到達距離的平方
			//開始接近下一路點 將下一路點設為目標
			if (dist.sqrMagnitude < sqrArriveDistance)
			{
				currentNode ++;
				target = waypoints[currentNode].transform;				
			}
			//計算預期速度和操控向量
			desiredVelocity = dist.normalized * maxSpeed;
			force = desiredVelocity - m_vehicle.velocity;		

		}
		
		return force;
		

	}

}
