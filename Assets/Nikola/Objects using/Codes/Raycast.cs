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
        if (Physics.Raycast(_camera.transform.position, transform.forward, out RaycastHit hit, rayLength))
        {
            var readableItem = hit.collider.GetComponent<NoteController>();
            var useableObject = hit.collider.GetComponent<ObjectInteracting>();
            var watchableObject = hit.collider.GetComponent<ObjectExam>();

            if (readableItem != null)
            {
                _noteController = readableItem;
                HighlightCurseur(true);
            }

            if (useableObject != null)
            {
                _objectInteracting = useableObject;
                HighlightCurseur(true);

            }

            if (watchableObject != null)
            {
                _objectExam = watchableObject;
                HighlightCurseur(true);

            }
        }
        else
        {
            ClearInteraction();
        }


        //Prendre les notes
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

        //Item qu'on peut prendre
        if (_objectInteracting != null)
        {
            if (_objectInteracting.gameObject.tag == "Item")
            {
                _objectInteracting.ShowObject();
            }
            else
            {
                _objectInteracting.DisableObject();
            }
        }

        //Regarder les objets
        if (_objectExam != null)
        {
            if (Input.GetKeyDown(interactKey))
            {
                if (_objectExam.gameObject.tag == "Objet")
                {
                    _objectExam.StartExam();
                    //Debug.Log("AAAAAA");
                }

                else
                {
                    _objectExam.FinishExam();
                }
            }
        }

        void ClearInteraction()
        {
            if (_noteController != null)
            {
                HighlightCurseur(false);
                _noteController = null;
            }

            if (_objectExam != null)
            {
                HighlightCurseur(false);
                _objectExam = null;
            }

            if (_objectInteracting != null)
            {
                HighlightCurseur(false);
                _objectInteracting = null;
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
}
