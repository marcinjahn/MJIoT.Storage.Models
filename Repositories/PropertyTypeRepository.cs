using MjIot.Storage.Models.EF6Db;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MJIot.Storage.Models.Repositiories
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
            List<PropertyType> properties = new List<PropertyType>();
            var currentType = deviceType;

            do
            {
                properties.AddRange(
                    Context.PropertyTypes
                        .Include(n => n.DeviceType)
                        .Where(n => n.DeviceType.Id == currentType.Id)
                );

                currentType = Context.DeviceTypes
                    .Include(n => n.BaseDeviceType)
                    .Where(n => n.Id == currentType.Id)
                    .First()
                    .BaseDeviceType;
            }
            while (currentType != null);

            return properties;
        }

        public MJIoTDBContext Context
        {
            get { return _context as MJIoTDBContext; }
        }
    }
}
