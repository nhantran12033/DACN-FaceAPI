using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FaceAPI.Timesheets
{
    public abstract class TimesheetCreateDtoBase
    {
        public string? Url { get; set; }
        public string? Image { get; set; }
        public string? Code { get; set; }
        public DateTime Time { get; set; }
        public string? Note { get; set; }
        public Guid? ScheduleId { get; set; }
        public Guid? ScheduleDetailId { get; set; }
        public Guid? ScheduleFormatId { get; set; }
    }
    public class AmazonItem
    {
        public double Confidence { get; set; }
        public string Face_Id { get; set; }
    }

    public class AmazonResponse
    {
        public string Status { get; set; }
        public List<AmazonItem> Items { get; set; } = new List<AmazonItem>();
        public double Cost { get; set; }
    }

    public class ApiResponse
    {
        public AmazonResponse Amazon { get; set; }
    }
}