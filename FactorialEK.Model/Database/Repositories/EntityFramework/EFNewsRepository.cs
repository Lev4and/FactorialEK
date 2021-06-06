using System;
using System.Linq;
using FactorialEK.Model.Database.Entities;
using FactorialEK.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FactorialEK.Model.Database.Repositories.EntityFramework
{
    public class EFNewsRepository : INewsRepository
    {
        private readonly FactorialEKDbContext _context;

        public EFNewsRepository(FactorialEKDbContext context)
        {
            _context = context;
        }

        public bool SaveNews(News entity)
        {
            if (entity.Id == default)
            {
                _context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                _context.Entry(entity).State = EntityState.Modified;
            }

            _context.SaveChanges();

            return true;
        }

        public int GetCountNews()
        {
            return _context.News.Count();
        }

        public News GetNewsById(Guid id, bool track = false)
        {
            if (track)
            {
                return _context.News.SingleOrDefault(news => news.Id == id);
            }
            else
            {
                return _context.News.AsNoTracking().SingleOrDefault(news => news.Id == id);
            }
        }

        public IQueryable<News> GetNews(bool track = false)
        {
            if (track)
            {
                return _context.News;
            }
            else
            {
                return _context.News.AsNoTracking();
            }
        }

        public IQueryable<News> GetNews(int itemsPerPage, int numberPage, bool track = false)
        {
            if (track)
            {
                return _context.News
                    .OrderByDescending(news => news.CreatedAt)
                    .Skip((numberPage -  1) * itemsPerPage)
                    .Take(itemsPerPage);
            }
            else
            {
                return _context.News
                    .OrderByDescending(news => news.CreatedAt)
                    .Skip((numberPage -  1) * itemsPerPage)
                    .Take(itemsPerPage)
                    .AsNoTracking();
            }
        }

        public void DeleteNewsById(Guid id)
        {
            _context.News.Remove(GetNewsById(id));
            _context.SaveChanges();
        }
    }
}