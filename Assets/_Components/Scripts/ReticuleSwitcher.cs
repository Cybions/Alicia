using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticuleSwitcher : MonoBehaviour
{
    private void Update()
    {
        
    }
    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 20.0f))
        {
            if(hit.collider.tag == "Ennemy") { HUDManager.Instance.ChangeReticule(HUDManager.ReticuleState.Focused); checkTar();  return; }
            if (hit.collider.GetComponent<InteractionTarget>() && hit.collider.GetComponent<InteractionTarget>().isInteractable) { HUDManager.Instance.ChangeReticule(HUDManager.ReticuleState.Interact); PlayerController.Instance.CanInteract(hit.collider.GetComponent<InteractionTarget>()); return; }
            HUDManager.Instance.ChangeReticule(HUDManager.ReticuleState.Free);
            checkTar();
        }
        else
        {
            HUDManager.Instance.ChangeReticule(HUDManager.ReticuleState.Free);
            checkTar();
        }
    }

    private void checkTar()
    {
        if(PlayerController.Instance.interact_target != null) { PlayerController.Instance.CanInteract(null); }
    }
}
