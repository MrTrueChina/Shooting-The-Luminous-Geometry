using NUnit.Framework;
using UnityEngine;


[TestFixture]
public class CreatFilesSpeedTest
{
    [Test]
    public void CreatFilesTime()
    {
        const int loopTime = 10000000;

        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

        timer.Start();
        for (int i = 0; i < loopTime; i++)
        {
            float f1 = i;
        }
        timer.Stop();
        Debug.Log("创建并赋值 " + loopTime + " 次，耗时 " + timer.ElapsedMilliseconds);


        timer.Reset();


        timer.Start();
        float f2 = 0;
        for (int i = 0; i < loopTime; i++)
        {
            f2 = i;
        }
        timer.Stop();
        Debug.Log("先创建后赋值 " + loopTime + " 次，耗时 " + timer.ElapsedMilliseconds);

        Debug.Log("创建字段耗时极短，一般不需要过度关心");
    }
}
