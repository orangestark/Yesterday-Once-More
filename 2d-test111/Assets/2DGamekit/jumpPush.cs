using System;
using System.Collections;
using System.Collections.Generic;
using Gamekit2D;
using UnityEngine;

public class jumpPush : MonoBehaviour {
    public float addHeight = 0.5f; // 一次碰撞往上的高度
    public float upTime = 0.2f; // 往上的时间
    public GameObject platform;
    
    private float originY;
    private float currentY;
    private bool isCollider = false;
    
    private bool isUp = false;
    private bool isDown = false;
    private float time = 0.0f;
    void Start()
    {
        originY = platform.transform.position.y;
        print("origin height: "+originY);
    }

    private void Update() {
        if (isCollider) {
            var nowPos = transform.position;
            time += Time.deltaTime;
            if (isUp) {
                nowPos += Vector3.up * addHeight * Time.deltaTime;
            } else if (isDown) {
                nowPos -= Vector3.up * addHeight * Time.deltaTime;
            }
            // 上升结束，开始下降
            if (isUp && time >= upTime) {
                isUp = false;
                isDown = true;
                time = 0.0f;
            }
            // 还原到原本位置
            if(isDown && Mathf.Abs(nowPos.y - originY)<0.02f){
                isUp = false;
                isDown = false;
                time = 0.0f;
                nowPos.y = originY;
                isCollider = false;
            }
            transform.position = nowPos;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // 检测是不是玩家碰撞
        var chartController = collision.gameObject.GetComponent<CharacterController2D>();
        if (null == chartController) {
            return;
        }
        //从下方碰撞才触发
        if(collision.contacts[0].normal.y != 1)
        {
            return;
        }
        if(isCollider){
            return;
        }

        isCollider = true;
        isUp = true;
        print("collided");
    }
}