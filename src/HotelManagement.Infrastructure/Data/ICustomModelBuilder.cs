using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Infrastructure.Data
{
    public interface ICustomModelBuilder
    {
        void Build(ModelBuilder modelBuilder);
    }
}