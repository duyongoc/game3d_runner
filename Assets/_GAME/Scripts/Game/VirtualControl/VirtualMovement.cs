using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualMovement : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    [Header("[Setting]")]
    [SerializeField] private Transform touchZone;
    [SerializeField] private Image jsTarget;


    // [private]
    private Image jsContainer;
    private Vector3 direction;
    private bool isTouch = false;
    private bool firstTouch = true;


    // [properties]
    public bool IsTouch { get => isTouch; set => isTouch = value; }
    public Vector3 GetDirection { get => direction; set => direction = value; }


    #region UNITY
    private void Start()
    {
        CacheComponent();
        CacheDefine();
    }

    // private void Update()
    // {
    // }
    #endregion



    public void OnDrag(PointerEventData ped)
    {
        Vector2 position = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(jsContainer.rectTransform, ped.position, ped.pressEventCamera, out position);

        position.x = (position.x / jsContainer.rectTransform.sizeDelta.x);
        position.y = (position.y / jsContainer.rectTransform.sizeDelta.y);

        direction = new Vector3(position.x * 2, position.y * 2, 0);
        direction = (direction.magnitude > 1) ? direction.normalized : direction;

        jsTarget.rectTransform.anchoredPosition = new Vector3(
            direction.x * (jsContainer.rectTransform.sizeDelta.x / 3),
            direction.y * (jsContainer.rectTransform.sizeDelta.y) / 3);
    }


    public void OnPointerDown(PointerEventData ped)
    {
        isTouch = true;
        if (firstTouch)
        {
            touchZone.position = new Vector2(ped.position.x, ped.position.y);
            firstTouch = false;
        }

    }


    public void OnPointerUp(PointerEventData ped)
    {
        isTouch = false;
        firstTouch = true;
        direction = Vector3.zero;
        touchZone.localPosition = Vector2.zero;
        jsTarget.rectTransform.anchoredPosition = Vector3.zero;
    }


    public void Reset()
    {
        isTouch = false;
        firstTouch = true;
        direction = Vector3.zero;
        touchZone.localPosition = Vector2.zero;
        jsTarget.rectTransform.anchoredPosition = Vector3.zero;
    }


    private void CacheDefine()
    {
        direction = Vector3.zero;
    }


    private void CacheComponent()
    {
        jsContainer = touchZone.GetComponent<Image>();
    }

}
