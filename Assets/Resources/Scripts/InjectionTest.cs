using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InjectionTest : MonoBehaviour
{
    private ITest _test;
    [Inject]
    public void Init(ITest t)
    {
        _test = t;
    }

    private void Start()
    {
        _test.Echo();
    }
}
