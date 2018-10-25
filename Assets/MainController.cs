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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField]
    GameObject _enemyPrefab;
    [SerializeField]
    GameObject _playerPrefab;



    //选关（存入敌人预制）
    public void SetEnemyPrefab(GameObject enemyPrefab)
    {
        _enemyPrefab = enemyPrefab;
    }
}
