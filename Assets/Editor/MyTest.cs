﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;


[TestFixture]
public class MyTest
{
    [Test]
    public void Mathf_()
    {
        Debug.Log("1,1 -> " + Mathf.Sqrt(2));
        Debug.Log("1,2 -> " + Mathf.Sqrt(1 + 4));
    }
}