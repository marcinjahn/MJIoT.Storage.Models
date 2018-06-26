using MjIot.Storage.Models.EF6Db;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MJIot.Storage.Models.Repositiories
{
    public interface IConnectionRepository : IRepository<Connection>
    {
        List<Connection> GetUserConnections(int userId);
        List<Connection> GetDeviceConnections(Device device);
        Connection FindDuplicate(Connection connection);
    }

    public class ConnectionRepository : Repository<Connection>, IConnectionRepository
    {
        public ConnectionRepository(MJIoTDBContext context)
            : base(context)
        {
        }

        public List<Connection> GetUserConnections(int userId)
        {
            return Context.Connections
                .Include(n => n.SenderDevice)
                .Include(n => n.ListenerDevice)
                .Include(n => n.SenderProperty)
                .Include(n => n.ListenerProperty)
                .Where(n => n.User.Id == userId)
                .ToList();
        }

        public List<Connection> GetDeviceConnections(Device device)
        {
            return Context.Connections
                .Include(n => n.ListenerDevice)
                .Include(n => n.SenderProperty)
                .Include(n => n.ListenerProperty)
                //.Include(n => n.CalculationValue)
                //.Include(n => n.FilterValue)
                .Where(n => n.SenderDevice.Id == device.Id)
                .ToList();
        }

        public Connection FindDuplicate(Connection connection)
        {
            return Context.Connections
                .Where(n => n.SenderDevice.Id == connection.SenderDevice.Id &&
                    n.SenderProperty.Id == connection.SenderProperty.Id &&
                    n.ListenerDevice.Id == connection.ListenerDevice.Id &&
                    n.ListenerProperty.Id == connection.ListenerProperty.Id &&
                    n.Filter == connection.Filter &&
                    n.FilterValue == connection.FilterValue &&
                    n.Calculation == connection.Calculation &&
                    n.CalculationValue == connection.CalculationValue)
                    .FirstOrDefault();
        }

        public MJIoTDBContext Context
        {
            get { return _context as MJIoTDBContext; }
        }
    }
}
