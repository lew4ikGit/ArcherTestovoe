using UnityEngine;
using Zenject;

public class Bow : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject mainCanvas;

    private float power;
    private Transform bow;
    private float enter;
    [Inject] private InputController inputController;
    [Inject] private TrajectoryRenderer trajectory;
    [Inject] private ArcherAnimations archerAnimations;

    private Camera mainCamera;

    private void OnEnable()
    {
        inputController.OnShot += Shot;
        inputController.OnShot += trajectory.DisableTrajectory;
        inputController.OnAim += ShowTrajectory;

        mainCamera = Camera.main;
        bow = transform;
    }
    private void OnDisable()
    {
        inputController.OnShot -= Shot;
        inputController.OnShot -= trajectory.DisableTrajectory;
        inputController.OnAim -= ShowTrajectory;
    }

    private void Shot()
    {
        if (!archerAnimations.GetIsShot())
        {
            Rigidbody2D bullet = Instantiate(arrowPrefab, transform.localPosition, Quaternion.identity).GetComponent<Rigidbody2D>();

            bullet.transform.position = transform.position;
            bullet.AddForce(transform.right * power, ForceMode2D.Impulse);
        }
       
    }

    private void ShowTrajectory()
    {
        if (!archerAnimations.GetIsShot())
        {
            power = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), bow.position) * 2;
            power = Mathf.Clamp(power, 2, 18);
            trajectory.ShowTrajectory(transform.position, transform.right * power);
        }      
    }
}
