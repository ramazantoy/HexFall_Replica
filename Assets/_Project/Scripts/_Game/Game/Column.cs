using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


    public class Column : MonoBehaviour
    {
        private List<Row> _rows = new List<Row>();

        private void Start()
        {
            foreach (Transform child in transform)
            {
                if (child.TryGetComponent(out Row row))
                {
                    _rows.Add(row);
                }
            }
        }

        public Row GetRow(int index)
        {
            return _rows[index];
        }

        public void Select(int index)
        {
           // Debug.Log("Select Index : " + index);
            _rows[index].Select();
        }

        public void CleanSelections()
        {
            for (int i = 0; i < _rows.Count; i++)
            {
                _rows[i].UnSelect();
            }
        }
    }

