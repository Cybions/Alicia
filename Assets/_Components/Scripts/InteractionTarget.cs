using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionTarget : MonoBehaviour
{
    public bool isInteractable = true;
    public bool LockAfterInteraction = false;
    public UnityEvent OnInteract;
    [SerializeField]
    private float DelayBeforeInteraction = 1.2f;

    public void ChangeLockState(bool unlock)
    {
        isInteractable = unlock;
    }

    public void Interact()
    {
        isInteractable = false;
        StartCoroutine(ResetState());
        OnInteract.Invoke();
    }

    private IEnumerator ResetState()
    {
        yield return new WaitForSeconds(DelayBeforeInteraction);
        isInteractable = true;
    }
}
