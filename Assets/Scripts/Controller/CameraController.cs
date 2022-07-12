using System;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
    [SerializeField] Transform target;
    [SerializeField] float followSpeed;
    [SerializeField] float rotateSpeed;

    private Vector3 followOffset;
    private bool isControlActive;

    private IInputGetter rotateInputGetter;


    private void Awake() => rotateInputGetter = GetComponent<IInputGetter>();
    private void Start() => Initialize();
    private void OnDestroy() {
        GameController.Instance.OnControlToggled -= OnControlToggled;
    }

    private void Update() 
    {
        if(!isControlActive) return;

        
        Vector3 targetPosition = target.position - followOffset;
        transform.position = Vector3.Lerp(transform.position,targetPosition,followSpeed * Time.deltaTime);

        transform.RotateAround(target.position,Vector3.up,rotateInputGetter.Horizontal * rotateSpeed * Time.deltaTime);
    }

    private void Initialize()
    {
        followOffset = target.position - transform.position;
        Cursor.lockState = CursorLockMode.Locked;
        GameController.Instance.OnControlToggled += OnControlToggled;
    }

    private void OnControlToggled(bool isActive)
    {
        isControlActive = isActive;
    }
}