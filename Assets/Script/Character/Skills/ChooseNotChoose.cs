using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChooseNotChoose
{
    public delegate void Choose();
    public static event Choose choose;

    private static void Start()
    {

    }

    public static void NotChoose()
    {
        choose();
    }
}
