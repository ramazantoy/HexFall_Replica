using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


    public class Row : MonoBehaviour
    {

        public bool HaveHex
        {
            get
            {
                return transform.childCount != 0;
            }
        }
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
            if(Hex==null) return;
            Hex.UnSelect();
        }

        public void SetHexPos()
        {
            Hex.transform.DOLocalMove(Vector3.zero, .5f);
        }
    }
