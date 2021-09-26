using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;

public class EyeGazeTracking_Implementation : MonoBehaviour
{
    /// <summary>
    /// ポインターオブジェクト
    /// </summary>
    [SerializeField, Tooltip("PointerPrefab")]
    private GameObject m_SpawnPrefab;

    /// <summary>
    /// スポーンオブジェクトの参照
    /// </summary>
    private GameObject m_Pointer;

    /// <summary>
    /// 視線データの参照
    /// </summary>
    private IMixedRealityEyeGazeProvider m_EyeGazeProvider;

    /// <summary>
    /// 開始処理
    /// </summary>
    private void Start()
    {
        // 視線データの参照を取得
        m_EyeGazeProvider = CoreServices.InputSystem.EyeGazeProvider;
    }

    /// <summary>
    /// 定期実行
    /// </summary>
    private void Update()
    {
        // 視線トラッキングが有効か否か
        if (m_EyeGazeProvider.IsEyeTrackingEnabled)
        {
            // 視線のヒット位置にポインターを移動する
            m_Pointer.transform.position = m_EyeGazeProvider.HitPosition;

            // ヒット位置が存在しない(距離 0.0f)の場合
            if (m_EyeGazeProvider.HitInfo.distance <= 0.0f)
            {
                // 視線方向 2.0 メートル先の位置にポインターを表示
                float defaultDistanceInMeters = 2.0f;
                m_Pointer.transform.position =
                    m_EyeGazeProvider.GazeOrigin +
                    m_EyeGazeProvider.GazeDirection.normalized * defaultDistanceInMeters;
            }
        }
    }

    /// <summary>
    /// アクティブ時の処理
    /// </summary>
    private void OnEnable()
    {
        // プレハブを生成する
        m_Pointer = Instantiate(m_SpawnPrefab);
    }

    /// <summary>
    /// 非アクティブ時の処理
    /// </summary>
    private void OnDisable()
    {
        // プレハブを破棄する
        Destroy(m_Pointer);
    }
}