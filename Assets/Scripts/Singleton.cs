using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton instance { get; private set; }
    public CameraManager cameraMngr { get; private set; }
    //public EnvironmentManager environmentMngr { get; private set; }

}
