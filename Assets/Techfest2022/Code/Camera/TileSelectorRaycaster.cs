using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// A simple class to raycast on mouse click and trigger an action
/// if the raycast detects a tile.
/// </summary>
public class TileSelectorRaycaster : MonoBehaviour
{
    private Camera _mainCamera = null;
    private Ray _ray;
    private RaycastHit _hit;

    public static Action<Tile> TileSelected = null;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonUp(0))
            return;
        if (IsPointerOverUI())
            return;
        _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit, 100f))
        {
            if (_hit.collider.gameObject.layer == 3)
            {
                Tile tile = _hit.collider.GetComponent<Tile>();

                if (tile != null)
                    TileSelected?.Invoke(tile);
            }
        }
    }

    /// <summary>
    /// Don't raycast if the click happens over a UI.
    /// </summary>
    /// <returns></returns>
    private bool IsPointerOverUI()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}
