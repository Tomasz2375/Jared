﻿using Jared.Shared.Dtos.UserDtos;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Presentation.Requests.User.Login;

public sealed record UserLoginCommand(UserLoginDto dto) : IRequest<Result<string>>;
