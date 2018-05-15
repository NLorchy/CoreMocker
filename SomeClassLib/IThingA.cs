namespace SomeClassLib
{
    public interface IThingA
    {
        string GetSomeString();

        IThingB Instance { get; }
    }
}