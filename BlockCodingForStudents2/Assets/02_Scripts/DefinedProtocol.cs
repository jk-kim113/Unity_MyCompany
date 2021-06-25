using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefinedProtocol
{
    public enum eFromClient
    {
        StudentInfo = 100,
        UUIDInfo,

        max
    }

    public enum eToClient
    {
        MyUUID,
        DownGameInfo,
        SuccessLogIn,

        max
    }
}
