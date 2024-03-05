// Merle Roji

using UnityEngine;

public static class AnimationQoL
{
    public static void ChangeAnimation(Animator anim, string newAnim)
    {
        string currentAnim = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        if (currentAnim.Contains(newAnim))
            anim.ForceStateNormalizedTime(0.0f);
        else
            anim.Play(newAnim); // play anim
    }
}
