﻿using Jared.Application.Dtos.Abstractions;

namespace Jared.Application.Dtos.EpicDtos;

public class EpicRootDto : EntityDto<int>
{
    public string Title { get; set; } = default!;
    public int? ParentId { get; set; }
    public int ProjectId { get; set; }
}
