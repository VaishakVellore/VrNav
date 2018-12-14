//accpedometer.cs - Step Detection and Virtual Locomotion.
//Team 52
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System;

//Player Game Object needs to have a mass and detect collisions.
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class accpedometer : MonoBehaviour
{
    //Declarations for the various parameters used in the program.
    private Rigidbody rigidBody;
    private CapsuleCollider capsuleCollider;
    int numSteps = 0;
    public double accCur = 0.0f;
    public double accAvg = 0.0f;
    int sampleCount = 1;
    double accNoGravCur = 0.0f;
    double accSum = 0.0f;
    long tPeakCur;
    long tPeakPrev;
    int numStepsPrev = 0;
    Stopwatch sw;
    public Transform target;
    int peak = 0;
    public int waitTime = 30;
    int waitIndex = 30;
    float velocity = 0.0f;
    public float tMin = 200.0f;
    public float tMax = 500.0f;
    public float vMin = 0.0f;
    public float vMax = 10.0f;
    string moveState = "Rest";
    private bool isWalking;
    long lastElapsedTime;
    private long delta;
    public bool isTreadmill = true;
    private bool isWalkingprev = false;
    Stopwatch vw;
    private long deltaTime;
    private long runningTime = 0;
    private string distance;
    private float avgVelocity = 0;
    AudioSource tread;
    AudioClip walk;
    private bool isAudioPlay;

    //Perform the following initializations right after the program starts.
    void Awake()
    {
        tread = GameObject.Find("Camera").GetComponent<AudioSource>();
        walk = (AudioClip)Resources.Load("walk", typeof(AudioClip));
        tread.clip = (AudioClip)walk;
        tread.loop = true;
        target = GameObject.Find("Camera").GetComponent<Transform>();
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
        capsuleCollider = GetComponent<CapsuleCollider>();
        sw = Stopwatch.StartNew();
        if (isTreadmill)
        {
            accCur = Input.acceleration.magnitude * 10;
        }
    }

    //Call every time after a frame is displayed
    void Update()
    {
        if (isTreadmill)
        {
            RunTreadmill();
        }
    }

    //The core function of the program. Performs both Step Detection and Virtual Locomotion. 
    void RunTreadmill()
    {
        if (waitIndex > waitTime)
        {
            waitIndex--;
            return;
        }
        waitIndex = waitTime;
        accCur = Input.acceleration.magnitude * 10;
        accAvg = Mean();
        accNoGravCur = accCur - accAvg;
        numStepsPrev = numSteps;
        numSteps += StepCount();
        Vector3 direction = new Vector3(target.transform.forward.x, 0, target.transform.forward.z).normalized * velocity / 2.0f * Time.deltaTime;
        rigidBody.MovePosition(transform.position + direction);
        sampleCount++;
        DetectSteps();
        CalculateAvgVelocity();
    }

    //Compute the mean value of accelerometer input values.
    double Mean()
    {
        accSum = accSum + accCur;
        return accSum / sampleCount;
    }

    //Returns a count of the number of steps detected between two consecutive frames.
    int StepCount()
    {
        int steps = 0;
        if (peak == 0)
        {
            if (accNoGravCur > 5)
            {
                tPeakPrev = tPeakCur;
                tPeakCur = sw.ElapsedMilliseconds;
                steps += 1;
                CalculateVelocity(steps);
                peak = 1;
            }

        }
        else
        {
            if (accNoGravCur < -2)
            {
                peak = 0;
            }

        }
        return steps;
    }

    //Compute both instananeous speed of user movement for Virtual Locomotion.
    void CalculateVelocity(int steps)
    {
        if (isWalkingprev == false)
        {
            vw = Stopwatch.StartNew();
            isWalkingprev = true;
            lastElapsedTime = vw.ElapsedMilliseconds;
        }
        else if (isWalkingprev)
        {
            deltaTime = vw.ElapsedMilliseconds - lastElapsedTime;
            runningTime += deltaTime;
            velocity = 1 / (float)deltaTime * 1000;
            velocity = (float)Math.Round((Decimal)velocity, 1, MidpointRounding.AwayFromZero);
            if (velocity > 5.0f)
            {
                velocity = 0;
                steps = 0;
            }
            lastElapsedTime = vw.ElapsedMilliseconds;
        }
    }

    //Detect if steps are taken by the user, and indicate how fast the user is moving. Play audio for movement.
    void DetectSteps()
    {
        delta = tPeakCur - tPeakPrev;
        if (numStepsPrev < numSteps)
        {
            isWalking = true;
            if (delta < 300)
            {
                moveState = "Fast";
            }
            else
            {
                moveState = "Slow ";
            }
            if(isAudioPlay == false)
            {
                tread.Play();
                isAudioPlay = true;
            }
        }
        else
        {
            if(isAudioPlay && sw.ElapsedMilliseconds - tPeakCur > 700)
            {
                isAudioPlay = false;
                tread.Stop();
            }

            if (sw.ElapsedMilliseconds - tPeakCur > 800 && tPeakCur != 0)
            {
                isWalkingprev = false;
                vw.Stop();
                isWalking = false;
                velocity = 0;
                moveState = "Rest";
            }
        }
    }

    //Calculate average velocity of the user's walking-in-place speed for Virtual Locomotion.
    void CalculateAvgVelocity()
    {
        if (numSteps != 0)
        {
            avgVelocity = (float)numSteps / (float)runningTime * 1000;
            avgVelocity = (float)Math.Round((Decimal)avgVelocity, 1, MidpointRounding.AwayFromZero);
        }
        else
            avgVelocity = 0;
    }
}
