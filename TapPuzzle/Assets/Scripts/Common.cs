using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace Common 
{
    public static class GrovalConst
    {
        /// <summary>
        /// GameManager
        /// </summary>
        public const int HP = 150;

        /// <summary>
        /// BlockInstantiater
        /// </summary>
        public const int MaxBlockCount = 100;  // �u���b�N�̍ő吔�̗�
        public const float RayDistance = 100f; // ���C�̋���
        public const float RayDuration = 3f;   // ���C�̕\������        

        /// <summary>
        /// CameraSlidet
        /// </summary>
        public const float DefaultMousePosZ = 3f; //Z���W�̏����ʒu
        public const float DefaultRotationSpeed = 5f; // �J�����̉�]���x
        public const float DefaultVerticalRotationRange = 60f; // �J�����̐�����]�͈�
        public const float DefaultCameraDistance = 5f; // �J�����Ɖ�]���S�̋���


        /// <summary>
        /// PlayerMove
        /// </summary>
        public const float DefaultMovePerSecond = 7f; // �ړ����x�im/�b�j 
        public const float DefaultAngularPerPixel = 1f; // �J�������x�i��/px�j

        /// <summary>
        /// BlockMove
        /// </summary>
        public const float MoveForce = 10f; //�ړ���
        public const float ResetPosition = 10f; //������܂ł̋���
        public const int DecreaseIQAmount = 10; //�_���[�W��
    }
}
