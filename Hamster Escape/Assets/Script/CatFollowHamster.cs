using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFollowHamster : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = MovingPlayer.instance.transform.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}
