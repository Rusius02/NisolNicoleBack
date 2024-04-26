namespace Application.Utils
{
    public interface IQuery<out TO>
    {
        TO Execute();
    }
}