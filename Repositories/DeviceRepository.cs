using MJIot.Storage.Models.Enums;
using MjIot.Storage.Models.EF6Db;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MJIot.Storage.Models.Repositiories
{


    public interface IDeviceRepository : IRepository<Device>
    {
        IEnumerable<Device> GetDevicesOfUser(int userId);
        string GetDeviceName(Device device);
        DeviceRole GetDeviceRole(Device device);
        DeviceType GetDeviceType(int deviceId);
    }

    public class DeviceRepository : Repository<Device>, IDeviceRepository
    {
        public DeviceRepository(MJIoTDBContext context)
            : base(context)
        {
        }

        public IEnumerable<Device> GetDevicesOfUser(int userId)
        {
            return Context.Devices.Include(n => n.DeviceType)
                .Where(n => n.User.Id == userId).ToList();
        }

        public string GetDeviceName(Device device)
        {
            return Context.DeviceProperties.Include("PropertyType")
                .Where(n => n.Device.Id == device.Id && n.PropertyType.Name == "DisplayName")
                .FirstOrDefault().PropertyValue;
        }

        public DeviceRole GetDeviceRole(Device device)
        {
            var currentType = device.DeviceType;
            bool isSender = false, isListener = false;
            do
            {
                isSender =  isSender ? isSender : Context.PropertyTypes.Where(n => n.DeviceType.Id == currentType.Id).Any(n => n.IsSenderProperty);
                isListener = isListener ? isListener : Context.PropertyTypes.Where(n => n.DeviceType.Id == currentType.Id).Any(n => n.IsListenerProperty);

                if (isSender && isListener)
                    return DeviceRole.bidirectional; 

                currentType = Context.DeviceTypes
                    .Include(n => n.BaseDeviceType)
                    .Where(n => n.Id == currentType.Id)
                    .First()
                    .BaseDeviceType;
            }
            while (currentType != null);

            if (isSender)
                return DeviceRole.sender;
            else if (isListener)
                return DeviceRole.listener;
            else
                return DeviceRole.none;
        }

        public DeviceType GetDeviceType(int deviceId)
        {
            return Context.Devices
                .Include(n => n.DeviceType)
                .Where(n => n.Id == deviceId)
                .Select(n => n.DeviceType)
                .FirstOrDefault();
        }

        public MJIoTDBContext Context
        {
            get { return _context as MJIoTDBContext; }
        }
    }
}
