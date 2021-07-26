using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;

public class TeacherClient : MonoBehaviour
{
    static TeacherClient _uniqueInstance;
    public static TeacherClient _instance { get { return _uniqueInstance; } }

    [SerializeField]
    JoinStudent _joinStudent;

    const string _ip = "192.168.0.52";
    //const string _ip = "203.248.252.2";
    const int _port = 4578;

    Socket _server;

    bool _isConnect = false;

    Queue<DefinedStructure.PacketInfo> _toClientQueue = new Queue<DefinedStructure.PacketInfo>();
    Queue<byte[]> _fromClientQueue = new Queue<byte[]>();

    Dictionary<int, List<int>> _classInfoDic = new Dictionary<int, List<int>>();

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void Start()
    {
        ConnectServer();
        SendManagerInfo();
    }

    public void ConnectServer()
    {
        _isConnect = Connect(_ip, _port);

        StartCoroutine(AddOrder());
        StartCoroutine(DoOrder());
        StartCoroutine(SendOrder());
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
                Debug.Log("Connect");

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

    IEnumerator AddOrder()
    {
        while (true)
        {
            if (_isConnect && _server != null && _server.Poll(0, SelectMode.SelectRead))
            {
                byte[] buffer = new byte[1032];
                int recvLen = _server.Receive(buffer);
                if (recvLen > 0)
                {
                    try
                    {
                        DefinedStructure.PacketInfo pToClient = new DefinedStructure.PacketInfo();
                        pToClient = (DefinedStructure.PacketInfo)ConvertPacket.ByteArrayToStructure(buffer, pToClient.GetType(), recvLen);

                        _toClientQueue.Enqueue(pToClient);
                    }
                    catch (NullReferenceException ex)
                    {
                        Debug.LogWarning(ex.Message);
                        Debug.LogWarning(ex.StackTrace);
                    }
                }
            }

            yield return null;
        }
    }

    IEnumerator DoOrder()
    {
        while (true)
        {
            if (_toClientQueue.Count != 0)
            {
                DefinedStructure.PacketInfo pToClient = _toClientQueue.Dequeue();

                switch ((DefinedProtocol.eToClient)pToClient._id)
                {
                    case DefinedProtocol.eToClient.ClassInfo:

                        DefinedStructure.P_ClassInfo pClassInfo = new DefinedStructure.P_ClassInfo();
                        pClassInfo = (DefinedStructure.P_ClassInfo)ConvertPacket.ByteArrayToStructure(pToClient._data, pClassInfo.GetType(), pToClient._totalSize);
                        
                        if(_classInfoDic.ContainsKey(pClassInfo._grade))
                            _classInfoDic[pClassInfo._grade].Add(pClassInfo._group);
                        else
                        {
                            List<int> groupList = new List<int>();
                            groupList.Add(pClassInfo._group);
                            _classInfoDic.Add(pClassInfo._grade, groupList);
                        }

                        break;

                    case DefinedProtocol.eToClient.StudentInfo:

                        DefinedStructure.P_StudentInfo pStudentInfo = new DefinedStructure.P_StudentInfo();
                        pStudentInfo = (DefinedStructure.P_StudentInfo)ConvertPacket.ByteArrayToStructure(pToClient._data, pStudentInfo.GetType(), pToClient._totalSize);

                        _joinStudent.AddStudent(1, "일반 학생", "리얼고등학교", pStudentInfo._grade, pStudentInfo._group, pStudentInfo._number, "홍길동");

                        break;

                    case DefinedProtocol.eToClient.FinishSend:

                        DefinedStructure.P_FinishSend pFinishSend = new DefinedStructure.P_FinishSend();
                        pFinishSend = (DefinedStructure.P_FinishSend)ConvertPacket.ByteArrayToStructure(pToClient._data, pFinishSend.GetType(), pToClient._totalSize);
                        
                        switch((DefinedProtocol.eToClient)pFinishSend._kind)
                        {
                            case DefinedProtocol.eToClient.ClassInfo:
                                while(PersonalInfo._instance == null)
                                {
                                    yield return null;
                                }
                                PersonalInfo._instance.InitClasGroup("Reality_Reality", _classInfoDic);
                                break;
                        }

                        break;
                }
            }

            yield return null;
        }
    }

    IEnumerator SendOrder()
    {
        while (true)
        {
            if (_fromClientQueue.Count != 0)
                _server.Send(_fromClientQueue.Dequeue());

            yield return null;
        }
    }

    public void SendDownloadInfo(int gameIndex, int level)
    {
        DefinedStructure.P_DownGameInfo pDownloadGameInfo;
        pDownloadGameInfo._gameIndex = gameIndex;
        pDownloadGameInfo._level = level;

        Debug.Log(gameIndex);
        Debug.Log(level);

        ToPacket(DefinedProtocol.eFromClient.DownloadInfo, pDownloadGameInfo);
    }

    void SendManagerInfo()
    {
        DefinedStructure.P_ManagerInfo pManagerInfo;
        pManagerInfo._managerID = 1;

        ToPacket(DefinedProtocol.eFromClient.ManagerInfo, pManagerInfo);
    }

    void ToPacket(DefinedProtocol.eFromClient fromClientID, object str)
    {
        DefinedStructure.PacketInfo packetRecieve;
        packetRecieve._id = (int)fromClientID;
        packetRecieve._data = new byte[1024];
        byte[] temp = ConvertPacket.StructureToByteArray(str);
        for (int n = 0; n < temp.Length; n++)
            packetRecieve._data[n] = temp[n];
        packetRecieve._totalSize = temp.Length;

        _fromClientQueue.Enqueue(ConvertPacket.StructureToByteArray(packetRecieve));
    }
}
