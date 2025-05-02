using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class NoteController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private KeyCode closeKey;

    [Space(10)]
    [SerializeField] private PlayerMovement player;

    [Header("UI Text")]
    [SerializeField] private GameObject noteCanvas;
    [SerializeField] private TMP_Text noteTextAreaUI;

    [Space(10)]
    [SerializeField] [TextArea] private string noteText;

    [Space(10)]
    [SerializeField] private UnityEvent openEvent;
    public bool isOpen = false;

    public void ShowNote()
    {
        noteTextAreaUI.text = noteText;
        noteCanvas.SetActive(true);
        openEvent.Invoke();
        isOpen = true;
        player.LockControl = true;
    }

    public void DisableNote()
    {
        noteCanvas.SetActive(false);
        noteTextAreaUI.text = null;
        isOpen = false;
        player.LockControl = false;
    }

}
