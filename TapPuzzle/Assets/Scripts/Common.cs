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
        public const int MaxBlockCount = 100;  // ブロックの最大数の例
        public const float RayDistance = 100f; // レイの距離
        public const float RayDuration = 3f;   // レイの表示期間        

        /// <summary>
        /// CameraSlidet
        /// </summary>
        public const float DefaultMousePosZ = 3f; //Z座標の初期位置
        public const float DefaultRotationSpeed = 5f; // カメラの回転速度
        public const float DefaultVerticalRotationRange = 60f; // カメラの垂直回転範囲
        public const float DefaultCameraDistance = 5f; // カメラと回転中心の距離


        /// <summary>
        /// PlayerMove
        /// </summary>
        public const float DefaultMovePerSecond = 7f; // 移動速度（m/秒） 
        public const float DefaultAngularPerPixel = 1f; // カメラ速度（°/px）

        /// <summary>
        /// BlockMove
        /// </summary>
        public const float MoveForce = 10f; //移動量
        public const float ResetPosition = 10f; //消えるまでの距離
        public const int DecreaseIQAmount = 10; //ダメージ数
    }
}
