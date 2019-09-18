using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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
    [SerializeField]
    private Sprite ReticuleInteract;
    [SerializeField]
    private Color InteractColor;
    [SerializeField]
    private TextMeshProUGUI InteractIndice;

    private Tweener ReticuleAnimation;

    public enum ReticuleState
    {
        Focused,
        Free,
        Interact
    }

    private ReticuleState currentReticuleState = ReticuleState.Focused;

    [SerializeField]
    private Image Spell;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        ChangeReticule(ReticuleState.Free);

    }

    public void ChangeReticule(ReticuleState NewState)
    {
        if(currentReticuleState == NewState) { return; }
        switch (NewState)
        {
            case (ReticuleState.Focused):
                Reticule.sprite = ReticuleFocused;
                Reticule.color = FocusedColor;
                InteractIndice.color = Color.clear;
                ResetReticule();
                break;
            case (ReticuleState.Free):
                Reticule.sprite = ReticuleFree;
                Reticule.color = FreeColor;
                InteractIndice.color = Color.clear;
                ResetReticule();
                break;
            case (ReticuleState.Interact):
                Reticule.sprite = ReticuleInteract;
                Reticule.color = InteractColor;
                InteractIndice.text = "E";
                InteractIndice.color = InteractColor;
                ReticuleAnimation = rotateReticule();
                break;
        }
        currentReticuleState = NewState;
    }

    public void ChangeSpell(Sprite SpellSprite)
    {
        Spell.sprite = SpellSprite;
    }

    private Tweener rotateReticule()
    {
        return Reticule.transform.DORotate(new Vector3(0, 0, Reticule.transform.rotation.z + 90), .25f).SetEase(Ease.Linear);
    }
    private void ResetReticule()
    {
        Reticule.transform.DORotate(new Vector3(0, 0, Reticule.transform.rotation.z - 90), 0f);
    }
}
