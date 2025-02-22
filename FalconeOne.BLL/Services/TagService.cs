﻿using FalconeOne.BLL.Helpers;
using FalconeOne.BLL.Interfaces;
using FalconOne.DAL.Contracts;
using FalconOne.Models.Dtos.Tags;
using FalconOne.Models.Entities.Tags;
using FalconOne.Models.Entities.Users;
using IdenticonSharp.Identicons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace FalconeOne.BLL.Services
{
    public class TagService : BaseService, ITagService
    {
        public TagService(UserManager<User> userManager, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ITenantProvider tenantProvider, IIdenticonProvider identiconProvider) : base(userManager, unitOfWork, httpContextAccessor, configuration, tenantProvider, identiconProvider)
        {

        }

        public async Task<bool> TagEntityAsync(TagEntityDto model, CancellationToken cancellationToken)
        {
            switch (model.Type)
            {
                case FalconOne.Enumerations.Tags.TaggableEntityTypeEnum.User:
                    await TagUserAsync(model, cancellationToken);
                    break;
            }

            return false;
        }

        public async Task<bool> TagUserAsync(TagEntityDto model, CancellationToken cancellationToken)
        {
            if (!model.Tag.IsNew)
            {
                await CheckTagExistence(model.Tag.Id, cancellationToken);
            }

            var tag = await _unitOfWork.TagRepository.FindAsync(
                x => x.Name == model.Tag.Name && !x.IsDeleted, cancellationToken);

            var tagId = tag?.Id ?? await CreateNewTagAsync(model.Tag.Name, cancellationToken);
            if (tagId == Guid.Empty) return false;

            return await AssociateTagWithUserAsync(model.UserId, tagId, cancellationToken);
        }

        private async Task<Guid> CreateNewTagAsync(string tagName, CancellationToken cancellationToken)
        {
            var newTag = await _unitOfWork.TagRepository.AddAsync(new Tag
            {
                Name = tagName,
                CreatedOn = DateTime.UtcNow,
            }, cancellationToken);

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return result > 0 ? newTag.Id : Guid.Empty;
        }

        private async Task<bool> AssociateTagWithUserAsync(Guid userId, Guid tagId, CancellationToken cancellationToken)
        {
            await _unitOfWork.EntityTagRepository.AddAsync(new EntityTag
            {
                UserId = userId,
                TagId = tagId,
                CreatedOn = DateTime.UtcNow
            }, cancellationToken);

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }

        private async Task CheckTagExistence(Guid id, CancellationToken cancellationToken)
        {
            var tag = await _unitOfWork.TagRepository.FindAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

            if(tag is null)
            {
                throw new Exception(ErrorMessages.SOMETHING_WENT_WRONG);
            }
        }
    }
}
