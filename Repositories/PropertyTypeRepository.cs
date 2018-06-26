using MJIoT_DBModel;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MJIoT.Storage.Models
{
    public interface IPropertyTypeRepository : IRepository<PropertyType>
    {
        IEnumerable<PropertyType> GetPropertiesOfDevice(DeviceType deviceType);
    }

    public class PropertyTypeRepository : Repository<PropertyType>, IPropertyTypeRepository
    {
        public PropertyTypeRepository(MJIoTDBContext context)
            : base(context)
        {
        }

        public IEnumerable<PropertyType> GetPropertiesOfDevice(DeviceType deviceType)
        {
            var properties = Context.PropertyTypes
                .Include(n => n.DeviceType)
                .Where(n => n.DeviceType.Id == deviceType.Id)
                .ToList();

            return properties;
        }

        public MJIoTDBContext Context
        {
            get { return _context as MJIoTDBContext; }
        }
    }
}
