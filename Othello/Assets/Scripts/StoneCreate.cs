using UnityEngine;
using System.Collections;

public class StoneCreate : MonoBehaviour
{
    enum TableState
    {
        NONE,
        BLACK,
        WHITE,
    }
    struct StoneState
    {
        public TableState tableState;
        public GameObject stone;
    }
    StoneState[,] _stoneDate = null;

    const float _stonePutingPos = 0.1f;


    OthelloTableView _table = null;


    [SerializeField]
    GameObject _stonePrefab = null;
    public GameObject Stone
    {
        get
        {
            if (_stonePrefab != null) { return _stonePrefab; }
            _stonePrefab = Resources.Load<GameObject>("Stone/Stone");
            return _stonePrefab;
        }
    }

    bool _isReady = false;
    bool _isWhite = false;

    //-----------------------------------------------

    void Start()
    {
        _table =
            FindObjectOfType<OthelloTableView>();

        _stoneDate = new StoneState[
            _table.GetZLength, _table.GetXLength];

        for (var z = 0; z < _stoneDate.GetLength(0); ++z)
        {
            for (var x = 0; x < _stoneDate.GetLength(1); ++x)
            {
                _stoneDate[z, x].tableState = TableState.NONE;
            }
        }

    }

    void Update()
    {
        if (!_isReady)
        {
            CreateStone(3, 3);
            CreateStone(3, 4);
            CreateStone(4, 4);
            CreateStone(4, 3);

            _isReady = true;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (_stoneDate[_table.GetSelectZ, _table.GetSelectX].
                tableState == TableState.NONE)
            {
                CreateStone(_table.GetSelectZ, _table.GetSelectX);
            }
        }
    }

    void CreateStone(int z, int x)
    {
        var _tablePos =
            _table.CellNumber[z, x].transform;

        _stoneDate[z, x].stone = Instantiate(Stone);
        _stoneDate[z, x].stone.name = string.Format("Stone({0},{1})", z, x);
        _stoneDate[z, x].stone.transform.parent = transform;
        _stoneDate[z, x].stone.transform.localPosition =
           _tablePos.localPosition + (Vector3.up * _stonePutingPos);
        _stoneDate[z, x].stone.transform.Rotate((_isWhite) ? 180 : 0, 0, 0);

        _stoneDate[z, x].tableState =
            (_isWhite) ? TableState.WHITE : TableState.BLACK;

        _isWhite = (_isWhite) ? false : true;
    }

}
