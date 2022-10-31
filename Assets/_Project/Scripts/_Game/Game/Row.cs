using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Row : MonoBehaviour
    {
        public Hex Hex
        {
            get
            {
                return transform.GetChild(0).GetComponent<Hex>();
            }
        }

        public void Select()
        {
            Hex.Select();
        }
        
        public void UnSelect()
        {
            Hex.UnSelect();
        }
    }
