﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Organizations;
using Refit;

namespace ExternDotnetSDK.Clients.Organizations
{
    public class OrganizationsClient
    {
        private readonly IOrganizationClientRefit clientRefit;

        public OrganizationsClient(HttpClient client) 
            => clientRefit = RestService.For<IOrganizationClientRefit>(client);

        public async Task<OrganizationBatch> SearchOrganizationsAsync(
            Guid accountId,
            string inn = null,
            string kpp = null,
            int skip = 0,
            int take = 1000) => await clientRefit.GetAllOrganizations(accountId, inn, kpp, skip, take);

        public async Task<Organization> GetOrganizationAsync(Guid accountId, Guid orgId) 
            => await clientRefit.GetOrganization(accountId, orgId);

        public async Task<Organization> UpdateOrganizationAsync(Guid accountId, Guid orgId, string newName)
        {
            var request = new UpdateOrganizationRequestDto
            {
                Name = newName
            };
            return await clientRefit.UpdateOrganization(accountId, orgId, request);
        }

        public async Task<Organization> CreateOrganizationAsync(Guid accountId, string inn, string kpp, string name)
        {
            var request = new CreateOrganizationRequestDto
            {
                Inn = inn,
                Kpp = kpp,
                Name = name
            };
            return await clientRefit.CreateOrganization(accountId, request);
        }

        public async Task DeleteOrganization(Guid accountId, Guid orgId)
            => await clientRefit.DeleteOrganization(accountId, orgId);
    }
}