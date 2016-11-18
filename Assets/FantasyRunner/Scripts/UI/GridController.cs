using UnityEngine;
using System.Collections;

public class GridController : MonoBehaviour
{
    [SerializeField] private Transform _topLeft;
    [SerializeField] private Transform _bottomRight;
    [SerializeField] private int _cols;
    [SerializeField] private int _rows;
    [SerializeField] private float _zPosition = 0;

    private int _currentCol = 0;
    private int _currentRow = 0;
    private float _colSize = 0;
    private float _rowSize = 0;

    private void Awake()
    {
        this._colSize = (this._bottomRight.position.x - this._topLeft.position.x) / (this._cols - 1);
        this._rowSize = (this._topLeft.position.y - this._bottomRight.position.y) / (this._rows - 1);
    }

    public Vector3 GetNextPosition()
    {
        float x = this._topLeft.position.x + (this._currentCol * this._colSize);
        float y = this._topLeft.position.y - (this._currentRow * this._rowSize);

        this.SetNewPosition();

        return new Vector3(x, y, this._zPosition);
    }

    private void SetNewPosition()
    {
        this._currentCol++;

        if (this._currentCol == this._cols)
        {
            this._currentCol = 0;
            this._currentRow++;
        }
    }
}
