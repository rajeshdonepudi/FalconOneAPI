﻿using FalconOne.Models.Entities.Security;
using FalconOne.Models.EntityContracts;

namespace FalconOne.DAL.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ITagRepository TagRepository { get; }
        IEntityTagRepository EntityTagRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        ISecurityGroupRepository SecurityGroupRepository { get; }
        ITenantUserRepository TenantUserRepository { get; }
        ITenantUserSecurityGroupRepository TenantUserSecurityGroupRepository { get; }
        IPermissionRepository PermissionRepository { get; }
        IGenericRepository<RefreshToken> RefreshTokenRepository { get; }
        ISecurityRolesRepository SecurityRolesRepository { get; }
        IAppThemeRepository AppThemeRepository { get; }
        IUserRepository UserRepository { get; }
        ITenantRepository TenantRepository { get; }
        IMailRepository MailRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IExpenseRepository ExpenseRepository { get; }
        IExpenseCategoryRepository ExpenseCategoryRepository { get; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
