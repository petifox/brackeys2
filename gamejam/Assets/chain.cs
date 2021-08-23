using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chain : MonoBehaviour
{
    public AnimationCurve XPos;
    public AnimationCurve YPos;
    public AnimationCurve ZPos;

    public List<Transform> chains;

    public List<Transform> fake;
    void Update()
    {
        //remove current keys
        for (int i = 0; i < XPos.length; i++)
        {
            XPos.RemoveKey(i);
            YPos.RemoveKey(i);
            ZPos.RemoveKey(i);
        }

        //update curve
        for (int i = 0; i < chains.Count; i++)
        {
            float time = i * (1f / (chains.Count - 1f));

            XPos.AddKey(time, chains[i].position.x);

            YPos.AddKey(time, chains[i].position.y);

            ZPos.AddKey(time, chains[i].position.z);
        }
        
        //update positions
        for (int i = 0; i < fake.Count; i++)
        {
            float _time = i * (1f / (fake.Count - 1f));
            fake[i].position = new Vector3(XPos.Evaluate(_time), YPos.Evaluate(_time), ZPos.Evaluate(_time));
        }
    }
}
