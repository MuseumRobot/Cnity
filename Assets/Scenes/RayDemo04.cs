using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayDemo04 : MonoBehaviour {
	public float viewRadius = 8.0f;      // 代表视野最远的距离
	public float viewAngleStep = 768;     // 射线数量，越大就越密集，效果更好但硬件耗费越大。
    public string feedbackstr;          //每一条射线返回的距离先取整，把float变为int，单位mm，0,0,0,0,0,0,0,0,0,123,42134,12321,231,3213,123,0,0|323,123!
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame

	void Update () {
		//调用
		DrawFieldOfView();

	}

	void DrawFieldOfView(){

		// 获得最左边那条射线的向量，相对正前方，角度是-45

		Vector3 forward_left = Quaternion.Euler(0, -70, 0) * transform.forward * viewRadius;

		// 依次处理每一条射线

		for (int i = 0; i <= viewAngleStep; i++){

			// 每条射线都在forward_left的基础上偏转一点，最后一个正好偏转90度到视线最右侧

			Vector3 v = Quaternion.Euler(0, (145.0f / viewAngleStep) * i, 0) * forward_left;

			// Player位置加v，就是射线终点pos

			Vector3 pos = transform.position + v;

			// 从玩家位置到pos画线段，只会在编辑器里看到

			Ray ray = new Ray(transform.position, pos);  
			RaycastHit hit;
            float dist = 0.0f;
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {  
				// 如果射线与平面碰撞，打印碰撞物体信息  
				Debug.Log ("碰撞对象: " + hit.collider.name);  
				// 在场景视图中绘制射线  
				Debug.DrawLine (ray.origin, hit.point, Color.green);
                //float dist = sqrt(hit.point - ray.origin);
                //dist = ...;
			} else {
				Debug.DrawLine(transform.position, pos, Color.red);
                dist = 0.0f;
			}
            //feedbackstr += sprintf("%d,",dist);

		}
        //feedbackstr += ("|%d,%d!",pos.x,pos.y);
	}
}