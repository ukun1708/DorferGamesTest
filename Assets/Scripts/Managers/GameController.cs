using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Singleton
    public static GameController instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public DynamicJoystick joystick;

    public GameObject player;

    public float playerMaxSpeed;

    [HideInInspector]
    public bool weaponActivate;

    public GameObject wheat;

    public GameObject wheatDestroyer;
}
