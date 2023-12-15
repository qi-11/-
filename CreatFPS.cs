using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatFPS : MonoBehaviour
{
    public GameObject game;
    public ClipFPS clip;
    // Start is called before the first frame update
    void Start()
    {
        clip.AddFPS(20, 1, (i) =>
        {
            GameObject go = Instantiate(game,transform);
            go.transform.position = new Vector3(i,0,i);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
