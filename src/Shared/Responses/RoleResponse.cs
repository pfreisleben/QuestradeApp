﻿using System.ComponentModel.DataAnnotations;

namespace Shared.Responses;

public class RoleResponse
{
    public string Id { get; set; }

    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
}
