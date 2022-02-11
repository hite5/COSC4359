using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelSectionLogic : MonoBehaviour
{

    public bool selected = false;
    public Image WheelPartImage;
    public float wheelPartGrow = 1.5f;
    public Vector3 growSpeed = new Vector3(0.1f,0.1f,0.1f);
    public Vector3 DefaultSize; 
    public Vector3 maxSize;
    WaitForSeconds shortWait = new WaitForSeconds(0.1f);

    private void awake()
    {
        DefaultSize = transform.localScale;
        maxSize = DefaultSize * wheelPartGrow;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (selected == true)
        {
            WheelPartImage.color = Color.black;
            if (transform.localScale.x <= maxSize.x)
                transform.localScale += growSpeed;
        }
        else
        {
            WheelPartImage.color = Color.white;
            if (transform.localScale.x >= DefaultSize.x)
                transform.localScale -= growSpeed;
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
