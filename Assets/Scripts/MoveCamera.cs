using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Dictionary<int, Vector3> positions;
    public Dictionary<int, Quaternion> rotations = new Dictionary<int, Quaternion>();

    public Grid grid;
    private Vector3 middlePoint;
    private int xAngle = 45;
    private int startYAngle = 180;
    private int zAngle = 0;
    private int rotationAmount = 45;
    private int AmountOfAngles = 8;

    private int currentKey;
    private Vector3 currentPosition;
    private Vector3 nextPosition;
    private Quaternion currentRotation;
    private Quaternion nextRotation;

    private bool isLerping;
    private float startLerpTime;
    private float lerpTime = .5f;

    void Start()
    {
        middlePoint = CalculateMiddlePoint();
        SetCameraPosition();
        SetCameraRotation();
        currentKey = 0;
        gameObject.transform.position = positions[currentKey];
        gameObject.transform.rotation = rotations[currentKey];
        currentPosition = positions[currentKey];
        currentRotation = rotations[currentKey];
        nextPosition = positions[currentKey];
        nextRotation = rotations[currentKey];
    }

    private void SetCameraRotation()
    {
        for (int i = 0; i < AmountOfAngles; i++)
        {
            int y = startYAngle - (rotationAmount * i);
            rotations.Add(i, Quaternion.Euler(xAngle,y,zAngle));
        }
    }

    private void SetCameraPosition()
    {   
        float offset = (grid.gridSize * 2) * grid.scale;
        float y = middlePoint.y + offset;

        positions = new Dictionary<int, Vector3>()
        {
            {0,new Vector3(middlePoint.x,           y,  middlePoint.z+offset)},
            {1,new Vector3( -(offset*.5f),          y,  offset-grid.scale)},
            {2,new Vector3( middlePoint.x-offset,   y,  middlePoint.z)},
            {3,new Vector3( -(offset*.5f),          y,  -(offset*.5f))},
            {4,new Vector3( middlePoint.x,          y,  middlePoint.z-offset)},
            {5,new Vector3( offset-grid.scale,      y,  -(offset*.5f))},
            {6,new Vector3( middlePoint.x+offset,   y,  middlePoint.z)},
            {7,new Vector3( offset-grid.scale,      y,  offset-grid.scale)}
        };
    }
    
    public void StartRotate()
    {
        if (Input.mousePosition.x <= Screen.width * .5f)
        {
            RotateCamera(false);
            StartLerping();
        }
        else
        {
            RotateCamera(true);
            StartLerping();
        }
    }

    private void FixedUpdate()
    {
        if (isLerping)
        {
            Lerp();

        }
    }

    private void Lerp()
    {
        float timeSinceStarted = Time.time - startLerpTime;
        float percentageComplete = timeSinceStarted / lerpTime;

        transform.position = Vector3.Lerp(currentPosition, nextPosition, percentageComplete);
        transform.rotation = Quaternion.Lerp(currentRotation, nextRotation, percentageComplete);
        
        if (percentageComplete >= 1.0f)
        {
            isLerping = false;
        }
    }

    private void StartLerping()
    {
        isLerping = true;
        startLerpTime = Time.time;
    }


    Vector3 CalculateMiddlePoint()
    {
        Vector3 middlePoint = new Vector3(  (grid.gridSize * grid.scale) * .5f - (grid.scale * .5f),
                                            grid.gridSize * .5f,
                                            (grid.gridSize * grid.scale) * .5f - (grid.scale * .5f));
        return middlePoint;
    }

    void RotateCamera(bool direction)
    {
        currentPosition = positions[currentKey];
        currentRotation = rotations[currentKey];
        currentKey = direction ? currentKey + 1 : currentKey - 1;
        currentKey = (currentKey >= positions.Count) ? 0 : currentKey;
        currentKey = (currentKey < 0) ? positions.Count - 1 : currentKey;
        nextPosition = positions[currentKey];
        nextRotation = rotations[currentKey];
    }
}
