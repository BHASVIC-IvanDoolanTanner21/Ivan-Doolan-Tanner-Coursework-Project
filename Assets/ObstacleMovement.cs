using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public Transform positionOne, positionTwo;
    public float moveSpeed = 20f;

    void Start()
    {
        transform.position = positionOne.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, positionTwo.position, moveSpeed * Time.deltaTime);
        if (transform.position == positionTwo.position)
        {
            transform.position = positionOne.position;
        }
    }
}
