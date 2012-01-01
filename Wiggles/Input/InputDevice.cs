// Credit goes to Sean James for the InputDevice code, courtesy of his blog. 
// The code is freely available for use by his blog readers.
// http://www.innovativegames.net/blog/blog/2008/11/16/engine-tutorial-6/

using System;

namespace InnovationEngine.Input
{
    public abstract class InputDevice <T> //: Component
    {
        public abstract T State { get; }
    }

    public class InputDeviceEventArgs<O, S> : EventArgs
    {
        public O Object;

        public InputDevice<S> Device;

        public S State;

        public InputDeviceEventArgs(O Object, InputDevice<S> Device)
        {
            this.Object = Object;
            this.Device = Device;
            this.State = ((InputDevice<S>)Device).State;
        }
    }
}
