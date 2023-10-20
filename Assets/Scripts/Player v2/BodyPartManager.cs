using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BodyPartManager", menuName = "ScriptableObjects/BodyPartManager")]
public class BodyPartManager : ScriptableObject {


    public bool[] HasBodyPart { get; private set; } = new bool[Enum.GetNames(typeof(BodyPartType)).Length];

    public bool[] HasAbility { get; private set; } = new bool[Enum.GetNames(typeof(PlayerAbilities)).Length];


    public static event Action<BodyPartType> OnBodyPartAdded;
    public static event Action<BodyPartType> OnBodyPartRemoved;
    public static event Action<bool[]> OnAbiltiesChanged;

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
    /// Cannot remove the head.
    /// </summary>
    /// <param name="type">The type to remove</param>
    public void RemoveBodyPart(BodyPartType type) {
        if (type == BodyPartType.Head) return;
        HasBodyPart[(int)type] = false;
        OnBodyPartRemoved?.Invoke(type);
        Debug.Log("Removed Part " + (int)type);
        EvaluateAbilities();
    }

    /// <summary>
    /// Sets all bodyparts except the head as inactive. Re-evalutes players abilities
    /// </summary>
    public void ResetBodyParts() {
        for(int i = 0; i < Enum.GetNames(typeof(BodyPartType)).Length; i++) {
            if (HasBodyPart[i]) RemoveBodyPart( (BodyPartType) i );
        }
        AddBodyPart(BodyPartType.Head);
        EvaluateAbilities();
    }

    /// <summary>
    /// Invokes the events in the right way so that listeners on new scene load will store accurate information.
    /// </summary>
    public void UpdateEventListeners() {
        for(int i = 0; i < Enum.GetNames(typeof(BodyPartType)).Length; i++) {
            if (HasBodyPart[i]) AddBodyPart( (BodyPartType) i );
            else RemoveBodyPart( (BodyPartType) i );
        }
    }

    /// <summary>
    /// (re-)Evaluates the different abilities of the player.
    /// </summary>
    public void EvaluateAbilities() {
        HasAbility[(int)PlayerAbilities.Walk] = HasBodyPart[(int)BodyPartType.LeftLeg] && HasBodyPart[(int)BodyPartType.RightLeg];
        HasAbility[(int)PlayerAbilities.Jump] = HasBodyPart[(int)BodyPartType.LeftLeg] || HasBodyPart[(int)BodyPartType.RightLeg];
        HasAbility[(int)PlayerAbilities.Pull] = HasBodyPart[(int)BodyPartType.LeftArm] && HasBodyPart[(int)BodyPartType.RightArm];
        HasAbility[(int)PlayerAbilities.Push] = HasBodyPart[(int)BodyPartType.LeftArm] || HasBodyPart[(int)BodyPartType.RightArm];
        HasAbility[(int)PlayerAbilities.Roll] = HasBodyPart[(int)BodyPartType.Head]    && !HasAbility[(int)PlayerAbilities.Walk];
        Debug.Log("Re-evaluated abilities");
        OnAbiltiesChanged?.Invoke(HasAbility);
    }
}

[Serializable]
public enum BodyPartType {
    Head,      //0
    Torso,     //1
    RightArm,  //2
    LeftArm,   //3
    RightLeg,  //4
    LeftLeg    //5
}

public enum PlayerAbilities {
    Roll, 
    Walk,
    Jump,
    Push,
    Pull
}

