﻿using Jared.Shared.Dtos.UserDtos;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Application.Requests.Users.Login;

public sealed record UserLoginCommand(UserLoginDto dto) : IRequest<Result<string>>;
