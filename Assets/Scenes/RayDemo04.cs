using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System;
using UnityEngine;

public class RayDemo04 : MonoBehaviour {
	public float viewRadius = 8.0f;      // 代表视野最远的距离
	public float viewAngleStep = 768;     // 射线数量，越大就越密集，效果更好但硬件耗费越大。
    public string feedbackstr;          //每一条射线返回的距离先取整，把float变为int，单位mm，0,0,0,0,0,0,0,0,0,123,42134,12321,231,3213,123,0,0|323,123!
    GameObject cube;
    // Use this for initialization
    void Start () {
        cube = GameObject.Find("Cube");
    }

	// Update is called once per frame

	void Update () {

	}

	public void DrawFieldOfView(){

		// 获得最左边那条射线的向量，相对正前方，角度是-45

		Vector3 forward_left = Quaternion.Euler(0, -70, 0) * cube.transform.forward * viewRadius;

		// 依次处理每一条射线

		for (int i = 0; i <= viewAngleStep; i++){

			// 每条射线都在forward_left的基础上偏转一点，最后一个正好偏转90度到视线最右侧

			Vector3 v = Quaternion.Euler(0, (145.0f / viewAngleStep) * i, 0) * forward_left;

			// Player位置加v，就是射线终点pos

			Vector3 pos = cube.transform.position + v;

			Ray ray = new Ray(cube.transform.position, pos);  
			RaycastHit hit;
            int dist;
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {  
				// 如果射线与平面碰撞，打印碰撞物体信息  
				Debug.Log ("碰撞对象: " + hit.collider.name);
                dist = (int)(hit.distance);
			} else {
                dist = 0;
			}
            feedbackstr += Convert.ToString(dist) + ",";
		}
	}
}