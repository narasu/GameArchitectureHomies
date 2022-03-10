using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandPlayerMove : ICommandVector2
{
    CharacterController charCtrl;
    float speed;

    public CommandPlayerMove(CharacterController _charCtrl, float _speed)
    {
        charCtrl = _charCtrl;
        speed = _speed;
    }

    public void Execute(Vector2Command _vector2)
    {
        Vector3 input = new Vector3(_vector2.pRawValue.x, 0f, _vector2.pRawValue.y).normalized;
        Vector3 movement = charCtrl.transform.right * input.x + charCtrl.transform.forward * input.z;
        charCtrl.Move(movement * speed * Time.deltaTime);
    }
}
