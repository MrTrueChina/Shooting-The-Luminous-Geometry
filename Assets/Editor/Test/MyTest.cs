using NUnit.Framework;
using UnityEngine;


[TestFixture]
public class MyTest
{
    [Test]
    public void Mathf_Sqrt()
    {
        Debug.Log("1,1 -> " + Mathf.Sqrt(2));
        Debug.Log("1,2 -> " + Mathf.Sqrt(1 + 4));
    }
}
