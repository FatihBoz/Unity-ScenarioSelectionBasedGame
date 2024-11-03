using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour //SERIALIZABLE BÝR SOUND CLASSI KULLANJP TEK BÝR AUDIO MANAGER ÝLE HALLET
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
