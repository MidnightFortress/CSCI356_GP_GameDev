using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMe : MonoBehaviour
{
/*    [SerializeField] private Vector3 startPos;*//**/

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Debug.Log(gameObject.scene.name);
    }
}
