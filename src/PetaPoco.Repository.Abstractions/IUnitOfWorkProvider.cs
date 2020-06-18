namespace PetaPoco.Repository.Abstractions
{
    public interface IUnitOfWorkProvider
    {
        /// <summary>
        /// starts a unit of work
        /// </summary>
        /// <returns></returns>
        IUnitOfWork GetUnitOfWork();

        /// <summary>
        /// starts a unit of work and actively enlists all specified repositories
        /// </summary>
        /// <param name="enlistRepositories"></param>
        /// <returns></returns>
        IUnitOfWork GetUnitOfWork(params IRepository[] enlistRepositories);
    }
}
