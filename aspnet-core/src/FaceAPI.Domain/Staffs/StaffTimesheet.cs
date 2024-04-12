using System;
using Volo.Abp.Domain.Entities;

namespace FaceAPI.Staffs
{
    public class StaffTimesheet : Entity
    {

        public Guid StaffId { get; protected set; }

        public Guid TimesheetId { get; protected set; }

        private StaffTimesheet()
        {

        }

        public StaffTimesheet(Guid staffId, Guid timesheetId)
        {
            StaffId = staffId;
            TimesheetId = timesheetId;
        }

        public override object[] GetKeys()
        {
            return new object[]
                {
                    StaffId,
                    TimesheetId
                };
        }
    }
}