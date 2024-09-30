using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics; //namespace conflicts
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class FreeRotateCube : MonoBehaviour
{
    Vector3 previousMousePosition;
    Vector3 mouseDelta;
    float scrollDelta;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Drag();
    }

    void Drag()
    {
        if(Input.GetMouseButton(1))
        {
            mouseDelta = 0.25f*(Input.mousePosition - previousMousePosition);
            transform.rotation = Quaternion.Euler(-mouseDelta.y, -mouseDelta.x, mouseDelta.y) * transform.rotation;
        }
        if(Input.mouseScrollDelta.y != 0)
        {
            scrollDelta = 3.0f*Input.mouseScrollDelta.y;
            transform.rotation = Quaternion.Euler(scrollDelta, scrollDelta, scrollDelta) * transform.rotation;
        }
        //turn towards target, maybe put in another script if multiple things are going to edit cube target
        previousMousePosition = Input.mousePosition;
    }

}
