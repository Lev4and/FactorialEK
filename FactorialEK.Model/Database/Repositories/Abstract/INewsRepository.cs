using System;
using System.Linq;
using FactorialEK.Model.Database.Entities;

namespace FactorialEK.Model.Database.Repositories.Abstract
{
    public interface INewsRepository
    {
        bool SaveNews(News entity);

        News GetNewsById(Guid id, bool track = false);

        IQueryable<News> GetNews(bool track = false);

        IQueryable<News> GetNews(int itemsPerPage, int numberPage, bool track = false);

        void DeleteNewsById(Guid id);
    }
}