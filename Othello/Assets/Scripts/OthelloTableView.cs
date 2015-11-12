using UnityEngine;
using System.Collections;

public class OthelloTableView : MonoBehaviour
{

    [SerializeField]
    GameObject _cell = null;
    public GameObject Cell
    {
        get
        {
            if (_cell != null) { return _cell; }
            _cell = Resources.Load<GameObject>("Cell/Cell");
            return _cell;
        }
    }

    const float _cellInterval = 1.05f;

    const int _xLength = 8;
    public int GetXLength { get { return _xLength; } }
    const int _zLength = 8;
    public int GetZLength { get { return _zLength; } }


    [SerializeField]
    Material _selected = null;
    public Material SelectedMaterial
    {
        get
        {
            if (_selected != null) { return _selected; }
            _selected = Resources.Load<Material>("Cell/Selected");
            return _selected;
        }
    }

    [SerializeField]
    Material _normal = null;
    public Material NormalMaterial
    {
        get
        {
            if (_normal != null) { return _normal; }
            _normal = Resources.Load<Material>("Cell/Normal");
            return _normal;
        }
    }

    int _selectX = 0;
    public int GetSelectX { get { return _selectX; } }
    int _selectZ = 0;
    public int GetSelectZ { get { return _selectZ; } }

    GameObject[,] _cellNumber =
        new GameObject[_zLength, _xLength];

    public GameObject[,] CellNumber { get { return _cellNumber; } }

    //---------------------------------------------------------

    void Start()
    {
        for (var z = 0; z < _zLength; ++z)
        {
            for (var x = 0; x < _xLength; ++x)
            {
                _cellNumber[z, x] = Instantiate(Cell);

                _cellNumber[z, x].name = Cell.name + "(" + z + "," + x + ")";

                _cellNumber[z, x].transform.parent = transform;

                _cellNumber[z, x].transform.localPosition =
                    new Vector3((_cellInterval * x), 0, (_cellInterval * z));
            }
        }
        _cellNumber[0, 0].GetComponent<Renderer>().material = SelectedMaterial;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _cellNumber[_selectZ, _selectX].GetComponent<Renderer>().material = NormalMaterial;
            _selectX -= (_selectX > 0) ? 1 : 0;
            _cellNumber[_selectZ, _selectX].GetComponent<Renderer>().material = SelectedMaterial;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _cellNumber[_selectZ, _selectX].GetComponent<Renderer>().material = NormalMaterial;
            _selectX += (_selectX < _xLength - 1) ? 1 : 0;
            _cellNumber[_selectZ, _selectX].GetComponent<Renderer>().material = SelectedMaterial;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _cellNumber[_selectZ, _selectX].GetComponent<Renderer>().material = NormalMaterial;
            _selectZ -= (_selectZ > 0) ? 1 : 0;
            _cellNumber[_selectZ, _selectX].GetComponent<Renderer>().material = SelectedMaterial;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _cellNumber[_selectZ, _selectX].GetComponent<Renderer>().material = NormalMaterial;
            _selectZ += (_selectZ < _zLength - 1) ? 1 : 0;
            _cellNumber[_selectZ, _selectX].GetComponent<Renderer>().material = SelectedMaterial;
        }
    }

}
