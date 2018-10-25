using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    UIController _UIController;

    [SerializeField]
    GameObject _enemyPrefab;
    [SerializeField]
    GameObject _playerPrefab;

    [SerializeField]
    Vector3 _playerStartEntrancePosition;
    [SerializeField]
    Vector3 _playerEntrancePosition;
    [SerializeField]
    Vector3 _enemyStartEntrancePosition;
    [SerializeField]
    Vector3 _enemyEntrancePosition;

    [SerializeField]
    float _entranceTime;
    

    List<GameObject> _enemys = new List<GameObject>();



    //选关（存入敌人预制）
    public void SetEnemyPrefab(GameObject enemyPrefab)
    {
        _enemyPrefab = enemyPrefab;
    }



    //开始游戏，创建玩家和敌人，入场
    public void StartGame()
    {
        StartCoroutine(DoStartGame());
    }
    IEnumerator DoStartGame()
    {
        GameObject player = InstantiatePlayer();
        GameObject enemy = InstantiateEnemy();

        PlayerController playerController = player.GetComponent<PlayerController>();
        EnemyController enemyController = enemy.GetComponent<EnemyController>();

        playerController.enabled = false;
        enemyController.enabled = false;

        StartCoroutine(Entrance(player.transform, _playerStartEntrancePosition, _playerEntrancePosition, _entranceTime));
        StartCoroutine(Entrance(enemy.transform, _enemyStartEntrancePosition, _enemyEntrancePosition, _entranceTime));

        yield return new WaitForSeconds(_entranceTime);

        playerController.enabled = true;
        enemyController.enabled = true;
    }
    GameObject InstantiatePlayer()
    {
        GameObject player = Instantiate(_playerPrefab, _playerEntrancePosition, Quaternion.identity);
        player.AddComponent<PlayerDieTransmitter>().gameController = this;

        return player;
    }
    GameObject InstantiateEnemy()
    {
        GameObject enemy = Instantiate(_enemyPrefab, _enemyEntrancePosition, Quaternion.identity);
        enemy.AddComponent<EnemyDieTransmitter>().gameController = this;
        _enemys.Add(enemy);

        return enemy;
    }
    IEnumerator Entrance(Transform transform, Vector3 startPosition, Vector3 endPosition, float entranceTime)
    {
        float elapsedTime = 0;
        while (elapsedTime < entranceTime)
        {
            float positionRate = Mathf.InverseLerp(0, entranceTime, elapsedTime);
            transform.position = Vector3.Lerp(startPosition, endPosition, positionRate);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        transform.position = endPosition;
    }



    //敌人被击破
    public void EnemyDie(GameObject enemy)
    {
        _enemys.Remove(enemy);
        if (_enemys.Count == 0)
            PlayerWin();
        Debug.Log("Enemy Die");
    }
    void PlayerWin()
    {
        Debug.Log("WIN");
    }


    
    //玩家被击落
    public void PlayerDie()
    {
        Debug.Log("Player Die");

        _UIController.OpenRestartCanvas();
    }
}
