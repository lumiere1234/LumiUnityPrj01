using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyClass : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this); 
    }
}
