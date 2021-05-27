using System;
using System.Linq;
using FactorialEK.Model.Database.Entities;
using FactorialEK.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FactorialEK.Model.Database.Repositories.EntityFramework
{
    public class EFManufacturingOrServicePricesRepository : IManufacturingOrServicePricesRepository
    {
        private readonly FactorialEKDbContext _context;

        public EFManufacturingOrServicePricesRepository(FactorialEKDbContext context)
        {
            _context = context;
        }

        public bool ContainsManufacturingOrServicePriceByManufacturingOrServiceId(Guid manufacturingOrServiceId)
        {
            return _context.ManufacturingOrServicePrices.SingleOrDefault(manufacturingOrServicePrice =>
                manufacturingOrServicePrice.ManufacturingOrServiceId == manufacturingOrServiceId) != null;
        }

        public bool SaveManufacturingOrServicePrice(ManufacturingOrServicePrice entity)
        {
            if (entity.Id == default)
            {
                if (!ContainsManufacturingOrServicePriceByManufacturingOrServiceId(entity.ManufacturingOrServiceId))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetManufacturingOrServicePriceById(entity.Id);

                if (oldVersionEntity.ManufacturingOrServiceId != entity.ManufacturingOrServiceId)
                {
                    if (!ContainsManufacturingOrServicePriceByManufacturingOrServiceId(entity.ManufacturingOrServiceId))
                    {
                        _context.Entry(entity).State = EntityState.Modified;
                        _context.SaveChanges();

                        return true;
                    }
                }
                else
                {
                    _context.Entry(entity).State = EntityState.Modified;
                    _context.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        public ManufacturingOrServicePrice GetManufacturingOrServicePriceById(Guid id, bool track = false)
        {
            if (track)
            {
                return _context.ManufacturingOrServicePrices.SingleOrDefault(manufacturingOrServicePrice =>
                    manufacturingOrServicePrice.Id == id);
            }
            else
            {
                return _context.ManufacturingOrServicePrices.AsNoTracking().SingleOrDefault(manufacturingOrServicePrice =>
                    manufacturingOrServicePrice.Id == id);
            }
        }

        public ManufacturingOrServicePrice GetManufacturingOrServicePriceByManufacturingOrServiceId(Guid manufacturingOrServiceId,
            bool track = false)
        {
            if (track)
            {
                return _context.ManufacturingOrServicePrices.SingleOrDefault(manufacturingOrServicePrice =>
                    manufacturingOrServicePrice.ManufacturingOrServiceId == manufacturingOrServiceId);
            }
            else
            {
                return _context.ManufacturingOrServicePrices.AsNoTracking().SingleOrDefault(manufacturingOrServicePrice =>
                    manufacturingOrServicePrice.ManufacturingOrServiceId == manufacturingOrServiceId);
            }
        }

        public void DeleteManufacturingOrServicePriceById(Guid id)
        {
            _context.ManufacturingOrServicePrices.Remove(GetManufacturingOrServicePriceById(id));
            _context.SaveChanges();
        }
    }
}