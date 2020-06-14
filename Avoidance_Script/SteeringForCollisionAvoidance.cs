using UnityEngine;
using System.Collections;

public class SteeringForCollisionAvoidance : Steering 
{
	public bool isPlanar;	
	private Vector3 desiredVelocity;
	private Vehicle m_vehicle;
	private float maxSpeed;
	private float maxForce;
	//避開障礙所產生的操控力
	public float avoidanceForce;
	//向前看的最大距離
	public float MAX_SEE_AHEAD = 2.0f;
	//場景中所有碰撞體組成的陣列
	private GameObject[] allColliders;

	void Start () 
	{
		m_vehicle = GetComponent<Vehicle>();
		maxSpeed = m_vehicle.maxSpeed;
		maxForce = m_vehicle.maxForce;
		isPlanar = m_vehicle.isPlanar;
		//avoidanceForce = 20.0f;
		//如果為了避開障礙的操控力大於最大操控力 改為最大操控力
		if (avoidanceForce > maxForce)
			avoidanceForce = maxForce;

		//MAX_SEE_AHEAD = 20.0f;
		//儲存場景中所有碰撞體 帶有(obstacle)Tag的遊戲物件
		allColliders = GameObject.FindGameObjectsWithTag("obstacle");
	}

	public override Vector3 Force()
	{
		RaycastHit hit;
		Vector3 force = new Vector3(0,0,0);
		//Debug.DrawLine(transform.position, transform.position + transform.forward * 10);

		Vector3 velocity = m_vehicle.velocity;
		Vector3 normalizedVelocity = velocity.normalized;

		//Debug.DrawLine(transform.position, transform.position + normalizedVelocity * MAX_SEE_AHEAD * (velocity.magnitude / maxSpeed));

		//if (Physics.Raycast(transform.position, normalizedVelocity, out hit, MAX_SEE_AHEAD))
		//畫出一條射線 
		if (Physics.Raycast(transform.position, normalizedVelocity, out hit, MAX_SEE_AHEAD * velocity.magnitude / maxSpeed))
		{
			//Vector3 ahead = transform.position + normalizedVelocity * MAX_SEE_AHEAD;
			//如果射線與碰撞體相交 表示可能產生碰撞
			Vector3 ahead = transform.position + normalizedVelocity * MAX_SEE_AHEAD * (velocity.magnitude / maxSpeed);
			force = ahead - hit.collider.transform.position;
			//計算避免碰撞的操控力
			force *= avoidanceForce; 
			
			if (isPlanar)
				force.y = 0;

			//Debug.DrawLine(transform.position, transform.position + force);	
			//change color use when there is only one AI in the scene, or multiple actions will conflict
			//將這個碰撞體改為黑色 其他都為白色
			foreach (GameObject c in allColliders)
			{
				if (hit.collider.gameObject == c)
				{
					c.renderer.material.color = Color.black;//Color.green;
				}
				else
					c.renderer.material.color = Color.white;//Color.gray;
			}
		}
		else
		{
			//如果向前看沒有產生碰撞 所有碰撞體為白色
			foreach (GameObject c in allColliders)
			{
				c.renderer.material.color = Color.white;//Color.gray;
			}
		}

		return force;
	}
}
