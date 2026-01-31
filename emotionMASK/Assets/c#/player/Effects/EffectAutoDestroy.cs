using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于一次性播放的动画特效：自动根据 Animator 中非循环 AnimationClip 的时长销毁实例。
/// 如果需要更精确的销毁时机，可在动画最后一帧添加 Animation Event 调用 DestroyNow()。
/// 把此脚本挂在特效预制体上（或者如上代码所示，在运行时自动添加也可以）。
/// </summary>
public class EffectAutoDestroy : MonoBehaviour
{
    [Tooltip("当无法自动读取时使用的回退销毁时间（秒）")]
    public float fallbackDuration = 1.5f;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        float destroyAfter = fallbackDuration;

        if (animator != null && animator.runtimeAnimatorController != null)
        {
            float longestNonLoop = 0f;
            foreach (var clip in animator.runtimeAnimatorController.animationClips)
            {
                if (clip == null) continue;
                if (clip.isLooping) continue;
                if (clip.length > longestNonLoop) longestNonLoop = clip.length;
            }

            if (longestNonLoop > 0f) destroyAfter = longestNonLoop;
        }

        Destroy(gameObject, destroyAfter);
    }

    /// <summary>
    /// 可在动画最后一帧通过 Animation Event 调用，立即销毁实例。
    /// </summary>
    public void DestroyNow()
    {
        Destroy(gameObject);
    }
}