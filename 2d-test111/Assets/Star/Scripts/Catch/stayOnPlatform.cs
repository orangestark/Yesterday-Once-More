using System;
using System.Collections;
using System.Collections.Generic;
using Gamekit2D;
using UnityEngine;

public class stayOnPlatform : MonoBehaviour
{
    //public Transform transform;
    private bool _playerCaught;
    public GameObject player;
    private CharacterController2D _character;
    //private Transform _playerTransform;
    public GameObject platform;
    private Vector2 _lastPos;
    private bool _moved;
    [SerializeField] private float wierdOffset = 0f;

    void Start()
    {
        _character = player.GetComponent<CharacterController2D>();
        _lastPos = new Vector2(0, 0);
        _moved = false;
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        //col.transform.SetParent(transform);
        if (col.gameObject.name == "Ellen")
        {
            _playerCaught = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        //col.transform.SetParent(null);
        if (col.gameObject.name == "Ellen")
        {
            _playerCaught = false;
            _moved = false;
        }
    }

    void FixedUpdate()
    {
        Vector2 temp;
        int i;
        if (!_moved)
        {
            _lastPos = platform.transform.position;
            _moved = true;
        }
        else
        {
            temp.x = platform.transform.position.x - _lastPos.x + wierdOffset; 
            temp.y = platform.transform.position.y - _lastPos.y + wierdOffset;
            _character.Move(temp);
            Debug.Log(temp);
            _lastPos = platform.transform.position;
        }
    }
}
