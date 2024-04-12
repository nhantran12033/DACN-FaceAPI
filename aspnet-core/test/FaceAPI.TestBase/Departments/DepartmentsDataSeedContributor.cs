using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using FaceAPI.Departments;

namespace FaceAPI.Departments
{
    public class DepartmentsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public DepartmentsDataSeedContributor(IDepartmentRepository departmentRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _departmentRepository = departmentRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _departmentRepository.InsertAsync(new Department
            (
                id: Guid.Parse("c9488700-be07-443f-b0c3-edb970564bc2"),
                code: "b647dd069c254d1d869a56326b533aa9e98d396f76d440149ab54c4589e6fbcee61694576c96466d8167d0",
                name: "a8d3a1ea6421408da711d469fe2366e877d288c57",
                date: new DateTime(2018, 2, 10),
                note: "4d33cab6742140c0ba1e659491902c324d21317fba374031a8d83daeaee23cbc6"
            ));

            await _departmentRepository.InsertAsync(new Department
            (
                id: Guid.Parse("e0f9f448-881f-4013-94ca-df88c9fa759e"),
                code: "a326b0b0db47432aa548c1b98457333f78b257b109bb48609fd8305b957618b0",
                name: "553a6b1ed1",
                date: new DateTime(2006, 7, 27),
                note: "5750a2a8724c4af6bc401543a5f2c6d7b8d682fd5f3c4162a75bb77093"
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}