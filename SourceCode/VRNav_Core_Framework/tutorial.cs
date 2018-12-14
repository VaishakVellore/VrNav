//tutorial.cs - A tutorial application for helping new users get familiar with our framework.
//Team 52
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Diagnostics;

public class Tutorial : MonoBehaviour {
    
    //Declarations.
    public Text textTutorial;
    public bool[] tasks;
    public string[] displayMessages;
    public int taskID;
    Stopwatch sw;
    public string[] successMessages;
    public static int numSteps;
    bool success;
    int successID;
    public static bool selectObject;
    public static bool headYes;
    bool doneTutorial;

    //Called once at the beginning of the programming. Initializes all messages to display to user.
    void Start () 
    {
        sw = Stopwatch.StartNew();
        success = false;
        displayMessages = new string[10];
        displayMessages[0] = "Hello there!\nLook around.";
        displayMessages[1] = "Let's navigate!";
        displayMessages[2] = "Start jogging in place.";
        displayMessages[3] = "Remember that you can\njog at varying speeds.";
        displayMessages[4] = "Look around to\nfind a PC monitor.";
        displayMessages[5] = "Let's purchase it.";
        displayMessages[6] = "Stare at the monitor for\na few seconds";
        displayMessages[7] = "Nod your head to buy.";
        displayMessages[8] = "You're done!\nFeel free to explore.";
        displayMessages[9] = "Signing off,\nVRNav team.";
        successMessages = new string[10];
        successMessages[0] = "Good!";
        successMessages[1] = "Awesome!";
        successMessages[2] = "Brilliant!";
        successMessages[3] = "Amazing!";
        taskID = 0;
        successID = 0;
        selectObject = cartaddobject.selectObject;
        headYes = headgestures.headYes;
        doneTutorial = false;
    }
	
	//Called once after each frame. This is the flow of the tutorial, based on user actions, messages are displayed, step-by-step.
    //For each task completed, user gets a message of appreciation. Subsequent messages are displayed afte user successfully completes a task.
	void Update () {
        textTutorial.text = displayMessages[taskID];
        numSteps = accpedometer.numSteps;
        selectObject = cartaddobject.selectObject;
        headYes = headgestures.headYes;
        if (doneTutorial == false)
        {
            if (success == false)
            {
                if (sw.ElapsedMilliseconds > 7000 && taskID == 0)
                {
                    taskID += 1;
                    sw = Stopwatch.StartNew();
                }
                if (sw.ElapsedMilliseconds > 3000 && taskID == 1)
                {
                    taskID += 1;
                }
                if (numSteps > 10 && taskID == 2)
                {
                    taskID += 1;
                    success = true;
                    sw = Stopwatch.StartNew();
                }
                if (sw.ElapsedMilliseconds > 3000 && taskID == 3)
                {
                    taskID += 1;
                    sw = Stopwatch.StartNew();
                }
                if (sw.ElapsedMilliseconds > 3000 && taskID == 4)
                {
                    taskID += 1;
                    sw = Stopwatch.StartNew();
                }
                if (sw.ElapsedMilliseconds > 3000 && taskID == 5)
                {
                    taskID += 1;
                    sw = Stopwatch.StartNew();
                }
                if (selectObject == true && taskID == 6)
                {
                    taskID += 1;
                    success = true;
                    sw = Stopwatch.StartNew();
                }
                if (headYes == true && taskID == 7)
                {
                    taskID += 1;
                    success = true;
                    sw = Stopwatch.StartNew();
                }
                if (sw.ElapsedMilliseconds > 3000 && taskID == 8)
                {
                    taskID += 1;
                    sw = Stopwatch.StartNew();
                    doneTutorial = true;
                    sw.Stop();
                }
            }
            else
            {
                textTutorial.text = successMessages[successID];
                if (sw.ElapsedMilliseconds > 3000)
                {
                    success = false;
                    successID += 1;
                    sw = Stopwatch.StartNew();
                }

            }
        }
        else
        {
            textTutorial.text = displayMessages[9];
        }

    }
}
