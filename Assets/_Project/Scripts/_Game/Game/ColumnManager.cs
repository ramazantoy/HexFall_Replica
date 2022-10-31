using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


	public class ColumnManager : MonoBehaviour
	{
		public static ColumnManager Instance;

		private void Awake()
		{
			if(Instance == null)
			{
				Instance = this;
			}
			else if(Instance != this)
			{
				Destroy(gameObject);
			}
		}

		private Camera _camera;
		
		private void Start()
		{
			_camera = Camera.main;
			UserInput.instance.TouchEvent += Touch;

			UserInput.instance.RotateEvent += Rotate;
			
			foreach (Transform child in transform)
			{
				if (child.TryGetComponent(out Column column))
				{
					_columns.Add(column);
				}
			}
		}

		private List<Column> _columns = new List<Column>();

		[SerializeField]
		private float _columnPadding = 1.15f;
		[SerializeField]
		private float _rowPadding = 1.15f;
		[SerializeField]
		private float _columnElevation = 0.5f;

		[SerializeField]
		private int _objectPerColumn = 8;

		[SerializeField] private LayerMask _groundMask;
		
		private Vector3 _position = Vector3.zero;
		
		void Touch(TouchType type)
		{
			if (type == TouchType.PressDown)
			{
				CleanSelections();

				RaycastHit raycastHit = UserInput.instance.Raycast(_camera, _groundMask);
				
				if(raycastHit.collider==null) return;
				_position = raycastHit.point;
				
				_position.z = 0f;
				
				FindSelectedIndexes();
			}
		}

		void FindSelectedIndexes()
		{
			int xIndex = -1;

			Vector3 xLast = new Vector3(_columnPadding * (_columns.Count - 1), 0, 0);
			
			float xPercentage = _position.InverseLerp(Vector3.zero, xLast);

			xIndex = (int)Mathf.Lerp(0, _columns.Count-1, xPercentage);
			
			int yIndex = -1;

			Vector3 yLast = new Vector3(0, (_columnElevation * xIndex % 2) + (_objectPerColumn * _rowPadding), 0);
			float yPercentage = _position.InverseLerp(Vector3.zero, yLast);
			
			yIndex = (int)Mathf.Lerp(0, _objectPerColumn-1, yPercentage);
	
			SelectPoint(xIndex , yIndex);
		}

		private List<Vector2Int> _selectedIndexes = new List<Vector2Int>();

		void SelectPoint(int x, int y)
		{
			
			_columns[x].Select(y);
			_selectedIndexes.Add(new Vector2Int(x, y));

			if (x == _columns.Count - 1)
			{
				if (x % 2 == 0 && y == _objectPerColumn -1)
				{
					_columns[x - 1].Select(y - 1);
					_selectedIndexes.Add(new Vector2Int(x - 1, y - 1));
				}
				else
				{
					if (x % 2 == 1 && y == 0)
					{
						_columns[x - 1].Select(y + 1);
						_selectedIndexes.Add(new Vector2Int(x - 1, y + 1));
					}
					else
					{
						_columns[x - 1].Select(y);
						_selectedIndexes.Add(new Vector2Int(x - 1, y));
					}
				}
			}
			else
			{
				if (x % 2 == 0 && y == _objectPerColumn - 1)
				{
					_columns[x + 1].Select(y - 1);
					_selectedIndexes.Add(new Vector2Int(x + 1, y - 1));
				}
				else
				{
					if (x % 2 == 1 && y == 0)
					{
						_columns[x + 1].Select(y + 1);
						_selectedIndexes.Add(new Vector2Int(x + 1, y + 1));
					}
					else
					{
						_columns[x + 1].Select(y);
						_selectedIndexes.Add(new Vector2Int(x + 1, y));
					}
				}
			}
			
			if (y == 0 || x % 2 == 0)
			{
				_columns[x].Select(y + 1);
				_selectedIndexes.Add(new Vector2Int(x , y + 1));
			}
			else
			{
				_columns[x].Select(y - 1);
				_selectedIndexes.Add(new Vector2Int(x , y - 1));
			}
			
		}

		void CleanSelections()
		{
			for (int i = 0; i < _columns.Count; i++)
			{
				_columns[i].CleanSelections();
			}
			
			_selectedIndexes.Clear();
		}


		
		private	void Rotate()
		{
			for (int i = 0; i < _selectedIndexes.Count; i++)
			{
				GetHex(_selectedIndexes[i]).GoToTarget(_selectedIndexes[(i + 1) % (_selectedIndexes.Count)]);
			}
		}

		public void GoToTarget(Hex hex, int x, int y)
		{
			hex.transform.parent = _columns[x].GetRow(y).transform;
			hex.transform.localPosition = Vector3.zero;
		}

		Hex GetHex(Vector2Int index)
		{
			return _columns[index.x].GetRow(index.y).Hex;
		}
	}

