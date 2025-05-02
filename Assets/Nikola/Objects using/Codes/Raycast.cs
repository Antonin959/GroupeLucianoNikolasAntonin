using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    [Header("Raycast Feature")]
    [SerializeField] private float rayLength = 5;
    private Camera _camera;

    private NoteController _noteController;

    private ObjectInteracting _objectInteracting;

    private ObjectExam _objectExam;

    [Header("Curseur")]
    [SerializeField] private Image curseur;

    [Header("Input Key")]
    [SerializeField] private KeyCode interactKey;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Physics.Raycast(_camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f)), transform.forward, out RaycastHit hit, rayLength))
        {
            var readableItem = hit.collider.GetComponent<NoteController>();
            if (readableItem != null)
            {
                _noteController = readableItem;
                HighlightCurseur(true);
            }
            else
            {
                ClearNote();
            }

            var useableObject = hit.collider.GetComponent<ObjectInteracting>();
        }
        else
        {
            ClearNote();
        }

        if (_noteController != null)
        {
            if (Input.GetKeyDown(interactKey))
            {
                if (_noteController.isOpen) 
                {
                    _noteController.DisableNote();
                }
                else 
                { 
                    _noteController.ShowNote(); 
                }
            }
        }

        if (_objectInteracting != null)
        {
            if (tag == "Item")
            {

            }
        }

        if (_objectExam != null)
        {
            if (tag == "ObjetVisu")
            {

            }
        }
    }

    void ClearNote()
    {
        if (_noteController != null)
        {
            HighlightCurseur(false);
            _noteController = null;
        }
    }

    void HighlightCurseur(bool on)
    {
        if (on)
        {
            curseur.color = Color.red;
        }
        else
        {
            curseur.color = Color.white;
        }
    }

}
