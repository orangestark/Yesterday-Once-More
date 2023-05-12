using System.Collections;
using System.Collections.Generic;
using Gamekit2D;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static bool usingController = false;

    private GameObject _player;

    private PlayerInput _playerInput;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Ellen");
        if (_player != null)
        {
            _playerInput = _player.GetComponent<PlayerInput>();
            if (usingController)
            {
                _playerInput.inputType = InputComponent.InputType.Controller;
            }
            else
            {
                _playerInput.inputType = InputComponent.InputType.MouseAndKeyboard;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            usingController = !usingController;
            Debug.Log("Using Controller: " + usingController);
            if (_playerInput != null)
            {
                if (usingController)
                {
                    _playerInput.inputType = InputComponent.InputType.Controller;
                }
                else
                {
                    _playerInput.inputType = InputComponent.InputType.MouseAndKeyboard;
                }
            }
        }
    }
}
