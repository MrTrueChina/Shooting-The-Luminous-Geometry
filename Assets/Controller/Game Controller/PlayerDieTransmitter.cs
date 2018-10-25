using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieTransmitter : MonoBehaviour
{
    public GameController gameController
    {
        set { _gameController = value; }
    }
    GameController _gameController;

    private void OnDestroy()
    {
        _gameController.PlayerDie();
    }
}
