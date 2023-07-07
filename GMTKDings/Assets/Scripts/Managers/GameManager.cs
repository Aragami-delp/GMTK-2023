using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }
}
