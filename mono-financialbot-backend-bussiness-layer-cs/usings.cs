#region Globals
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using System.ComponentModel.DataAnnotations;
global using mono_financialbot_backend_bussiness_layer_cs.Interfaces;
global using mono_financialbot_backend_bussiness_layer_cs.Dtos;
global using mono_financialbot_backend_cs_datalayer.Utils.Responses;
global using AutoMapper;
global using Microsoft.AspNetCore.Identity;
global using mono_financialbot_backend_cs_datalayer.Persistence.Context;
global using mono_financialbot_backend_cs_datalayer.Models.Users;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Configuration;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using mono_financialbot_backend_bussiness_layer_cs.Utils.JsonWebToken;
#endregion