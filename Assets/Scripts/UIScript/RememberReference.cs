using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RememberReference : MonoBehaviour
{
    public List<GameObject> refList;

    public GameObject GetReference(int i)
    {
        return refList[i];
    }

}
