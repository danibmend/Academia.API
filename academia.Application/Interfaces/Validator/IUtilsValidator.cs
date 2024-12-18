﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace academia.Application.Interfaces.Validator
{
    public interface IUtilsValidator
    {
        Task<bool> NomeUsuarioExistenteAsync(string? nome, CancellationToken cancellationToken);
        Task<bool> EmailExistenteAsync(string? email, CancellationToken cancellationToken);
        Task<bool> NomeUsuarioExistenteAtualizarAsync(string? nome, long id, CancellationToken cancellationToken);
        Task<bool> EmailExistenteAtualizarAsync(string? email, long id, CancellationToken cancellationToken);
        bool EmailValido(string email);
    }
}
