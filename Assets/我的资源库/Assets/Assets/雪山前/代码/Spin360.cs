using System.Collections;
using UnityEngine;

/// <summary>
/// 示例表现：让物体快速旋转指定角度。
/// 只负责“表现”，由 InteractionTemplate 的 onInteract 调用，
/// 因此不包含任何检测或按键逻辑，可以挂到任何需要旋转的物体上。
/// </summary>
public class Spin360 : MonoBehaviour
{
    [Tooltip("旋转一圈所需时间（秒）")]
    public float duration = 0.6f;

    [Tooltip("旋转角度")]
    public float angle = 360f;

    [Tooltip("是否顺时针旋转")]
    public bool clockwise = true;

    private Quaternion baseRotation;
    private bool isSpinning;

    private void Awake()
    {
        // 记录初始朝向，旋转结束后用它还原
        baseRotation = transform.localRotation;
    }

    /// <summary>
    /// 开始旋转。可在 Inspector 的 UnityEvent 中直接选中本方法。
    /// 旋转过程中重复调用会被忽略。
    /// </summary>
    public void Play()
    {
        if (isSpinning) return;
        StartCoroutine(SpinRoutine());
    }

    private IEnumerator SpinRoutine()
    {
        isSpinning = true;

        float direction = clockwise ? -1f : 1f;
        float totalTime = Mathf.Max(0.01f, duration);
        float elapsed = 0f;

        while (elapsed < totalTime)
        {
            elapsed += Time.deltaTime;

            // 0~1 的进度
            float progress = Mathf.Clamp01(elapsed / totalTime);

            // 三次缓出：开始快、结束慢，旋转更干脆
            float eased = 1f - Mathf.Pow(1f - progress, 3f);

            // 在初始朝向上叠加局部 Z 轴旋转
            transform.localRotation = baseRotation * Quaternion.Euler(0f, 0f, direction * angle * eased);

            yield return null;
        }

        // 消除浮点误差，精确还原
        transform.localRotation = baseRotation;
        isSpinning = false;
    }

    private void OnDisable()
    {
        // 物体被禁用时协程会中断，这里复位状态，避免再次启用后无法旋转
        isSpinning = false;
        transform.localRotation = baseRotation;
    }
}
