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
            float time = i * 1 / chains.Count;

            Keyframe XFrame = new Keyframe(time, chains[i].position.x);
            XPos.AddKey(XFrame);

            Keyframe YFrame = new Keyframe(time, chains[i].position.y);
            YPos.AddKey(YFrame);

            Keyframe ZFrame = new Keyframe(time, chains[i].position.z);
            ZPos.AddKey(ZFrame);
        }
        
        //update positions
        /*for (int i = 0; i < fake.Count; i++)
        {
            float time = (1 / fake.Count) * i;
            Vector3 POS = new Vector3(
                XPos.Evaluate(time),
                YPos.Evaluate(time),
                ZPos.Evaluate(time));
            fake[i].position = POS;
        }*/
    }
}
