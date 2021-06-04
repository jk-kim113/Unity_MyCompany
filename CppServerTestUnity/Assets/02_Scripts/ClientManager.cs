using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using System.IO;

public class ClientManager : MonoBehaviour
{
    static ClientManager _uniqueInstance;
    public static ClientManager _instance { get { return _uniqueInstance; } }

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

        //StartCoroutine(AddOrder());
        //StartCoroutine(DoOrder());
        //StartCoroutine(SendOrder());

        var fileStream = new FileStream(Path.Combine(Application.dataPath, "1_triceratops"), FileMode.Open, FileAccess.Read);

        if (fileStream == null)
            Debug.Log("Failed to Access File Stream");

        AssetBundle assetbundle = AssetBundle.LoadFromFile(Path.Combine(Application.dataPath + "/", "1_triceratops"));

        GameObject prefab = assetbundle.LoadAsset<GameObject>("1_triceratops");
        Instantiate(prefab);

        //var bundleLoadRequest = AssetBundle.LoadFromStreamAsync(fileStream);

        //var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
        //if (myLoadedAssetBundle == null)
        //{
        //    Debug.Log("Failed to load AssetBundle!");
        //}

        //var assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("1_triceratops");

        //GameObject prefab = assetLoadRequest.asset as GameObject;
        //Instantiate(prefab);

        //myLoadedAssetBundle.Unload(false);
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

                Debug.Log("연결 성공");

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
                try
                {
                    Debug.Log("사진 받아오는 중");
                    int total = 0;
                    int size = 0;
                    int left_data = 0;
                    int recv_data = 0;

                    //byte[] data_size = new byte[4];
                    //recv_data = _server.Receive(data_size, 0, 4, SocketFlags.None);
                    //size = BitConverter.ToInt32(data_size, 0);

                    left_data = 2168368;
                    byte[] data = new byte[2168368];

                    while (total < size)
                    {
                        recv_data = _server.Receive(data, total, left_data, 0);
                        if (recv_data == 0)
                            break;
                        total += recv_data;
                        left_data -= recv_data;
                    }

                    File.WriteAllBytes(Application.dataPath + "\\TestPrefab", data);
                    Debug.Log("Finish Download");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


                //byte[] buffer = new byte[1024];
                //int recvLen = _server.Receive(buffer);
                //Debug.Log(recvLen);
                //if (recvLen > 0)
                //{
                //    try
                //    {
                //        DefinedStructure.PacketInfo pToClient = new DefinedStructure.PacketInfo();
                //        pToClient = (DefinedStructure.PacketInfo)ConvertPacket.ByteArrayToStructure(buffer, pToClient.GetType(), recvLen);

                //        Debug.Log("Receive Packet");
                //        _toClientQueue.Enqueue(pToClient);
                //    }
                //    catch (NullReferenceException ex)
                //    {
                //        Debug.LogWarning(ex.Message);
                //        Debug.LogWarning(ex.StackTrace);
                //    }
                //}
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
                Debug.Log("Receive Data");
                DefinedStructure.PacketInfo pToClient = _toClientQueue.Dequeue();

                DefinedStructure.Packet_Chat pChat = new DefinedStructure.Packet_Chat();
                pChat = (DefinedStructure.Packet_Chat)ConvertPacket.ByteArrayToStructure(pToClient._data, pChat.GetType(), pToClient._totalSize);

                TestManager._instance.ReceiveMsg(pChat._chat);

                //switch ((DefinedProtocol.eToClient)pToClient._id)
                //{
                    
                //}
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

    public void SendMsg(string msg)
    {
        DefinedStructure.Packet_Chat pChat;
        pChat._chat = msg;

        ToPacket(DefinedProtocol.eFromClient.SendMessage, pChat);
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