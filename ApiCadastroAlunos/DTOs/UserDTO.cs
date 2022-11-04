﻿using Microsoft.EntityFrameworkCore;

namespace ApiCadastroAlunos.DTOs
{
    [Keyless]
    public class UserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
