using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;
public class BlockInstantiater : MonoBehaviour
{

    Vector2 displayCenter;

    // �u���b�N��ݒu����ʒu���ꉞ���A���^�C���Ŋi�[
    private Vector3 pos;


    #region SerializeField
    [SerializeField]
    private List<GameObject> blockPrefab; //�u���b�N�̃��X�g

    [SerializeField]
    private Camera createcam; //�N���G�C�g���[�h�̃J����

    [SerializeField]
    private Camera playcam; //�N���G�C�g���[�h�̃J����


    [SerializeField]
    private GameObject optionpanel; //�ݒ�̃p�l��

    [SerializeField]
    private GameObject player; //�v���C���[

    [SerializeField]
    private GameObject playercanvas; //�v���C���[�̃L�����o�X

    [SerializeField]
    GameObject ClearPanel; //�N���A��ʂ̃p�l��

    [SerializeField]
    GameObject SavePanel;//�Z�[�u�̂��߂̃p�l��

    #endregion


    // Use this for initialization
    int blocknumber; //�u���b�N���X�g�̔ԍ�
    int blockCount;
    public GameObject guideBlock; // �K�C�h�u���b�N
    private Vector3 blockSize; // �u���b�N�̃T�C�Y
    void Start()
    {
        // �� ��ʒ����̕��ʍ��W���擾����
        displayCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        blocknumber= 0;
        blockCount = 0;
        optionpanel.SetActive(false);
        SavePanel.SetActive(false);

        // �K�C�h�u���b�N�̐���
        guideBlock = Instantiate(guideBlock, Vector3.zero, Quaternion.identity);
        // �ŏ��̃u���b�N���g�p���ăT�C�Y���擾
        blockSize = blockPrefab[blocknumber].transform.localScale;
        guideBlock.SetActive(false); // �ŏ��͔�\���ɂ���
    }

    // Update is called once per frame
    void Update()
    {
        // �� �u�J��������̃��C�v����ʒ����̕��ʍ��W�����΂�
        Ray ray = createcam.ScreenPointToRay(displayCenter);
        // �� ���������I�u�W�F�N�g�����i�[����ϐ�
        RaycastHit hit;

        // �� Physics.Raycast() �Ń��C���΂�
        if (Physics.Raycast(ray, out hit))
        {
            // �� �����ʒu�̕ϐ��̒l���u�u���b�N�̌��� + �u���b�N�̈ʒu�v
            pos = hit.normal + hit.collider.transform.position;
            if(hit.collider.CompareTag("Cube"))
            {
                UpdateGuideBlockPosition(pos);
            }
            else
            {
                // �����Ȃ��Ƃ��ɃK�C�h�u���b�N���\���ɂ���
                guideBlock.SetActive(false);
            }

            DrawBlockOutline(pos, blockPrefab[blocknumber].transform.localScale);
            // �� �E�N���b�N
            if (Input.GetKeyDown(KeyCode.Q))
            {
                // �����ʒu�̕ϐ��̍��W�Ƀu���b�N�𐶐�
                Instantiate(blockPrefab[blocknumber], pos, Quaternion.identity);
                float distance = 100; // ��΂�&�\������Ray�̒���
                float duration = 3;   // �\�����ԁi�b�j
                Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, duration, false);
            }

            // �� ���N���b�N
            if (Input.GetKeyDown(KeyCode.E))
            {
                // �� ���C���������Ă���I�u�W�F�N�g���폜
                Destroy(hit.collider.gameObject);
            }
        }
        else
        {
            // ���C�������q�b�g���Ȃ��Ƃ��ɂ��K�C�h�u���b�N���\���ɂ���
            guideBlock.SetActive(false);
        }


    }

    // �K�C�h�u���b�N�̈ʒu�ɘg����`�悷�郁�\�b�h
    void DrawBlockOutline(Vector3 position, Vector3 blockSize)
    {
        float halfX = blockSize.x / 2f;
        float halfY = blockSize.y / 2f;
        float halfZ = blockSize.z / 2f;

        Vector3[] corners = new Vector3[]
        {
        new Vector3(position.x - halfX, position.y - halfY, position.z - halfZ),
        new Vector3(position.x + halfX, position.y - halfY, position.z - halfZ),
        new Vector3(position.x - halfX, position.y + halfY, position.z - halfZ),
        new Vector3(position.x + halfX, position.y + halfY, position.z - halfZ),
        new Vector3(position.x - halfX, position.y - halfY, position.z + halfZ),
        new Vector3(position.x + halfX, position.y - halfY, position.z + halfZ),
        new Vector3(position.x - halfX, position.y + halfY, position.z + halfZ),
        new Vector3(position.x + halfX, position.y + halfY, position.z + halfZ)
        };

        // Draw lines between corners to form the outline
        Debug.DrawLine(corners[0], corners[1], Color.red, 0.1f);
        Debug.DrawLine(corners[1], corners[3], Color.red, 0.1f);
        Debug.DrawLine(corners[3], corners[2], Color.red, 0.1f);
        Debug.DrawLine(corners[2], corners[0], Color.red, 0.1f);

        Debug.DrawLine(corners[4], corners[5], Color.red, 0.1f);
        Debug.DrawLine(corners[5], corners[7], Color.red, 0.1f);
        Debug.DrawLine(corners[7], corners[6], Color.red, 0.1f);
        Debug.DrawLine(corners[6], corners[4], Color.red, 0.1f);

        Debug.DrawLine(corners[0], corners[4], Color.red, 0.1f);
        Debug.DrawLine(corners[1], corners[5], Color.red, 0.1f);
        Debug.DrawLine(corners[2], corners[6], Color.red, 0.1f);
        Debug.DrawLine(corners[3], corners[7], Color.red, 0.1f);
    }

    // �K�C�h�u���b�N�̈ʒu���X�V���郁�\�b�h
    void UpdateGuideBlockPosition(Vector3 position)
    {
        if (blockCount == 0)
        {
            guideBlock.SetActive(true);
            guideBlock.transform.position = Vector3.zero;
        }
        else
        {
            guideBlock.SetActive(true);
            guideBlock.transform.position = position;
        }

    }

    void OnDestroy()
    {
        // �V�[�����I������Ƃ��ɃK�C�h�u���b�N���j������
        Destroy(guideBlock);
    }
    #region �u���b�N�̐ݒ�

    public void UpBlockButton()
    {
        blocknumber = 0;
    }

    public void DownBlockButton()
    {
        blocknumber = 1;
    }

    public void RightBlockButton()
    {
        blocknumber = 2;
    }

    public void LeftBlockButton()
    {
        blocknumber = 3;
    }

    public void ForwardBlockButton()
    {
        blocknumber = 4;
    }

    public void BackBlockButton()
    {
        blocknumber = 5;
    }

    public void CreateBlock()
    {

        // �� �u�J��������̃��C�v����ʒ����̕��ʍ��W�����΂�
        Ray ray = createcam.ScreenPointToRay(displayCenter);
        // �� ���������I�u�W�F�N�g�����i�[����ϐ�
        RaycastHit hit;
        if (blockCount == 0)
        {
            Instantiate(blockPrefab[blocknumber], Vector3.zero, Quaternion.identity);
            blockCount++;
 
        }        
        if (Physics.Raycast(ray, out hit))
        {
            // �� �����ʒu�̕ϐ��̒l���u�u���b�N�̌��� + �u���b�N�̈ʒu�v
            pos = hit.normal + hit.collider.transform.position;
            // �����ʒu�̕ϐ��̍��W�Ƀu���b�N�𐶐�
            Instantiate(blockPrefab[blocknumber], pos, Quaternion.identity);
            float distance = 100; // ��΂�&�\������Ray�̒���
            float duration = 3;   // �\�����ԁi�b�j
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, duration, false);
            blockCount++;
        }
    }

    public void DeleteBlock()
    {
        // �� �u�J��������̃��C�v����ʒ����̕��ʍ��W�����΂�
        Ray ray = createcam.ScreenPointToRay(displayCenter);
        // �� ���������I�u�W�F�N�g�����i�[����ϐ�
        RaycastHit hit;

        // �� Physics.Raycast() �Ń��C���΂�
        if (Physics.Raycast(ray, out hit))
        {
            // �� �����ʒu�̕ϐ��̒l���u�u���b�N�̌��� + �u���b�N�̈ʒu�v
            pos = hit.normal + hit.collider.transform.position;


            // �� ���C���������Ă���I�u�W�F�N�g���폜
            Destroy(hit.collider.gameObject);
            blockCount--;
        }
    }
    #endregion

}