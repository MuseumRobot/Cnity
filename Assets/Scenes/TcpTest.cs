﻿using UnityEngine;
using System.Collections;
 
public class TcpTest : MonoBehaviour{
    string editString = "hello wolrd"; //编辑框文字
    GameObject cube;

    TcpClientHandler tcpClient;
    // Use this for initialization
    void Start(){
        //初始化网络连接
        //tcpClient=new TcpClientHandler(); //因为tcp的类继承了monobehaviour所以不能用new，或者去掉对monobehaviour继承就可以用new
        tcpClient = gameObject.AddComponent<TcpClientHandler>();
        tcpClient.InitSocket();

        //找到cube
        cube = GameObject.Find("Cube");
    }

    void OnGUI(){
        editString = GUI.TextField(new Rect(10, 10, 100, 20), editString);
        GUI.Label(new Rect(10, 30, 300, 20), tcpClient.GetRecvStr());
        if (GUI.Button(new Rect(10, 50, 60, 20), "send"))
            tcpClient.SocketSend(editString);
    }

    // Update is called once per frame
    void Update(){
        if (tcpClient.GetRecvStr() != null){
            switch (tcpClient.GetRecvStr()){
                case "leftrotate":
                    cube.transform.Rotate(Vector3.up, 50 * Time.deltaTime);
                    break;
                case "rightrotate":
                    cube.transform.Rotate(Vector3.down, 50 * Time.deltaTime);
                    break;
            }
        }
    }

    void OnApplicationQuit(){
        //退出时关闭连接
        tcpClient.SocketQuit();
    }
}