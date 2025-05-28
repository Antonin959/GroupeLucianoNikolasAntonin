using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public Image invSelector;
    public Transform inventorySlotsParent;
    Image[] invSlots;

    const int invSize = 5;
    static int selectIndex = 0;
    public static bool invLock = false;

    static int[] inventory;

    static bool invError = false;
    static GameObject self;

    void Start()
    {
        self = gameObject;

        if (invSelector == null || inventorySlotsParent == null)
        {
            string errorMessDetail = invSelector == null ? "invSelector is null" : "" +
                      inventorySlotsParent == null ? " inventorySlotsParent is null" : "";

            Debug.LogError("Inventory parameters are null, inventory may not work! " + errorMessDetail);
            invError = true;
        }

            inventory = new int[invSize];
        invSlots = new Image[invSize];

        for (int i = 0; i < invSize; i++)
        {
            inventory[i] = 0;
            invSlots[i] = inventorySlotsParent.GetChild(i).GetComponent<Image>();
        }

        SetSelectorPosition(0);


    }

    void Update()
    {

        if (!invLock) selectIndex -= (int)(Input.GetAxis("Mouse ScrollWheel")*10);
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (selectIndex >= inventory.Length) selectIndex = 0;
            if (selectIndex < 0) selectIndex = inventory.Length - 1;

            SetSelectorPosition(selectIndex);
        }
    }

    void SetSelectorPosition(int invIndex) => invSelector.rectTransform.localPosition = new Vector3(90 * invIndex - 180, invSelector.rectTransform.localPosition.y);

    public void TryTakeItem(GameObject Item, Action action = null)
    {
        if (!Item.CompareTag("takable")) return;

        int ItemIndex = GetIndex(Item.name);

        for (int i = 0; i < invSize; i++)
        {
            if (inventory[i] != 0) continue;

            Debug.Log("object taken");

            inventory[i] = ItemIndex;
 
            invSlots[i].sprite = itemsImage[ItemIndex-1];

            action();

            if (Item.activeSelf)
                Item.SetActive(false);

            break;
        }
    }

    public Sprite[] itemsImage;
    /*
    Index Mirroir_object = 1
    Index teste = 2
    Index flashlight = 3
    */
    static int GetIndex(string name)
    {
        return name switch
        {
            "Mirroir_object" => 1,
            "cube" => 2,
            "FlashLight" => 3,

            _ => 0
        };
    }


    public static void HasItemInventoryName(string name, Action action) => HasItemInventoryIndex(GetIndex(name), action);
    /// <summary>
    /// Indexes :
    /// Mirroir_object = 1
    /// teste = 2.
    /// flashlight = 3.
    /// </summary>
    public static void HasItemInventoryIndex(int itemIndex, Action action)
    {
        if (invError)
        {
            Debug.LogError("<b>Item action disable.</b> Inventory script error, please check last intetory error message");
            return;
        }
        if (self == null)
        {
            Debug.LogError("<b>Item action disable.</b> Missing inventory script for item action\nId item executor : " + itemIndex);
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) && inventory[selectIndex] == itemIndex)
        {
            action();
            invLock = !invLock;
        }
    }
    public static void UnLockInventory() => invLock = false;
}
