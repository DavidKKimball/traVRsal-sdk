using UnityEngine;
using XNode;

namespace traVRsal.SDK
{
    [CreateNodeMenu(menuName: "Objects/Tween Position")]
    public class TweenPositionNode : Node
    {
        [Input] public bool call;
        [Input] public Transform transform;
        [Input] public Vector3 pos;
        [Input] public float duration;
        [Input] public bool relative;
        [Output(connectionType: ConnectionType.Override)] public bool Done;

        public override object GetValue(NodePort port)
        {
            if (port.IsOutput) return null;

            switch (port.fieldName)
            {
                case "call":
                    return call;

                case "transform":
                    return transform;

                case "pos":
                    return pos;

                case "duration":
                    return duration;

                case "relative":
                    return relative;


            }
            return null;
        }
    }
}