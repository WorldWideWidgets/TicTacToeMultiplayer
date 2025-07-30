using UnityEngine;
using UnityEngine.EventSystems;

public class GridPosition : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private int x;
    [SerializeField] private int y;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        GameManager.Instance.ClickedOnGridPositionRpc(x, y, GameManager.Instance.GetLocalPlayerType());
    }

}
