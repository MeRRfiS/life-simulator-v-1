using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        var hits = Physics.SphereCastAll(transform.localPosition, 1000f, transform.localPosition, 0, 5);
        Debug.Log(transform.localPosition + " " + hits.Length);
    }
}
