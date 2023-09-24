using UnityEngine;

public class GameFieldController : MonoBehaviour
{
    public GridController gridController;
    private void Awake()
    {
        gridController = new GridController();
    }
}
