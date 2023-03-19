using System;
using System.Collections;
using System.Collections.Generic;
using Gamekit2D;
using UnityEngine;

public class stayOnBox : MonoBehaviour
{
    //public Transform transform;
    private bool _playerCaught;
    public GameObject player;
    private CharacterController2D _character;
    //private Transform _playerTransform;
    public TimeBackObject self;
    private ObjectStage StageData = new ObjectStage();
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
        if (_playerCaught && self.isForwarding)
        {
            if (self.forwardCounter >= self.TimeForwardData.Count)
            {
                i = 0;
            }
            else
            {
                i = self.forwardCounter;
            }

            if (!_moved)
            {
                _lastPos = self.TimeForwardData[i].Position;
                _moved = true;
            }
            else
            {
                temp.x = self.TimeForwardData[i].Position.x - _lastPos.x + wierdOffset; 
                temp.y = self.TimeForwardData[i].Position.y - _lastPos.y + wierdOffset;
                _character.Move(temp);
                Debug.Log(temp);
                _lastPos = self.TimeForwardData[i].Position;
            }
        }
    }
}
