using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    [SerializeField]
    private List<Spell> SpellList;
    private int CurrentSpell = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0){
            ChangeSpell(1);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            ChangeSpell(-1);
        }
    }

    private void ChangeSpell(int value)
    {
        CurrentSpell += value;
        if(CurrentSpell < 0) { CurrentSpell = SpellList.Count - 1; }
        if(CurrentSpell > SpellList.Count - 1) { CurrentSpell = 0; }

        HUDManager.Instance.ChangeSpell(SpellList[CurrentSpell].SpellIcon);
    }
}
