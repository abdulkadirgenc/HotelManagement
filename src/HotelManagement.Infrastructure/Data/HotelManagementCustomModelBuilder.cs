using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Infrastructure.Data
{
    class HotelManagementCustomModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<XYAssociation>()
            //    .HasKey(xya => new { xyaa.XId, xya.YId });
        }
    }
}