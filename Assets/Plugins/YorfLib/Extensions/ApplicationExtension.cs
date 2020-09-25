using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ApplicationHelper
{
    public static bool IsQuitting { get; private set;}

    [RuntimeInitializeOnLoadMethod]
    static void RunOnStart()
    {
        IsQuitting = false;
        Application.quitting += Quit;
    }

    private static void Quit()
    {
        IsQuitting = true;
    }
}
