﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FitnessAppEnterprise.Models.Enums;
using Microsoft.AspNetCore.Http;

namespace FitnessAppEnterprise.Services.Interfaces
{
  public interface IRemoteService
  {
    Task<T> GetSingleModelDataAsync<T>(EndpointType endpointType, HttpMethod httpMethod);

    Task<List<T>> GetMultipleModelDataAsync<T>(EndpointType endpointType, HttpMethod httpMethod);

    Task<HttpResponseMessage> PostDataAsync<T>(EndpointType endpointType, T data);
  }
}