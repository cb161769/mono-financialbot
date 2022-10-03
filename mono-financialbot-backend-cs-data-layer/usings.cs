#region Globals
global using Microsoft.EntityFrameworkCore;
global using AutoMapper;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using mono_financialbot_backend_cs_datalayer.Core.Interfaces;
global using mono_financialbot_backend_cs_datalayer.Core.Models;
global using System.ComponentModel.DataAnnotations.Schema;
global using mono_financialbot_backend_cs_datalayer.Persistence.Context;
global using System;
global using Microsoft.EntityFrameworkCore.InMemory;
global using System.Diagnostics;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.AspNetCore.Identity;
global using System.ComponentModel.DataAnnotations;
global using mono_financialbot_backend_cs_datalayer.Models.Users;
global using mono_financialbot_backend_cs_datalayer.Persistence.Core;
global using mono_financialbot_backend_cs_datalayer.Models.Configuration;
global using mono_financialbot_backend_cs_datalayer.Mappings.Profiles;
global using mono_financialbot_backend_cs_datalayer.Models.User.Dto;
global using mono_financialbot_backend_cs_datalayer.Core.Dtos;
global using System.Text.Json.Serialization;
global using System.Linq.Expressions;
global using mono_financialbot_backend_cs_datalayer.Core.Pagination;
#endregion