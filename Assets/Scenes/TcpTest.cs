using UnityEngine;
using System.Collections;
 
public class TcpTest : MonoBehaviour{
    public float speed = 3.0f;
    string editString = "hello wolrd"; //编辑框文字
    GameObject cube;
    RayDemo04 fakeURG;
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
                    cube.transform.Rotate(0, -100 * speed * Time.deltaTime, 0, Space.Self);
                    tcpClient.recvStr = "";
                    break;
                case "rightrotate":
                    cube.transform.Rotate(0, 100 * speed * Time.deltaTime, 0, Space.Self);
                    tcpClient.recvStr = "";
                    break;
                case "forward":
                    cube.transform.Translate(0, 0,speed*Time.deltaTime);
                    tcpClient.recvStr = "";
                    break;
                case "backward":
                    //cube.transform.Translate(0, 0, -speed * Time.deltaTime);
                    fakeURG.DrawFieldOfView();
                    Debug.Log(fakeURG.feedbackstr);
                    //tcpClient.recvStr = "";
                    fakeURG.feedbackstr += string.Format("|{0:G},",(int)(cube.transform.position.x));
                    fakeURG.feedbackstr += string.Format("{0:G}!",(int)(cube.transform.position.y));
                    tcpClient.SocketSend(fakeURG.feedbackstr);
                    Debug.Log(fakeURG.feedbackstr);
                    fakeURG.feedbackstr = "";
                    break;
            }
        }

        //获取需要返回的数据

    }

    void OnApplicationQuit(){
        //退出时关闭连接
        tcpClient.SocketQuit();
    }
}