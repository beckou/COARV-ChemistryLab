using System;

/*
 * Represent a hand gesture state.
 * Given a function determining whether the gesture is active or not, provide info regarding if the gesture
 * was started, stopped, or held this current frame.
 */
public class Gesture
{
    public bool Start => state == State.START;
    public bool Stop => state == State.STOP;
    public bool Hold => state == State.HOLD;
    
    private enum State
    {
        NONE,
        START,
        HOLD,
        STOP
    }

    private State state = State.NONE;
    
    /*
     * Getter to overwrite in custom gestures / children classes.
     * Returns true if gesture is active, false otherwise.
     */
    protected virtual bool Active => false;
    
    /*
     * Update the gesture state.
     * Should be called once per update.
     */
    public void Update()
    {
        switch (state)
        {
            case State.NONE:
                state = Active ? State.START : State.NONE;
                break;
            case State.START:
            case State.HOLD:
                state = Active ? State.HOLD : State.STOP;
                break;
            case State.STOP:
                state = Active ? State.START : State.NONE;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
