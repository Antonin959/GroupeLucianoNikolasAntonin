using UnityEngine;

public class HidingSpotScript : MonoBehaviour
{
    Transform player;
    public Transform hidePos, outHidePos;
    public bool viewHiding = false, isHidingCrouch = false;

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    void OnDrawGizmos()
    {
        if (viewHiding)
        {
            
            player = GameObject.Find("Player").transform;
            Gizmos.color = Color.green;
            Gizmos.DrawMesh(player.GetComponent<MeshFilter>().sharedMesh, 0, hidePos.position, hidePos.rotation, new Vector3(player.transform.localScale.x, player.transform.localScale.y * (isHidingCrouch ? 0.5f : 1), player.transform.localScale.z));
            Gizmos.DrawRay(hidePos.position, hidePos.forward);

            Gizmos.color = Color.red;
            Gizmos.DrawMesh(player.GetComponent<MeshFilter>().sharedMesh, 0, outHidePos.position, outHidePos.rotation, new Vector3(player.transform.localScale.x, player.transform.localScale.y, player.transform.localScale.z));
            Gizmos.DrawRay(outHidePos.position, outHidePos.forward);

            Gizmos.color = Color.white;
            Gizmos.DrawLine(hidePos.position, outHidePos.position);
        }
    }
}
