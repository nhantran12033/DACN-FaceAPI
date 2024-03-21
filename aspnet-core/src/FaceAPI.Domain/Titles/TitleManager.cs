using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace FaceAPI.Titles
{
    public abstract class TitleManagerBase : DomainService
    {
        protected ITitleRepository _titleRepository;

        public TitleManagerBase(ITitleRepository titleRepository)
        {
            _titleRepository = titleRepository;
        }

        public virtual async Task<Title> CreateAsync(
        string? code = null, string? name = null, string? note = null)
        {

            var title = new Title(
             GuidGenerator.Create(),
             code, name, note
             );

            return await _titleRepository.InsertAsync(title);
        }

        public virtual async Task<Title> UpdateAsync(
            Guid id,
            string? code = null, string? name = null, string? note = null, [CanBeNull] string? concurrencyStamp = null
        )
        {

            var title = await _titleRepository.GetAsync(id);

            title.Code = code;
            title.Name = name;
            title.Note = note;

            title.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _titleRepository.UpdateAsync(title);
        }

    }
}