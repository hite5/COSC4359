using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RememberReference : MonoBehaviour
{
    public List<GameObject> refList;

    public GameObject GetReference(int i)
    {
        return refList[i];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
