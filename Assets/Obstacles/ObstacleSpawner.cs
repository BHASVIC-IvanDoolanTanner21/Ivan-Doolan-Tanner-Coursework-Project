using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    
    public GameObject[] obstacles = {}; //Make a list of all the obstacles in the game. This is filled in the unity editor, hence why it's empty
    public float moveSpeed; // Speed at which object will move
    public Transform positionOne, positionTwo; //Positions of start and end point of obstacle
    private int randomObject;
    public bool spawnObject = true;
    private GameObject obstacleClone;
    public GameObject scoreController;
    public double speedMultiplier = 1.2;
    //private Rigidbody2D 
    

    private void Update()
    {
        if (scoreController.GetComponent<SpeedController>().gameStarted == false)
        {
            obstacleClone.transform.position = positionOne.position;
        }
    }
    private void FixedUpdate()
    {

        //This sets the movespeed to the main movespeed of the project
        moveSpeed = (float)(scoreController.GetComponent<SpeedController>().mainSpeed*speedMultiplier);
        
        if (spawnObject)
        {
            //This sets randomObject to a random integer in the range 0 to the length of the obstacles array
            //It then instantiates the obstacle, and puts it at positionOne and sets spawnObject to false so that it only spawns one obstacle
            randomObject = (int)Random.Range(0f, obstacles.Length);
            obstacleClone = Instantiate(obstacles[randomObject]); 
            obstacleClone.transform.position = positionOne.position;
            spawnObject = false;
        }

        //This moves the object towards position two at the speed selected
        obstacleClone.transform.position = Vector3.MoveTowards(obstacleClone.transform.position, positionTwo.position, moveSpeed*Time.deltaTime);
        
        //Deletes the instance of the obstacle when it hits position two and sets spawn object to true to create another object
        if(obstacleClone.transform.position == positionTwo.position)
        {
            Destroy(obstacleClone);
            spawnObject = true;
        }
    }
}
