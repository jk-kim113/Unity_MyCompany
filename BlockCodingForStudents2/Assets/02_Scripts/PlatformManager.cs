using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class PlatformManager : MonoBehaviour
{
    static PlatformManager _uniqueInstance;
    public static PlatformManager _instance { get { return _uniqueInstance; } }

    LogInInfo _loginInfo;

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void Start()
    {
        //PlayerPrefs.DeleteAll();

        CheckData();
    }

    void CheckData()
    {
        string data = PlayerPrefs.GetString("MyUUID");

        if(!string.IsNullOrEmpty(data))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(data));

            _loginInfo = (LogInInfo)formatter.Deserialize(stream);
            stream.Close();

            StudentClient._instance.SendUUIDInfo(_loginInfo._myUUID);
        }
        else
        {   
            StudentMainUI._instance._InitSettingObj.SetActive(true);
        }
    }

    public void SaveData(int uuid)
    {
        _loginInfo = new LogInInfo();
        _loginInfo._myUUID = uuid;

        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream stream = new MemoryStream();

        formatter.Serialize(stream, _loginInfo);

        string data = Convert.ToBase64String(stream.GetBuffer());

        PlayerPrefs.SetString("MyUUID", data);
        stream.Close();

        StudentMainUI._instance.ShowGameList();
    }

    [Serializable]
    internal class LogInInfo
    {
        public int _myUUID;
    }
}
