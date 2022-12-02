using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compression : MonoBehaviour
{
    const float FLOAT_PRECISION_MULT = 100f;

    private void Start()
    {
        Debug.Log("Hello World!");
        Debug.Log($"Size of uncompressed: {sizeof(float)}");
        Debug.Log($"Size of compressed: {sizeof(ushort)}");

        for (int i = 0; i < 10; i++)
        {
            float r = Random.Range(256.0f, 1024.0f);
            SendVelocity(r);
        }
    }

    void SendVelocity(float _vel)
    {
        Debug.Log($"Original vel:{_vel}");
        short velComp = CompressVelocity(_vel);
        Debug.Log($"Compressed: {velComp}");
        float velDecomp = DecompressVelocity(velComp);
        Debug.Log($"Decompressed: {velDecomp}");
        Debug.Log($"============\n\n\n");
    }

    short CompressVelocity(float _vel)
    {
        return (short)(_vel * FLOAT_PRECISION_MULT);
    }

    float DecompressVelocity(short _vel)
    {
        return (float)_vel / FLOAT_PRECISION_MULT;
    }
}
