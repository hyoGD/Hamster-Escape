using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMananger : MonoBehaviour
{
    public static DataMananger instance;

    public Data data;

    private void Awake()
    {
        instance = this;
    }
}
