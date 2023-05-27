namespace SimApi.Test;

public abstract class Bird
{
    public abstract void Fly();
    public abstract void Walk( );
}

public class Chicken : Bird
{
    public override void Fly()
    {
        throw new NotImplementedException();
    }

    public override void Walk()
    {
       
    }
}
public class Canary : Bird
{
    public override void Fly()
    {
    }

    public override void Walk()
    {
    }
}



// liskov
interface ILive
{

}
interface IFlyable : ILive
{
     void Fly();
}
interface IWalkable  : ILive
{
    void Walk();
}
class ChickenL : IWalkable
{
    public void Walk()
    {
    }
}
class CanaryL : IWalkable, IFlyable
{
    public void Fly()
    {
    }

    public void Walk()
    {
    }
}