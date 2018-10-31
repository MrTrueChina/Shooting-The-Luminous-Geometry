using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StandardValue
{
    /// <summary>
    /// 摄像机视野边界
    /// </summary>
    public static readonly Border viewBorder = new Border(600, 375, -600, -375);

    /// <summary>
    /// 子弹移动边界
    /// </summary>
    public static readonly Border bulletMoveBorder = new Border(650, 425, -650, -425);
}
