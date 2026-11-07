
using QuizMaster.Domane.Interface;

namespace QuizMaster.InfroStructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QuizMasterDbContext _context;

        public UnitOfWork(QuizMasterDbContext context)
        {
            _context = context;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
