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
    public struct P_StudentInfo
    {
        [MarshalAs(UnmanagedType.I4)]
        public int _schoolID;
        [MarshalAs(UnmanagedType.I4)]
        public int _grade;
        [MarshalAs(UnmanagedType.I4)]
        public int _group;
        [MarshalAs(UnmanagedType.I4)]
        public int _number;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct P_MyUUID
    {
        [MarshalAs(UnmanagedType.I4)]
        public int _myUUID;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct P_DownGameInfo
    {
        [MarshalAs(UnmanagedType.I4)]
        public int _gameIndex;
        [MarshalAs(UnmanagedType.I4)]
        public int _level;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct P_SuccessLogIn
    {
        [MarshalAs(UnmanagedType.I4)]
        public int _schoolID;
        [MarshalAs(UnmanagedType.I4)]
        public int _grade;
        [MarshalAs(UnmanagedType.I4)]
        public int _group;
        [MarshalAs(UnmanagedType.I4)]
        public int _number;
    }
}
