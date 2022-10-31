using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Hex : MonoBehaviour
    {
        [SerializeField] 
        private HexColor _color = HexColor.Blue;
        
        public void Select()
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        
        public void UnSelect()
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        public void GoToTarget(Vector2Int index)
        {
            ColumnManager.Instance.GoToTarget(this , index.x , index.y);
        }
    }

    public enum HexColor
    {
        Red,
        Purple,
        Blue,
        Yellow
    }

