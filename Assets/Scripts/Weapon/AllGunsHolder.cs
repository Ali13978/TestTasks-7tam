using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllGunsHolder : MonoBehaviour
{
    #region Singleton
    public static AllGunsHolder Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion

    [SerializeField] List<Gun> allGuns;

    public Gun SelectGun<T>()
    {
        Gun gun = null;

        foreach(Gun i in allGuns)
        {
            if(i.gameObject.TryGetComponent<T>(out T t))
            {
                gun = i;
                break;
            }
        }

        return gun;
    }
}
