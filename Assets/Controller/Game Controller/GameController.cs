/*
 *  总控器
 *  
 *  包括关卡开始和结束
 *  
 *  开始
 *      隐藏UI
 *      生成玩家和敌人         需要玩家和敌人的预制，敌人可以通过存入解决，拖拽一个默认的来测试
 *          最好是移动进场
 */

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
    Vector3 _playerEntrancePosition;
    [SerializeField]
    Vector3 _enemyEntrancePosition;


    //选关（存入敌人预制）
    public void SetEnemyPrefab(GameObject enemyPrefab)
    {
        _enemyPrefab = enemyPrefab;
    }



    //开始游戏
    public void StartGame()
    {
        InstantiatePlayer();
        InstantiateEnemy();
    }
    void InstantiatePlayer()
    {
        GameObject player = Instantiate(_playerPrefab, _playerEntrancePosition, Quaternion.identity);
        player.AddComponent<PlayerDieTransmitter>().gameController = this;
    }
    void InstantiateEnemy()
    {
        GameObject enemy = Instantiate(_enemyPrefab, _enemyEntrancePosition, Quaternion.identity);
        enemy.AddComponent<EnemyDieTransmitter>().gameController = this;
    }



    public void EnemyDie()
    {
        Debug.Log("Enemy Die");
    }




    public void PlayerDie()
    {
        Debug.Log("Player Die");

        _UIController.OpenRestartCanvas();
    }
}
