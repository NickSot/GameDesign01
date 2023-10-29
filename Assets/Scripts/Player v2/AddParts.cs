using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class AddParts : MonoBehaviour
{
    //private bool[] _parts = new bool[6];
    private PlayerManager _player;

    [SerializeField] private GameObject _leftLegObject;
    [SerializeField] private GameObject _rightLegObject;
    [SerializeField] private GameObject _leftArmObject;
    [SerializeField] private GameObject _rightArmObject;
    [SerializeField] private GameObject _torsoObject;




    void Start()
    {
        _player = GetComponent<PlayerManager>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            if (_player.PartManager.HasBodyPart[(int)BodyPartType.LeftLeg]) {
                _player.PartManager.RemoveBodyPart(BodyPartType.LeftLeg);
                Instantiate(_leftLegObject, transform.position + 0.8f * Vector3.up, Quaternion.identity);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            if (_player.PartManager.HasBodyPart[(int)BodyPartType.RightLeg]) {
                _player.PartManager.RemoveBodyPart(BodyPartType.RightLeg);
                Instantiate(_rightLegObject, transform.position + 0.8f * Vector3.up, Quaternion.identity);
            }
        }
        if( Input.GetKeyDown(KeyCode.Alpha3)) {
            if (_player.PartManager.HasBodyPart[(int)BodyPartType.LeftArm]) {
                _player.PartManager.RemoveBodyPart(BodyPartType.LeftArm);
                Instantiate(_leftArmObject, transform.position + 0.625f * Vector3.up, Quaternion.identity);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            if (_player.PartManager.HasBodyPart[(int)BodyPartType.RightArm]) {
                _player.PartManager.RemoveBodyPart(BodyPartType.RightArm);
                Instantiate(_rightArmObject, transform.position + 0.625f * Vector3.up, Quaternion.identity);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            if (_player.PartManager.HasBodyPart[(int)BodyPartType.Torso]) {
                _player.PartManager.RemoveBodyPart(BodyPartType.Torso);
                Instantiate(_torsoObject, transform.position + 0.625f * Vector3.up, Quaternion.identity);
            }
        }
    }
}























