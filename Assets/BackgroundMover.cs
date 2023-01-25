using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover: MonoBehaviour

{
    public float offset; // Speed at which object will move
    public double speedMultiplier = 0.000000001; //Multiplier which will be different for each object depending on how far back it is, creating fake parallax
    public GameObject scoreController;
    private Renderer rend;
    public bool isPaused = false;

    void Start()
    {
        //getting the objects renderer to use later
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        isPaused = scoreController.GetComponent<SpeedController>().isPaused;

        //Here I use repeating textures to infinitely scroll the background to give the illusion of movement

        //This sets the offset speed to the main movespeed of the project
        offset = scoreController.GetComponent<SpeedController>().mainSpeed;
        //creates a vector variable to add to the current texture offset
        
        Vector2 textureOffset = new Vector2((float)((speedMultiplier * offset * Time.deltaTime) /1000),0);

        //This offsets renderers texture by the textureOffset variable that has just been updated
        if(!isPaused)
        {
            rend.material.mainTextureOffset += textureOffset;
        }
    }
}