using Leap.Unity;

/*
 * Pinching gesture: index tip touch thumb tip.
 */
public class PinchGesture : Gesture
{
    private readonly RiggedHand hand;
    public PinchGesture(RiggedHand hand)
    {
        this.hand = hand;
    }
    protected override bool Active
    {
        get
        {
            var hand = this.hand.GetLeapHand();
            if (hand == null) return false;
            var index = hand.GetIndex();
            var thumb = hand.GetThumb();
            return (thumb.TipPosition.DistanceTo(index.TipPosition) < 0.04);
        }
    }
}