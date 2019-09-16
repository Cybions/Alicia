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
            if(hit.collider.tag != "Ennemy") { HUDManager.Instance.ChangeReticule(false);  return; }
            HUDManager.Instance.ChangeReticule(true);
        }
        else
        {
            HUDManager.Instance.ChangeReticule(false);
        }
    }
}
