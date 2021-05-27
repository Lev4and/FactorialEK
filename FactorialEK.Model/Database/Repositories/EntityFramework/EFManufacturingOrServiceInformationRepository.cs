using System;
using System.Linq;
using FactorialEK.Model.Database.Entities;
using FactorialEK.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FactorialEK.Model.Database.Repositories.EntityFramework
{
    public class EFManufacturingOrServiceInformationRepository : IManufacturingOrServiceInformationRepository
    {
        private readonly FactorialEKDbContext _context;

        public EFManufacturingOrServiceInformationRepository(FactorialEKDbContext context)
        {
            _context = context;
        }

        public bool ContainsManufacturingOrServiceInformationByManufacturingOrServiceId(Guid manufacturingOrServiceId)
        {
            return _context.ManufacturingOrServiceInformation.SingleOrDefault(manufacturingOrServiceInformation =>
                manufacturingOrServiceInformation.ManufacturingOrServiceId == manufacturingOrServiceId) != null;
        }

        public bool SaveManufacturingOrServiceInformation(ManufacturingOrServiceInformation entity)
        {
            if (entity.Id == default)
            {
                if (!ContainsManufacturingOrServiceInformationByManufacturingOrServiceId(
                    entity.ManufacturingOrServiceId))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetManufacturingOrServiceInformationById(entity.Id);

                if (oldVersionEntity.ManufacturingOrServiceId != entity.ManufacturingOrServiceId)
                {
                    if (!ContainsManufacturingOrServiceInformationByManufacturingOrServiceId(
                        entity.ManufacturingOrServiceId))
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

        public ManufacturingOrServiceInformation GetManufacturingOrServiceInformationById(Guid id, bool track = false)
        {
            if (track)
            {
                return _context.ManufacturingOrServiceInformation
                    .SingleOrDefault(manufacturingOrServiceInformation => manufacturingOrServiceInformation.Id == id);
            }
            else
            {
                return _context.ManufacturingOrServiceInformation.AsNoTracking()
                    .SingleOrDefault(manufacturingOrServiceInformation => manufacturingOrServiceInformation.Id == id);
            }
        }

        public ManufacturingOrServiceInformation GetManufacturingOrServiceInformationByManufacturingOrServiceId(
            Guid manufacturingOrServiceId, bool track = false)
        {
            if (track)
            {
                return _context.ManufacturingOrServiceInformation
                    .SingleOrDefault(manufacturingOrServiceInformation => 
                        manufacturingOrServiceInformation.ManufacturingOrServiceId == manufacturingOrServiceId);
            }
            else
            {
                return _context.ManufacturingOrServiceInformation.AsNoTracking()
                    .SingleOrDefault(manufacturingOrServiceInformation => 
                        manufacturingOrServiceInformation.ManufacturingOrServiceId == manufacturingOrServiceId);
            }
        }

        public void DeleteManufacturingOrServiceInformationById(Guid id)
        {
            _context.ManufacturingOrServiceInformation.Remove(GetManufacturingOrServiceInformationById(id));
            _context.SaveChanges();
        }
    }
}