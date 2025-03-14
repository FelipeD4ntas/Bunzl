﻿using System.Security.Claims;

namespace Bunzl.Core.Domain.Interfaces
{
    public interface ICurrentUser
    {
        string Name { get; }

        Guid GetUserId();

        string GetUserEmail();

        //string GetTenant();

        bool IsAuthenticated();

        bool IsInRole(string role);

        IEnumerable<Claim> GetUserClaims();
    }
}
