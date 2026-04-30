using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private string clickKey = "BasicClick";
    [SerializeField] private string hoverKey = "Hover";

    // для OnClick()
    public void PlayClick()
    {
        AudioBootstrap.Instance?.PlayUI(clickKey, gameObject);
    }

    // hover
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioBootstrap.Instance?.PlayUI(hoverKey, gameObject);
    }
}