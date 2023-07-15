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

    public async Task<T> GetSingleModelDataAsync<T>(EndpointType endpointType, HttpMethod httpMethod, string path = "",
      string param = "")
    {
      var endpointUrl = CreateEndpoint(endpointType, httpMethod);
      if (!string.IsNullOrEmpty(path))
      {
        endpointUrl = string.Concat(endpointUrl, path, "/");
      }
      if (!string.IsNullOrEmpty(param))
      {
        endpointUrl = string.Concat(endpointUrl, param);
      }
      var client = await InitializeHttpClient();
      var response = await client.GetAsync(endpointUrl);

      var result = await response.Content.ReadAsStringAsync();
      var model = JsonConvert.DeserializeObject<T>(result);

      return model;
    }

    public async Task<List<T>> GetMultipleModelDataAsync<T>(EndpointType endpointType, HttpMethod httpMethod)
    {
      var endpointUrl = CreateEndpoint(endpointType, httpMethod);
      var client = await InitializeHttpClient();
      var response = await client.GetAsync(endpointUrl);

      var result = await response.Content.ReadAsStringAsync();
      var models = JsonConvert.DeserializeObject<List<T>>(result);

      return models;
    }

    public async Task<HttpResponseMessage> PostDataAsync<T>(EndpointType endpointType, T data)
    {
      var client = await InitializeHttpClient();
      var contentString = JsonConvert.SerializeObject(data);
      var endpointUrl = CreateEndpoint(endpointType, HttpMethod.Post);

      HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, endpointUrl);
      message.Content = new StringContent(contentString, Encoding.UTF8, "application/json");
      var response = await client.SendAsync(message);

      return response;
    }

    public async Task<TOutput> PostDataWithSpecialParamsAsync<TData, TOutput>(EndpointType endpointType, TData data,
      string path, string param = "")
    {
      var client = await InitializeHttpClient();
      var contentString = JsonConvert.SerializeObject(data);
      var endpointUrl = CreateEndpoint(endpointType, HttpMethod.Get); // special case
      endpointUrl = string.Concat(endpointUrl, path, "/");
      if (!string.IsNullOrEmpty(param))
      {
        endpointUrl = string.Concat(endpointUrl, param);
      }

      HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, endpointUrl);
      message.Content = new StringContent(contentString, Encoding.UTF8, "application/json");
      var response = await client.SendAsync(message);

      var content = await response.Content.ReadAsStringAsync();
      var modelData = JsonConvert.DeserializeObject<TOutput>(content);
      return modelData;
    }

    public async Task<CountModel> GetModelCountsAsync(string userId)
    {
      var endpointUrl = CreateEndpoint(EndpointType.Workout, HttpMethod.Get);
      var countEndpoint = string.Concat(endpointUrl, "count/", userId);
      var client = await InitializeHttpClient();

      var response = await client.GetAsync(countEndpoint);

      var result = await response.Content.ReadAsStringAsync();
      var model = JsonConvert.DeserializeObject<CountModel>(result);

      return model;
    }

    public async Task<List<DetailModel>> GetDetailModels(EndpointType endpointType, string userId)
    {
      var endpointUrl = CreateEndpoint(endpointType, HttpMethod.Get);
      var detailsEndPoint = string.Concat(endpointUrl, "details/", userId);
      var client = await InitializeHttpClient();

      var response = await client.GetAsync(detailsEndPoint);

      var result = await response.Content.ReadAsStringAsync();
      var model = JsonConvert.DeserializeObject<List<DetailModel>>(result);

      return model;
    }

    public async Task<HttpResponseMessage> PutDataAsync<T>(EndpointType endpointType, int id, T data)
    {
      var client = await InitializeHttpClient();
      var contentString = JsonConvert.SerializeObject(data);
      var endpointUrl = CreateEndpoint(endpointType, HttpMethod.Put);
      if (id > 0)
      {
        endpointUrl = string.Concat(endpointUrl, "/", id);
      }

      HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Put, endpointUrl);
      message.Content = new StringContent(contentString, Encoding.UTF8, "application/json");
      var response = await client.SendAsync(message);

      return response;
    }

    public async Task<HttpResponseMessage> DeleteAsync(EndpointType endpointType, int id)
    {
      var endpointUrl = CreateEndpoint(endpointType, HttpMethod.Delete);
      var deleteEndPoint = string.Concat(endpointUrl, "/", id);
      var client = await InitializeHttpClient();

      var response = await client.DeleteAsync(deleteEndPoint);
      return response;
    }

    private string CreateEndpoint(EndpointType endpointType, HttpMethod httpMethod)
    {
      //var baseUrl = Configuration.GetValue<string>("WorkoutBaseUrl");
      var baseUrl = Configuration.GetValue<string>("BaseUrl");
      var endpoint = _modelHelper.GetEndpoint(endpointType);
      var resource = _modelHelper.GetResourceByHttpMethod(httpMethod);
      var endpointUrl = string.Concat(baseUrl, endpoint, "/", resource);
      return endpointUrl;
    }

    private async Task<HttpClient> InitializeHttpClient()
    {
      var accessToken = await HttpContext.GetTokenAsync("access_token");
      var client = HttpClientFactory.CreateClient();
      client.SetBearerToken(accessToken);
      return client;
    }
  }
}
