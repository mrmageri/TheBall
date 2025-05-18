using UnityEngine;

public class ObstacleObject : MonoBehaviour
{
    [SerializeField] private float rotationDelta;
    [SerializeField] private bool isMovable = true;
    private bool _isSelected;
    private Cell _currentCell;
    
    private void Update()
    {
        if(!_isSelected) return;
        if (Input.GetKeyDown(KeyCode.Q)) Rotate(true);
        if (Input.GetKeyDown(KeyCode.E)) Rotate(false);
    }

    public void SetCell(Cell cell)
    {
        _currentCell = cell;
    }

    public Cell GetSell()
    {
        return _currentCell;
    }
    public bool GetMovableStatus()
    {
        return isMovable;
    }
    
    public void SetMovableStatus(bool status)
    {
        isMovable = status;
    }

    public void SetSelectedStatus(bool status)
    {
        _isSelected = status;
    }

    private void Rotate(bool isRotationToLeft)
    {
        var transform1 = transform;
        transform1.Rotate(0, isRotationToLeft ? -rotationDelta : rotationDelta, 0);
    }
}
