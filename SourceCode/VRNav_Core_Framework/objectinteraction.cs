//objectinteraction.cs - Object Interaction triggered through a Timed Gaze Radial Progress Bar..
//Team 52
using UnityEngine;
using System.Collections;

public class cartaddobject : MonoBehaviour {

    //Declarations.
    public static bool selectObject = false;
    public static bool progressBarOn = false;
    public static bool timedGazeClick = false;
    public static bool displayUI = false;

    //Call once every frame.
    public void Update()
    {
        //If the user has focused on the target object for 5 seconds or more, trigger and action
        if (timedGazeClick)
        {
            selectObject = true;
            timedGazeClick = false;
            displayUI = true;
        }
        else
        {
            displayUI = false;
        }
    }

    //The reticle is focued on an object, or in other words the user is staring at an object.
    public void PointerEnter()
    {
        if(progressBarOn == false)
        {
            progressBarOn = true;
        }
    }

    //The user is no longer looking at the object.
    public void PointerExit()
    {
        if(progressBarOn == true)
        {
            progressBarOn = false;
        }
    }

}
