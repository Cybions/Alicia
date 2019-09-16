using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Alicia/Spell", order = 1)]
public class Spell : ScriptableObject
{
    public Sprite SpellIcon;
    public string SpellName;
}
