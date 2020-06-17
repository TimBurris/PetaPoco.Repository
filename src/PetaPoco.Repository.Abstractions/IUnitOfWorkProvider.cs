namespace PetaPoco.Repository.Abstractions
{
    public interface IUnitOfWorkProvider
    {
        IUnitOfWork GetUnitOfWork();
    }
}
