namespace PetaPoco.Repository.Abstractions
{
    public interface IRepository
    {
        void AssignUnitOfWork(Abstractions.IUnitOfWork unitOfWork);

    }
}
