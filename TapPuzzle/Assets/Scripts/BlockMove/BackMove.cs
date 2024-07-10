using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Common;

public class BackMove : MonoBehaviour, IPointerClickHandler
{
    public Rigidbody rb;
    public float moveForce = Common.GrovalConst.MoveForce;
    private bool isClicked = false;
    GameManager gameManager;

    Vector3 sp;

    private void Start()
    {
        // Rigidbody2D�R���|�[�l���g���擾���܂�
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        sp = transform.position;
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

    void Move()
    {
        if (isClicked)
        {
            // �������Ɉړ�����
            rb.AddForce(Vector3.forward * moveForce, ForceMode.Impulse);
        }
        if(transform.position.z > Common.GrovalConst.ResetPosition)
        {
            Destroy(gameObject);
        }
    }

    //���̃u���b�N�ɓ��������ۂ�HP�����炵�����ʒu�ɖ߂�
    private void OnCollisionEnter(Collision collision)
    {
        transform.position = sp;
        rb.velocity= Vector3.zero;
        if(isClicked == true)
        {
            gameManager.DecreaseIQ(Common.GrovalConst.DecreaseIQAmount);
        }
        isClicked= false;


    }


}
