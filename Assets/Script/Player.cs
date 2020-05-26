﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region 欄位屬性
    [Header("移動速度"), Range(1, 1000)]
    public float Speed = 10;
    [Header("跳躍高度"), Range(1, 5000)]
    public float Height;

    /// <summary>
    /// 是否在地板上
    /// </summary>
    private bool isGround
    {
        get
        {
            if (transform.position.y < 0.096f) return true; // 如果 Y 軸 小於 0.051 傳回 true
            else return false;                              // 否則 傳回 false
        }
    }

    /// <summary>
    /// 旋轉角度
    /// </summary>
    private Vector3 angle;
    
    private Animator ani;
    private Rigidbody rig;

    /// <summary>
    ///  跳躍力道：從 0 慢慢增加
    /// </summary>
    private float jump;
    #endregion

    #region 方法
    /// <summary>
    /// 移動：透過鍵盤
    /// </summary>
    private void Move()
    {
        //if (rig.velocity.magnitude > 5) return;

        #region 移動        
        // 浮點數 前後值 = 輸入類別.取得軸向值("垂直") - 垂直 WS 上下
        float v = Input.GetAxisRaw("Vertical");
        // 水平 AD 左右
        float h = Input.GetAxisRaw("Horizontal");

        //if(v != 0)
        //{
        //    h = 0;
        //}

        // 剛體.添加推力(x, y, z) - 世界座標
        // rig.AddForce(0, 0, Speed*v);
        // 剛體.添加推力(三圍向量)
        // 前方 transform.forward - Z軸
        // 右方 transform.right - X軸
        // 上方 transform.up - Y軸
        rig.AddForce(transform.forward * Speed * Mathf.Abs(v));
        rig.AddForce(transform.forward * Speed * Mathf.Abs(h));

        // 動畫.設定布林值("跑步參數", 布林值) - 當前後取絕對值 > 0 時勾選
        if(Mathf.Abs(v) > 0||Mathf.Abs(h) > 0)
        {
            ani.SetBool("跑步開關", true);
        }
        else
        {
            ani.SetBool("跑步開關", false);
        }
        //ani.SetBool("跑步開關", Mathf.Abs(h) > 0);
        #endregion

        #region 轉向
        if (v == 1) angle = new Vector3(0, 0, 0);               // 前 Y 0度
        else if (v == -1) angle = new Vector3(0, 180, 0);       // 後 Y 180度
        else if (h == 1) angle = new Vector3(0, 90, 0);         // 右 Y 90度
        else if (h == -1) angle = new Vector3(0, 270, 0);       // 左 Y 270度
        // 只要類別後面有：MonoBehaviour
        // 就可以直接使用關鍵字 transform 取得此物件的 Transform 元件
        // eulerAngles 尤拉角度 0~360度
        transform.eulerAngles = angle;
        #endregion

    }

    /// <summary>
    /// 跳躍：判斷在地板上並按下空白鍵時跳躍
    /// </summary>
    private void Jump()
    {
        // 如果 在地板上 為 勾選 並且 按下空白鍵
        if (isGround && Input.GetButtonDown("Jump"))
        {
            // 每次跳躍 值都從 0 開始
            jump = 0;
            // 剛體.推力(0,跳躍高度,0)
            rig.AddForce(0, Height, 0);
        }
        // 如果 不在地板上(在空中)
        if (!isGround)
        {
            // 跳躍 遞增 時間.一禎時間
            jump += Time.deltaTime;
        }
        // 動畫.設定浮點數("跳躍參數",跳躍時間)
        ani.SetFloat("跳躍力道", jump);
    }

    /// <summary>
    /// 碰到道具：碰到帶有標籤[HotDog]的物件
    /// </summary>
    private void HitProp()
    {

    }
    #endregion

    #region 事件
    private void Start()
    {
        // GetComponent<泛型>() 泛型方法 - 泛型 所有類型 Rigidbody, Transform, Collider...
        // 剛體 = 取得元件<剛體>();
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
    }

    // 固定更新頻率：1 秒 50 禎，使用物理(含Rigidbody)必須在此事件內(官網建議)
    private void FixedUpdate()
    {
        Move();
    }

    // 更新事件：1 秒約 60 禎
    private void Update()
    {
        Jump();
    }
    #endregion



}