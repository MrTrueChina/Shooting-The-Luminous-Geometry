using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrafabContainer : MonoBehaviour
{
    [SerializeField]
    GameController _gameController;
    [SerializeField]
    GameObject _enemyPrafab;

    public void SetPrefabToGameController()
    {
        _gameController.SetEnemyPrefab(_enemyPrafab);
    }
}
