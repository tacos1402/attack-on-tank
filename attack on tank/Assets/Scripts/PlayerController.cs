using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�ړ����x
    public float _speed;

    //�W�����v���x
    public float jumpSpeed;

    //��]���x
    public float rotateSpeed;

    //x�������̓��͂�ۑ�
    private float _input_x;
    //z�������̓��͂�ۑ�
    public float _input_z;

    private bool Grounded;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //x�������Az�������̓��͂��擾
        //Horizontal�A�����A�������̃C���[�W
        _input_x = Input.GetAxis("Horizontal");
        //Vertical�A�����A�c�����̃C���[�W
        _input_z = Input.GetAxis("Vertical");

        transform.rotation.ToAngleAxis(out float angle, out Vector3 axis);

        angle = transform.localEulerAngles.y % 360;

        if(angle < 0)
        {
            angle = 360 + angle;
        }

        //�ړ��̌����ȂǍ��W�֘A��Vector3�ň���
        Vector3 velocity = new Vector3((float)Math.Sin(angle * (Math.PI / 180)) * _input_z, 0, (float)Math.Cos(angle * (Math.PI / 180))*_input_z);
        //�x�N�g���̌������擾
        Vector3 direction = velocity.normalized;

        //�ړ��������v�Z
        float distance = _speed * Time.deltaTime;
        //�ړ�����v�Z
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
        //�ړ���̍��W��ݒ�
        transform.position = destination;

        if (Grounded == true)//  �����AGrounded��true�Ȃ�A
        {
            if (Input.GetKeyDown(KeyCode.Space))//  �����A�X�y�[�X�L�[�������ꂽ�Ȃ�A  
            {
                Grounded = false;//  Grounded��false�ɂ���
                rb.AddForce(Vector3.up * jumpSpeed);//  ���JumpPower���͂�������
            }
        }
    }

    void OnCollisionEnter(Collision other)//  �n�ʂɐG�ꂽ���̏���
    {
        if (other.gameObject.tag == "Ground")//  ����Ground�Ƃ����^�O�������I�u�W�F�N�g�ɐG�ꂽ��A
        {
            Grounded = true;//  Grounded��true�ɂ���
        }
    }
}
