using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelLogic : MonoBehaviour
{
    public RememberReference ListOfImage;
    public Image WheelImageDisplay;
    public Text WheelText;
    public List<Image> imageList;
    public List<GameObject> WheelPartRef;
    private bool gotReference = false;
    public WheelSectionLogic previouslySelected;

    private void OnDisable()
    {
        previouslySelected = null;
    }

    public void switchImageNText(int i, string newName)
    {

        //ListOfImage.GetReference(i).TryGetComponent<Image>(out Image referance);
        //WheelImageDisplay = referance;
        if (gotReference == false)
        {
            for (int j = 0; j < ListOfImage.refList.Count; j++)
            {
                ListOfImage.GetReference(j).TryGetComponent<Image>(out Image referance);
                //WheelImageDisplay = referance;
                imageList.Add(referance);
            }
            gotReference = true;
        }
        else
        {
            WheelImageDisplay.sprite = imageList[i].sprite;
            WheelText.text = newName;
        }
    }

    public void selectWheel(int i)
    {
        WheelPartRef[i].TryGetComponent<WheelSectionLogic>(out WheelSectionLogic selectedPart);
        if (selectedPart != null)
        {
            if (previouslySelected == null)
            {
                previouslySelected = selectedPart;
                selectedPart.selected = true;
            }
            else if (selectedPart != previouslySelected)
            {
                previouslySelected.selected = false;
                selectedPart.selected = true;
                previouslySelected = selectedPart;
                //selectedPart.selected = true;
            }
            //selectedPart.selected = true;
        }
    }

}
