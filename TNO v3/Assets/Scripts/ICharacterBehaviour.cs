using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterBehaviour
{
    void MoveCharacter(float moveDirection, float characterSpeed);
    void Flip();
}
