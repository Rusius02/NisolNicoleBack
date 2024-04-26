namespace Application.Utils
{
    public interface IWriting<out TO, in TI>
    {
        TO Execute(TI dto);
    }
    
}