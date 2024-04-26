namespace Application.Utils
{
    public interface IDelete <in TI>
    {
        bool Execute(TI dto);
    }
}