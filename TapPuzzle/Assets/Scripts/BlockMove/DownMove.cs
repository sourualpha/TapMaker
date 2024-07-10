using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Common;

public class DownMove : MonoBehaviour, IPointerClickHandler
{
    public Rigidbody rb;
    public float moveForce = Common.GrovalConst.MoveForce;//�����x
    private bool isClicked = false;//�N���b�N�������ۂ�
    GameManager gameManager;
    Vector3 sp;

    private void Start()
    {
        // Rigidbody2D�R���|�[�l���g���擾���܂�
        rb = GetComponent<Rigidbody>();
        sp= transform.localPosition;
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        Move();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("�N���b�N�C�x���g������");
        isClicked = true;
    }

    //���̃u���b�N�ɓ��������ۂ�HP�����炵�����ʒu�ɖ߂�
    private void OnCollisionEnter(Collision collision)
    {
        transform.position = sp;
        rb.velocity = Vector3.zero;
        if(isClicked == true) {
            gameManager.DecreaseIQ(Common.GrovalConst.DecreaseIQAmount);
        }
        isClicked = false;

        
    }

    void Move()
    {
        if (isClicked)
        {
            // �������Ɉړ�����
            rb.AddForce(Vector3.down * moveForce, ForceMode.Impulse);
        }
        if(transform.position.y < -Common.GrovalConst.ResetPosition)
        {
            Destroy(gameObject);

        }
    }
}