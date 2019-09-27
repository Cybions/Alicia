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
    private Vector3 VectorOrigin;

    private void Start()
    {

        VectorOrigin = this.transform.position;
        if(typeOfMovement == TypeOfMovement.rotation)
        {
            VectorOrigin = new Vector3(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z);
        }
        vectorMovement = VectorOrigin + vectorMovement;
    }

    public void StartMovement()
    {
        if (!CanMove) { return; }
        CanMove = false;
        switch (typeOfMovement)
        {
            case TypeOfMovement.rotation:
                transform.DORotate(checkDestination(), duration).SetEase(ease).OnComplete(delegate { CanMove = true; });
                break;
            case TypeOfMovement.displacement:
                transform.DOMove(checkDestination(), duration).SetEase(ease).OnComplete(delegate { CanMove = true; });
                break;
        }
        FirstMove = !FirstMove;
    }
    private Vector3 checkDestination()
    {
        if (FirstMove) { return vectorMovement; }
        return VectorOrigin;
    }
}
