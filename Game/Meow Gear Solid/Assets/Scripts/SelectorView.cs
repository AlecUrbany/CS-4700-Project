using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SelectorView : MonoBehaviour
{
    public RectTransform rectTransform;

    private GameObject selected;
    [SerializeField] private float selectorSpeed = 50f;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        var selectedGameObject = EventSystem.current.currentSelectedGameObject;
        selected = (selectedGameObject == null) ? selected : selectedGameObject;
        EventSystem.current.SetSelectedGameObject(selected);
        if (selected == null) return;

        //Handles highlighter outline position
        transform.position = Vector3.Lerp(transform.position, selected.transform.position, selectorSpeed*Time.deltaTime);

        //This function will just teleport the selector
        //transform.position = selected.transform.position;

        //Handles sizing and shape of highlighter
        var otherRect = selected.GetComponent<RectTransform>();
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, otherRect.rect.size.x +4); //The plus 4 is to give a little buffer
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, otherRect.rect.size.y +4);
    }
}
