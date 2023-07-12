using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FitnessAppEnterprise.Extensions;
using FitnessAppEnterprise.Helpers;
using FitnessAppEnterprise.Models;
using FitnessAppEnterprise.Models.Enums;
using FitnessAppEnterprise.Services.Interfaces;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FitnessAppEnterprise.Services.Implementations
{
  public class ApiService : RemoteService, IRemoteService
  {
    private readonly ModelHelper _modelHelper;

    public ApiService(
      IConfiguration configuration, 
      IHttpContextAccessor contextAccessor, 
      IHttpClientFactory clientFactory, 
      ModelHelper modelHelper) 
      : base(configuration, contextAccessor, clientFactory)
    {
      _modelHelper = modelHelper;
    }

    public async Task<T> GetSingleModelDataAsync<T>(EndpointType endpointType, HttpMethod httpMethod)
    {
      var endpointUrl = CreateEndpoint(endpointType, httpMethod);
      var accessToken = await HttpContext.GetTokenAsync("access_token");
      var client = HttpClientFactory.CreateClient();
      client.SetBearerToken(accessToken);
      var response = await client.GetAsync(endpointUrl);

      var result = await response.Content.ReadAsStringAsync();
      var model = JsonConvert.DeserializeObject<T>(result);

      return model;
    }

    public async Task<List<T>> GetMultipleModelDataAsync<T>(EndpointType endpointType, HttpMethod httpMethod)
    {
      var endpointUrl = CreateEndpoint(endpointType, httpMethod);
      var accessToken = await HttpContext.GetTokenAsync("access_token");
      var client = HttpClientFactory.CreateClient();
      client.SetBearerToken(accessToken);
      var response = await client.GetAsync(endpointUrl);

      var result = await response.Content.ReadAsStringAsync();
      var models = JsonConvert.DeserializeObject<List<T>>(result);

      return models;
    }

    public async Task<HttpResponseMessage> PostDataAsync<T>(EndpointType endpointType, T data)
    {
      var accessToken = await HttpContext.GetTokenAsync("access_token");
      var client = HttpClientFactory.CreateClient();
      client.SetBearerToken(accessToken);
      var contentString = JsonConvert.SerializeObject(data);
      var endpointUrl = CreateEndpoint(endpointType, HttpMethod.Post);

      HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, endpointUrl);
      message.Content = new StringContent(contentString, Encoding.UTF8, "application/json");
      var response = await client.SendAsync(message);

      return response;
    }

    private string CreateEndpoint(EndpointType endpointType, HttpMethod httpMethod)
    {
      var baseUrl = Configuration.GetValue<string>("WorkoutBaseUrl");
      var endpoint = _modelHelper.GetEndpoint(endpointType);
      var resource = _modelHelper.GetResourceByHttpMethod(httpMethod);
      var endpointUrl = string.Concat(baseUrl, endpoint, "/", resource);
      return endpointUrl;
    }
  }
}
