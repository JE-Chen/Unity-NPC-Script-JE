using UnityEngine;
using System.Collections;

public class ColliderColorChange : MonoBehaviour 
{
	void Start () {
	
	}	

	void Update () {
	
	}

	//如果與其他碰撞體相撞改成紅色
	void OnTriggerEnter(Collider other)
	{
		print("collide0!");
		if (other.gameObject.GetComponent<Vehicle>()!= null)
		{
			print("collide!");
			this.renderer.material.color = Color.red;
		}
	}


	void OnTriggerExit(Collider other)
	{
			//碰撞體改成白色
		this.renderer.material.color = Color.white;
	}
}
