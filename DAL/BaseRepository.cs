using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL
{
    public class BaseRepository<TEntity, TPrimary> : IBaseRepository<TEntity, TPrimary> where TEntity : BaseEntity<TPrimary>
    {
        public AppDbContext _context { get; set; }

        protected DbSet<TEntity> _entities;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public TEntity Create(TEntity entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public void Delete(TEntity entity, bool softDelete = true)
        {
            if (softDelete)
            {
                entity.IsDeleted = true;

                Update(entity);
            }
            else
            {
                _context.Attach(entity);
                _context.Remove(entity);

                _context.SaveChanges();
            }
        }

        public TEntity? FindById(TPrimary id)
        {
            return _entities.Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _entities.AsNoTracking();
        }

        public TEntity Update(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Attach(entity);
            }

            _context.Entry(entity).State = EntityState.Modified;

            _context.SaveChanges();

            return entity;
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.AsNoTracking().Where(predicate);
        }
    }
}
