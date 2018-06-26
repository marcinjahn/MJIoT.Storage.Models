using MJIoT.Storage.Models;
using MJIoT_DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJIoT_ModelStorage.Repositories
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
