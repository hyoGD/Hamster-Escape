using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlayer : MonoBehaviour
{
    public static MovingPlayer instance;
    
    public bool isMoving;
    public float speed;
    //private void Awake()
    //{
    //    if(instance == null)
    //    {
    //        DontDestroyOnLoad(this);
    //        instance = this;
    //    }
    //    else
    //    {
    //        DestroyImmediate(this);
    //    }
    //}
    private void Update()
    {
        Moving();
    }

    public void Moving()
    {
        if (isMoving)
        {
            transform.position += Vector3.left * Time.deltaTime * speed;
        }
        else
        {
            transform.position += Vector3.zero;
        }
    }

}
