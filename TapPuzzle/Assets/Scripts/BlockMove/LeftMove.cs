using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftMove : MonoBehaviour, IPointerClickHandler
{
    public Rigidbody rb;
    public float moveForce = 10f;
    private bool isClicked = false;
    private bool isTouching = false; // ���̃I�u�W�F�N�g�ƐڐG���Ă��邩�ǂ����𔻒肷��t���O
    GameManager gameManager;
    Vector3 sp;

    private void Start()
    {
        // Rigidbody2D�R���|�[�l���g���擾���܂�
        rb = GetComponent<Rigidbody>();
        sp = transform.localPosition;
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

    private void OnCollisionEnter(Collision collision)
    {
        transform.position = sp;
        rb.velocity = Vector3.zero;
        if (isClicked == true)
        {
            gameManager.DecreaseIQ(10);
        }
        isClicked = false;


    }

    void Move()
    {
        if (isClicked && !isTouching)
        {
            // �������Ɉړ�����
            rb.AddForce(Vector3.left * moveForce, ForceMode.Impulse);
        }
        if(transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}