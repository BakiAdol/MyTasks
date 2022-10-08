using Microsoft.EntityFrameworkCore;

namespace MyTasksClassLib.DataAccess.Repository
{
    public class Repository<TEntity> where TEntity : class
    {
        #region Props
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        #endregion

        #region Ctor
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }
        #endregion

        #region Methods
        public async Task CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveDatabase();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await SaveDatabase();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await SaveDatabase();
        }

        private async Task SaveDatabase()
        {
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}
