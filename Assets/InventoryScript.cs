using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public Transform inventorySlotsParent;
    Image[] invSlots;

    const int invSize = 5;

    int[] inventory;

    void Start()
    {
        inventory = new int[invSize];
        invSlots = new Image[invSize];

        for (int i = 0; i < invSize; i++)
        {
            inventory[i] = 0;
            invSlots[i] = inventorySlotsParent.GetChild(i).GetComponent<Image>();
        }
    }

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
            break;
        }
    }

    public Sprite[] itemsImage;
    /*
    Index Mirroir_object = 1
    Index teste = 2
    */
    int GetIndex(string name)
    {
        return name switch
        {
            "Mirroir_object" => 1,
            "cube" => 2,

            _ => 0
        };
    }
}
