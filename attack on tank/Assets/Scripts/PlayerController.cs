using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //移動速度
    public float _speed;

    //ジャンプ速度
    public float jumpSpeed;

    //回転速度
    public float rotateSpeed;

    //x軸方向の入力を保存
    private float _input_x;
    //z軸方向の入力を保存
    public float _input_z;

    private bool Grounded;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //x軸方向、z軸方向の入力を取得
        //Horizontal、水平、横方向のイメージ
        _input_x = Input.GetAxis("Horizontal");
        //Vertical、垂直、縦方向のイメージ
        _input_z = Input.GetAxis("Vertical");

        transform.rotation.ToAngleAxis(out float angle, out Vector3 axis);

        angle = transform.localEulerAngles.y % 360;

        if(angle < 0)
        {
            angle = 360 + angle;
        }

        //移動の向きなど座標関連はVector3で扱う
        Vector3 velocity = new Vector3((float)Math.Sin(angle * (Math.PI / 180)) * _input_z, 0, (float)Math.Cos(angle * (Math.PI / 180))*_input_z);
        //ベクトルの向きを取得
        Vector3 direction = velocity.normalized;

        //移動距離を計算
        float distance = _speed * Time.deltaTime;
        //移動先を計算
        Vector3 destination = transform.position + direction * distance;

        if (_input_z == 0)
        {
            transform.Rotate(new Vector3(0, _input_x * 0.5f * rotateSpeed, 0));
        }
        else if (_input_z > 0)
        {
            transform.Rotate(new Vector3(0, _input_x * (float)Math.Ceiling(_input_z) * 0.5f * rotateSpeed, 0));
        }
        else
        {
            transform.Rotate(new Vector3(0, _input_x * (float)Math.Floor(_input_z) * 0.5f * rotateSpeed, 0));
        }
        //移動先の座標を設定
        transform.position = destination;

        if (Grounded == true)//  もし、Groundedがtrueなら、
        {
            if (Input.GetKeyDown(KeyCode.Space))//  もし、スペースキーがおされたなら、  
            {
                Grounded = false;//  Groundedをfalseにする
                rb.AddForce(Vector3.up * jumpSpeed);//  上にJumpPower分力をかける
            }
        }
    }

    void OnCollisionEnter(Collision other)//  地面に触れた時の処理
    {
        if (other.gameObject.tag == "Ground")//  もしGroundというタグがついたオブジェクトに触れたら、
        {
            Grounded = true;//  Groundedをtrueにする
        }
    }
}
