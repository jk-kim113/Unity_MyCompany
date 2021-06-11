using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class TeacherClient : MonoBehaviour
{
    static TeacherClient _uniqueInstance;
    public static TeacherClient _instance { get { return _uniqueInstance; } }

    const string _ip = "127.0.0.1";
    const int _port = 80;

    Socket _server;

    bool _isConnect = false;

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void Start()
    {
        //ConnectServer();
    }

    public void ConnectServer()
    {
        _isConnect = Connect(_ip, _port);
    }

    bool Connect(string ipAddress, int port)
    {
        try
        {
            if (!_isConnect)
            {
                // make socket
                _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _server.Connect(ipAddress, port);

                return true;
            }
        }
        catch (System.Exception ex)
        {
            // 메세지 창에 띄운다.
            Debug.Log(ex.Message);
        }

        return false;
    }
}
