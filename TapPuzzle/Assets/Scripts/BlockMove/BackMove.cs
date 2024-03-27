using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackMove : MonoBehaviour, IPointerClickHandler
{
    public Rigidbody rb;
    public float moveForce = 10f;
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
        if(transform.position.z > 10f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        transform.position = sp;
        rb.velocity= Vector3.zero;
        if(isClicked == true)
        {
            gameManager.DecreaseIQ(10);
        }
        isClicked= false;


    }


}
