using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;

public class StudentClient : MonoBehaviour
{
    static StudentClient _uniqueInstance;
    public static StudentClient _instance { get { return _uniqueInstance; } }

    const string _ip = "192.168.0.52";
    //const string _ip = "203.248.252.2";
    const int _port = 4578;

    Socket _server;

    bool _isConnect = false;

    Queue<DefinedStructure.PacketInfo> _toClientQueue = new Queue<DefinedStructure.PacketInfo>();
    Queue<byte[]> _fromClientQueue = new Queue<byte[]>();

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void Start()
    {
        //ConnectServer();

        //PlatformManager._instance.CheckData();
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
                    case DefinedProtocol.eToClient.MyUUID:

                        DefinedStructure.P_MyUUID pMyUUID = new DefinedStructure.P_MyUUID();
                        pMyUUID = (DefinedStructure.P_MyUUID)ConvertPacket.ByteArrayToStructure(pToClient._data, pMyUUID.GetType(), pToClient._totalSize);

                        PlatformManager._instance.SaveData(pMyUUID._myUUID);

                        break;

                    case DefinedProtocol.eToClient.DownGameInfo:

                        DefinedStructure.P_DownGameInfo pDownGameInfo = new DefinedStructure.P_DownGameInfo();
                        pDownGameInfo = (DefinedStructure.P_DownGameInfo)ConvertPacket.ByteArrayToStructure(pToClient._data, pDownGameInfo.GetType(), pToClient._totalSize);

                        StudentMainUI._instance.MoveSystemMessage(pDownGameInfo._gameIndex, pDownGameInfo._level);
                        StudentMainUI._instance.DownGame(pDownGameInfo._gameIndex, pDownGameInfo._level);

                        break;

                    case DefinedProtocol.eToClient.SuccessLogIn:

                        DefinedStructure.P_SuccessLogIn pSuccessLogIn = new DefinedStructure.P_SuccessLogIn();
                        pSuccessLogIn = (DefinedStructure.P_SuccessLogIn)ConvertPacket.ByteArrayToStructure(pToClient._data, pSuccessLogIn.GetType(), pToClient._totalSize);



                        StudentMainUI._instance.ShowGameList();

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

    public void SendClientInfo(int schoolID, int grade, int group, int number)
    {
        DefinedStructure.P_StudentInfo pStudentInfo;
        pStudentInfo._schoolID = schoolID + 100000;
        pStudentInfo._grade = grade;
        pStudentInfo._group = group;
        pStudentInfo._number = number;

        ToPacket(DefinedProtocol.eFromClient.StudentInfo, pStudentInfo);
    }

    public void SendUUIDInfo(int uuid)
    {
        DefinedStructure.P_MyUUID pMyUUID;
        pMyUUID._myUUID = uuid;
        
        ToPacket(DefinedProtocol.eFromClient.UUIDInfo, pMyUUID);
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
