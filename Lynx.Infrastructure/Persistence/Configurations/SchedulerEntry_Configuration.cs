using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Constants;
using Lynx.Domain.Entities;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class SchedulerEntry_Configuration
    {
        protected override void ConfigureProperty(BasePropertyBuilder<SchedulerEntry> builder)
        {
            builder.Property(a => a.Remarks)
                .HasMaxLength(StringLengthConstant.Remarks);
        }

        protected override void ConfigureRelationship(BaseRelationshipBuilder<SchedulerEntry> builder)
        {
            builder.HasOne<ProviderTypeConfigScheduler>()
                .WithMany(a => a.N_ScheduleEntries)
                .HasForeignKey(a => a.TrackBillSchedulerID);
        }

        //protected override void SeedData(BaseSeeder<SchedulerEntry> builder)
        //{
        //    #region Lancaster
        //    //// lancaster
        //    //builder.HasData(new TrackBillScheduler
        //    //{
        //    //    ID = Guid.Empty.Increment(1),
        //    //    TrackBillID = Guid.Empty.Increment(3),
        //    //    StartDate = new DateTime(2021, 2, 19),
        //    //    EndDate = new DateTime(2021, 5, 19),
        //    //    Amount = 21000,
        //    //    DayFrequency = 23
        //    //});

        //    builder.HasData(new SchedulerEntry
        //    {
        //        ID = Guid.Empty.Increment(1),
        //        TrackBillSchedulerID = Guid.Parse(TrackBillIDConstants.Lancaster),
        //        Amount = 21000,
        //        DueDate = new DateTime(2021, 2, 23),
        //        IsGenerated = true,
        //        GeneratedUserBillID = Guid.Empty.Increment(2)
        //    });

        //    builder.HasData(new SchedulerEntry
        //    {
        //        ID = Guid.Empty.Increment(2),
        //        TrackBillSchedulerID = Guid.Parse(TrackBillIDConstants.Lancaster),
        //        Amount = 21000,
        //        DueDate = new DateTime(2021, 3, 23)
        //    });

        //    builder.HasData(new SchedulerEntry
        //    {
        //        ID = Guid.Empty.Increment(3),
        //        TrackBillSchedulerID = Guid.Parse(TrackBillIDConstants.Lancaster),
        //        Amount = 21000,
        //        DueDate = new DateTime(2021, 4, 23)
        //    });

        //    builder.HasData(new SchedulerEntry
        //    {
        //        ID = Guid.Empty.Increment(4),
        //        TrackBillSchedulerID = Guid.Parse(TrackBillIDConstants.Lancaster),
        //        Amount = 21000,
        //        DueDate = new DateTime(2021, 5, 23)
        //    });
        //    #endregion


        //    #region 588
        //    // 588
        //    //builder.HasData(new TrackBillScheduler
        //    //{
        //    //    ID = Guid.Empty.Increment(2),
        //    //    TrackBillID = Guid.Empty.Increment(4),
        //    //    StartDate = new DateTime(2021, 2, 19),
        //    //    EndDate = new DateTime(2021, 5, 19),
        //    //    Amount = 14000,
        //    //    DayFrequency = 28
        //    //});

        //    builder.HasData(new SchedulerEntry
        //    {
        //        ID = Guid.Empty.Increment(5),
        //        TrackBillSchedulerID = Guid.Parse(TrackBillIDConstants.Condo588),
        //        Amount = 14000,
        //        DueDate = new DateTime(2021, 2, 28),
        //        IsGenerated = true,
        //        GeneratedUserBillID = Guid.Empty.Increment(3)
        //    });

        //    builder.HasData(new SchedulerEntry
        //    {
        //        ID = Guid.Empty.Increment(6),
        //        TrackBillSchedulerID = Guid.Parse(TrackBillIDConstants.Condo588),
        //        Amount = 14000,
        //        DueDate = new DateTime(2021, 3, 28)
        //    });

        //    builder.HasData(new SchedulerEntry
        //    {
        //        ID = Guid.Empty.Increment(7),
        //        TrackBillSchedulerID = Guid.Parse(TrackBillIDConstants.Condo588),
        //        Amount = 14000,
        //        DueDate = new DateTime(2021, 4, 28)
        //    });

        //    builder.HasData(new SchedulerEntry
        //    {
        //        ID = Guid.Empty.Increment(8),
        //        TrackBillSchedulerID = Guid.Parse(TrackBillIDConstants.Condo588),
        //        Amount = 14000,
        //        DueDate = new DateTime(2021, 5, 28)
        //    });
        //    #endregion


        //    #region BRV
        //    // brv
        //    //builder.HasData(new TrackBillScheduler
        //    //{
        //    //    ID = Guid.Empty.Increment(3),
        //    //    TrackBillID = Guid.Empty.Increment(5),
        //    //    StartDate = new DateTime(2021, 2, 19),
        //    //    EndDate = new DateTime(2021, 5, 19),
        //    //    Amount = 15000,
        //    //    DayFrequency = 13
        //    //});

        //    builder.HasData(new SchedulerEntry
        //    {
        //        ID = Guid.Empty.Increment(9),
        //        TrackBillSchedulerID = Guid.Parse(TrackBillIDConstants.HondaBRV),
        //        Amount = 15000,
        //        DueDate = new DateTime(2021, 2, 13),
        //        IsGenerated = true,
        //        GeneratedUserBillID = Guid.Empty.Increment(4)
        //    });

        //    builder.HasData(new SchedulerEntry
        //    {
        //        ID = Guid.Empty.Increment(10),
        //        TrackBillSchedulerID = Guid.Parse(TrackBillIDConstants.HondaBRV),
        //        Amount = 15000,
        //        DueDate = new DateTime(2021, 3, 13)
        //    });

        //    builder.HasData(new SchedulerEntry
        //    {
        //        ID = Guid.Empty.Increment(11),
        //        TrackBillSchedulerID = Guid.Parse(TrackBillIDConstants.HondaBRV),
        //        Amount = 15000,
        //        DueDate = new DateTime(2021, 4, 13)
        //    });

        //    builder.HasData(new SchedulerEntry
        //    {
        //        ID = Guid.Empty.Increment(12),
        //        TrackBillSchedulerID = Guid.Parse(TrackBillIDConstants.HondaBRV),
        //        Amount = 15000,
        //        DueDate = new DateTime(2021, 5, 13)
        //    });
        //    #endregion
        //}
    }
}
