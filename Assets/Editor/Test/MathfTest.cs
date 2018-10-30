using NUnit.Framework;
using UnityEngine;


[TestFixture]
public class MathfTest
{
    [Test]
    public void Mathf_Sqrt()
    {
        Debug.Log("1,1 -> " + Mathf.Sqrt(2));
        Debug.Log("1,2 -> " + Mathf.Sqrt(1 + 4));

        Debug.Log("Mathf的开方很准确，一般情况下不需要担心精度问题");
    }
}
