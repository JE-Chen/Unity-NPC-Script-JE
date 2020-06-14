using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

public class Vehicle : MonoBehaviour {

	//這個AI角色包含的操控力列表
	private Steering[] steerings;
	//AI角色的最大速度
	public float maxSpeed = 10;
	//可以施加到AI角色的力的最大值
	public float maxForce = 100;
	//最大速度的平方 先算出省內存
	protected float sqrMaxSpeed;
	//質量
	public float mass = 1;
	//AI角色的速度
	public Vector3 velocity;
	//轉向的速度
	public float damping = 0.9f;
	//操控力的計算間隔，每畫面更新會LAG 所以不需要每畫面更新
	public float computeInterval = 0.2f;
	//是否在二為平面上 如果是 計算2Gameobject時 忽略y值不同
	public bool isPlanar = true;
	//計算獲得的操控力
	private Vector3 steeringForce;
	//AI角色的加速度
	protected Vector3 acceleration;
	//private CharacterController controller;
	//private Rigidbody theRigidbody;
	//private Vector3 moveDistance;
	//計時器
	private float timer;


	protected void Start () 
	{
		steeringForce = new Vector3(0,0,0);
		sqrMaxSpeed = maxSpeed * maxSpeed;
		//moveDistance = new Vector3(0,0,0);
		timer = 0;

		//獲得AI角色的操控行為列表
		steerings = GetComponents<Steering>();

		//controller = GetComponent<CharacterController>();
		//theRigidbody = GetComponent<Rigidbody>();
	}


	void Update () 
	{
		timer += Time.deltaTime;
		steeringForce = new Vector3(0,0,0);  

		//ticked part, we will not compute force every frame
		//如果距離上次獲得操控力的時間大於computeInterval 重新獲得
		//再次計算操控力
		if (timer > computeInterval)
		{
			//操控行為的操控力進行帶權數的總和
			foreach (Steering s in steerings)
			{
				if (s.enabled)
					steeringForce += s.Force()*s.weight;
			}

			//使操控力不大於MaxForce
			steeringForce = Vector3.ClampMagnitude(steeringForce,maxForce);
			//力除以質量 求出加速度
			acceleration = steeringForce / mass;
			//重新從0計算
			timer = 0;
		}

	}

	/*
	void FixedUpdate()
	{
		velocity += acceleration * Time.fixedDeltaTime; 
		
		if (velocity.sqrMagnitude > sqrMaxSpeed)
			velocity = velocity.normalized * maxSpeed;
		
		moveDistance = velocity * Time.fixedDeltaTime;
		
		if (isPlanar)
			moveDistance.y = 0;
		
		if (controller != null)
			controller.SimpleMove(velocity);
		else if (theRigidbody == null || theRigidbody.isKinematic)
			transform.position += moveDistance;
		else
			theRigidbody.MovePosition(theRigidbody.position + moveDistance);		
		
		//updata facing direction
		if (velocity.sqrMagnitude > 0.00001)
		{
			Vector3 newForward = Vector3.Slerp(transform.forward, velocity, damping * Time.deltaTime);
			newForward.y = 0;
			transform.forward = newForward;
		}
	}*/
}
