using Spine;
using Spine.Unity;
using UnityEngine;
using Zenject;

public class LookAtTarget : MonoBehaviour
{

    [SerializeField] private SkeletonAnimation skeletonAnimation;

    //[SpineBone(dataField: "skeletonAnimation")]
    //[SerializeField] private string boneName;
    [SerializeField] private Camera cam;

    [SerializeField] Transform arrowPoint;
    private Bone arrowBone;
    private Bone gunBone;

    [Inject] private InputController inputController;

    [SpineSlot] [SerializeField] string slot;

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

        //arrowPoint.position = skeletonAnimation.transform.TransformPoint(new Vector2(arrowBone.WorldX, arrowBone.WorldY));
        //arrowPoint.rotation = Quaternion.Euler(0, 0, arrowBone.WorldRotationX);

       
    }

}


