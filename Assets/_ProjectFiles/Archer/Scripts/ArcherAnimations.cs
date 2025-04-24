using Spine.Unity;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
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

    private async void ShotAnim()
    {
        if (!isShoot)
        {
            isShoot = true;
            skeletonAnimation.AnimationState.SetAnimation(0, "attack_finish", false);
            skeletonAnimation.AnimationState.AddAnimation(0, "attack_start", false, 0f);     

            await Task.Delay(1000);
            isShoot = false;
        }
    }
 
    public bool GetIsShot()
    {
        return isShoot;
    } 
}
