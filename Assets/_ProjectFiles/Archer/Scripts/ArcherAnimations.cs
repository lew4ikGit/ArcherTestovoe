using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using Zenject;

public class ArcherAnimations : MonoBehaviour
{
    private SkeletonAnimation skeletonAnimation;
    private bool isShoot;

    private float animationTime;
    private float animationDuration;

    [Inject] private InputController inputController;


    private void OnEnable()
    {
        inputController.OnShot += ShotAnim;
    }
    private void OnDisable()
    {
        inputController.OnShot -= ShotAnim;
    }

    private void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    private void ShotAnim()
    {
        if (!isShoot)
        {
            isShoot = true;
            skeletonAnimation.AnimationState.SetAnimation(0, "attack_finish", false);
            skeletonAnimation.AnimationState.AddAnimation(0, "attack_start", false, 0f);
            skeletonAnimation.AnimationState.AddAnimation(0, "attack_target", false, 0f);
        }

        StartCoroutine("ReloadCor");
    }


    IEnumerator ReloadCor()
    {
        yield return new WaitWhile(()=>skeletonAnimation.state.GetCurrent(0).Animation.Name != "attack_target");  

        isShoot = false;
    }

    public bool GetIsShot()
    {
        return isShoot;
    }
}
