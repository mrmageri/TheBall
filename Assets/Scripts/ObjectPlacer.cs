using Unity.VisualScripting;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField] private LayerMask cellLayer;
    [SerializeField] private LayerMask objectLayer;
    private Camera _camera;
    private GameObject _selectedGameObject;
    private ObstacleObject _selectedObstacleObject;
    private bool _hasSelectedObject;
    private Cell _currentCell;
    private bool _tookObject;
    private ObstacleManager _obstacleManager;
    
    private const float ReachDistance = 50f;


    
    private void Awake()
    {
        _obstacleManager = GetComponent<ObstacleManager>();
        _camera = Camera.main;
    }
    private void Update()
    {
        SelectCell();
        if(Input.GetMouseButton((int) MouseButton.Left) && !_tookObject) PlaceObject();
        if (Input.GetKeyDown(KeyCode.R) && _hasSelectedObject) RemoveObject();
    }

    public void SetSelectedObject(GameObject newObj)
    {
        if (newObj.TryGetComponent(out ObstacleObject obj))
        {
            if (obj.GetMovableStatus())
            {
                _selectedObstacleObject = obj;
                _selectedObstacleObject.SetSelectedStatus(true);
                if (_selectedObstacleObject.GetSell()) _selectedObstacleObject.GetSell().RemoveObject();
                _selectedGameObject = newObj;
                _hasSelectedObject = true;
            }
        }
    }

    private void SelectCell()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit,ReachDistance,_hasSelectedObject ? cellLayer : objectLayer))
        {
            if (_hasSelectedObject)
            {
                GameObject hitObj = hit.collider.gameObject;
                if (hitObj.TryGetComponent(out Cell cell))
                {
                    if(cell.HasObj()) return;
                    cell.DisplayObject(_selectedGameObject.transform);
                    _currentCell = cell;
                }
            }
            else
            {
                if (Input.GetMouseButton((int) MouseButton.Right))
                {
                    GameObject hitObj = hit.collider.gameObject;
                    if (hitObj.TryGetComponent(out ObstacleObject obj))
                    {
                        if (obj.GetMovableStatus())
                        {
                            _selectedObstacleObject = obj;
                            _selectedObstacleObject.SetSelectedStatus(true);
                            if (_selectedObstacleObject.GetSell()) _selectedObstacleObject.GetSell().RemoveObject();
                            _selectedGameObject = hitObj;
                            _hasSelectedObject = true;
                        }
                    }
                }
            }
        }
    }

    private void RemoveObject()
    {
        if (_selectedObstacleObject.GetMovableStatus())
        {
            _obstacleManager.ReturnObstacle(_selectedObstacleObject.GetId());
            Destroy(_selectedGameObject);
            _hasSelectedObject = false;
            _selectedGameObject = null;
            _selectedObstacleObject = null;
        }
    }

    private void PlaceObject()
    {
        if (_hasSelectedObject && !_currentCell.HasObj())
        {
            _currentCell.PlaceObject();
            _selectedObstacleObject.SetSelectedStatus(false);
            _selectedObstacleObject.SetCell(_currentCell);
            _hasSelectedObject = false;
            _selectedGameObject = null;
            _selectedObstacleObject = null;
        }
    }
}
