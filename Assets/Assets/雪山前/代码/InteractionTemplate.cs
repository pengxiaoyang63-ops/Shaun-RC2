using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 通用交互模板：检测 → 触发 → 反应 → 重新检测，循环往复。
///
/// 本脚本只负责“交互流程”，不包含任何具体表现。
/// 具体表现（旋转、动画、音效、对话、开门等）通过 onInteract
/// 这个 UnityEvent 在 Inspector 中挂接，因此可以复用到任何物体。
/// </summary>
public class InteractionTemplate : MonoBehaviour
{
    [Header("检测")]
    [Tooltip("交互者（通常是主角）。留空时会自动查找 Tag 为 Player 的对象")]
    public Transform interactor;

    [Tooltip("交互者进入该距离内，才允许触发交互")]
    public float detectDistance = 3f;

    [Header("触发")]
    [Tooltip("触发交互的按键")]
    public KeyCode triggerKey = KeyCode.F;

    [Header("反应")]
    [Tooltip("反应表现：在 Inspector 中挂接任意方法，例如旋转、动画、音效、对话")]
    public UnityEvent onInteract;

    [Tooltip("反应持续时间。这段时间内不会重复触发，结束后重新进入检测阶段")]
    public float reactionDuration = 0.6f;

    /// <summary>是否处于反应阶段。</summary>
    public bool IsReacting { get; private set; }

    private void Start()
    {
        // 未手动绑定交互者时，自动查找主角
        if (interactor == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                interactor = playerObject.transform;
            }
            else
            {
                Debug.LogWarning($"{name}：未找到 Tag 为 Player 的对象，请手动指定 interactor。", this);
            }
        }
    }

    private void Update()
    {
        // 反应阶段：不检测、不触发，反应结束后自动回到检测阶段
        if (IsReacting) return;

        // 检测阶段：交互者必须在范围内
        if (!IsInRange()) return;

        // 触发阶段：本帧刚按下触发键
        if (Input.GetKeyDown(triggerKey))
        {
            StartCoroutine(ReactRoutine());
        }
    }

    /// <summary>交互者是否在检测距离内。</summary>
    public bool IsInRange()
    {
        if (interactor == null) return false;

        // 用平方距离比较，避免开平方，性能更好
        Vector3 offset = interactor.position - transform.position;
        return offset.sqrMagnitude <= detectDistance * detectDistance;
    }

    /// <summary>
    /// 一次完整的“触发 → 反应”流程。
    /// 反应结束后 IsReacting 复位，Update 会重新开始检测。
    /// </summary>
    private IEnumerator ReactRoutine()
    {
        IsReacting = true;

        // 执行 Inspector 中挂接的具体表现
        onInteract?.Invoke();

        // 等待反应结束，避免连续触发
        if (reactionDuration > 0f)
        {
            yield return new WaitForSeconds(reactionDuration);
        }

        IsReacting = false;
    }

    private void OnDrawGizmosSelected()
    {
        // 选中物体时显示检测范围，便于调试
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectDistance);
    }
}
