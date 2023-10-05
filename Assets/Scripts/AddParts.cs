using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class AddParts : MonoBehaviour
{
    private bool[] _parts = new bool[6];
    private PlayerManager _player;

    void Start()
    {
        _player = GetComponent<PlayerManager>();    
        for(int i = 0; i < 6; i++) {
            _parts[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            _parts[0] = !_parts[0];
            if (_parts[0]) _player.PartManager.AddBodyPart(BodyPartType.Head);
            else        _player.PartManager.RemoveBodyPart(BodyPartType.Head);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            _parts[1] = !_parts[1];
            if (_parts[1]) _player.PartManager.AddBodyPart(BodyPartType.LeftLeg);
            else        _player.PartManager.RemoveBodyPart(BodyPartType.LeftLeg);
        }
        if ( Input.GetKeyDown(KeyCode.Alpha3)) {
            _parts[2] = !_parts[2];
            if (_parts[2]) _player.PartManager.AddBodyPart(BodyPartType.RightLeg);
            else        _player.PartManager.RemoveBodyPart(BodyPartType.RightLeg);
        }
        if( Input.GetKeyDown(KeyCode.Alpha4)) {
            _parts[3] = !_parts[3];
            if (_parts[3]) _player.PartManager.AddBodyPart(BodyPartType.LeftArm);
            else        _player.PartManager.RemoveBodyPart(BodyPartType.LeftArm);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            _parts[4] = !_parts[4];
            if (_parts[4]) _player.PartManager.AddBodyPart(BodyPartType.RightArm);
            else        _player.PartManager.RemoveBodyPart(BodyPartType.RightArm);
        }
        if(Input.GetKeyDown(KeyCode.Alpha6)) {
            _parts[5] = !_parts[5];
            if (_parts[5]) _player.PartManager.AddBodyPart(BodyPartType.Torso);
            else        _player.PartManager.RemoveBodyPart(BodyPartType.Torso);
        }
    }
}
