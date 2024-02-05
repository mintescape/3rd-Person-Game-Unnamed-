using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Attack //data for an attack, including it's animation name, runtime, blend to time, force it applies on the player
{
    [field: SerializeField] public string AnimationName { get; private set; } //nam eof animation
    [field: SerializeField] public float TransitionDuration { get; private set; } //time to blend from current anim to attack's anim
    [field: SerializeField] public int ComboStateIndex { get; private set; } = -1; //index of the next animation in the list to play after this attack's done
    [field: SerializeField] public float ComboAttackTime { get; private set; } //duration to wait before switching to the next animation, usually animation length
    [field: SerializeField] public float ForceTime { get; private set; } //time to apply force at
    [field: SerializeField] public float Force { get; private set; } //magnitude of forward force to apply to player
    [field: SerializeField] public float Knockback { get; private set; }
    [field: SerializeField] public int Damage { get; private set; } //actual damage number for the attack animation (not to be confused with WeaponDamage, whcih is a class that handles the damage logic and dealing)
}
