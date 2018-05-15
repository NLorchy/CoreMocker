using System;

namespace SomeClassLib
{
    public class SomeClass
    {
        private readonly IThingA _thingA;
        private readonly IThingB _thingB;

        public SomeClass(IThingA thingA, IThingB thingB)
        {
            _thingA = thingA ?? throw new ArgumentNullException(nameof(thingA));
            _thingB = thingB ?? throw new ArgumentNullException(nameof(thingB));
        }

        public int ComputeSomething(int val)
        {
            return _thingB.SomeInt + val;
        }

        public string ConcatTopThing(string val)
        {
            return $"{val}{_thingA.GetSomeString()}";
        }

        public string SuperConcat(string val)
        {
            return $"{val}{_thingA.GetSomeString()}{_thingB.SampleMethod()}";
        }
    }
}
