
global using Microsoft.Extensions.Options;

global using MongoDB.Driver;
global using MongoDB.Bson;
global using MongoDB.Bson.Serialization.Attributes;

global using backend_dev_project.Models;
global using backend_dev_project.Repositories;
global using backend_dev_project.Services;
global using backend_dev_project.DataContext;
global using backend_dev_project.Configuration;
global using backend_dev_project.Profiles;
global using backend_dev_project.DTO;
global using backend_dev_project.GraphQL;
global using backend_dev_project.GraphQL.Recepes;

global using FluentValidation;
global using FluentValidation.AspNetCore;
global using backend_dev_project.Validators;

global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using System.Security.Claims;
global using Microsoft.AspNetCore.Authorization;

global using AutoMapper;

global using HotChocolate.Data.Filters;

global using GraphQL.Server.Ui.Voyager;

global using BCrypt.Net;
