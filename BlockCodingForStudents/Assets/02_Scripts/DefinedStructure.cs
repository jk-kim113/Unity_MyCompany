using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class DefinedStructure
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PacketInfo                                        // 전체 사이즈 1032byte
    {
        [MarshalAs(UnmanagedType.I4)]
        public int _id;
        [MarshalAs(UnmanagedType.I4)]
        public int _totalSize;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
        public byte[] _data;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct P_ManagerInfo
    {
        [MarshalAs(UnmanagedType.I4)]
        public int _managerID;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct P_ClassInfo
    {
        [MarshalAs(UnmanagedType.I4)]
        public int _grade;
        [MarshalAs(UnmanagedType.I4)]
        public int _group;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct P_StudentInfo
    {
        [MarshalAs(UnmanagedType.I4)]
        public int _grade;
        [MarshalAs(UnmanagedType.I4)]
        public int _group;
        [MarshalAs(UnmanagedType.I4)]
        public int _number;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct P_FinishSend
    {
        [MarshalAs(UnmanagedType.I4)]
        public int _kind;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct P_DownGameInfo
    {
        [MarshalAs(UnmanagedType.I4)]
        public int _gameIndex;
        [MarshalAs(UnmanagedType.I4)]
        public int _level;
    }
}
