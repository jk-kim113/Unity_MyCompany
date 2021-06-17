﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class ConvertPacket
{
    public static byte[] StructureToByteArray(object obj)
    {
        int datasize = Marshal.SizeOf(obj);
        IntPtr buff = Marshal.AllocHGlobal(datasize);
        Marshal.StructureToPtr(obj, buff, false);
        byte[] data = new byte[datasize];
        Marshal.Copy(buff, data, 0, datasize);
        Marshal.FreeHGlobal(buff);
        return data;
    }

    public static object ByteArrayToStructure(byte[] data, Type type, int size)
    {
        IntPtr buff = Marshal.AllocHGlobal(data.Length);
        Marshal.Copy(data, 0, buff, data.Length);
        object obj = Marshal.PtrToStructure(buff, type);
        Marshal.FreeHGlobal(buff);

        if (Marshal.SizeOf(obj) != size)
            return null;

        return obj;
    }
}
