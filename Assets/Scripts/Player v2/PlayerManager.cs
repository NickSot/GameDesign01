using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public BodyPartManager PartManager;

    private void Start() {
        PartManager.EvaluateAbilities();
        PartManager.UpdateEventListeners();
        PartManager.EvaluateAbilities();
        PartManager.AddBodyPart(BodyPartType.Head);
    }


    //public static event Action<PlayerState> OnPlayerStateChanged;
    //public PlayerState State {  get; private set; }
    //public void ChangeState(PlayerState newState) {
    //    if (State == newState) return;
    //    State = newState;

    //    switch (State) {
    //        case PlayerState.Starting:
    //            handleStarting();
    //            break;
    //        case PlayerState.Idle:
    //            handleIdle();
    //            break;
    //        case PlayerState.Moving:
    //            handleMoving();
    //            break;
    //        case PlayerState.Jumping:
    //            handleJumping();
    //            break;
    //        default:
    //            throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
    //    }
    //    OnPlayerStateChanged?.Invoke(newState);
    //    return;

    //    void handleStarting(){

    //    }

    //    void handleIdle() {

    //    }

    //    void handleMoving() {

    //    }

    //    void handleJumping() {

    //    }
    //}
}

[Serializable]
public enum PlayerState {
    Starting, 
    Idle,
    Moving,
    Jumping
}