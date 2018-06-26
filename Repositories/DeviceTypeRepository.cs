using MJIot.Storage.Models;
using MjIot.Storage.Models.EF6Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJIot.Storage.Models.Repositiories
{
    public interface IDeviceTypeRepository : IRepository<DeviceType>
    {

    }

    public class DeviceTypeRepository : Repository<DeviceType>, IDeviceTypeRepository
    {
        public DeviceTypeRepository(MJIoTDBContext context)
            : base(context)
        {
        }

        public MJIoTDBContext Context
        {
            get { return _context as MJIoTDBContext; }
        }
    }
}
