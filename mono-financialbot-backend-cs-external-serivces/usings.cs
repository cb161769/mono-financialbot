#region Globals
global using  System;
global using System.Collections.Generic;
global using System.Globalization;
global using System.IO;
global using Microsoft.Extensions.Logging;
global using System.Linq;
global using System.Net;
global using System.Text;
global using RabbitMQ.Client;
global using Microsoft.Extensions.Options;
global using System.Text.RegularExpressions;
global using mono_financialbot_backend_cs_external_serivces.Providers.Hubs.Models;
global using System.Threading.Tasks;
global using Newtonsoft.Json;
global using mono_financialbot_backend_cs_external_serivces.Providers.Hubs.Interfaces;
global using mono_financialbot_backend_cs_external_serivces.Providers.Hubs.Services;
global using Microsoft.AspNetCore.SignalR;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using mono_financialbot_backend_cs_external_serivces.Providers.RabbitMQ.Interfaces;
global using mono_financialbot_backend_cs_external_serivces.Providers.RabbitMQ.Models;
#endregion