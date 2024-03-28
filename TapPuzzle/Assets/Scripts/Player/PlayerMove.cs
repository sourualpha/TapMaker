using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    #region �ϐ�

    // �ړ�������󂯕t����^�b�`�G���A 
    
    [SerializeField]
    private DragHandler _moveController;
    
    // �ړ����x�im/�b�j 
    
    [SerializeField]
    private float _movePerSecond = 7f;

    // �ړ�����̃^�b�`�ʒu�|�C���^ 
    
    [SerializeField]
    private Sprite _leftPointer;

    [SerializeField]
    private GameObject _camera;

    //�J��������̃^�b�`�ʒu�|�C���^
    [SerializeField]
    private Sprite _rightPointer;
    
    // �J����������󂯕t����^�b�`�G���A 
    
    [SerializeField]
    private DragHandler _lookController;

    
    // �J�������x�i��/px�j 
    
    [SerializeField]
    private float _angularPerPixel = 1f;


    //�W���C�X�e�B�b�N
    
    [SerializeField]
    private VariableJoystick _moveControllers;


    [SerializeField]
    private GameManager _gameManager;
    //�J��������Ƃ��đO�t���[���Ƀ^�b�`�����L�����o�X��̍��W
    private Vector2 _lookPointerPosPre;
    
    /// �ړ�����Ƃ��ă^�b�`�J�n�����X�N���[�����W 
    
    private Vector2 _movePointerPosBegin;

    private Vector3 _moveVector;

    #endregion
    
    // �N���� 
    
    private void Awake()
    {
        _moveController.OnBeginDragEvent += OnBeginDragMove;
        _moveController.OnDragEvent += OnDragMove;
        _moveController.OnEndDragEvent += OnEndDragMove;
        _lookController.OnBeginDragEvent += OnBeginDragLook;
        _lookController.OnDragEvent += OnDragLook;
    }

    //�X�V���� 
    private void Update()
    {
        if(_gameManager.isOption == false)
        {
            UpdateMove(_moveVector);
            PCMove();
        }

    }


    // �ړ�����
    #region Move

    //�h���b�O����J�n�i�ړ��p�j 
    private void OnBeginDragMove(PointerEventData eventData)
    {
        // �^�b�`�J�n�ʒu��ێ�
        _movePointerPosBegin = eventData.position;
    }

    //�h���b�O���쒆�i�ړ��p�j 
    private void OnDragMove(PointerEventData eventData)
    {
        // �^�b�`�J�n�ʒu����̃X���C�v�ʂ��ړ��x�N�g���ɂ���
        var vector = eventData.position - _movePointerPosBegin;
        _moveVector = new Vector3(vector.x, 0f, vector.y);

    }

    private void UpdateMove(Vector3 vector)
    {
        // ���݌�������ɁA���͂��ꂽ�x�N�g���Ɍ������Ĉړ�
        transform.position += transform.rotation * vector.normalized * _movePerSecond * Time.deltaTime;
    }

    //�h���b�O����I���i�ړ��p�j 
    private void OnEndDragMove(PointerEventData eventData)
    {
        // �ړ��x�N�g��������
        _moveVector = Vector3.zero;
    }

    //WASD�A���L�[�ł̈ړ�
    private void PCMove()
    {
        // �J�����̌����ɍ��킹���ړ��x�N�g�����v�Z����
        Vector3 moveDirection = Vector3.zero;
        if (_camera != null)
        {
            // �J�����̌�������x����z���̐������擾���Ĉړ��x�N�g���Ƃ���
            Vector3 cameraForward = _camera.transform.forward;
            // y��������0�ɂ��邱�ƂŐ����ȕ����݂̂��l������
            cameraForward.y = 0; 
            Vector3 cameraRight = _camera.transform.right;
            

            Vector3 cameraUp = _camera.transform.up;
            cameraUp.z = 0;
            cameraUp.x = 0;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                moveDirection += cameraForward;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                moveDirection -= cameraForward;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                moveDirection -= cameraRight;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                moveDirection += cameraRight;
            }

            if(Input.GetKey(KeyCode.Space))
            {
                moveDirection += cameraUp;
            }
            else if(Input.GetKey(KeyCode.LeftShift))
            {
                moveDirection -= cameraUp;
            }

            // �ړ��x�N�g���̑傫����1�ɐ��K������
            if (moveDirection.magnitude > 0)
            {
                moveDirection.Normalize();
            }

            // �v���C���[�L�����N�^�[���ړ�������
            this.transform.position += moveDirection * _movePerSecond * Time.deltaTime;
        }
    }

    #endregion


    // �J��������
    #region Look
    //�h���b�O����J�n�i�J�����p�j 
    private void OnBeginDragLook(PointerEventData eventData)
    {
        _lookPointerPosPre = _lookController.GetPositionOnCanvas(eventData.position);
    }

    //�h���b�O���쒆�i�J�����p�j 
    private void OnDragLook(PointerEventData eventData)
    {
        var pointerPosOnCanvas = _lookController.GetPositionOnCanvas(eventData.position);
        // �L�����o�X��őO�t���[�����牽px���삵�������v�Z
        var vector = pointerPosOnCanvas - _lookPointerPosPre;
        // ����ʂɉ����ăJ��������]
        LookRotate(new Vector2(-vector.y, vector.x));
        _lookPointerPosPre = pointerPosOnCanvas;
    }

    private void LookRotate(Vector2 angles)
    {
        Vector2 deltaAngles = angles * _angularPerPixel;
        transform.eulerAngles += new Vector3(0f, deltaAngles.y);
        _camera.transform.localEulerAngles += new Vector3(deltaAngles.x, 0f);
    }
    #endregion
}