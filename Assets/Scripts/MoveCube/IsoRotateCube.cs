using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics; //namespace conflicts
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class IsoRotateCube : MonoBehaviour
{
    public GameObject isotarget;
    public float Swipe_threshold;
    public float Swipe_forgiveness;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    float swipeAngle;
    float speed = 200f;

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DetectSwipe();

        if (transform.rotation != isotarget.transform.rotation)
        {
            var step = speed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, isotarget.transform.rotation, step);
        }
    }

    //detect mouse swipes
    void DetectSwipe()
    {
        if (Input.GetMouseButtonDown(1))
        {
            firstPressPos= new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (Input.GetMouseButtonUp(1))
        {
            secondPressPos= new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentSwipe = secondPressPos - firstPressPos;
            swipeAngle = (float)(Math.Atan2(currentSwipe.y, currentSwipe.x) * (180/Math.PI)); //in degrees, -180 to 180
            int swipeNumber = (int)Math.Round(6*swipeAngle/360);
            double swipeDifference = Math.Abs((6*swipeAngle/180)%(2)-1);

            //check magnitude and angular direction against magnitude and forgiveness (how far from the 6 swipe directions is allowed, though intuitively where you swipe also impacts the angle but I didn't feel like making this even more complicated)
            if ((currentSwipe.magnitude > Swipe_threshold) && (swipeDifference < Swipe_forgiveness))
            {
                IsoRotate(swipeNumber);
            }
        }
    }

    void IsoRotate(int swipeNumber)
    {
        if (swipeNumber == 2)
        {
            isotarget.transform.Rotate(0,0,0,Space.World);
        }
        if (swipeNumber == 1)
        {
            isotarget.transform.Rotate(0,0,0,Space.World);
        }
        if (swipeNumber == 0)
        {
            isotarget.transform.Rotate(0,0,0,Space.World);
        }
        if (swipeNumber == -1)
        {
            isotarget.transform.Rotate(0,0,0,Space.World);
        }
        if (swipeNumber == -2)
        {
            isotarget.transform.Rotate(0,0,0,Space.World);
        }
        if ((swipeNumber == 3) || (swipeNumber == -3))
        {
            isotarget.transform.Rotate(0,0,0,Space.World);
        }
    }

}
