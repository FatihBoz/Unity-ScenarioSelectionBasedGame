using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour //SERIALIZABLE B�R SOUND CLASSI KULLANJP TEK B�R AUDIO MANAGER �LE HALLET
{
    public static SoundEffectManager Instance {  get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }

        Destroy(this.gameObject);
    }


}
