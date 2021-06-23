using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class PlatformManager : MonoBehaviour
{
    LogInInfo _loginInfo;

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
        }
        else
        {
            // 최초 로그인
            //_loginInfo = new LogInInfo();
            //_loginInfo._myUUID = 10000;
            //SaveData();

            StudentMainUI._instance._InitSettingObj.SetActive(true);
        }
    }

    void SaveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream stream = new MemoryStream();

        formatter.Serialize(stream, _loginInfo);

        string data = Convert.ToBase64String(stream.GetBuffer());

        PlayerPrefs.SetString("MyUUID", data);
        stream.Close();
    }

    [Serializable]
    internal class LogInInfo
    {
        public long _myUUID;
    }
}
