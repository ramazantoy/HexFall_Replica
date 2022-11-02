using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
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
        
        public void LookMatches()
        {

            Row rowTemp;
            List<Row> matchesRows;

            for (int i = 0; i <_rows.Count; i++)
            {
                
                rowTemp = _rows[i];
                if (!rowTemp.HaveHex)
                {
                    continue;
                }
                
                matchesRows = new List<Row>();
                
                matchesRows.Add(rowTemp);
                
                for (int j = i+1; j < _rows.Count; j++)
                {
                    if (!_rows[j].HaveHex)
                    {
                        break;
                    } 
                    
                    if (rowTemp.Hex.HexColor == _rows[j].Hex.HexColor)
                    {
                        matchesRows.Add(_rows[j]);
                    }
                    else
                    {
                        break;
                    }
                }

                if (matchesRows.Count >= 3)
                {
                    foreach (Row row in matchesRows)
                    {
                        row.Hex.PlayParticle();
                        HexPool.Instance.AddHex(row.Hex);
                    }
                    StartCoroutine(DownToHex());
                }
            }
        }
        

        private IEnumerator DownToHex()
        {
            for (int i = 0; i < _rows.Count; i++)
            {
                if (_rows[i].HaveHex)
                {
                    continue;
                }
                for (int j = i+1; j < _rows.Count; j++)
                {
                    if (_rows[j].HaveHex)
                    {
                        _rows[j].Hex.transform.parent = _rows[i].transform;
                        _rows[i].SetHexPos();
                        yield return new WaitForSeconds(.15f);
                        break;
                    }
                }
            }

            StartCoroutine(TakeHex());
        }

        private IEnumerator TakeHex()
        {
            int colorIndex = -1;
            foreach (Row row in _rows)
            {
                if (!row.HaveHex)
                {
                    Hex hexTemp = HexPool.Instance.TakeHex();

                    colorIndex = (colorIndex + 1) % 4;
                    hexTemp.ReGenerate(colorIndex);
                    Vector3 hexPos = new Vector3(transform.position.x, hexTemp.transform.position.y, hexTemp.transform.position.z);

                    hexTemp.transform.position = hexPos;
                    
                    hexTemp.transform.parent = row.transform;
                    
                    hexTemp.transform.DOLocalMove(Vector3.zero, .5f);
                    yield return new WaitForSeconds(.15f);
                }
            }
        }
    }

