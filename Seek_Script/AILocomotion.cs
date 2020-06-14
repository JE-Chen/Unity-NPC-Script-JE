using UnityEngine;
using System.Collections;

public class AILocomotion : Vehicle 
{
	//AI角色的角色控制器
	private CharacterController controller;
	//AI角色的Rigidbody
	private Rigidbody theRigidbody;
	//AI角色的每次移動距離
	private Vector3 moveDistance;
	public bool displayTrack;

	// Use this for initialization
	void Start () 
	{
		//獲得角色控制器
		controller = GetComponent<CharacterController>();
		//獲得Rigidbody
		theRigidbody = GetComponent<Rigidbody>();
		moveDistance = new Vector3(0,0,0);
		//呼叫基礎類別的Start() 進行初始化
		base.Start();
	}
	
	//相關操作在FixedUpdate()中更新
	void FixedUpdate()
	{
		//計算速度
		velocity += acceleration * Time.fixedDeltaTime; 
		
		//限制速度要低於最大速度
		if (velocity.sqrMagnitude > sqrMaxSpeed)
			velocity = velocity.normalized * maxSpeed;
		
		//計算移動距離
		moveDistance = velocity * Time.fixedDeltaTime;
		
		//如果要求AI角色在平面移動 那麼y歸零
		if (isPlanar)
		{
			velocity.y = 0;
			moveDistance.y = 0;
		}

		if (displayTrack)
			//Debug.DrawLine(transform.position, transform.position + moveDistance, Color.red,30.0f);
			Debug.DrawLine(transform.position, transform.position + moveDistance, Color.black, 30.0f);
		
		//如果有角色控制器 可利用角色控制器讓其移動
		if (controller != null)
		{
			//if (displayTrack)
				//Debug.DrawLine(transform.position, transform.position + moveDistance, Color.blue,20.0f);
			controller.SimpleMove(velocity);

		}
		//如果沒有角色控制器也沒有Rigidbody
		//或是有Rigidbody 要用動力學控制他移動
		else if (theRigidbody == null || theRigidbody.isKinematic)
		{
			transform.position += moveDistance;
		}
		//用Rigidbod用控制角色移動
		else
		{
			theRigidbody.MovePosition(theRigidbody.position + moveDistance);		
		}
		
		//updata facing direction
		//更新朝向 如果速度大於某值 (防抖動)
		if (velocity.sqrMagnitude > 0.00001)
		{
			//目前朝向與速度的內插 算出新朝向
			Vector3 newForward = Vector3.Slerp(transform.forward, velocity, damping * Time.deltaTime);
			if (isPlanar)
				newForward.y = 0;
			//將當前方向設為新朝向
			transform.forward = newForward;
		}
		//撥放行走動畫
		//gameObject.animation.Play("walk");
	}
}
