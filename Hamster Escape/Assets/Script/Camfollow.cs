using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camfollow : MonoBehaviour
{
    private Transform player;
    public Vector3 target;
    Vector3 velocity = Vector3.zero;
    float smoothTime = 0.1f;
    private void OnEnable()
    {
        if (player == null)
        {
            player = GameObject.Find("Cat").GetComponent<Transform>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Vector3 newPosition = player.position + target;
        transform.position = transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }
}
