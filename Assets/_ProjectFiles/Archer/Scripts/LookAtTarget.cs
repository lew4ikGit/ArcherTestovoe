using Spine;
using Spine.Unity;
using UnityEngine;
using Zenject;

public class LookAtTarget : MonoBehaviour
{

    [SerializeField] private SkeletonAnimation skeletonAnimation;
    [SerializeField] private Camera cam;
    private Bone arrowBone;
    private Bone gunBone;

    [Inject] private InputController inputController;

    void OnValidate()
    {
        if (skeletonAnimation == null) skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    private void OnEnable()
    {
        inputController.OnAim += Aim;
        gunBone = skeletonAnimation.Skeleton.FindBone("gun");
        arrowBone = skeletonAnimation.Skeleton.FindBone("bullet");
    }
    private void OnDisable()
    {
        inputController.OnAim -= Aim;
    }

    private void Aim()
    {
        var mousePosition = Input.mousePosition;
        var worldMousePosition = cam.ScreenToWorldPoint(mousePosition);

        Vector3 boneWorldPosition = skeletonAnimation.transform.TransformPoint(new Vector3(gunBone.X, gunBone.Y, 0));
        Vector3 direction = (worldMousePosition - boneWorldPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        gunBone.Rotation = angle;
    }

}


