using FaceAPI.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace FaceAPI.Permissions;

public class FaceAPIPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(FaceAPIPermissions.GroupName);

        myGroup.AddPermission(FaceAPIPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        myGroup.AddPermission(FaceAPIPermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(FaceAPIPermissions.MyPermission1, L("Permission:MyPermission1"));

        var departmentPermission = myGroup.AddPermission(FaceAPIPermissions.Departments.Default, L("Permission:Departments"));
        departmentPermission.AddChild(FaceAPIPermissions.Departments.Create, L("Permission:Create"));
        departmentPermission.AddChild(FaceAPIPermissions.Departments.Edit, L("Permission:Edit"));
        departmentPermission.AddChild(FaceAPIPermissions.Departments.Delete, L("Permission:Delete"));

        var positionPermission = myGroup.AddPermission(FaceAPIPermissions.Positions.Default, L("Permission:Positions"));
        positionPermission.AddChild(FaceAPIPermissions.Positions.Create, L("Permission:Create"));
        positionPermission.AddChild(FaceAPIPermissions.Positions.Edit, L("Permission:Edit"));
        positionPermission.AddChild(FaceAPIPermissions.Positions.Delete, L("Permission:Delete"));

        var salaryPermission = myGroup.AddPermission(FaceAPIPermissions.Salaries.Default, L("Permission:Salaries"));
        salaryPermission.AddChild(FaceAPIPermissions.Salaries.Create, L("Permission:Create"));
        salaryPermission.AddChild(FaceAPIPermissions.Salaries.Edit, L("Permission:Edit"));
        salaryPermission.AddChild(FaceAPIPermissions.Salaries.Delete, L("Permission:Delete"));

        var schedulePermission = myGroup.AddPermission(FaceAPIPermissions.Schedules.Default, L("Permission:Schedules"));
        schedulePermission.AddChild(FaceAPIPermissions.Schedules.Create, L("Permission:Create"));
        schedulePermission.AddChild(FaceAPIPermissions.Schedules.Edit, L("Permission:Edit"));
        schedulePermission.AddChild(FaceAPIPermissions.Schedules.Delete, L("Permission:Delete"));

        var scheduleDetailPermission = myGroup.AddPermission(FaceAPIPermissions.ScheduleDetails.Default, L("Permission:ScheduleDetails"));
        scheduleDetailPermission.AddChild(FaceAPIPermissions.ScheduleDetails.Create, L("Permission:Create"));
        scheduleDetailPermission.AddChild(FaceAPIPermissions.ScheduleDetails.Edit, L("Permission:Edit"));
        scheduleDetailPermission.AddChild(FaceAPIPermissions.ScheduleDetails.Delete, L("Permission:Delete"));

        var positionsPermission = myGroup.AddPermission(FaceAPIPermissions.Positionss.Default, L("Permission:Positionss"));
        positionsPermission.AddChild(FaceAPIPermissions.Positionss.Create, L("Permission:Create"));
        positionsPermission.AddChild(FaceAPIPermissions.Positionss.Edit, L("Permission:Edit"));
        positionsPermission.AddChild(FaceAPIPermissions.Positionss.Delete, L("Permission:Delete"));

        var titlePermission = myGroup.AddPermission(FaceAPIPermissions.Titles.Default, L("Permission:Titles"));
        titlePermission.AddChild(FaceAPIPermissions.Titles.Create, L("Permission:Create"));
        titlePermission.AddChild(FaceAPIPermissions.Titles.Edit, L("Permission:Edit"));
        titlePermission.AddChild(FaceAPIPermissions.Titles.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<FaceAPIResource>(name);
    }
}