using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexPool : MonoBehaviour
{
   public static HexPool Instance;

   private void Awake()
   {
      Instance = this;
   }

   [SerializeField]
   private Hex _hexPrefab;
   
   [SerializeField]
   private List<Hex> _hexes;

   public Hex TakeHex()
   {
      if (_hexes.Count > 0)
      {
         Hex hexTemp = _hexes[0];
         _hexes.RemoveAt(0);
         hexTemp.transform.parent = null;
         hexTemp.gameObject.SetActive(true);
         return hexTemp;
      }
      else
      {
         Hex hexTemp=Instantiate(_hexPrefab);
         hexTemp.gameObject.SetActive(true);
         return hexTemp;
 
      }
   }

   public void AddHex(Hex hex)
   {
      if(_hexes.Contains(hex)) return;

      hex.transform.parent = transform;
      hex.transform.localPosition=Vector3.zero;
      
      hex.gameObject.SetActive(false);
      _hexes.Add(hex);
   }
}
