using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Transform objectPlace;
    private bool _hasObj;

    public bool HasObj()
    {
        return _hasObj;
    }

    public void PlaceObject()
    {
        _hasObj = true;
    }

    public void RemoveObject()
    {
        _hasObj = false;
    }
    
    public void DisplayObject(Transform gameObjectTransform)
    {
        gameObjectTransform.position = objectPlace.position;
    }
}
