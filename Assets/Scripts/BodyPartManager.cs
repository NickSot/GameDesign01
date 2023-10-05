using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BodyPartManager", menuName = "ScriptableObjects/BodyPartManager")]
public class BodyPartManager : ScriptableObject {


    private static readonly int _totalAmountOfBodyParts = 6;
    public bool[] HasBodyPart { get; private set; } = new bool[_totalAmountOfBodyParts];

    public bool[] HasAbility { get; private set; } = new bool[5];


    public static event Action<BodyPartType> OnBodyPartAdded;
    public static event Action<BodyPartType> OnBodyPartRemoved;
    public static event Action OnAbiltiesChanged;

    /// <summary>
    /// Sets the bodypart as active. Re-evalutes players abilities. Also invokes the OnBodyPartAdded event. 
    /// </summary>
    /// <param name="type">The type to add</param>
    public void AddBodyPart(BodyPartType type) {
        HasBodyPart[(int)type] = true;
        OnBodyPartAdded?.Invoke(type);
        Debug.Log("Added Part " + (int)type);
        EvaluateAbilities();
    }

    /// <summary>
    /// Sets the bodypart as active. Re-evalutes players abilities. Also invokes the OnBodyPartRemoved event.
    /// </summary>
    /// <param name="type">The type to remove</param>
    public void RemoveBodyPart(BodyPartType type) {
        HasBodyPart[(int)type] = false;
        OnBodyPartRemoved?.Invoke(type);
        EvaluateAbilities();
    }

    /// <summary>
    /// Sets all bodyparts except the head as inactive. Re-evalutes players abilities
    /// </summary>
    public void ResetBodyParts() {
        for(int i = 0; i < _totalAmountOfBodyParts; i++) {
            if (HasBodyPart[i]) RemoveBodyPart( (BodyPartType) i );
        }
        AddBodyPart(BodyPartType.Head);
        EvaluateAbilities();
    }

    /// <summary>
    /// (re-)Evaluates the different abilities of the player.
    /// </summary>
    private void EvaluateAbilities() {
        HasAbility[(int)PlayerAbilities.Walk] = HasBodyPart[(int)BodyPartType.LeftLeg] && HasBodyPart[(int)BodyPartType.RightLeg];
        HasAbility[(int)PlayerAbilities.Jump] = HasBodyPart[(int)BodyPartType.LeftLeg] || HasBodyPart[(int)BodyPartType.RightLeg];
        HasAbility[(int)PlayerAbilities.Pull] = HasBodyPart[(int)BodyPartType.LeftArm] && HasBodyPart[(int)BodyPartType.RightArm];
        HasAbility[(int)PlayerAbilities.Push] = HasBodyPart[(int)BodyPartType.LeftArm] || HasBodyPart[(int)BodyPartType.RightArm];
        HasAbility[(int)PlayerAbilities.Roll] = HasBodyPart[(int)BodyPartType.Head]    && !HasAbility[(int)PlayerAbilities.Walk];
        Debug.Log("Re-evaluated abilities");
        OnAbiltiesChanged?.Invoke();
    }
}

[Serializable]
public enum BodyPartType {
    Head,
    Torso,
    RightArm,
    LeftArm,
    RightLeg,
    LeftLeg
}

public enum PlayerAbilities {
    Roll, 
    Walk,
    Jump,
    Push,
    Pull
}