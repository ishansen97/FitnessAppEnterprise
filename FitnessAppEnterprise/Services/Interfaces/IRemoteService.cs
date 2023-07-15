using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FitnessAppEnterprise.Models;
using FitnessAppEnterprise.Models.Enums;
using Microsoft.AspNetCore.Http;

namespace FitnessAppEnterprise.Services.Interfaces
{
  public interface IRemoteService
  {
    Task<T> GetSingleModelDataAsync<T>(EndpointType endpointType, HttpMethod httpMethod, string path = "", string param = "");

    Task<List<T>> GetMultipleModelDataAsync<T>(EndpointType endpointType, HttpMethod httpMethod);

    Task<HttpResponseMessage> PostDataAsync<T>(EndpointType endpointType, T data);

    Task<TOutput> PostDataWithSpecialParamsAsync<TData, TOutput>(EndpointType endpointType, TData data, string path,
      string param = "");

    Task<CountModel> GetModelCountsAsync(string userId);

    Task<List<DetailModel>> GetDetailModels(EndpointType endpointType, string userId);

    Task<HttpResponseMessage> PutDataAsync<T>(EndpointType endpointType, int id, T data);

    Task<HttpResponseMessage> DeleteAsync(EndpointType endpointType, int id);
  }
}
