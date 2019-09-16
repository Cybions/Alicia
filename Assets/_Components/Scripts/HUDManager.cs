using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    [SerializeField]
    private Image Reticule;
    [SerializeField]
    private Sprite ReticuleFocused;
    [SerializeField]
    private Color FocusedColor;
    [SerializeField]
    private Sprite ReticuleFree;
    [SerializeField]
    private Color FreeColor;
    private bool CurrentReticuleIsFocused = false;

    [SerializeField]
    private Image Spell;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        ChangeReticule(false);
    }

    public void ChangeReticule(bool isFocused)
    {
        if(CurrentReticuleIsFocused == isFocused) { return; }
        if (isFocused)
        {
            Reticule.sprite = ReticuleFocused;
            Reticule.color = FocusedColor;
        }
        else
        {
            Reticule.sprite = ReticuleFree;
            Reticule.color = FreeColor;
        }
        CurrentReticuleIsFocused = isFocused;
    }

    public void ChangeSpell(Sprite SpellSprite)
    {
        Spell.sprite = SpellSprite;
    }
}
