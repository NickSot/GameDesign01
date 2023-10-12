using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class AddParts : MonoBehaviour
{
    //private bool[] _parts = new bool[6];
    private PlayerManager _player;

    void Start()
    {
        _player = GetComponent<PlayerManager>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            if (!_player.PartManager.HasBodyPart[(int)BodyPartType.Head]) _player.PartManager.AddBodyPart(BodyPartType.Head);
            else  _player.PartManager.RemoveBodyPart(BodyPartType.Head);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            if (!_player.PartManager.HasBodyPart[(int)BodyPartType.LeftLeg]) _player.PartManager.AddBodyPart(BodyPartType.LeftLeg);
            else  _player.PartManager.RemoveBodyPart(BodyPartType.LeftLeg);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            if (!_player.PartManager.HasBodyPart[(int)BodyPartType.RightLeg]) _player.PartManager.AddBodyPart(BodyPartType.RightLeg);
            else  _player.PartManager.RemoveBodyPart(BodyPartType.RightLeg);
        }
        if( Input.GetKeyDown(KeyCode.Alpha4)) {
            if (!_player.PartManager.HasBodyPart[(int)BodyPartType.LeftArm]) _player.PartManager.AddBodyPart(BodyPartType.LeftArm);
            else  _player.PartManager.RemoveBodyPart(BodyPartType.LeftArm);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            if (!_player.PartManager.HasBodyPart[(int)BodyPartType.RightArm]) _player.PartManager.AddBodyPart(BodyPartType.RightArm);
            else  _player.PartManager.RemoveBodyPart(BodyPartType.RightArm);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            if (!_player.PartManager.HasBodyPart[(int)BodyPartType.Torso]) _player.PartManager.AddBodyPart(BodyPartType.Torso);
            else  _player.PartManager.RemoveBodyPart(BodyPartType.Torso);
        }
    }
}



