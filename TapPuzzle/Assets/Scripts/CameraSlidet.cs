using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.InputSystem;
using Common; 

public class CameraSlidet : MonoBehaviour
{
    // �J�����̉�]���S
    public Transform rotationCenter;
    // �J�����̉�]���x
    public float rotationSpeed = GrovalConst.DefaultRotationSpeed;
    // �J�����̐�����]�͈�
    public float verticalRotationRange = GrovalConst.DefaultVerticalRotationRange;
    // �J�����Ɖ�]���S�̋���
    public float cameraDistance = GrovalConst.DefaultCameraDistance;

    // �J��������]�����ǂ���
    private bool isRotating = false;

    // ���������̉�]�p�x
    private float horizontalRotation = 0f;
    // ���������̉�]�p�x
    private float verticalRotation = 0f;

    // TitleScene�̃C���X�^���X
    private TitleScene _title;


    [SerializeField]
    private Camera _cam;

    [SerializeField]
    private GameObject _hitPrefab; //�N���b�N�������̃G�t�F�N�g

    private void Start()
    {
        // TitleScene�̃C���X�^���X���擾
        _title = FindObjectOfType<TitleScene>();
    }


    private void Update()
    {
        // �J�����̑��쏈��
        PlayCameraControler();
    }

    // �J�����̑��쏈��
    void PlayCameraControler()
    {
        // �}�E�X�̈ʒu���擾���AZ���W��ݒ�
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = GrovalConst.DefaultMousePosZ;
        // �}�E�X�̈ʒu�����[���h���W�ɕϊ�
        Vector3 worldPos = _cam.ScreenToWorldPoint(mousePos);

        if (Input.GetMouseButtonDown(0))
        {
            // �v���C���[�h�̏ꍇ�A�q�b�g�G�t�F�N�g�𐶐�
            if (_title.isplayMode == true)
            {
                _hitPrefab.SetActive(true);
                Instantiate(_hitPrefab, worldPos, Quaternion.identity);
            }

            // �J�����̉�]���J�n
            isRotating = true;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            // �J�����̉�]���~
            isRotating = false;
        }

        // �J��������]���̏ꍇ
        if (isRotating)
        {
            // �}�E�X�̐��������̓��͂��擾
            float horizontalInput = Input.GetAxis("Mouse X");
            // �}�E�X�̐��������̓��͂��擾
            float verticalInput = Input.GetAxis("Mouse Y");

            // ���������̉�]�p�x���X�V
            horizontalRotation += horizontalInput * rotationSpeed;
            // ���������̉�]�p�x���X�V
            verticalRotation -= verticalInput * rotationSpeed;
            // ���������̉�]�p�x�𐧌�
            verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationRange, verticalRotationRange);

            // ��]�̌v�Z
            Quaternion rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
            // �J�����̈ʒu���v�Z���X�V
            Vector3 offset = rotation * new Vector3(0f, 0f, -cameraDistance);
            transform.position = rotationCenter.position + offset;
            // �J��������]���S�Ɍ�����
            transform.LookAt(rotationCenter);
        }
    }
}
