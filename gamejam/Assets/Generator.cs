using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Generator : MonoBehaviour
{
    public List<GameObject> rocks;
    public Transform RockParent;
    public int RockAmount;
    [MinMaxSlider(0, 100)]
    public Vector2 RockScale; 
    [Space(10)]

    public List<GameObject> trees;
    public Transform TreeParent;
    public int TreeAmount;
    [MinMaxSlider(0, 100)]
    public Vector2 TreeScale;
    [Space(10)]

    public float space;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < RockAmount; i++)
        {
            Vector3 pos = Random.insideUnitSphere * space;
            pos.y = 0;

            Quaternion rot = Quaternion.Euler(new Vector3(Random.Range(0, 365), Random.Range(0, 365), Random.Range(0, 365)));

            Vector3 scl = new Vector3(Random.Range(RockScale.x, RockScale.y), Random.Range(RockScale.x, RockScale.y), Random.Range(RockScale.x, RockScale.y)); 

            GameObject obj = Instantiate(rocks[Random.Range(0, rocks.Count)], pos, rot, RockParent);

            obj.transform.localScale = scl;
        }

        for (int i = 0; i < TreeAmount; i++)
        {
            Vector3 pos = Random.insideUnitSphere * space;
            pos.y = 0;

            Quaternion rot = Quaternion.Euler(0, Random.Range(0, 365), 0);

            Vector3 scl = new Vector3(Random.Range(TreeScale.x, TreeScale.y), Random.Range(TreeScale.x, TreeScale.y), Random.Range(TreeScale.x, TreeScale.y));

            GameObject obj = Instantiate(trees[Random.Range(0, trees.Count)], pos, rot, TreeParent);

            obj.transform.localScale = scl;
        }
    }
}
