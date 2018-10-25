/*
 *  执行顺序要在所有子弹控制器之前，抢在所有控制器执行Start开始控制前把他们都关掉
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpownEffect : BulletContorllerBase
{
    /*
     *  第一步是抢先一步关闭所有子弹控制器
     */


    //关闭所有控制器
    private void Start()
    {
        DisableBulletControllers();
    }
    void DisableBulletControllers()   //假设有的控制器本来就是关闭的会不会被重新打开
    {
        BulletContorllerBase[] controllers = GetComponents<BulletContorllerBase>();
        foreach (BulletContorllerBase controller in controllers)
            if (controller != this)
                controller.enabled = false;
    }



}
