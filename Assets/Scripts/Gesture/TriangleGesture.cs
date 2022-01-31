using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Leap;
using Leap.Unity;
using UnityEngine;
/*
 * Triangle gesture: left index tip touch right index tip, same for both thumb, forming a triangle with both hands.
 * Also calculate a pointing ray, coming from the camera and passing through the triangle's center.
 */
public class TriangleGesture : Gesture
{
    private RiggedHand left;
    private RiggedHand right;
    
    public Vector3 direction = Vector3.forward; // pointing direction
    public Vector3 center = Vector3.zero; // pointing center

    public TriangleGesture(RiggedHand left, RiggedHand right)
    {
        this.left = left;
        this.right = right;
    }
    protected override bool Active
    {
        get
        {
            var left = this.left.GetLeapHand();
            var right = this.right.GetLeapHand();
            if (left == null || right == null) return false;

            var lindex = left.GetIndex();
            var lthumb = left.GetThumb();

            var rindex = right.GetIndex();
            var rthumb = right.GetThumb();

            center = Vector3.zero;
            center += ((rindex.TipPosition + lindex.TipPosition) / 2).ToVector3(); // point top of the triangle
            center += ((lindex.Bone(Bone.BoneType.TYPE_PROXIMAL).Center +
                        lthumb.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center) / 2).ToVector3(); // point left of the triangle
            center += ((rindex.Bone(Bone.BoneType.TYPE_PROXIMAL).Center +
                        rthumb.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center) / 2).ToVector3(); // point right of the triangle
            center /= 3;

            direction = (center - Camera.main.transform.position).normalized;

            return (lindex.TipPosition.DistanceTo(rindex.TipPosition) < 0.05 &&
                    lthumb.TipPosition.DistanceTo(rthumb.TipPosition) < 0.05);
        }
    }
}
