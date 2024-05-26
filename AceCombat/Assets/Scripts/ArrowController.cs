using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour

{
    Vector3 dest;
    FighterController fighterControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        FighterController fighterControllerScript = GetComponent<FighterController>();
        dest=fighterControllerScript.destinationPoint;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(dest);
    }
}
