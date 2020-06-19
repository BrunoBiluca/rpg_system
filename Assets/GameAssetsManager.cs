using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssetsManager : MonoBehaviour {

    private static GameAssetsManager _i;
    public static GameAssetsManager i { 
        get {
            if(_i == null) _i = FindObjectOfType<GameAssetsManager>();
            return _i;
        }
    }

    [SerializeField] public GameObject damagePopup;

}
