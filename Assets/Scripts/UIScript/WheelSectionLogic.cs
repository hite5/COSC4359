using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelSectionLogic : MonoBehaviour
{

    public bool selected = false;
    public Image WheelPartImage;
    public float wheelPartGrow = 1.5f;
    public float growSpeed = 0.1f;
    public Vector3 growSpeedVec;
    
    public Vector3 DefaultSize; 
    public Vector3 maxSize;
    WaitForSeconds shortWait = new WaitForSeconds(0.1f);


    void Start()
    {
        DefaultSize = transform.localScale;
        maxSize = DefaultSize * wheelPartGrow;
        growSpeedVec = new Vector3(growSpeed, growSpeed, growSpeed);
    }


    // Update is called once per frame
    void Update()
    {
        if (selected == true)
        {
            //WheelPartImage.color = Color.black;
            if (transform.localScale.x <= maxSize.x)
                transform.localScale += growSpeedVec;
            if (transform.localScale.x > maxSize.x)
            {
                transform.localScale = maxSize;
            }
        }
        else
        {
            //WheelPartImage.color = Color.white;
            if (transform.localScale.x >= DefaultSize.x)
                transform.localScale -= growSpeedVec;
            if (transform.localScale.x < DefaultSize.x)
            {
                transform.localScale = DefaultSize;
            }
        }
    }

    void OnDisable()
    {
        selected = false;
        transform.localScale = DefaultSize;
    }

}
