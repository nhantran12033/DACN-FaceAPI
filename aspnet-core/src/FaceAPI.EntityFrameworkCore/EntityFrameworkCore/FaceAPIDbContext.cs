using FaceAPI.ScheduleFormats;
using FaceAPI.Staffs;
using FaceAPI.Timesheets;
using FaceAPI.Titles;
using FaceAPI.ScheduleDetails;
using FaceAPI.Schedules;
using FaceAPI.Salaries;
using FaceAPI.Positions;
using FaceAPI.Departments;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;
using Volo.Saas.Editions;
using Volo.Saas.Tenants;
using Volo.Abp.Gdpr;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.FileManagement.EntityFrameworkCore;
using Volo.Chat.EntityFrameworkCore;

namespace FaceAPI.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityProDbContext))]
[ReplaceDbContext(typeof(ISaasDbContext))]
[ConnectionStringName("Default")]
public class FaceAPIDbContext :
    AbpDbContext<FaceAPIDbContext>,
    IIdentityProDbContext,
    ISaasDbContext
{
    public DbSet<ScheduleFormat> ScheduleFormats { get; set; } = null!;
    public DbSet<Staff> Staffs { get; set; } = null!;
    public DbSet<Timesheet> Timesheets { get; set; } = null!;
    public DbSet<Title> Titles { get; set; } = null!;
    public DbSet<ScheduleDetail> ScheduleDetails { get; set; } = null!;
    public DbSet<Schedule> Schedules { get; set; } = null!;
    public DbSet<Salary> Salaries { get; set; } = null!;
    public DbSet<Position> Positions { get; set; } = null!;
    public DbSet<Department> Departments { get; set; } = null!;
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // SaaS
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Edition> Editions { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public FaceAPIDbContext(DbContextOptions<FaceAPIDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentityPro();
        builder.ConfigureOpenIddictPro();
        builder.ConfigureFeatureManagement();
        builder.ConfigureLanguageManagement();
        builder.ConfigureSaas();
        builder.ConfigureTextTemplateManagement();
        builder.ConfigureBlobStoring();
        builder.ConfigureGdpr();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(FaceAPIConsts.DbTablePrefix + "YourEntities", FaceAPIConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Position>(b =>
{
    b.ToTable(FaceAPIConsts.DbTablePrefix + "Positions", FaceAPIConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Code).HasColumnName(nameof(Position.Code));
    b.Property(x => x.Name).HasColumnName(nameof(Position.Name));
    b.Property(x => x.Note).HasColumnName(nameof(Position.Note));
    b.HasOne<Department>().WithMany().HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }

        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Staff>(b =>
{
    b.ToTable(FaceAPIConsts.DbTablePrefix + "Staffs", FaceAPIConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Image).HasColumnName(nameof(Staff.Image));
    b.Property(x => x.Code).HasColumnName(nameof(Staff.Code));
    b.Property(x => x.Name).HasColumnName(nameof(Staff.Name));
    b.Property(x => x.Sex).HasColumnName(nameof(Staff.Sex));
    b.Property(x => x.Birthday).HasColumnName(nameof(Staff.Birthday));
    b.Property(x => x.StartWork).HasColumnName(nameof(Staff.StartWork));
    b.Property(x => x.Phone).HasColumnName(nameof(Staff.Phone));
    b.Property(x => x.Email).HasColumnName(nameof(Staff.Email));
    b.Property(x => x.Address).HasColumnName(nameof(Staff.Address));
    b.Property(x => x.Debt).HasColumnName(nameof(Staff.Debt));
    b.Property(x => x.Note).HasColumnName(nameof(Staff.Note));
    b.HasOne<Department>().WithMany().HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Title>().WithMany().HasForeignKey(x => x.TitleId).OnDelete(DeleteBehavior.NoAction);
    b.HasMany(x => x.Timesheets).WithOne().HasForeignKey(x => x.StaffId).IsRequired().OnDelete(DeleteBehavior.NoAction);
});

            builder.Entity<StaffTimesheet>(b =>
{
    b.ToTable(FaceAPIConsts.DbTablePrefix + "StaffTimesheet", FaceAPIConsts.DbSchema);
    b.ConfigureByConvention();

    b.HasKey(
        x => new { x.StaffId, x.TimesheetId }
    );

    b.HasOne<Staff>().WithMany(x => x.Timesheets).HasForeignKey(x => x.StaffId).IsRequired().OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Timesheet>().WithMany().HasForeignKey(x => x.TimesheetId).IsRequired().OnDelete(DeleteBehavior.NoAction);

    b.HasIndex(
            x => new { x.StaffId, x.TimesheetId }
    );
});
        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Department>(b =>
{
    b.ToTable(FaceAPIConsts.DbTablePrefix + "Departments", FaceAPIConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Code).HasColumnName(nameof(Department.Code));
    b.Property(x => x.Name).HasColumnName(nameof(Department.Name));
    b.Property(x => x.Date).HasColumnName(nameof(Department.Date));
    b.Property(x => x.Note).HasColumnName(nameof(Department.Note));
    b.HasMany(x => x.Titles).WithOne().HasForeignKey(x => x.DepartmentId).IsRequired().OnDelete(DeleteBehavior.NoAction);
});

            builder.Entity<DepartmentTitle>(b =>
{
    b.ToTable(FaceAPIConsts.DbTablePrefix + "DepartmentTitle", FaceAPIConsts.DbSchema);
    b.ConfigureByConvention();

    b.HasKey(
        x => new { x.DepartmentId, x.TitleId }
    );

    b.HasOne<Department>().WithMany(x => x.Titles).HasForeignKey(x => x.DepartmentId).IsRequired().OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Title>().WithMany().HasForeignKey(x => x.TitleId).IsRequired().OnDelete(DeleteBehavior.NoAction);

    b.HasIndex(
            x => new { x.DepartmentId, x.TitleId }
    );
});
        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        builder.ConfigureFileManagement();
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Salary>(b =>
{
    b.ToTable(FaceAPIConsts.DbTablePrefix + "Salaries", FaceAPIConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Code).HasColumnName(nameof(Salary.Code));
    b.Property(x => x.Allowance).HasColumnName(nameof(Salary.Allowance));
    b.Property(x => x.Basic).HasColumnName(nameof(Salary.Basic));
    b.Property(x => x.Bonus).HasColumnName(nameof(Salary.Bonus));
    b.Property(x => x.Total).HasColumnName(nameof(Salary.Total));
    b.HasOne<Department>().WithMany().HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Title>().WithMany().HasForeignKey(x => x.TitleId).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Title>(b =>
{
    b.ToTable(FaceAPIConsts.DbTablePrefix + "Titles", FaceAPIConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Code).HasColumnName(nameof(Title.Code));
    b.Property(x => x.Name).HasColumnName(nameof(Title.Name));
    b.Property(x => x.Note).HasColumnName(nameof(Title.Note));
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Schedule>(b =>
{
    b.ToTable(FaceAPIConsts.DbTablePrefix + "Schedules", FaceAPIConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Code).HasColumnName(nameof(Schedule.Code));
    b.Property(x => x.Name).HasColumnName(nameof(Schedule.Name));
    b.Property(x => x.DateFrom).HasColumnName(nameof(Schedule.DateFrom));
    b.Property(x => x.DateTo).HasColumnName(nameof(Schedule.DateTo));
    b.Property(x => x.Note).HasColumnName(nameof(Schedule.Note));
    b.HasOne<Staff>().WithMany().IsRequired().HasForeignKey(x => x.StaffId).OnDelete(DeleteBehavior.NoAction);
    b.HasMany(x => x.ScheduleDetails).WithOne().HasForeignKey(x => x.ScheduleId).IsRequired().OnDelete(DeleteBehavior.NoAction);
});

            builder.Entity<ScheduleScheduleDetail>(b =>
{
    b.ToTable(FaceAPIConsts.DbTablePrefix + "ScheduleScheduleDetail", FaceAPIConsts.DbSchema);
    b.ConfigureByConvention();

    b.HasKey(
        x => new { x.ScheduleId, x.ScheduleDetailId }
    );

    b.HasOne<Schedule>().WithMany(x => x.ScheduleDetails).HasForeignKey(x => x.ScheduleId).IsRequired().OnDelete(DeleteBehavior.NoAction);
    b.HasOne<ScheduleDetail>().WithMany().HasForeignKey(x => x.ScheduleDetailId).IsRequired().OnDelete(DeleteBehavior.NoAction);

    b.HasIndex(
            x => new { x.ScheduleId, x.ScheduleDetailId }
    );
});
        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        builder.ConfigureChat();
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<ScheduleFormat>(b =>
{
    b.ToTable(FaceAPIConsts.DbTablePrefix + "ScheduleFormats", FaceAPIConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Name).HasColumnName(nameof(ScheduleFormat.Name));
    b.Property(x => x.Date).HasColumnName(nameof(ScheduleFormat.Date));
    b.Property(x => x.FromHours).HasColumnName(nameof(ScheduleFormat.FromHours));
    b.Property(x => x.ToHours).HasColumnName(nameof(ScheduleFormat.ToHours));
    b.Property(x => x.Note).HasColumnName(nameof(ScheduleFormat.Note));
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<ScheduleDetail>(b =>
{
    b.ToTable(FaceAPIConsts.DbTablePrefix + "ScheduleDetails", FaceAPIConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Name).HasColumnName(nameof(ScheduleDetail.Name));
    b.Property(x => x.FromDate).HasColumnName(nameof(ScheduleDetail.FromDate));
    b.Property(x => x.ToDate).HasColumnName(nameof(ScheduleDetail.ToDate));
    b.Property(x => x.Note).HasColumnName(nameof(ScheduleDetail.Note));
    b.HasMany(x => x.ScheduleFormats).WithOne().HasForeignKey(x => x.ScheduleDetailId).IsRequired().OnDelete(DeleteBehavior.NoAction);
});

            builder.Entity<ScheduleDetailScheduleFormat>(b =>
{
    b.ToTable(FaceAPIConsts.DbTablePrefix + "ScheduleDetailScheduleFormat", FaceAPIConsts.DbSchema);
    b.ConfigureByConvention();

    b.HasKey(
        x => new { x.ScheduleDetailId, x.ScheduleFormatId }
    );

    b.HasOne<ScheduleDetail>().WithMany(x => x.ScheduleFormats).HasForeignKey(x => x.ScheduleDetailId).IsRequired().OnDelete(DeleteBehavior.NoAction);
    b.HasOne<ScheduleFormat>().WithMany().HasForeignKey(x => x.ScheduleFormatId).IsRequired().OnDelete(DeleteBehavior.NoAction);

    b.HasIndex(
            x => new { x.ScheduleDetailId, x.ScheduleFormatId }
    );
});
        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Timesheet>(b =>
{
    b.ToTable(FaceAPIConsts.DbTablePrefix + "Timesheets", FaceAPIConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Image).HasColumnName(nameof(Timesheet.Image));
    b.Property(x => x.Code).HasColumnName(nameof(Timesheet.Code));
    b.Property(x => x.Time).HasColumnName(nameof(Timesheet.Time));
    b.Property(x => x.Note).HasColumnName(nameof(Timesheet.Note));
    b.HasOne<Schedule>().WithMany().HasForeignKey(x => x.ScheduleId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<ScheduleDetail>().WithMany().HasForeignKey(x => x.ScheduleDetailId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<ScheduleFormat>().WithMany().HasForeignKey(x => x.ScheduleFormatId).OnDelete(DeleteBehavior.NoAction);
});

        }
    }
}