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
    private bool isJumping = false;

    [SerializeField]
    public PostProcessVolume sprintVolume;
    Tweener TransitionVolume;

    Tweener MovementTweener;
    float origin;
    private void Start()
    {
        origin = transform.localPosition.y;
    }

    private void Update()
    {
        if (playerController.isMoving && !isMoving && !isJumping)
        {
            isMoving = true;
            StartCoroutine(f_HeadMovement());
        }

        if ((playerController.isSprinting && playerController.isMoving) && !isSprinting )
        {
            isSprinting = true;
            StopCoroutine(PlayTransition()); StartCoroutine(PlayTransition());
        }else if((!playerController.isSprinting || !playerController.isMoving) && isSprinting)
        {
            isSprinting = false;
            StopCoroutine(PlayTransition()); StartCoroutine(PlayTransition());
        }
            if (Input.GetKeyDown(KeyCode.Space))
        {

            Jump();
        }
    }

    IEnumerator f_HeadMovement()
    {
        while (playerController.isMoving)
        {
            float animDuration = .6f;
            if (isSprinting) { animDuration = .3f; /*if (!isSprinting) { isSprinting = true;   } } else { if (isSprinting) { isSprinting = false; StopCoroutine(PlayTransition()); StartCoroutine(PlayTransition()); }*/ }
            yield return new WaitForSeconds(animDuration);
            MovementTweener = transform.DOLocalMoveY(origin - .03f, animDuration/2).OnComplete(delegate { transform.DOLocalMoveY(origin + .03f, animDuration/2); });
            yield return new WaitWhile(()=> TweenerStatus() == true);
        }
        isMoving = false;
    }

    IEnumerator PlayTransition()
    {
        if(isSprinting)
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
        if (playerController.isSprinting && !isSprinting && !isJumping)
        {
            MovementTweener.Complete();
        }
        return MovementTweener.IsPlaying();
    }

    public void Jump()
    {
        if (isJumping) { return; }

        MovementTweener.Complete();
        
        StopCoroutine(f_HeadMovement());
        StopCoroutine(PlayTransition());
        isMoving = false;
        isSprinting = false;
        transform.DOLocalMoveY(origin, 0);
        StartCoroutine(PlayJump());

    }

    private IEnumerator PlayJump()
    {
        isJumping = true;
        float animDuration = .2f;
        transform.parent.GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Impulse);
        MovementTweener = transform.DOLocalMoveY(origin - .5f, animDuration);
        yield return new WaitWhile(() => TweenerStatus() == true);
        
        MovementTweener = transform.DOLocalMoveY(origin, animDuration);
        
        yield return new WaitWhile(() => TweenerStatus() == true);
        isJumping = false;

    }
}
