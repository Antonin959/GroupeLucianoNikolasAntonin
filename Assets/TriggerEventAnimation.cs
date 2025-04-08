using UnityEngine;

public class TriggerEventAnimation : MonoBehaviour
{
    bool isExecuted = false;

    [SerializeField] string animName;
    [SerializeField] Animator animator;

    void OnTriggerEnter(Collider other)
    {
        if (!isExecuted && other.name == "Player")
        {
            animator.Play(animName);
            isExecuted = true;
        }
    }
}
