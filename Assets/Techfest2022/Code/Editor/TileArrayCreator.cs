using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class TileArrayCreator : EditorWindow
{
    private Tile _tilePrefab = null;
    private Vector2Int _arraySize = Vector2Int.zero;

    private const float MAX_WIDTH_SMALL = 250f;

    private const float SPACE_TINY = 10f;
    private const float SPACE_SMALL = 20f;

    private Vector3 _currentTilePosition = Vector3.zero;
    private Tile[,] _tileArray = null;
    private List<Tile> _tileHive = null;

    private Transform _parentObject = null;

    private GameObject _tileSocket = null;
    private Tile _tileArchetype = null;

    private const float HEXAGON_CIRCUMRADIUS_TO_SIDE = 0.433f; ///This value is concluded with the help of a bit of geometry.

    [MenuItem("TechFest2022/Tile Array Creator")]
    private static void Init()
    {
        TileArrayCreator window = GetWindow<TileArrayCreator>("Tile Array Creator");
        window.Show();
    }

    private void OnDestroy()
    {
        _tileSocket = null;
    }

    private void OnGUI()
    {
        ///Exercise: 
        /// - Create 1 Object Field that accepts a Tiles Parent.
        /// - Create 1 Object Field that accepts a Tile prefab.
        /// - Create 2 Integer Fields for accepting the number Rows and Columns of the Tile array to be constructed.
        /// - Create 1 Button that calls for ProcessTile AND THEN CreateHive.
        /// Play around with EditorGUI.indentLevel, EditorGUILayout.Space, EditorGuilayout.BeginHorizontal & EditorGuilayout.EndHorizontal.
    }

    private void ProcessTile()
    {
        Vector3 currentSocketPosition = _tilePrefab.transform.position;
        float currentAngle = 0;

        if (_tileSocket == null)
        {
            _tileArchetype = Instantiate(_tilePrefab, Vector3.zero, Quaternion.identity, _parentObject);
            _tileArchetype.TileSockets = new Transform[6];
        }

        if (_tileSocket == null)
        {
            _tileSocket = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            _tileSocket.transform.localScale = Vector3.one * 0.01f;
        }

        for (int i = 0; i < 3; i++)
        {
            ///This part gives the 2-axis coordinate of the centre of a side of the hexagon. A basic gameObject which we choose to call 'socket' is just placed there.
            
            currentSocketPosition.x = HEXAGON_CIRCUMRADIUS_TO_SIDE * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
            currentSocketPosition.z = HEXAGON_CIRCUMRADIUS_TO_SIDE * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

            _tileArchetype.TileSockets[i] = Instantiate(_tileSocket, currentSocketPosition, Quaternion.identity, _tileArchetype.transform).transform;

            currentAngle += 60f;
        }

        DestroyImmediate(_tileSocket);
        _tileSocket = null;
    }

    private void CreateHive()
    {
        _tileArray = new Tile[_arraySize.x, _arraySize.y];
        _currentTilePosition = Vector3.zero;
        
        for (int j = 0; j < _arraySize.y; j++)
        {
            if (j == 0)
            {
                _currentTilePosition.z = 0f;
            }
            else
            {
                ///The whole idea behind this part is that the next hexagon in a column 
                ///is placed adjacent to 1 of 2 different sides of the last hexagon.

                if (j % 2 != 0)
                {
                    _currentTilePosition += (_tileArray[0, j - 1].TileSockets[2].position - _tileArray[0, j - 1].transform.position) * 2f;
                }
                else
                {
                    _currentTilePosition += (_tileArray[0, j - 1].TileSockets[1].position - _tileArray[0, j - 1].transform.position) * 2f;
                }
            }
            _currentTilePosition.y = 0f;

            _tileArray[0, j] = Instantiate(_tileArchetype, _currentTilePosition, Quaternion.identity, _parentObject);
        }
        
        for (int j = 0; j < _arraySize.y; j++)
        {
            _currentTilePosition.x = _tileArray[0, j].transform.position.x;
            for (int i = 1; i < _arraySize.x; i++)
            {
                ///here you just place the tiles in a row one after the other.
                _currentTilePosition.x += (_tileArray[i - 1, j].TileSockets[0].position - _tileArray[i - 1, j].transform.position).x * 2;
                _currentTilePosition.z = _tileArray[i - 1, j].transform.position.z;
                _currentTilePosition.y = 0f;

                _tileArray[i, j] = Instantiate(_tileArchetype, _currentTilePosition, Quaternion.identity, _parentObject);
            }
        }
        
        DestroyImmediate(_tileArchetype.gameObject);
    }
}
