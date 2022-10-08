#region Globals
global using System.Configuration;
global using  mono_financialbot_backend_cs_external_serivces.Providers.RabbitMQ.Interfaces;
global using mono_financialbot_backend_cs_external_serivces.Providers.RabbitMQ.Services;
global using mono_financialbot_backend_cs_external_serivces.Providers.RabbitMQ.Models;
global using mono_financialbot_backend_cs_external_serivces.Providers.Bot;
global using  mono_financialbot_backend_cs_external_serivces_integration.Providers.Stocks.Service;
global using mono_financialbot_api_cs.Models;
global using mono_financialbot_api_cs.Extensions;
global using mono_financialbot_api_cs.Controllers.Users;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.AspNetCore.Mvc;
global using System.Text;
#endregion