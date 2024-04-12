using System;

namespace FaceAPI.Timesheets;

public abstract class TimesheetExcelDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}