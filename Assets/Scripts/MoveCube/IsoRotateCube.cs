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
    float speed = 300f;
    Vector3 previousMousePosition;
    Vector3 mouseDelta;

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DetectSwipe();
        DragFeedback();
    }

    void DragFeedback()
    {
        if(Input.GetMouseButton(1))
        {
            mouseDelta = 0.15f*(Input.mousePosition - previousMousePosition);
            transform.rotation = Quaternion.Euler(-mouseDelta.y, -mouseDelta.x, mouseDelta.y) * transform.rotation;
        }
        //turn towards target, maybe put in another script if multiple things are going to edit cube target
        else if (transform.rotation != isotarget.transform.rotation)
        {
            var step = speed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, isotarget.transform.rotation, step);
        }
        previousMousePosition = Input.mousePosition;
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
            double swipeDifference = Math.Abs(1-((2)%(6*swipeAngle/180))); // 0 to 1 how far from ideal
            print(swipeDifference);

            //check magnitude and angular direction against magnitude and forgiveness (how far from the 6 swipe directions is allowed, though intuitively where you swipe also impacts the angle but I didn't feel like making this even more complicated)
            if (currentSwipe.magnitude > Swipe_threshold && swipeDifference > Swipe_forgiveness)
            {
                IsoRotate(swipeNumber);
            }
        }
    }

    private void IsoRotate(int swipeNumber)
    {
        switch (swipeNumber)
        {
            case 2:
                isotarget.transform.Rotate(-90,0,0,Space.World);
                break;
            case 1:
                isotarget.transform.Rotate(0,0,90,Space.World);
                break;
            case 0:
                isotarget.transform.Rotate(0,-90,0,Space.World);
                break;
            case -1:
                isotarget.transform.Rotate(90,0,0,Space.World);
                break;
            case -2:
                isotarget.transform.Rotate(0,0,-90,Space.World);
                break;
            case 3 or -3:
                isotarget.transform.Rotate(0,90,0,Space.World);
                break;
        }
    }

}
