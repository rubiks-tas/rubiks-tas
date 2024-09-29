using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    private Vector2 _firstPressPos;
    private Vector2 _secondPressPos;
    private Vector2 _currentSwipe;
    
    public GameObject target;
    
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Swipe();
    }

    private void Swipe()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // get 2d position of first mouse click
            _firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //print(_firstPressPos);
        }

        if (Input.GetMouseButtonUp(1))
        {
            // get 2d position of second mouse click 
            _secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            // create a vector from the two clicks
            _currentSwipe = _secondPressPos - _firstPressPos;
            _currentSwipe.Normalize(); 

            if (LeftSwipe(_currentSwipe))
            {
                target.transform.Rotate(0, 90, 0, Space.World);
            }
            else if (RightSwipe(_currentSwipe)) 
            {
                target.transform.Rotate(0, -90, 0, Space.World);
            }
        }
    }

  private bool LeftSwipe(Vector2 swipe)
    {
        // -x is left direction, -0.5 < y < 0.5 not much y movement
        return _currentSwipe.x < 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f;
    }

    private bool RightSwipe(Vector2 swipe)
    {
        return _currentSwipe.x > 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f;
    }  
}
