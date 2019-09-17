using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;

public class HeadMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    private bool isMoving = false;
    private bool isSprinting = false;

    [SerializeField]
    public PostProcessVolume sprintVolume;
    Tweener TransitionVolume;

    Tweener MovementTweener;

    private void Update()
    {
        if (playerController.isMoving && !isMoving)
        {
            isMoving = true;
            StartCoroutine(f_HeadMovement());
        }
    }

    IEnumerator f_HeadMovement()
    {
        while (playerController.isMoving)
        {
            float animDuration = .6f;
            if (playerController.isSprinting) { animDuration = .3f; if (!isSprinting) { isSprinting = true;  StopCoroutine(PlayTransition()); StartCoroutine(PlayTransition()); } } else { if (isSprinting) { isSprinting = false; StopCoroutine(PlayTransition()); StartCoroutine(PlayTransition()); } }
            yield return new WaitForSeconds(animDuration);
            MovementTweener = transform.DOMoveY(transform.position.y - .03f, animDuration/2).OnComplete(delegate { transform.DOMoveY(transform.position.y + .03f, animDuration/2); });
            yield return new WaitWhile(()=> TweenerStatus() == true);
        }
        isMoving = false;
    }

    IEnumerator PlayTransition()
    {
        if(playerController.isSprinting)
        {
            sprintVolume.gameObject.SetActive(true);
            while (sprintVolume.weight < 1)
            {
                sprintVolume.weight += .03f;
                yield return new WaitForSeconds(0.01f);
            }
            sprintVolume.weight = 1;
        }
        else
        {
            while (sprintVolume.weight > 0)
            {
                sprintVolume.weight -= .05f;
                yield return new WaitForSeconds(0.01f);
            }
            sprintVolume.weight = 0;
            sprintVolume.gameObject.SetActive(false);
        }
    }

    private bool TweenerStatus()
    {
        if (playerController.isSprinting && !isSprinting)
        {
            MovementTweener.Complete();
        }
        return MovementTweener.IsPlaying();
    }

}
