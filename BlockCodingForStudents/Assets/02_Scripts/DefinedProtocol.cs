using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefinedProtocol
{
    public enum eFromClient
    {
        ManagerInfo,
        DownloadInfo,
        OnOffInfo,

        max
    }

    public enum eToClient
    {
        ClassInfo,
        StudentInfo,
        FinishSend,

        max
    }
}
