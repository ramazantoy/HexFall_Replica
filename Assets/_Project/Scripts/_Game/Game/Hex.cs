using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Hex : MonoBehaviour
    {

        [SerializeField]
        private HexData _properties;

        public HexColor HexColor
        {
            get
            {
                return _properties.HexColor;
            }
        }

        private void OnEnable()
        {
            UnSelect();
        }

        public void ReGenerate(int colorIndex)
        {
            _properties.HexParticle.transform.parent = transform;
            _properties.HexParticle.transform.localPosition=Vector3.zero;
            
            _properties.HexColor = (HexColor)colorIndex;
            transform.GetComponent<MeshRenderer>().material = _properties.HexMaterials[colorIndex];
            UnSelect();
        }
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

        public void PlayParticle()
        {
            _properties.HexParticle.gameObject.transform.parent = null;
            _properties.HexParticle.Play();
        }
    }

    public enum HexColor
    {
        Null=-1,
        Red=2,
        Purple=3,
        Blue=0,
        Orange=1
    }

[System.Serializable]
public class HexData
{
    public HexColor HexColor;
    public ParticleSystem HexParticle;
    public List<Material> HexMaterials;
  
}



