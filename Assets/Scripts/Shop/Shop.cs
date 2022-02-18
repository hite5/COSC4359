using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    public static bool CurrentlyInShop = false;
    public GameObject ShopManager;
    public GameObject shop;
    bool loadOnce = false;
    [HideInInspector]
    public bool ThymusPresent = false;
    public Player playerScript;
    public bool inShopRange = false;

    /*
    void loadStats()
    {
        if (loadOnce == false) 
        {
            loadOnce = true;
            ShopManager.GetComponent<ShopManager>().openShopAndUpdate();

        }
    }
    */
    private void Update()
    {
        if (playerScript != null)
        {
            if (playerScript.Utilities.InteractButtonPressed && inShopRange == true)
            {
                if (!shop.activeSelf)
                {
                    shop.SetActive(true);
                    CurrentlyInShop = true;
                    GlobalPlayerVariables.inActiveUI = true;
                    Debug.Log("IN SHOP");
                }
            }
            if (inShopRange == true && GlobalPlayerVariables.inActiveUI == true)
            {
                if (playerScript.Utilities.EscapeButtonPressed)
                {
                    shop.SetActive(false);
                    CurrentlyInShop = false;
                    GlobalPlayerVariables.inActiveUI = false;
                   // Cursor.visible = false;
                }
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("ENTER COLLIDER");
        if (other.CompareTag("Player") == true && !ThymusPresent) {
            //loadStats();
            if (playerScript == null)
            {
                playerScript = other.GetComponent<Player>();
            }
            inShopRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("LEAVING COLLIDER");
        if (other.CompareTag("Player") == true)
        {
            Debug.Log("LEAVING SHOP");
            shop.SetActive(false);
            CurrentlyInShop = false;
            inShopRange = false;
        }
    }

    public void leaveShopOnTitle()
    {
        CurrentlyInShop = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        CurrentlyInShop = false;



    }


}
