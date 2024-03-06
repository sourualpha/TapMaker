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
        UpdateMove(_moveVector);
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