using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MovableItem : MonoBehaviour
{
    private enum TypeOfMovement
    {
        rotation,
        displacement
    }
    [SerializeField]
    private Vector3 vectorMovement;
    [SerializeField]
    private TypeOfMovement typeOfMovement;
    [SerializeField]
    private float duration;
    [SerializeField]
    private Ease ease;
    private bool CanMove = true;
    private bool FirstMove = true;
    public void StartMovement()
    {
        if (!CanMove) { return; }
        CanMove = false;
        switch (typeOfMovement)
        {
            case TypeOfMovement.rotation:
                transform.DORotate(checkDestination() + new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z), duration).SetEase(ease).OnComplete(delegate { CanMove = true; });
                break;
            case TypeOfMovement.displacement:
                transform.DOMove(checkDestination() + transform.position, duration).SetEase(ease).OnComplete(delegate { CanMove = true; });
                break;
        }
        FirstMove = !FirstMove;
    }
    private Vector3 checkDestination()
    {
        if (FirstMove) { return vectorMovement; }
        return vectorMovement * -1;
    }
}
