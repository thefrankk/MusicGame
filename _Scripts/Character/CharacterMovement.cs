using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public float Min;
    public float Max;
    public Transform rectTransform;
    private bool isDragging;
    private Vector3 NewPosition;
    private Vector3 initialMousePosition;

    private float _dragSpeed = 15;


    void Update()
    {
        if (isDragging)
        {
            float NewValue = Mathf.Clamp(NewPosition.x, Min, Max);
            rectTransform.transform.localPosition = Vector3.Lerp(rectTransform.transform.localPosition, new Vector3(NewValue,0,0),  _dragSpeed * Time.deltaTime  );
        }
    }
    void Start()
    {
        isDragging = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        initialMousePosition = Camera.main.ScreenToWorldPoint(eventData.position);
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        initialMousePosition = Vector3.zero;
        isDragging = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector3 mousePosition = eventData.position;
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            NewPosition = new Vector3(worldMousePosition.x, 0, 0);
            bool flipX = worldMousePosition.x < initialMousePosition.x;
            bool flipY = worldMousePosition.y < initialMousePosition.y;

            /// Position Change And Flip Rotation
            rectTransform.localScale = new Vector3(flipX ? 0.85f : -0.85f, flipY ? 0.85f : 0.85f, 0.85f);
        }
    }
}
