using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
public class ZoomToolController_script : MonoBehaviour
{
    private Camera camera;
    private bool isActive = false;
    private Vector3 initialScale;
    public float tweenDuration = 0.3f;

    private void Awake()
    {
        camera = Camera.main;
        initialScale = this.transform.localScale;
    }

    private void Start()
    {
        this.transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        // Checking button down to only start the tweening once
        if (Input.GetMouseButtonDown(0) && isActive && !IsMouseOverUI())
        {
            if (this.transform.localScale.x != initialScale.x)
            {
                TweenLenseScale(initialScale);
            }
        }
        // This function moves around the zoom lense
        else if (Input.GetMouseButton(0) && isActive && !IsMouseOverUI())
        {
            SetZoomTool();
        }
    }

    private void SetZoomTool()
    {
        Vector3 mouseInWorldCoordinates = camera.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = new Vector3(mouseInWorldCoordinates.x, mouseInWorldCoordinates.y, this.transform.position.z);
    }

    private void TweenLenseScale(Vector3 endValue)
    {
        SoundFX.instance.playSound(ref SoundFX.instance.zoomSlide, 0.5f, true);
        this.transform.DOScale(endValue, tweenDuration);
    }

    // Could be part of an interface for a system with more tools
    public void SetActive(bool _isActive)
    {
        print("SetActive called with: " + _isActive);
        isActive = _isActive;
        if (isActive == false)
        {
            TweenLenseScale(Vector3.zero);
        }
    }

    public bool GetActive()
    {
        return isActive;
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
