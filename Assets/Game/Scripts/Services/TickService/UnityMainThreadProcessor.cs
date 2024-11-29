using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityMainThreadProcessor : MonoBehaviour
{
    public Action OnMainThread = () => { };
    public Action OnLateMainThread = () => { };
    public Action OnFixedMainThread = () => { };

    // Update is called once per frame
    void Update()
    {
        OnMainThread.Invoke();
        OnMainThread = () => { };
    }

    private void LateUpdate()
    {
        OnLateMainThread.Invoke();
        OnLateMainThread = () => { };
    }

    private void FixedUpdate()
    {
        OnFixedMainThread.Invoke();
        OnFixedMainThread = () => { };
    }
}
