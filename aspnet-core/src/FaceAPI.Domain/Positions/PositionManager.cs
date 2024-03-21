using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace FaceAPI.Positions
{
    public abstract class PositionManagerBase : DomainService
    {
        protected IPositionRepository _positionRepository;

        public PositionManagerBase(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public virtual async Task<Position> CreateAsync(
        Guid? departmentId, string? code = null, string? name = null, string? note = null)
        {

            var position = new Position(
             GuidGenerator.Create(),
             departmentId, code, name, note
             );

            return await _positionRepository.InsertAsync(position);
        }

        public virtual async Task<Position> UpdateAsync(
            Guid id,
            Guid? departmentId, string? code = null, string? name = null, string? note = null, [CanBeNull] string? concurrencyStamp = null
        )
        {

            var position = await _positionRepository.GetAsync(id);

            position.DepartmentId = departmentId;
            position.Code = code;
            position.Name = name;
            position.Note = note;

            position.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _positionRepository.UpdateAsync(position);
        }

    }
}