using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public Image invSelector;
    public Transform inventorySlotsParent;
    Image[] invSlots;

    const int invSize = 5;
    int selectIndex = 0;


    static int[] inventory;

    void Start()
    {
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

        selectIndex += (int)(Input.GetAxis("Mouse ScrollWheel")*10);
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Debug.Log("delta");
            if (selectIndex >= inventory.Length) selectIndex = 0;
            if (selectIndex < 0) selectIndex = inventory.Length - 1;

            SetSelectorPosition(selectIndex);
        }

        Debug.Log(selectIndex);
    }

    void SetSelectorPosition(int invIndex) => invSelector.rectTransform.localPosition = new Vector3(90 * invIndex - 180, invSelector.rectTransform.localPosition.y);

    public void TryTakeItem(GameObject Item)
    {
        if (!Item.CompareTag("takable")) return;

        int ItemIndex = GetIndex(Item.name);

        for (int i = 0; i < invSize; i++)
        {
            if (inventory[i] != 0) continue;

            Debug.Log("object taken");

            inventory[i] = ItemIndex;
 
            invSlots[i].sprite = itemsImage[ItemIndex-1];

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

    
    public static bool HasItemInventoryName(string name) => HasItemInventoryIndex(GetIndex(name));
    public static bool HasItemInventoryIndex(int itemIndex)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == itemIndex)
                return true;
        }
        return false;
    }
}
