﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using academia.Domain.Exceptions;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;

namespace academia.Infrastructure
{
    public class ErrorHandlerMiddleware
    { //Classe que vai lidar com qualquer excessao gerada dentro da nossa aplicação, tanto as esperadas como as inesperadas.
        private readonly RequestDelegate next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //dentro desse trycatch ele vai analisar o tipo de excessão gerada de acordo com as classes criadas ou a genérica (exception) e vai lidar com elas.
            try
            {
                await next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (NotFoundException ex)
            {
                await HandleNotFoundExceptionAsync(context, ex);
            }
            catch (BusinessException ex)
            {
                await HandleBusinnesExceptionAsync(context, ex);
            }
            catch (DatabaseException ex)
            {
                await HandleDatabaseExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleNotFoundExceptionAsync(HttpContext context, NotFoundException exception)
        {
            await HandleExceptionAsync(context, exception, HttpStatusCode.NotFound);
        }

        private async Task HandleBusinnesExceptionAsync(HttpContext context, BusinessException exception)
        {
            await HandleExceptionAsync(context, exception, HttpStatusCode.BadRequest);
        }

        private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            await HandleExceptionAsync(context, exception, HttpStatusCode.Unauthorized);
        }

        private async Task HandleDatabaseExceptionAsync(HttpContext context, DatabaseException exception)
        {
            await HandleExceptionAsync(context, exception, HttpStatusCode.Conflict);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
        {
            //Aqui é montado o nosso response padrão para qualquer endpoint em caso de erro, para ajudar o frontend (mensagem) e o desenvolvedor em caso de debug
            var code = statusCode;

            var result = JsonConvert.SerializeObject(new
            {
                code = (int)code,
                method = context.Request.Method,
                url = context.Request.Host.Value + context.Request.Path.Value,
                mensagem = exception.Message,
                details = exception.StackTrace
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(result);
        }
    }
}
