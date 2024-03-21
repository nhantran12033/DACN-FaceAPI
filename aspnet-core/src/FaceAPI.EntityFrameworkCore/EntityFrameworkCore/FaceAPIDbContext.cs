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

namespace FaceAPI.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityProDbContext))]
[ReplaceDbContext(typeof(ISaasDbContext))]
[ConnectionStringName("Default")]
public class FaceAPIDbContext :
    AbpDbContext<FaceAPIDbContext>,
    IIdentityProDbContext,
    ISaasDbContext
{
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
            builder.Entity<ScheduleDetail>(b =>
{
    b.ToTable(FaceAPIConsts.DbTablePrefix + "ScheduleDetails", FaceAPIConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Name).HasColumnName(nameof(ScheduleDetail.Name));
    b.Property(x => x.From).HasColumnName(nameof(ScheduleDetail.From));
    b.Property(x => x.To).HasColumnName(nameof(ScheduleDetail.To));
    b.Property(x => x.Note).HasColumnName(nameof(ScheduleDetail.Note));
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
            builder.Entity<Schedule>(b =>
{
    b.ToTable(FaceAPIConsts.DbTablePrefix + "Schedules", FaceAPIConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Code).HasColumnName(nameof(Schedule.Code));
    b.Property(x => x.Name).HasColumnName(nameof(Schedule.Name));
    b.Property(x => x.DateFrom).HasColumnName(nameof(Schedule.DateFrom));
    b.Property(x => x.DateTo).HasColumnName(nameof(Schedule.DateTo));
    b.Property(x => x.Note).HasColumnName(nameof(Schedule.Note));
    b.HasOne<Department>().WithMany().HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.NoAction);
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
            builder.Entity<Salary>(b =>
{
    b.ToTable(FaceAPIConsts.DbTablePrefix + "Salaries", FaceAPIConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Code).HasColumnName(nameof(Salary.Code));
    b.Property(x => x.Allowance).HasColumnName(nameof(Salary.Allowance));
    b.Property(x => x.Basic).HasColumnName(nameof(Salary.Basic));
    b.Property(x => x.Bonus).HasColumnName(nameof(Salary.Bonus));
    b.Property(x => x.Total).HasColumnName(nameof(Salary.Total));
    b.HasMany(x => x.Departments).WithOne().HasForeignKey(x => x.SalaryId).IsRequired().OnDelete(DeleteBehavior.NoAction);
});

            builder.Entity<SalaryDepartment>(b =>
{
    b.ToTable(FaceAPIConsts.DbTablePrefix + "SalaryDepartment", FaceAPIConsts.DbSchema);
    b.ConfigureByConvention();

    b.HasKey(
        x => new { x.SalaryId, x.DepartmentId }
    );

    b.HasOne<Salary>().WithMany(x => x.Departments).HasForeignKey(x => x.SalaryId).IsRequired().OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Department>().WithMany().HasForeignKey(x => x.DepartmentId).IsRequired().OnDelete(DeleteBehavior.NoAction);

    b.HasIndex(
            x => new { x.SalaryId, x.DepartmentId }
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
    }
}