﻿using MediatR;

namespace Library.Auth.Business.CQRS.Contracts.Queries
{
    public class GetAllUserWithRolesQuery : IRequest<GetAllUserWithRolesQueryResult>
    {
    }
}