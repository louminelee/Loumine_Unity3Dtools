using UnityEngine;
using System.Collections;

public class CollisionSoundTrigger : MonoBehaviour
{
    // Wwise 事件
    public AK.Wwise.Event collisionSoundEvent;

    // 标志位，控制是否允许检测碰撞
    private bool canDetectCollision = true;

    // 延迟时间（单位为毫秒）
    [Tooltip("延迟时间，单位为毫秒")]
    public float delayTimeInMilliseconds = 10f;

    // 当物体开始发生碰撞时调用
    private void OnCollisionEnter(Collision collision)
    {
        if (canDetectCollision)
        {
            // 确保Wwise事件已分配
            if (collisionSoundEvent != null)
            {
                // 触发Wwise事件，播放碰撞声音
                collisionSoundEvent.Post(gameObject);
            }
            else
            {
                Debug.LogWarning("No Wwise event assigned for collision sound.");
            }

            // 停止检测碰撞，启动延迟恢复检测协程
            canDetectCollision = false;
            StartCoroutine(EnableCollisionDetectionAfterDelay());
        }
    }

    // 协程，延迟恢复碰撞检测
    private IEnumerator EnableCollisionDetectionAfterDelay()
    {
        // 将延迟时间从毫秒转换为秒
        float delayTimeInSeconds = delayTimeInMilliseconds / 1000f;

        yield return new WaitForSeconds(delayTimeInSeconds);
        canDetectCollision = true;
    }
}
