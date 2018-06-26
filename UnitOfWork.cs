using MjIot.Storage.Models.EF6Db;
using MJIot.Storage.Models.Repositiories;
using MJIoT_DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJIot.Storage.Models
{
    public interface IUnitOfWork : IDisposable
    {
        IDeviceRepository Devices { get; }
        IDeviceTypeRepository DeviceTypes { get; }
        IPropertyTypeRepository PropertyTypes { get; }
        IUserRepository Users { get; }
        IConnectionRepository Connections { get; }

        int Save();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly MJIoTDBContext _context;

        public UnitOfWork() : this(new MJIoTDBContext())
        {
        }

        public UnitOfWork(MJIoTDBContext context)
        {
            _context = context;

            Devices = new DeviceRepository(_context);
            DeviceTypes = new DeviceTypeRepository(_context);
            PropertyTypes = new PropertyTypeRepository(_context);
            Users = new UserRepository(_context);
            Connections = new ConnectionRepository(_context);
        }

        public IDeviceRepository Devices { get; private set; }
        public IDeviceTypeRepository DeviceTypes { get; private set; }
        public IPropertyTypeRepository PropertyTypes { get; private set; }
        public IUserRepository Users { get; private set; }
        public IConnectionRepository Connections { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
