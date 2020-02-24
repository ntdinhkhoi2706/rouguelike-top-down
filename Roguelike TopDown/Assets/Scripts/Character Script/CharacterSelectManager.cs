using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectManager : MonoBehaviour
{
    public static CharacterSelectManager Instance;
    [HideInInspector]
    public List<PlayerController> characters;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
}
